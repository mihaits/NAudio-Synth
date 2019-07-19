using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using NAudio.CoreAudioApi;
using NAudio.Midi;
using NAudio.Wave;
using NAudio_Synth;

namespace WPFtest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SynthEngine _synthEngine;

        private IWavePlayer _audioOut;

        private MidiIn[] _midiDevices;

        private int _keyboardOctave;
        private List<int> _pressedKeys;
        private Dictionary<int, int> _keyNote;

        private int _time;
        private int _percentOn;
        private ArpNotes _arpNotes;
        private Thread _arpThread;
        private bool _arpThreadRunning;

        public MainWindow()
        {
            InitializeComponent();

            InitAudio();
            InitMidi();
            InitKeyInput();
        }

        private void InitMidi()
        {
            _midiDevices = new MidiIn[MidiIn.NumberOfDevices];

            for (var k = 0; k < MidiIn.NumberOfDevices; ++k)
            {
                _midiDevices[k] = new MidiIn(k);
                _midiDevices[k].MessageReceived += MidiIn_MessageReceived;
                _midiDevices[k].Start();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _arpThreadRunning = false;

            foreach (var device in _midiDevices)
                device.Stop();

            _audioOut.Stop();
        }

        private void InitAudio()
        {
            _synthEngine = new SynthEngine(48000, 64) { FilterCutoff = 20000, FilterLevel = 1};

            _audioOut = new WasapiOut(AudioClientShareMode.Shared, 10);
            _audioOut.Init(_synthEngine);
            _audioOut.Play();

            _time = 150;
            _percentOn = 100;
            _arpNotes = new ArpNotes();
            _arpThreadRunning = false;
        }

        private void InitKeyInput()
        {
            _keyboardOctave = 5;
            _pressedKeys = new List<int>();
            _keyNote = new Dictionary<int, int>()
            {
                {44, 0},
                {66, 1},
                {62, 2},
                {48, 3},
                {47, 4},
                {49, 5},
                {63, 6},
                {50, 7},
                {68, 8},
                {51, 9},
                {64, 10},
                {53, 11},
                {54, 12},
                {58, 13},
                {55, 14},
                {59, 15},
                {140, 16},
                {152, 17},
                {151, 18}
            };
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch ((int) e.Key)
            {
                case 69:
                    foreach (var key in _pressedKeys)
                        _synthEngine.NoteOff(_keyNote[key] + _keyboardOctave * 12);
                    _pressedKeys.Clear();
                    _keyboardOctave--;
                    break;
                case 67:
                    foreach (var key in _pressedKeys)
                        _synthEngine.NoteOff(_keyNote[key] + _keyboardOctave * 12);
                    _pressedKeys.Clear();
                    _keyboardOctave ++;
                    break;
            }

            if (!_keyNote.ContainsKey((int) e.Key))
                return;

            if (_pressedKeys.Find(k => k == (int) e.Key) == default(int))
            {
                _pressedKeys.Add((int) e.Key);

                var note = _keyNote[(int)e.Key] + _keyboardOctave * 12;
                
                if (_arpThreadRunning)
                    _arpNotes.AddNote(note);
                else
                    _synthEngine.NoteOn(note);
            }

            e.Handled = true;
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!_keyNote.ContainsKey((int) e.Key))
                return;

            _pressedKeys.Remove((int) e.Key);

            var note = _keyNote[(int)e.Key] + _keyboardOctave * 12;

            if (_arpThreadRunning)
                _arpNotes.RemoveNote(note);
            else
                _synthEngine.NoteOff(note);

            e.Handled = true;
        }

        private void AttackKnob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.Attack = e.NewValue / 1000f;
        }

        private void DecayKnob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.Decay = e.NewValue / 1000f;
        }

        private void SustainKnob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.Sustain = e.NewValue / 1000f;
        }

        private void ReleaseKnob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.Release = e.NewValue / 1000f;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(IsLoaded)
                _synthEngine.Amplitude = (float) (e.NewValue / 100);
        }

        private void CutoffAttackKnob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.FilterAttack= e.NewValue / 1000f;
        }

        private void CutoffDecayKnob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.FilterDecay = e.NewValue / 1000f;
        }

        private void CutoffSustainKnob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.FilterSustain = e.NewValue / 1000f;
        }

        private void CutoffReleaseKnob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.FilterRelease = e.NewValue / 1000f;
        }

        private void FilterEnvAmpKnob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.FilterEnvelopeOctaves = e.NewValue / 100f;
        }

        private void FilterCutoff_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.FilterCutoff = e.NewValue;
        }

        private void FilterResonance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.FilterQ = e.NewValue / 1000f;
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_synthEngine != null)
                switch (filterComboBox.SelectedIndex)
                {
                    case 0:
                        _synthEngine.FilterMode = FilterMode.LowPass;
                        break;
                    case 1:
                        _synthEngine.FilterMode = FilterMode.HighPass;
                        break;
                    case 2:
                        _synthEngine.FilterMode = FilterMode.BandPass;
                        break;
                }
        }

        private void ReverbLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.ReverbLevel = e.NewValue / 1000f;
        }

        private void ReverbDamp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.ReverbDamping = e.NewValue / 1000f;
        }

        private void ReverbRoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _synthEngine.ReverbRoomSize = e.NewValue / 1000f;
        }

        private void Sine_Checked(object sender, RoutedEventArgs e)
        {
            if (_synthEngine != null)
                _synthEngine.Waveform = SynthEngine.Waveforms.Sine;
        }

        private void Saw_Checked(object sender, RoutedEventArgs e)
        {
            if (_synthEngine != null)
                _synthEngine.Waveform = SynthEngine.Waveforms.Sawtooth;
        }

        private void Triangle_Checked(object sender, RoutedEventArgs e)
        {
            if (_synthEngine != null)
                _synthEngine.Waveform = SynthEngine.Waveforms.Triangle;
        }

        private void Square_Checked(object sender, RoutedEventArgs e)
        {
            if (_synthEngine != null)
                _synthEngine.Waveform = SynthEngine.Waveforms.Square;
        }

        private void MidiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            var msg = BitConverter.GetBytes(e.RawMessage);

            if (msg[0] >> 4 == 9 && msg[2] > 0)
            {
                var channel = (msg[0] & 0x0F) + 1;
                var note = msg[1];
                var velocity = msg[2];

                if (_arpThreadRunning)
                    _arpNotes.AddNote(note);
                else
                    _synthEngine.NoteOn(note);
            }
            if (msg[0] >> 4 == 8 || (msg[0] >> 4 == 9 && msg[2] == 0))
            {
                var channel = (msg[0] & 0x0F) + 1;
                var note = msg[1];

                if (_arpThreadRunning)
                    _arpNotes.RemoveNote(note);
                else
                    _synthEngine.NoteOff(note);
            }

            if (msg[1] == 7)
                Dispatcher.Invoke(() =>
                {
                    volumeSlider.Value = msg[2] * 100 / 128f;
                });

            if (msg[1] == 1)
                Dispatcher.Invoke(() =>
                {
                    filterCutoff.LinearValue = msg[2] * 19980 / 127 - 20;
                });
        }

        private void ArpCheckBox_checked(object sender, RoutedEventArgs e)
        {
            _arpThreadRunning = true;

            foreach (var key in _pressedKeys)
            {
                var note = _keyNote[key] + _keyboardOctave * 12;
                _synthEngine.NoteOff(note);
            }
            _pressedKeys.Clear();

            _arpThread = new Thread(() =>
            {
                while (_arpThreadRunning)
                {

                    if (_arpNotes.IsEmpty())
                    {
                        Thread.Sleep(_time);
                        continue;
                    }

                    var currentNote = _arpNotes.NextNote();
                    _synthEngine.NoteOn(currentNote);
                    Thread.Sleep(_time * _percentOn / 100);
                    _synthEngine.NoteOff(currentNote);
                    Thread.Sleep(_time * (100 - _percentOn) / 100);
                }
            });

            _arpThread.Start();
        }

        private void ArpCheckBox_unchecked(object sender, RoutedEventArgs e)
        {
            _arpThreadRunning = false;
            _arpThread.Join();

            _arpNotes.Clear();
        }
    }

    public class ArpNotes
    {
        private List<int> _notes;
        private int _currentIndex;

        public ArpNotes() {
            _notes = new List<int>();
            _currentIndex = 0;
        }

        public bool IsEmpty()
        {
            lock(_notes)
                return _notes.Count == 0;
        }

        public void AddNote(int note) {
            lock(_notes)
            {
                _notes.Add(note);
                _notes.Sort((a, b) => - a.CompareTo(b));
            }
        }

        public void RemoveNote(int note) {
            lock (_notes)
            {
                var noteIndex = _notes.FindIndex(n => n == note);

                if (noteIndex < _currentIndex)
                    -- _currentIndex;

                _notes.Remove(note);

                if (_currentIndex >= _notes.Count)
                    _currentIndex = 0;
            }
        }

        public int NextNote()
        {
            lock (_notes)
                return _notes[_currentIndex = (_currentIndex + 1) % _notes.Count];
        }

        public void Clear() { _notes.Clear(); }
    }
}
