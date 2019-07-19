using System.Windows.Forms;
using NAudio.Wave;
using System;
using NAudio.Midi;
using System.Collections.Generic;
using System.Threading;

namespace NAudio_Synth
{
    public partial class MainWindow : Form
    {
        private SynthEngine synthEngine;

        private AsioOut audioOutput;

        private MidiIn midiIn;
        private Dictionary<int, String> midiInDevices;

        private List<int> pressedKeys;
        private Dictionary<int, int> keyNote;
        private int octave;

        public MainWindow()
        {
            InitializeComponent();
            InitAudio();
            InitGUI();
        }

        private void InitGUI()
        {
            octave = 5;
            KeyPreview = true;
            pressedKeys = new List<int>();
            keyNote = new Dictionary<int, int>()
            {
                {65, 0},
                {87, 1},
                {83, 2},
                {69, 3},
                {68, 4},
                {70, 5},
                {84, 6},
                {71, 7},
                {89, 8},
                {72, 9},
                {85, 10},
                {74, 11},
                {75, 12},
                {79, 13},
                {76, 14},
                {80, 15},
                {186, 16},
                {222, 17},
                {221, 18}
            };


            waveformList.DropDownStyle = ComboBoxStyle.DropDownList;

            waveformList.Items.Add("sine");
            waveformList.Items.Add("sawtooth");
            waveformList.Items.Add("square");
            waveformList.Items.Add("triangle");
            waveformList.Items.Add("noise");

            waveformList.SelectedIndex = 0;


            if (MidiIn.NumberOfDevices == 0)
            {
                midiDeviceList.Enabled = false;
                midiDeviceList.Text = "no device detected";
            }
            else
            {
                midiInDevices = new Dictionary<int, string>(MidiIn.NumberOfDevices);
                for (var i = 0; i < MidiIn.NumberOfDevices; ++i)
                    midiInDevices.Add(i, MidiIn.DeviceInfo(i).ProductName);

                midiDeviceList.DataSource = new BindingSource(midiInDevices, null);
                midiDeviceList.DisplayMember = "Value";
                midiDeviceList.ValueMember = "Key";
                midiDeviceList.DropDownStyle = ComboBoxStyle.DropDownList;

                midiDeviceList.SelectedIndex = 0;
            }

            filterModeList.DropDownStyle = ComboBoxStyle.DropDownList;

            filterModeList.Items.Add("LowPass");
            filterModeList.Items.Add("HighPass");
            filterModeList.Items.Add("BandPass");

            filterModeList.SelectedIndex = 0;
        }

        private void MidiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            var msg = BitConverter.GetBytes(e.RawMessage);

            if (msg[0] >> 4 == 9 && msg[2] > 0)
            {
                var channel = (msg[0] & 0x0F) + 1;
                var note = msg[1];
                var velocity = msg[2];

                synthEngine.NoteOn(note);
            }
            if (msg[0] >> 4 == 8 || (msg[0] >> 4 == 9 && msg[2] == 0))
            {
                var channel = (msg[0] & 0x0F) + 1;
                var note = msg[1];

                synthEngine.NoteOff(note);
            }
        }

        private void InitAudio()
        {
            synthEngine = new SynthEngine(48000, 32) { Amplitude = 0.5f };
            
            audioOutput = new AsioOut();
            audioOutput.Init(synthEngine);
            audioOutput.Play();
        }


        private void AttackSlider_Scroll(object sender, EventArgs e)
        {
            synthEngine.Attack = attackSlider.Value / 500f;
            attackLabel.Text = (attackSlider.Value / 500f).ToString("n3") + " s";
        }

        private void DecaySlider_Scroll(object sender, EventArgs e)
        {
            synthEngine.Decay = decaySlider.Value / 500f;
            decayLabel.Text = (decaySlider.Value / 500f).ToString("n3") + " s";
        }

