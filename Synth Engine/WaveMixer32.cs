using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NAudio_Synth
{
    public enum MixerMode { Additive, Averaging }

    public class WaveMixer32 : WaveProvider32
    {
        private readonly List<WaveProvider32> _inputs, _toAdd, _toRemove;

        public MixerMode Mode { get; set; }

        public WaveMixer32(int sampleRate, int channels) : base(sampleRate, channels)
        {
            _inputs      = new List<WaveProvider32>();
            _toAdd       = new List<WaveProvider32>();
            _toRemove    = new List<WaveProvider32>();

            Mode = MixerMode.Additive;
        }

        public WaveMixer32(WaveProvider32 firstInput) : this(firstInput.WaveFormat.SampleRate, firstInput.WaveFormat.Channels)
        {
            _inputs.Add(firstInput);
        }

        public void AddInput(WaveProvider32 waveProvider)
        {
            if (!waveProvider.WaveFormat.Equals(WaveFormat))
                throw new ArgumentException("All incoming channels must have the same format", "waveProvider.WaveFormat");

            _toAdd.Add(waveProvider);
        }

        public void AddInputs(IEnumerable<WaveProvider32> inputs)
        {
            inputs.ToList().ForEach(AddInput);
        }
        
        public void RemoveInput(WaveProvider32 waveProvider)
        {
            _toRemove.Add(waveProvider);
        }

        public int InputCount => _inputs.Count;

        public override int Read(float[] buffer, int offset, int count)
        {
            if (_toAdd.Count != 0)
            {
                _toAdd.ForEach(input => _inputs.Add(input));
                _toAdd.Clear();
            }
            if (_toRemove.Count != 0)
            {
                _toRemove.ForEach(input => _inputs.Remove(input));
                _toRemove.Clear();
            }

            for (var i = 0; i < count; ++i)
                buffer[offset + i] = 0;

            var readBuffer = new float[count];

            foreach (var input in _inputs)
            {
                input.Read(readBuffer, 0, count);

                for (var i = 0; i < count; ++i)
                    buffer[offset + i] += readBuffer[i];
            }

            if (Mode == MixerMode.Averaging && _inputs.Count != 0)
                for (var i = 0; i < count; ++i)
                    buffer[offset + i] /= _inputs.Count;

            return count;
        }
    }
}
