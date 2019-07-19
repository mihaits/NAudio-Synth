using NAudio.Wave;
using System;

namespace NAudio_Synth
{
    class DummyWaveProvider32 : WaveProvider32
    {
        private float[] buffer;

        public DummyWaveProvider32(int sampleRate, int channels) : base(sampleRate, channels) {}

        public void SetBuffer(float[] buffer, int offset, int sampleCount)
        {
            this.buffer = new float[sampleCount];
            for (var i = 0; i < sampleCount; ++i)
                this.buffer[i] = buffer[offset + i];
        }

        public float[] GetBuffer()
        {
            return buffer;
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            if (buffer.Length != sampleCount)
                throw new Exception();

            for (var i = 0; i < sampleCount; ++i)
                buffer[offset + i] = this.buffer[i];

            return sampleCount;
        }
    }
}
