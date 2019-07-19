namespace NAudio_Synth
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.attackSlider = new System.Windows.Forms.TrackBar();
            this.decaySlider = new System.Windows.Forms.TrackBar();
            this.sustainSlider = new System.Windows.Forms.TrackBar();
            this.releaseSlider = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.attackLabel = new System.Windows.Forms.Label();
            this.releaseLabel = new System.Windows.Forms.Label();
            this.sustainLabel = new System.Windows.Forms.Label();
            this.decayLabel = new System.Windows.Forms.Label();
            this.waveformList = new System.Windows.Forms.ComboBox();
            this.waveformLabel = new System.Windows.Forms.Label();
            this.midiDeviceList = new System.Windows.Forms.ComboBox();
            this.midiDeviceLabel = new System.Windows.Forms.Label();
            this.filterBypassCheckbox = new System.Windows.Forms.CheckBox();
            this.filterModeList = new System.Windows.Forms.ComboBox();
            this.filterModeLabel = new System.Windows.Forms.Label();
            this.cutoffTrackbar = new System.Windows.Forms.TrackBar();
            this.cutoffLabel = new System.Windows.Forms.Label();
            this.resonanceTrackbar = new System.Windows.Forms.TrackBar();
            this.resonanceLabel = new System.Windows.Forms.Label();
            this.reverbWetTrackBar = new System.Windows.Forms.TrackBar();
            this.roomSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.deverbWetLabel = new System.Windows.Forms.Label();
            this.dampingTrackBar = new System.Windows.Forms.TrackBar();
            this.roomSizeLabel = new System.Windows.Forms.Label();
            this.dampingLabel = new System.Windows.Forms.Label();
            this.keyboardCheckBox = new System.Windows.Forms.CheckBox();
            this.keyboardScaleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.attackSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decaySlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sustainSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.releaseSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cutoffTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resonanceTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reverbWetTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roomSizeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dampingTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // attackSlider
            // 
            this.attackSlider.Location = new System.Drawing.Point(12, 12);
            this.attackSlider.Maximum = 1000;
            this.attackSlider.Name = "attackSlider";
            this.attackSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.attackSlider.Size = new System.Drawing.Size(45, 185);
            this.attackSlider.TabIndex = 0;
            this.attackSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.attackSlider.Scroll += new System.EventHandler(this.AttackSlider_Scroll);
            // 
            // decaySlider
            // 
            this.decaySlider.Location = new System.Drawing.Point(63, 12);
            this.decaySlider.Maximum = 1000;
            this.decaySlider.Name = "decaySlider";
            this.decaySlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.decaySlider.Size = new System.Drawing.Size(45, 185);
            this.decaySlider.TabIndex = 1;
            this.decaySlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.decaySlider.Scroll += new System.EventHandler(this.DecaySlider_Scroll);
            // 
            // sustainSlider
            // 
            this.sustainSlider.Location = new System.Drawing.Point(114, 12);
            this.sustainSlider.Maximum = 1000;
            this.sustainSlider.Name = "sustainSlider";
            this.sustainSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sustainSlider.Size = new System.Drawing.Size(45, 185);
            this.sustainSlider.TabIndex = 2;
            this.sustainSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sustainSlider.Value = 1000;
            this.sustainSlider.Scroll += new System.EventHandler(this.SustainSlider_Scroll);
            // 
            // releaseSlider
            // 
            this.releaseSlider.Location = new System.Drawing.Point(165, 12);
            this.releaseSlider.Maximum = 1000;
            this.releaseSlider.Name = "releaseSlider";
            this.releaseSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.releaseSlider.Size = new System.Drawing.Size(45, 185);
            this.releaseSlider.TabIndex = 3;
            this.releaseSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.releaseSlider.Scroll += new System.EventHandler(this.ReleaseSlider_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "decay";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "attack";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "release";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(111, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "sustain";
            // 
            // attackLabel
            // 
            this.attackLabel.AutoSize = true;
            this.attackLabel.Location = new System.Drawing.Point(13, 217);
            this.attackLabel.Name = "attackLabel";
            this.attackLabel.Size = new System.Drawing.Size(21, 13);
            this.attackLabel.TabIndex = 11;
            this.attackLabel.Text = "0 s";
            // 
            // releaseLabel
            // 
            this.releaseLabel.AutoSize = true;
            this.releaseLabel.Location = new System.Drawing.Point(157, 217);
            this.releaseLabel.Name = "releaseLabel";
            this.releaseLabel.Size = new System.Drawing.Size(21, 13);
            this.releaseLabel.TabIndex = 12;
            this.releaseLabel.Text = "0 s";
            // 
            // sustainLabel
            // 
            this.sustainLabel.AutoSize = true;
            this.sustainLabel.Location = new System.Drawing.Point(111, 217);
            this.sustainLabel.Name = "sustainLabel";
            this.sustainLabel.Size = new System.Drawing.Size(33, 13);
            this.sustainLabel.TabIndex = 13;
            this.sustainLabel.Text = "100%";
            // 
            // decayLabel
            // 
            this.decayLabel.AutoSize = true;
            this.decayLabel.Location = new System.Drawing.Point(60, 217);
            this.decayLabel.Name = "decayLabel";
            this.decayLabel.Size = new System.Drawing.Size(21, 13);
            this.decayLabel.TabIndex = 14;
            this.decayLabel.Text = "0 s";
            // 
            // waveformList
            // 
            this.waveformList.FormattingEnabled = true;
            this.waveformList.Location = new System.Drawing.Point(201, 28);
            this.waveformList.Name = "waveformList";
            this.waveformList.Size = new System.Drawing.Size(121, 21);
            this.waveformList.TabIndex = 4;
            this.waveformList.SelectedIndexChanged += new System.EventHandler(this.WaveformList_SelectedIndexChanged);
            // 
            // waveformLabel
            // 
            this.waveformLabel.AutoSize = true;
            this.waveformLabel.Location = new System.Drawing.Point(198, 12);
            this.waveformLabel.Name = "waveformLabel";
            this.waveformLabel.Size = new System.Drawing.Size(53, 13);
            this.waveformLabel.TabIndex = 16;
            this.waveformLabel.Text = "waveform";
            // 
            // midiDeviceList
            // 
            this.midiDeviceList.FormattingEnabled = true;
            this.midiDeviceList.Location = new System.Drawing.Point(201, 68);
            this.midiDeviceList.Name = "midiDeviceList";
            this.midiDeviceList.Size = new System.Drawing.Size(121, 21);
            this.midiDeviceList.TabIndex = 5;
            this.midiDeviceList.SelectedIndexChanged += new System.EventHandler(this.MidiDeviceList_SelectedIndexChanged);
            // 
            // midiDeviceLabel
            // 
            this.midiDeviceLabel.AutoSize = true;
            this.midiDeviceLabel.Location = new System.Drawing.Point(198, 52);
            this.midiDeviceLabel.Name = "midiDeviceLabel";
            this.midiDeviceLabel.Size = new System.Drawing.Size(91, 13);
            this.midiDeviceLabel.TabIndex = 16;
            this.midiDeviceLabel.Text = "MIDI input device";
            // 
            // filterBypassCheckbox
            // 
            this.filterBypassCheckbox.AutoSize = true;
            this.filterBypassCheckbox.Checked = true;
            this.filterBypassCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filterBypassCheckbox.Location = new System.Drawing.Point(331, 183);
            this.filterBypassCheckbox.Name = "filterBypassCheckbox";
            this.filterBypassCheckbox.Size = new System.Drawing.Size(81, 17);
            this.filterBypassCheckbox.TabIndex = 10;
            this.filterBypassCheckbox.Text = "bypass filter";
            this.filterBypassCheckbox.UseVisualStyleBackColor = true;
            this.filterBypassCheckbox.CheckedChanged += new System.EventHandler(this.FilterBypassCheckBox_CheckedChanged);
            // 
            // filterModeList
            // 
            this.filterModeList.FormattingEnabled = true;
            this.filterModeList.Location = new System.Drawing.Point(331, 28);
            this.filterModeList.Name = "filterModeList";
            this.filterModeList.Size = new System.Drawing.Size(121, 21);
            this.filterModeList.TabIndex = 7;
            this.filterModeList.SelectedIndexChanged += new System.EventHandler(this.FilterModeList_SelectedIndexChanged);
            // 
            // filterModeLabel
            // 
            this.filterModeLabel.AutoSize = true;
            this.filterModeLabel.Location = new System.Drawing.Point(328, 12);
            this.filterModeLabel.Name = "filterModeLabel";
            this.filterModeLabel.Size = new System.Drawing.Size(55, 13);
            this.filterModeLabel.TabIndex = 21;
            this.filterModeLabel.Text = "filter mode";
            // 
            // cutoffTrackbar
            // 
            this.cutoffTrackbar.Location = new System.Drawing.Point(331, 68);
            this.cutoffTrackbar.Maximum = 20000;
            this.cutoffTrackbar.Minimum = 1;
            this.cutoffTrackbar.Name = "cutoffTrackbar";
            this.cutoffTrackbar.Size = new System.Drawing.Size(121, 45);
            this.cutoffTrackbar.TabIndex = 8;
            this.cutoffTrackbar.TickFrequency = 5000;
            this.cutoffTrackbar.Value = 1000;
            this.cutoffTrackbar.Scroll += new System.EventHandler(this.CutoffTrackbar_Scroll);
            // 
            // cutoffLabel
            // 
            this.cutoffLabel.AutoSize = true;
            this.cutoffLabel.Location = new System.Drawing.Point(328, 52);
            this.cutoffLabel.Name = "cutoffLabel";
            this.cutoffLabel.Size = new System.Drawing.Size(84, 13);
            this.cutoffLabel.TabIndex = 23;
            this.cutoffLabel.Text = "cutoff frequency";
            // 
            // resonanceTrackbar
            // 
            this.resonanceTrackbar.Location = new System.Drawing.Point(331, 132);
            this.resonanceTrackbar.Maximum = 3000;
            this.resonanceTrackbar.Minimum = 1;
            this.resonanceTrackbar.Name = "resonanceTrackbar";
            this.resonanceTrackbar.Size = new System.Drawing.Size(121, 45);
            this.resonanceTrackbar.TabIndex = 9;
            this.resonanceTrackbar.TickFrequency = 1000;
            this.resonanceTrackbar.Value = 1000;
            this.resonanceTrackbar.Scroll += new System.EventHandler(this.ResonanceTrackbar_Scroll);
            // 
            // resonanceLabel
            // 
            this.resonanceLabel.AutoSize = true;
            this.resonanceLabel.Location = new System.Drawing.Point(328, 116);
            this.resonanceLabel.Name = "resonanceLabel";
            this.resonanceLabel.Size = new System.Drawing.Size(57, 13);
            this.resonanceLabel.TabIndex = 24;
            this.resonanceLabel.Text = "resonance";
            // 
            // reverbWetTrackBar
            // 
            this.reverbWetTrackBar.Location = new System.Drawing.Point(458, 28);
            this.reverbWetTrackBar.Maximum = 100;
            this.reverbWetTrackBar.Name = "reverbWetTrackBar";
            this.reverbWetTrackBar.RightToLeftLayout = true;
            this.reverbWetTrackBar.Size = new System.Drawing.Size(104, 45);
            this.reverbWetTrackBar.TabIndex = 11;
            this.reverbWetTrackBar.TickFrequency = 50;
            this.reverbWetTrackBar.Value = 1;
            this.reverbWetTrackBar.Scroll += new System.EventHandler(this.ReverbWetTrackBar_Scroll);
            // 
            // roomSizeTrackBar
            // 
            this.roomSizeTrackBar.Location = new System.Drawing.Point(458, 79);
            this.roomSizeTrackBar.Maximum = 100;
            this.roomSizeTrackBar.Minimum = 1;
            this.roomSizeTrackBar.Name = "roomSizeTrackBar";
            this.roomSizeTrackBar.Size = new System.Drawing.Size(104, 45);
            this.roomSizeTrackBar.TabIndex = 12;
            this.roomSizeTrackBar.TickFrequency = 50;
            this.roomSizeTrackBar.Value = 84;
            this.roomSizeTrackBar.Scroll += new System.EventHandler(this.RoomSizeTrackBar_Scroll);
            // 
            // deverbWetLabel
            // 
            this.deverbWetLabel.AutoSize = true;
            this.deverbWetLabel.Location = new System.Drawing.Point(458, 12);
            this.deverbWetLabel.Name = "deverbWetLabel";
            this.deverbWetLabel.Size = new System.Drawing.Size(76, 13);
            this.deverbWetLabel.TabIndex = 27;
            this.deverbWetLabel.Text = "reverb dry/wet";
            // 
            // dampingTrackBar
            // 
            this.dampingTrackBar.Location = new System.Drawing.Point(458, 127);
            this.dampingTrackBar.Maximum = 100;
            this.dampingTrackBar.Minimum = 1;
            this.dampingTrackBar.Name = "dampingTrackBar";
            this.dampingTrackBar.Size = new System.Drawing.Size(104, 45);
            this.dampingTrackBar.TabIndex = 13;
            this.dampingTrackBar.TickFrequency = 50;
            this.dampingTrackBar.Value = 20;
            this.dampingTrackBar.Scroll += new System.EventHandler(this.DampingTrackBar_Scroll);
            // 
            // roomSizeLabel
            // 
            this.roomSizeLabel.AutoSize = true;
            this.roomSizeLabel.Location = new System.Drawing.Point(458, 60);
            this.roomSizeLabel.Name = "roomSizeLabel";
            this.roomSizeLabel.Size = new System.Drawing.Size(51, 13);
            this.roomSizeLabel.TabIndex = 29;
            this.roomSizeLabel.Text = "room size";
            // 
            // dampingLabel
            // 
            this.dampingLabel.AutoSize = true;
            this.dampingLabel.Location = new System.Drawing.Point(458, 111);
            this.dampingLabel.Name = "dampingLabel";
            this.dampingLabel.Size = new System.Drawing.Size(47, 13);
            this.dampingLabel.TabIndex = 30;
            this.dampingLabel.Text = "damping";
            // 
            // keyboardCheckBox
            // 
            this.keyboardCheckBox.AutoSize = true;
            this.keyboardCheckBox.Location = new System.Drawing.Point(201, 136);
            this.keyboardCheckBox.Name = "keyboardCheckBox";
            this.keyboardCheckBox.Size = new System.Drawing.Size(96, 17);
            this.keyboardCheckBox.TabIndex = 31;
            this.keyboardCheckBox.Text = "keyboard input";
            this.keyboardCheckBox.UseVisualStyleBackColor = true;
            // 
            // keyboardScaleLabel
            // 
            this.keyboardScaleLabel.AutoSize = true;
            this.keyboardScaleLabel.Location = new System.Drawing.Point(201, 163);
            this.keyboardScaleLabel.Name = "keyboardScaleLabel";
            this.keyboardScaleLabel.Size = new System.Drawing.Size(114, 13);
            this.keyboardScaleLabel.TabIndex = 32;
            this.keyboardScaleLabel.Text = "keyboard scale: C4-C5";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 245);
            this.Controls.Add(this.keyboardScaleLabel);
            this.Controls.Add(this.keyboardCheckBox);
            this.Controls.Add(this.dampingLabel);
            this.Controls.Add(this.roomSizeLabel);
            this.Controls.Add(this.dampingTrackBar);
            this.Controls.Add(this.deverbWetLabel);
            this.Controls.Add(this.roomSizeTrackBar);
            this.Controls.Add(this.reverbWetTrackBar);
            this.Controls.Add(this.resonanceLabel);
            this.Controls.Add(this.resonanceTrackbar);
            this.Controls.Add(this.cutoffLabel);
            this.Controls.Add(this.cutoffTrackbar);
            this.Controls.Add(this.filterModeLabel);
            this.Controls.Add(this.filterModeList);
            this.Controls.Add(this.filterBypassCheckbox);
            this.Controls.Add(this.midiDeviceLabel);
            this.Controls.Add(this.waveformLabel);
            this.Controls.Add(this.midiDeviceList);
            this.Controls.Add(this.waveformList);
            this.Controls.Add(this.decayLabel);
            this.Controls.Add(this.sustainLabel);
            this.Controls.Add(this.releaseLabel);
            this.Controls.Add(this.attackLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.releaseSlider);
            this.Controls.Add(this.sustainSlider);
            this.Controls.Add(this.decaySlider);
            this.Controls.Add(this.attackSlider);
            this.Name = "MainWindow";
            this.Text = "PDAV Synth";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.attackSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decaySlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sustainSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.releaseSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cutoffTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resonanceTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reverbWetTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roomSizeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dampingTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar attackSlider;
        private System.Windows.Forms.TrackBar decaySlider;
        private System.Windows.Forms.TrackBar sustainSlider;
        private System.Windows.Forms.TrackBar releaseSlider;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label attackLabel;
        private System.Windows.Forms.Label releaseLabel;
        private System.Windows.Forms.Label sustainLabel;
        private System.Windows.Forms.Label decayLabel;
        private System.Windows.Forms.ComboBox waveformList;
        private System.Windows.Forms.Label waveformLabel;
        private System.Windows.Forms.ComboBox midiDeviceList;
        private System.Windows.Forms.Label midiDeviceLabel;
        private System.Windows.Forms.CheckBox filterBypassCheckbox;
        private System.Windows.Forms.ComboBox filterModeList;
        private System.Windows.Forms.Label filterModeLabel;
        private System.Windows.Forms.TrackBar cutoffTrackbar;
        private System.Windows.Forms.Label cutoffLabel;
        private System.Windows.Forms.TrackBar resonanceTrackbar;
        private System.Windows.Forms.Label resonanceLabel;
        private System.Windows.Forms.TrackBar reverbWetTrackBar;
        private System.Windows.Forms.TrackBar roomSizeTrackBar;
        private System.Windows.Forms.Label deverbWetLabel;
        private System.Windows.Forms.TrackBar dampingTrackBar;
        private System.Windows.Forms.Label roomSizeLabel;
        private System.Windows.Forms.Label dampingLabel;
        private System.Windows.Forms.CheckBox keyboardCheckBox;
        private System.Windows.Forms.Label keyboardScaleLabel;
    }
}

