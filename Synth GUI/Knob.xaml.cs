using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SynthGUI
{
    /// <summary>
    /// Interaction logic for Knob.xaml
    /// </summary>
    /// 
    [DefaultEvent("ValueChanged"), DefaultProperty("Value")]
    public partial class Knob : UserControl
    {
        public event RoutedPropertyChangedEventHandler<int> ValueChanged = null;
        public enum KnobLabelMode { NoLabel, ValueLabel, CustomLabel};

        private void OnChanged(RoutedPropertyChangedEventArgs<int> e)
        {
            ValueChanged?.Invoke(this, e);
        }

        #region Private Fields

        private double _arcStartAngle;
        private double _arcEndAngle;
        private int _Value;
        private int _Minimum;
        private int _Maximum;
        private int _Factor;
        private double _LabelFontSize;
        private KnobLabelMode _LabelMode;
        private FontFamily _LabelFont;
        private string _CustomLabel;
        private double A, B;

        private bool isMouseDown = false;
        private Point previousMousePosition;

        #endregion Private Fields

        public Knob()
        {
            InitializeComponent();

            _arcStartAngle = -180;
            _arcEndAngle = 180;
            _Value = 0;
            _Minimum = 0;
            _Maximum = 100;
            _Factor = 1;
            _LabelFontSize = 22;
            _LabelFont = new FontFamily("Consolas");
            _CustomLabel = string.Empty;
            _LabelMode = KnobLabelMode.NoLabel;
            MouseSpeed = 500;
            WheelStep = 1;
            DefaultValue = 1;
            Logarithmic = false;
            UpdateAB();
            isMouseDown = false;
        }

        public double StartAngle
        {
            get { return _arcStartAngle; }
            set
            {
                _arcStartAngle = value;
                UpdateView();
            }
        }

        public double EndAngle
        {
            get { return _arcEndAngle; }
            set
            {
                _arcEndAngle = value;
                UpdateView();
            }
        }

        public int ViewFactor
        {
            get { return _Factor; }
            set
            {
                _Factor = value;
                UpdateView();
            }
        }

        public bool Logarithmic { get; set; }

        public int Value
        {
            get { return Logarithmic ? (int) Math.Pow(Math.E, A * _Value + B) : _Value; }
            set
            {
                int oldValue = Value;
                _Value = Logarithmic ? (int) ((Math.Log(value) - B) / A) : value;
                _Value = Math.Min(Math.Max(_Minimum, _Value), _Maximum);

                if (IsLoaded)
                {
                    if(oldValue != value)
                        OnChanged(new RoutedPropertyChangedEventArgs<int>(oldValue, Value));
                    UpdateView();
                }
            }
        }

        public int LinearValue {
            get { return _Value; }
            set
            {
                var oldValue = Value;
                _Value = value;

                if (IsLoaded)
                    if (oldValue != Value)
                        OnChanged(new RoutedPropertyChangedEventArgs<int>(oldValue, Value));

                UpdateView();
            }
        }

        public int DefaultValue
        {
            get; set;
        }

        public int Minimum
        {
            get { return _Minimum; }
            set
            {
                _Minimum = value;
                UpdateAB();
                UpdateView();
            }
        }

        public int Maximum
        {
            get { return _Maximum; }
            set
            {
                _Maximum = value;
                UpdateAB();
                UpdateView();
            }
        }

        public int MouseSpeed { get; set; }

        public int WheelStep { get; set; }

        public KnobLabelMode LabelMode
        {
            get { return _LabelMode; }
            set
            {
                _LabelMode = value;
                UpdateView();
            }
        }

        public string CustomLabel {
            get { return _CustomLabel;  }
            set
            {
                _CustomLabel = value;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            switch (LabelMode)
            {
                case KnobLabelMode.CustomLabel:
                    valueText.Text = CustomLabel;
                    valueText.FontFamily = LabelFont;
                    valueText.FontSize = LabelFontSize;
                    break;

                case KnobLabelMode.ValueLabel:
                    valueText.Text = (1d * Value / ViewFactor).ToString("F" + (int)Math.Log10(ViewFactor));
                    valueText.FontFamily = LabelFont;
                    valueText.FontSize = LabelFontSize;
                    break;

                case KnobLabelMode.NoLabel:
                    valueText.Text = string.Empty;
                    break;
            }

            needle.RenderTransform = new RotateTransform((EndAngle - StartAngle) / (Maximum - Minimum) * (_Value - Minimum) + StartAngle, 100, 100);
        }

        private void UpdateAB()
        {
            A = Math.Log(1f * _Maximum / _Minimum) / (_Maximum - _Minimum);
            B = Math.Log(_Maximum) - _Maximum * A;
        }

        public FontFamily LabelFont
        {
            get { return _LabelFont; }
            set
            {
                _LabelFont = value;
                UpdateView();
            }
        }

        public double LabelFontSize
        {
            get { return _LabelFontSize; }
            set
            {
                _LabelFontSize = value;
                UpdateView();
            }
        }

        private void Ellipse_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int oldValue = Value;

            int d = e.Delta / 120; // Mouse wheel 1 click (120 delta) = 1 step
            _Value += d * WheelStep;
            _Value = Math.Min(Math.Max(_Minimum, _Value), _Maximum);

            if (oldValue != Value)
                OnChanged(new RoutedPropertyChangedEventArgs<int>(oldValue, Value));

            UpdateView();
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            (sender as Ellipse).CaptureMouse();
            previousMousePosition = e.GetPosition((Ellipse)sender);
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                var oldValue = Value;
                Point newMousePosition = e.GetPosition((Ellipse)sender);

                double dY = (previousMousePosition.Y - newMousePosition.Y);

                _Value += (int) dY * (_Maximum - _Minimum) / MouseSpeed;
                _Value = Math.Min(Math.Max(_Minimum, _Value), _Maximum);

                if (oldValue != Value)
                    OnChanged(new RoutedPropertyChangedEventArgs<int>(oldValue, Value));
                
                previousMousePosition = newMousePosition;
                UpdateView();
            }
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
            (sender as Ellipse).ReleaseMouseCapture();
        }

        private void knobUserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Value = DefaultValue;
        }

        private void knobUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }
    }
}
