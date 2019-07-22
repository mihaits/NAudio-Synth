using System;
using NAudio.Wave;

namespace NAudio_Synth
{
    public class LookaheadLimiter : WaveEffect32
    {
        private readonly float[] _bufferL, _bufferR;
        private float _multiplierL, _multiplierR;
        private readonly float _limit;
        private int _posL, _posR;
        private bool _onLeftChannel;

        public bool ProcessStereoSeparately { get; set; }

        public LookaheadLimiter(WaveProvider32 input, float limit, int bufferSize) : base(input)
        {
            _multiplierL = _multiplierR = 1;

            _limit = limit;

            _bufferL = new float[bufferSize];
            _bufferR = new float[bufferSize];
        }

        private int Norm(int i)
        {
            return i % _bufferL.Length; 
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

        private void Process(float[] buffer, ref int pos, ref float multiplier, float newSample)
        {
            buffer[pos] = newSample;
            pos = Norm(pos + 1);

            var peak = buffer[pos];
            var peakPos = 0;

            for (var i = 0; i < buffer.Length; ++i)
                if ((Abs(buffer[Norm(pos + i)]) - _limit) /       (i + 1)
                    >
                    (Abs(peak) - _limit)                    / (peakPos + 1))
                {
                    peak = buffer[Norm(pos + i)];
                    peakPos = i;
                }

            if (Abs(peak) * multiplier >= _limit)
                multiplier -= (multiplier - _limit / Abs(peak)) / (peakPos + 1);
            else if (multiplier < 1)
                multiplier = Math.Min(
                    1,
                    multiplier + Bound(
                        0,
                        1f / WaveFormat.SampleRate,
                        (_limit / Abs(peak) - multiplier) / (peakPos + 1)));
        }

        public override float Apply(float sample)
        {
            _onLeftChannel = !_onLeftChannel;

            if (_onLeftChannel)
            {
                Process(_bufferL, ref _posL, ref _multiplierL, sample);

                return _bufferL[_posL] * (
                           ProcessStereoSeparately
                               ? _multiplierL
                               : Math.Min(_multiplierL, _multiplierR));
            }
            else
            {
                Process(_bufferR, ref _posR, ref _multiplierR, sample);

                return _bufferR[_posR] * (
                           ProcessStereoSeparately
                               ? _multiplierR
                               : Math.Min(_multiplierL, _multiplierR));
            }
        }
    }
}
