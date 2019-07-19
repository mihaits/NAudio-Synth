using System;
using NAudio.Wave;

namespace NAudio_Synth
{
    public abstract class WaveEffect32 : WaveProvider32
    {
        protected WaveProvider32 Input { get; }

        public float Wet { get; set; }

        protected WaveEffect32(int sampleRate, int channels) : base(sampleRate, channels) {}

        protected WaveEffect32(WaveProvider32 input) : base(input.WaveFormat.SampleRate, input.WaveFormat.Channels)
        {
            Input = input;
            Wet = 1;
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            var samplesRead = Input?.Read(buffer, offset, sampleCount) ?? sampleCount;

            if (Math.Abs(Wet) < float.Epsilon) return samplesRead;

            if (Math.Abs(Wet - 1) < float.Epsilon)
            {
                for (var i = 0; i < sampleCount; ++i)
                    buffer[offset + i] = Apply(buffer[offset + i]);

                return samplesRead;
            }

            for (var i = 0; i < sampleCount; ++ i)
                buffer[offset + i] = Apply(buffer[offset + i]) * Wet + buffer[offset + i] * (1 - Wet);

            return samplesRead;
        }

        public abstract float Apply(float sample);

    }
}
