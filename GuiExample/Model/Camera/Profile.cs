using System;
using System.Collections.Generic;
using FocalSpec.FsApiNet.Model;

namespace FocalSpec.GuiExample.Model.Camera
{
    public class Profile
    {
        public IList<FsApi.Point> Points { get; }

        public FsApi.Header Header { get; }

        public float AverageIntensity { get; set; }

        public int LayerId { get; }

        public float[] ZValues { get; }

        public float[] IntensityValues { get; }

        public int LineLength { get; }

        public double XStep { get; }

        public Profile(IList<FsApi.Point> points, FsApi.Header header)
        {
            Points = points;
            Header = header;
            AverageIntensity = 0.0f;
            LayerId = -1;
        }

        public Profile(Profile profile)
        {
            Points = profile.Points;
            Header = profile.Header;
            AverageIntensity = profile.AverageIntensity;
            LayerId = profile.LayerId;
        }

        public Profile(int layerId, float[] zValues, float[] intensityValues, int lineLength, double xStep, FsApi.Header header)
        {
            Header = header;
            LayerId = layerId;
            AverageIntensity = 0;
            LineLength = lineLength;
            XStep = xStep;

            ZValues = new float[lineLength]; 
            IntensityValues = new float[lineLength]; 
            Array.Copy(zValues, ZValues, lineLength); 
            Array.Copy(intensityValues, IntensityValues, lineLength);
        }
    }
}
