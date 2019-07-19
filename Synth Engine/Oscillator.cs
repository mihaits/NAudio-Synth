using System;
using NAudio.Wave;
using NAudio.Dsp;

namespace NAudio_Synth
{
    public class Oscillator : WaveProvider32
    {
        private readonly EnvelopeGenerator _amplitudeEnvelope;
        private bool _isPlaying;
        private double _phase;
        private float _tuning;
        private int _note; 

        public EventHandler FinishedPlaying;

        public Oscillator(int sampleRate, int channels) : base(sampleRate, channels)
        {
            _amplitudeEnvelope = new EnvelopeGenerator();
            _isPlaying = false;

            Attack = .001f;
            Decay = 0;
            Sustain = 1;
            Release = .001f;

            Amplitude = 1;

            _tuning = 440;
            _note = 69;
            Frequency = 440;

            Function = x => (float) Math.Cos(2 * Math.PI * x);

            _phase = 0;
        }

        public Func<float, float> Function { get; set; }

        public float Attack
        {
            get => _amplitudeEnvelope.AttackRate / WaveFormat.SampleRate;
            set => _amplitudeEnvelope.AttackRate = WaveFormat.SampleRate * value;
        }

        public float Decay
        {
            get => _amplitudeEnvelope.DecayRate / WaveFormat.SampleRate;
            set => _amplitudeEnvelope.DecayRate = WaveFormat.SampleRate * value;
        }

        public float Sustain
        {
            get => _amplitudeEnvelope.SustainLevel;
            set => _amplitudeEnvelope.SustainLevel = value;
        }

        public float Release
        {
            get => _amplitudeEnvelope.ReleaseRate / WaveFormat.SampleRate;
            set => _amplitudeEnvelope.ReleaseRate = WaveFormat.SampleRate * value;
        }

        public float Amplitude { get; set; }


        private static float NoteToFreq(int note, float tuning)
        {
            return (float)(tuning * Math.Pow(2, (note - 69.0) / 12));
        }
        public float Frequency { get; set; }

        public float Tuning {
            get => _tuning;
            set
            {
                _tuning = value;
                Frequency = NoteToFreq(_note, _tuning);
            }
        }

        // can be outdated
        public int Note
        {
            get => _note;
            set
            {
                _note = value;
                Frequency = NoteToFreq(_note, _tuning);
            }
        }

        public bool IsPlaying()
        {
            return _isPlaying;
        }

        public bool IsOnRelease()
        {
            return _amplitudeEnvelope.State == EnvelopeGenerator.EnvelopeState.Release;
        }

        public void NoteOn()
        {
            _amplitudeEnvelope.Gate(true);
            _isPlaying = true;
        }

        public void NoteOff() { _amplitudeEnvelope.Gate(false); }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            for (var index = 0; index < sampleCount; index += WaveFormat.Channels)
            {
                if(_amplitudeEnvelope.State != EnvelopeGenerator.EnvelopeState.Idle)
                {
                    _phase = (_phase + Frequency / WaveFormat.SampleRate) % 1;

                    buffer[offset + index] = Function((float) _phase) * Amplitude * _amplitudeEnvelope.Process();

                    for (var channel = 1; channel < WaveFormat.Channels; ++ channel)
                        buffer[offset + index + channel] = buffer[offset + index];
                }
                else
                {
                    if(_isPlaying)
                    {
                        _isPlaying = false;
                        FinishedPlaying?.Invoke(this, new EventArgs());
                    }

                    for (var channel = 0; channel < WaveFormat.Channels; ++ channel)
                        buffer[index + offset + channel] = 0;
                }
            }

            return sampleCount;
        }
    }
}
