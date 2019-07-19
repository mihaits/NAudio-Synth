using NAudio.Wave;

namespace NAudio_Synth
{
    public class AllPassFilterAprox : WaveEffect32
    {
        private readonly float[] _xl, _xr, _yl, _yr;
        private int _posl, _posr;
        private bool _left;

        public AllPassFilterAprox(WaveProvider32 input, int delay, float gain, int stereoSpread) : base(input)
        {
            _left = true;

            Gain = gain;

            _xl = new float[delay];
            _xr = new float[delay+stereoSpread];
            _yl = new float[delay];
            _yr = new float[delay+stereoSpread];

            for (var i = 0; i < delay; ++ i)
                _xl[i] = _xr[i] = _yl[i] = _yr[i] = 0;
            for (var i = delay; i < delay + stereoSpread; ++ i)
                _xr[i] = _yr[i] = 0;

            _posl = _posr = 0;
        }

        public float Gain { get; set; }

        // y[n] = g * y[n-delay] + (1 + g) * x[n-delay] - x[n]
        public override float Apply(float sample)
        {
            float yn;

            if (_left)
            {
                yn = Gain * _yl[_posl] + (1 + Gain) * _xl[_posl] - sample;

                _xl[_posl] = sample;
                _yl[_posl] = yn;

                _posl = (_posl + 1) % _xl.Length;
            }
            else
            {
                yn = Gain * _yr[_posr] + (1 + Gain) * _xr[_posr] - sample;

                _xr[_posr] = sample;
                _yr[_posr] = yn;

                _posr = (_posr + 1) % _xr.Length;
            }

            _left = !_left;

            return yn;
        }
    }
}
