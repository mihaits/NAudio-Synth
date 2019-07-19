using System;
using NAudio.Wave;

namespace NAudio_Synth
{
    public class Freeverb : WaveEffect32
    {
        private readonly LowPassFeedBackCombFilter[] _lbcf;
        private readonly AllPassFilterAprox[] _ap;
        private readonly DummyWaveProvider32 _inputCopy;

        public float RoomSize
        {
            get => _lbcf[0].Feedback;
            set
            {
                foreach (var comb in _lbcf)
                    comb.Feedback = value;
            }

        }

        public float Damping
        {
            get => _lbcf[0].Damping;
            set
            {
                foreach (var comb in _lbcf)
                    comb.Damping = value;
            }
        }

        // input x4 -> _lbcf[0-4] -> _mixer[0] \
        //                                      > _mixer[2] -> _ap[0] -> _ap[1] -> _ap[2] -> _ap[3] -> output
        // input x4 -> _lbcf[5-7] -> _mixer[1] /
        public Freeverb(WaveProvider32 input, float roomSize, float damping) : base(input)
        {
            _inputCopy = new DummyWaveProvider32(input.WaveFormat.SampleRate, input.WaveFormat.Channels);

            _lbcf = new []
            {
                new LowPassFeedBackCombFilter(_inputCopy, 1557 * WaveFormat.SampleRate / 44100, roomSize, damping, 23),
                new LowPassFeedBackCombFilter(_inputCopy, 1617 * WaveFormat.SampleRate / 44100, roomSize, damping, 23),
                new LowPassFeedBackCombFilter(_inputCopy, 1491 * WaveFormat.SampleRate / 44100, roomSize, damping, 23),
                new LowPassFeedBackCombFilter(_inputCopy, 1422 * WaveFormat.SampleRate / 44100, roomSize, damping, 23),
                new LowPassFeedBackCombFilter(_inputCopy, 1277 * WaveFormat.SampleRate / 44100, roomSize, damping, 23),
                new LowPassFeedBackCombFilter(_inputCopy, 1356 * WaveFormat.SampleRate / 44100, roomSize, damping, 23),
                new LowPassFeedBackCombFilter(_inputCopy, 1188 * WaveFormat.SampleRate / 44100, roomSize, damping, 23),
                new LowPassFeedBackCombFilter(_inputCopy, 1116 * WaveFormat.SampleRate / 44100, roomSize, damping, 23)
            };

            var mixers = new []
            {
                new WaveMixer32(input.WaveFormat.SampleRate, input.WaveFormat.Channels) {Mode = MixerMode.Averaging},
                new WaveMixer32(input.WaveFormat.SampleRate, input.WaveFormat.Channels) {Mode = MixerMode.Averaging},
                new WaveMixer32(input.WaveFormat.SampleRate, input.WaveFormat.Channels) {Mode = MixerMode.Averaging}
            };
            
            mixers[0].AddInputs(new [] {_lbcf[0], _lbcf[1], _lbcf[2], _lbcf[3]});
            mixers[1].AddInputs(new [] {_lbcf[4], _lbcf[5], _lbcf[6], _lbcf[7]});
            mixers[2].AddInputs(new [] {mixers[0], mixers[1]});

            _ap = new AllPassFilterAprox[4];

            _ap[0] = new AllPassFilterAprox(mixers[2], 225 * WaveFormat.SampleRate / 44100, .5f, 23);
            _ap[1] = new AllPassFilterAprox(_ap[0], 556 * WaveFormat.SampleRate / 44100, .5f, 23);
            _ap[2] = new AllPassFilterAprox(_ap[1], 441 * WaveFormat.SampleRate / 44100, .5f, 23);
            _ap[3] = new AllPassFilterAprox(_ap[2], 341 * WaveFormat.SampleRate / 44100, .5f, 23);
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            Input.Read(buffer, offset, sampleCount);

            _inputCopy.SetBuffer(buffer, offset, sampleCount);

            var samplesRead = _ap[3].Read(buffer, offset, sampleCount);

            for (var i = 0; i < sampleCount; ++i)
                buffer[offset + i] = Wet * buffer[offset + i] + (1f - Wet) * _inputCopy.GetBuffer()[i];

            return samplesRead;
        }

        public override float Apply(float sample)
        {
            throw new NotSupportedException();
        }
    }
}
