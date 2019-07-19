using System;
using NAudio.Wave;

namespace NAudio_Synth
{
    public class LookaheadLimiter : WaveEffect32
    {
        private float[] _buffer;
        private float _multiplier;
        private float _limit, _actualLimit;
        private int _pos;

        public LookaheadLimiter(WaveProvider32 input, float limit, int bufferSize) : base(input)
        {
            _multiplier = 1;

            this._limit = limit;
            _actualLimit = limit;

            _pos = 0;
            _buffer = new float[bufferSize];
            for (var i = 0; i < bufferSize; ++ i)
                _buffer[i] = 0;
        }

        private int Norm(int i)
        {
            return i % _buffer.Length; 
        }

        private static float Bound(float min, float max, float value)
        {
            return value < min
                ? min
                : value > max
                    ? max
                    : value;
        }

        private static float Abs(float x)
        {
            return Math.Abs(x);
        }

        public override float Apply(float sample)
        {
            _buffer[_pos] = sample;
            _pos = Norm(_pos + 1);

            var peak = _buffer[_pos];
            var peakPos = 0;

            for (var i = 0; i < _buffer.Length; ++i)
                if ((Abs(_buffer[Norm(_pos + i)]) - _limit) /       (i + 1)
                    >
                    (Abs(peak) - _limit)                  / (peakPos + 1))
                {
                    peak = _buffer[Norm(_pos + i)];
                    peakPos = i;
                }

            if (Abs(peak) * _multiplier >= _limit)
                _multiplier -= ((_multiplier - _limit / Abs(peak)) / (peakPos + 1));
            else if (_multiplier < 1)
                _multiplier = Math.Min(1, _multiplier + Bound(0, 1f / WaveFormat.SampleRate, ((_limit / Abs(peak) - _multiplier) / (peakPos + 1))));

            if (Abs(_buffer[_pos]) * _multiplier > _actualLimit)
            {
                _limit /= (Abs(_buffer[_pos]) * _multiplier);
                Console.WriteLine("limiter self correction: " + (_actualLimit - _limit));

                if (_limit / _actualLimit < .99)
                {
                    Console.WriteLine("limiter overcorrection, resetting internal limit");
                    _limit = _actualLimit;
                }

                return Math.Sign(_buffer[_pos]) * _actualLimit;
            }
            
            return _buffer[_pos] * _multiplier;
        }
    }
}
