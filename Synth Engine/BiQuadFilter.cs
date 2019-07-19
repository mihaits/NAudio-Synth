using System;
using NAudio.Wave;

namespace NAudio_Synth
{
    public enum FilterMode { LowPass, HighPass, BandPass };

    public class BiQuadFilter : WaveEffect32
    {

        private readonly double[] _a, _b;
        private readonly double[] _xl, _yl, _xr, _yr;
        private FilterMode _mode;
        private float _cutoff, _q;
        private bool _left;

        public BiQuadFilter(WaveProvider32 input) : base(input)
        {
            _a = new double[3];
            _b = new double[3];

            _xl = new double[2];
            _yl = new double[2];

            _xr = new double[2];
            _yr = new double[2];
            
            _left = true;

            _cutoff = 1000;
            _q = (float) (Math.Sqrt(2) / 2);
            _mode = FilterMode.LowPass;

            UpdateCoefsLowPass();
        }

        public BiQuadFilter(int sampleRate) : base(sampleRate, 2)
        {
            _a = new double[3];
            _b = new double[3];

            _xl = new double[2];
            _yl = new double[2];

            _xr = new double[2];
            _yr = new double[2];

            _left = true;

            _cutoff = 1000;
            _q = (float)(Math.Sqrt(2) / 2);
            _mode = FilterMode.LowPass;

            UpdateCoefsLowPass();
        }

        public FilterMode Mode
        {
            get => _mode;
            set
            {
                _mode = value;
                UpdateCoefs();
            }
        }

        public float Cutoff
        {
            get => _cutoff;
            set
            {
                var old = _cutoff;
                _cutoff = Math.Max(10, Math.Min(value, 20000));
                if (Math.Abs(old - _cutoff) > float.Epsilon)
                    UpdateCoefs();
            }
        }

        public float Q
        {
            get => _q;
            set
            {
                _q = value;
                UpdateCoefs();
            }
        }

        private void UpdateCoefs()
        {
            switch(Mode) {
                case FilterMode.LowPass:
                    UpdateCoefsLowPass();
                    break;
                case FilterMode.HighPass:
                    UpdateCoefsHighPass();
                    break;
                case FilterMode.BandPass:
                    UpdateCoefsBandPass();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateCoefsLowPass()
        {
            var w0 = 2 * Math.PI * Cutoff / WaveFormat.SampleRate;
            var alpha = Math.Sin(w0) / (2 * Q);
            var cosw0 = Math.Cos(w0);

            _b[0] = (1 - cosw0) / 2;
            _b[1] =  1 - cosw0;
            _b[2] = (1 - cosw0) / 2;
            _a[0] =  1 + alpha;
            _a[1] = -2 * cosw0;
            _a[2] =  1 - alpha;
        }

        private void UpdateCoefsHighPass()
        {
            var w0 = 2 * Math.PI * Cutoff / WaveFormat.SampleRate;
            var alpha = Math.Sin(w0) / (2 * Q);
            var cosw0 = Math.Cos(w0);

            _b[0] =  (1 + cosw0) / 2;
            _b[1] = -(1 + cosw0);
            _b[2] =  (1 + cosw0) / 2;
            _a[0] =   1 + alpha;
            _a[1] =  -2 * cosw0;
            _a[2] =   1 - alpha;
        }

        private void UpdateCoefsBandPass()
        {
            var w0 = 2 * Math.PI * Cutoff / WaveFormat.SampleRate;
            var alpha = Math.Sin(w0) / (2 * Q);
            var cosw0 = Math.Cos(w0);

            _b[0] =  Q * alpha;
            _b[1] =  0;
            _b[2] = -Q * alpha;
            _a[0] =  1 + alpha;
            _a[1] = -2 * cosw0;
            _a[2] =  1 - alpha;
        }

        public override float Apply(float xn)
        {
            double yn;

            if (_left)
            {
                yn = (_b[0] * xn + _b[1] * _xl[1] + _b[2] * _xl[0]
                                - _a[1] * _yl[1] - _a[2] * _yl[0])
                     / _a[0];

                _yl[0] = _yl[1];
                _yl[1] = yn;

                _xl[0] = _xl[1];
                _xl[1] = xn;
            }
            else
            {
                yn = (_b[0] * xn + _b[1] * _xr[1] + _b[2] * _xr[0]
                                - _a[1] * _yr[1] - _a[2] * _yr[0])
                     / _a[0];

                _yr[0] = _yr[1];
                _yr[1] = yn;

                _xr[0] = _xr[1];
                _xr[1] = xn;
            }

            _left = !_left;

            return (float) yn;
        }
    }
}
