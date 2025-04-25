using System;

namespace FocalSpec.GuiExample.View
{
    public static class ColorConverter
    {
        /// <summary>
        /// Convert HSV to RGB
        /// h is from 0-360
        /// s,v values are 0-1
        /// r,g,b values are 0-255
        /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
        /// </summary>
        public static void HsvToRgb(double h, out byte r, out byte g, out byte b)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;

            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = 0;
                double qv = 1 - f;
                double tv = f;
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = 1;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = 1;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = 1;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = 1;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = 1;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = 1;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = 1;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = 1;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = 1; // Just pretend its black/white
                        break;
                }
            }
            r = (byte)(R * 255.0);
            g = (byte)(G * 255.0);
            b = (byte)(B * 255.0);
        }
    }
}
