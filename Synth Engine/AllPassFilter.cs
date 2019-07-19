using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace NAudio_Synth
{
    class AllPassFilter : WaveEffect32
    {
        private float[] xl, xr, yl, yr;
        private float g;
        private int posl, posr;
        private bool left;

        public AllPassFilter(WaveProvider32 input, int delay, float gain) : base(input)
        {
            left = true;
            g = gain;
            xl = new float[delay];
            xr = new float[delay];
            yl = new float[delay];
            yr = new float[delay];
            for (int i = 0; i < delay; ++ i)
                xl[i] = xr[i] = yl[i] = yr[i] = 0;
            posl = posr = 0;
        }

        // y[n] = - g * x[n] + x[n-delay] + g * y[n-delay]
        public override float Apply(float sample)
        {
            float yn;

            if (left)
            {
                yn = - g * sample + xl[posl] + g * yl[posl];

                xl[posl] = sample;
                yl[posl] = yn;

                posl = (posl + 1) % xl.Length;
            }
            else
            {
                yn = - g * sample + xr[posr] + g * yr[posr];

                xr[posr] = sample;
                yr[posr] = yn;

                posr = (posr + 1) % xr.Length;
            }

            left = !left;

            return yn;
        }
    }
}
