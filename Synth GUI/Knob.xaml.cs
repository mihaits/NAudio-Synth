using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SynthGUI
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    /// Interaction logic for Knob.xaml
    /// </summary>
    [DefaultEvent("ValueChanged"), DefaultProperty("Value")]
    public partial class Knob
    {
        public event RoutedPropertyChangedEventHandler<int> ValueChanged;

        public enum KnobLabelMode { NoLabel, ValueLabel, CustomLabel};

        private void OnChanged(RoutedPropertyChangedEventArgs<int> e)
        {
            ValueChanged?.Invoke(this, e);
        }

        #region Private Fields

        private double _arcStartAngle;
        private double _arcEndAngle;
        private int _value;
        private int _minimum;
        private int _maximum;
        private int _factor;
        private double _labelFontSize;
        private KnobLabelMode _labelMode;
        private FontFamily _labelFont;
        private string _customLabel;
        private double _a, _b;

        private bool _isMouseDown;
        private Point _previousMousePosition;

        #endregion Private Fields

        public Knob()
        {
            InitializeComponent();

            _arcStartAngle = -180;
            _arcEndAngle = 180;
            _value = 0;
            _minimum = 0;
            _maximum = 100;
            _factor = 1;
            _labelFontSize = 22;
            _labelFont = new FontFamily("Consolas");
            _customLabel = string.Empty;
            _labelMode = KnobLabelMode.NoLabel;
            MouseSpeed = 500;
            WheelStep = 1;
            DefaultValue = 1;
            Logarithmic = false;
            UpdateAB();
            _isMouseDown = false;
        }

        public double StartAngle
        {
            get => _arcStartAngle;
            set
            {
                _arcStartAngle = value;
                UpdateView();
            }
        }

        public double EndAngle
        {
            get => _arcEndAngle;
            set
            {
                _arcEndAngle = value;
                UpdateView();
            }
        }

        public int ViewFactor
        {
            get => _factor;
            set
            {
                _factor = value;
                UpdateView();
            }
        }

        public bool Logarithmic { get; set; }

        public int Value
        {
            get => Logarithmic ? (int) Math.Pow(Math.E, _a * _value + _b) : _value;
            set
            {
                var oldValue = Value;
                _value = Logarithmic ? (int) ((Math.Log(value) - _b) / _a) : value;
                _value = Math.Min(Math.Max(_minimum, _value), _maximum);

                if (!IsLoaded) return;

                if(oldValue != value)
                    OnChanged(new RoutedPropertyChangedEventArgs<int>(oldValue, Value));

                UpdateView();
            }
        }

        public int LinearValue {
            get => _value;
            set
            {
                var oldValue = Value;
                _value = value;

                if (!IsLoaded)
                    return;

                if(oldValue != Value)
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
            get => _minimum;
            set
            {
                _minimum = value;
                UpdateAB();
                UpdateView();
            }
        }

        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                UpdateAB();
                UpdateView();
            }
        }

        public int MouseSpeed { get; set; }

        public int WheelStep { get; set; }

        public KnobLabelMode LabelMode
        {
            get => _labelMode;
            set
            {
                _labelMode = value;
                UpdateView();
            }
        }

        public string CustomLabel {
            get => _customLabel;
            set
            {
                _customLabel = value;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            switch (LabelMode)
            {
                case KnobLabelMode.CustomLabel:
                    ValueText.Text = CustomLabel;
                    ValueText.FontFamily = LabelFont;
                    ValueText.FontSize = LabelFontSize;
                    break;

                case KnobLabelMode.ValueLabel:
                    ValueText.Text = (1d * Value / ViewFactor).ToString("F" + (int)Math.Log10(ViewFactor));
                    ValueText.FontFamily = LabelFont;
                    ValueText.FontSize = LabelFontSize;
                    break;

                case KnobLabelMode.NoLabel:
                    ValueText.Text = string.Empty;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Needle.RenderTransform = new RotateTransform((EndAngle - StartAngle) / (Maximum - Minimum) * (_value - Minimum) + StartAngle, 100, 100);
        }

        private void UpdateAB()
        {
            _a = Math.Log(1f * _maximum / _minimum) / (_maximum - _minimum);
            _b = Math.Log(_maximum) - _maximum * _a;
        }

        public FontFamily LabelFont
        {
            get => _labelFont;
            set
            {
                _labelFont = value;
                UpdateView();
            }
        }

        public double LabelFontSize
        {
            get => _labelFontSize;
            set
            {
                _labelFontSize = value;
                UpdateView();
            }
        }

        private void Ellipse_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var oldValue = Value;

            var d = e.Delta / 120; // Mouse wheel 1 click (120 delta) = 1 step
            _value += d * WheelStep;
            _value = Math.Min(Math.Max(_minimum, _value), _maximum);

            if (oldValue != Value)
                OnChanged(new RoutedPropertyChangedEventArgs<int>(oldValue, Value));

            UpdateView();
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;
            (sender as Ellipse)?.CaptureMouse();
            _previousMousePosition = e.GetPosition((Ellipse)sender);
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMouseDown) return;
            
            var oldValue = Value;
            var newMousePosition = e.GetPosition((Ellipse)sender);

            var dY = (_previousMousePosition.Y - newMousePosition.Y);

            _value += (int) dY * (_maximum - _minimum) / MouseSpeed;
            _value = Math.Min(Math.Max(_minimum, _value), _maximum);

            if (oldValue != Value)
                OnChanged(new RoutedPropertyChangedEventArgs<int>(oldValue, Value));
                
            _previousMousePosition = newMousePosition;
            UpdateView();
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = false;
            (sender as Ellipse)?.ReleaseMouseCapture();
        }

        private void KnobUserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Value = DefaultValue;
        }

        private void KnobUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }
    }
}
