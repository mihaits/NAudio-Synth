# NAudio-Synth
A simple software synthesizer implemented in C# using the NAudio library. It was started as a personal introduction to digital signal processing because of interest in electronic music production and used as a BS thesis project.

Features include:
* [keyboard](https://sonicbloom.net/wp-content/uploads/tutorial_pics/ckeyboard.png) and MIDI input for triggering notes
* multiple waveforms: sine, sawtooth, square and triangle
* ADSR envelope for volume
* a LowPass, HighPass or BandPass filter with knobs cutoff frequency and resonance
* ADSR envelope for the filter cutoff frequency
* a simple Reverb effect (based on the popular open-source [Freeverb](https://ccrma.stanford.edu/~jos/pasp/Freeverb.html))
