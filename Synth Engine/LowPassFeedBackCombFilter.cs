using System;
using NAudio.Wave;

namespace NAudio_Synth
{
    public class LowPassFeedBackCombFilter : WaveEffect32
    {
        private int _stereoSpread;
        private readonly float[] _xl, _yl;
        private float[] _xr, _yr;
        private float _xlnN1, _xrnN1;
        private int _posl, _posr;
        private bool _left;

        public LowPassFeedBackCombFilter(WaveProvider32 input, int delay, float f, float d, int stereoSpread) : base(input)
        {
            _left = true;

            _stereoSpread = stereoSpread;
            Feedback = f;
            Damping = d;

            _xl = new float[delay];
            _xr = new float[delay+stereoSpread];
            _yl = new float[delay];
            _yr = new float[delay+stereoSpread];
        }

        public float Feedback { get; set; }

        public float Damping { get; set; }

        public int StereoSpread
        {
            get => _stereoSpread;
            set
            {
                _stereoSpread = value;

                Array.Resize(ref _xr, _xl.Length + _stereoSpread);
                Array.Resize(ref _yr, _yl.Length + _stereoSpread);
            }
        }
        
        // y[n] = (1 - d) * f * y[n-N] + d * y[n-1] - d * x[n-(N+1)] + x[n-N]
        public override float Apply(float sample)
        {
            float yn;

            if (_left)
            {
                yn = (1 - Damping) * Feedback * _yl[_posl] + Damping * _yl[_posl == 0 ? _yl.Length - 1 : _posl - 1] - Damping * _xlnN1 + _xl[_posl];

                _xlnN1 = _xl[_posl];
                _xl[_posl] = sample;
                _yl[_posl] = yn;

                _posl = (_posl + 1) % _xl.Length;
            }
            else
            {
                yn = (1 - Damping) * Feedback * _yr[_posr] + Damping * _yr[_posr == 0 ? _yr.Length - 1 : _posr - 1] - Damping * _xrnN1 + _xr[_posr];

                _xrnN1 = _xr[_posr];
                _xr[_posr] = sample;
                _yr[_posr] = yn;

                _posr= (_posr + 1) % _xr.Length;
            }

            _left = !_left;

            return yn;
        }
    }
}