        private void SustainSlider_Scroll(object sender, EventArgs e)
        {
            synthEngine.Sustain = sustainSlider.Value / 1000f;
            sustainLabel.Text = (sustainSlider.Value / 10f).ToString("n0") + "%";
        }

        private void ReleaseSlider_Scroll(object sender, EventArgs e)
        {
            synthEngine.Release = releaseSlider.Value / 500f;
            releaseLabel.Text = (releaseSlider.Value / 500f).ToString("n3") + " s";
        }

        private void WaveformList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (waveformList.SelectedItem)
            {
                case "sine":
                    synthEngine.Waveform = SynthEngine.Waveforms.Sine;
                    break;
                case "sawtooth":
                    synthEngine.Waveform = SynthEngine.Waveforms.Sawtooth;
                    break;
                case "square":
                    synthEngine.Waveform = SynthEngine.Waveforms.Square;
                    break;
                case "noise":
                    synthEngine.Waveform = SynthEngine.Waveforms.Noise;
                    break;
                case "triangle":
                    synthEngine.Waveform = SynthEngine.Waveforms.Triangle;
                    break;
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioOutput.Stop();
            audioOutput.Dispose();

            if (midiIn != default(MidiIn))
            {
                midiIn.Stop();
                midiIn.Dispose();
            }
        }

        private void MidiDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (midiIn != default(MidiIn))
            {
                midiIn.Stop();
                midiIn.Dispose();
            }

            midiIn = new MidiIn(((KeyValuePair<int, String>)midiDeviceList.SelectedItem).Key);
            midiIn.MessageReceived += MidiIn_MessageReceived;
            midiIn.Start();
        }

        private void FilterBypassCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            synthEngine.FilterLevel = filterBypassCheckbox.Checked ? 0 : 1;
        }

        private void FilterModeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (filterModeList.SelectedItem)
            {
                case "LowPass":
                    synthEngine.FilterMode = FilterMode.LowPass;
                    break;
                case "HighPass":
                    synthEngine.FilterMode = FilterMode.HighPass;
                    break;
                case "BandPass":
                    synthEngine.FilterMode = FilterMode.BandPass;
                    break;
            }
        }

        private void CutoffTrackbar_Scroll(object sender, EventArgs e)
        {
            synthEngine.FilterCutoff = cutoffTrackbar.Value;
        }

        private void ResonanceTrackbar_Scroll(object sender, EventArgs e)
        {
            synthEngine.FilterQ = resonanceTrackbar.Value / 1000f;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 90:
                    foreach (var key in pressedKeys)
                        synthEngine.NoteOff(keyNote[key] + octave * 12);
                    octave--;
                    keyboardScaleLabel.Text = String.Format("keyboard scale: C{0}-C{1}", octave - 1, octave);
                    break;
                case 88:
                    foreach (var key in pressedKeys)
                        synthEngine.NoteOff(keyNote[key] + octave * 12);
                    octave++;
                    keyboardScaleLabel.Text = String.Format("keyboard scale: C{0}-C{1}", octave - 1, octave);
                    break;
            }
            
            if (!keyNote.ContainsKey(e.KeyValue))
                return;

            if (pressedKeys.Find(k => k == e.KeyValue) == default(int))
            {
                pressedKeys.Add(e.KeyValue);
                synthEngine.NoteOn(keyNote[e.KeyValue]+octave*12);
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (!keyNote.ContainsKey(e.KeyValue))
                return;

            pressedKeys.Remove(e.KeyValue);
            synthEngine.NoteOff(keyNote[e.KeyValue]+octave*12);
        }

        private void ReverbWetTrackBar_Scroll(object sender, EventArgs e)
        {
            synthEngine.ReverbLevel= reverbWetTrackBar.Value / 100f;
        }

        private void RoomSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            synthEngine.ReverbRoomSize = roomSizeTrackBar.Value / 100f;
        }

        private void DampingTrackBar_Scroll(object sender, EventArgs e)
        {
            synthEngine.ReverbDamping = dampingTrackBar.Value / 100f;
        }
    }
}