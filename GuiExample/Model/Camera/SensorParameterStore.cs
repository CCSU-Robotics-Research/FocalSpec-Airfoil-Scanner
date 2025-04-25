namespace FocalSpec.GuiExample.Model.Camera
{
    public class SensorParameterStore
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        private static SensorParameterStore _instance;
        public static SensorParameterStore GetInstance() => _instance ?? (_instance = new SensorParameterStore());

        public class LayerParameter
        {
            public LayerParameter()
            {
                IntensityType = 0;
                MaxThickness = 0;
                MinThickness = 0;
                RefractiveIndex = 1;
            }
            public int IntensityType;
            public double MaxThickness;
            public double MinThickness;
            public double RefractiveIndex;
        }

        private SensorParameterStore()
        {
            Layers = new LayerParameter[10];
            for (int i = 0; i < Layers.Length; i++) { Layers[i] = new LayerParameter(); }

            MaxLedPulseWidth = Defines.DefaultMaxPulseWidth;
            Freq = Defines.DefaultTriggerFrequency;
            IsAgcEnabled = Defines.DefaultAgcState;
            AgcTargetIntensity = Defines.DefaultAgcTargetIntensity;
            PulseDivider = Defines.DefaultPulseDivider;
            JumboFrameMtu = Defines.DefaultJumboFrameMtu;
            Current = Defines.DefaultCurrent;
            Gain = Defines.DefaultGain;
            GainHs = Defines.DefaultGainHs;
            FirLength = Defines.DefaultFirLength;
            AverFirLength = Defines.DefaultAverFirLength;
            DetectionFilter = Defines.DefaultDetectionFilter;
            AverageIntensityFilter = Defines.DefaultAverageIntensityFilter;
            Threshold = Defines.DefaultThreshold;
            SensorWidth = Defines.DefaultSensorWidth;
            MaxPointCount = Defines.DefaultMaxPointCount;
            HdrEnabled = Defines.DefaultHdrEnabled;
            HdrVLow2 = Defines.DefaultHdrVLow2;
            HdrKp1Pos = Defines.DefaultHdrKp1Pos;
        }

        public float LedPulseWidth { get; set; }
        public int MaxLedPulseWidth { get; set; }
        public int Freq { get; set; }
        public bool IsExternalPulsingEnabled { get; set; }
        public double AveragePixelWidth { get; set; }
        public double AveragePixelHeight { get; set; }
        public bool IsAgcEnabled { get; set; }
        public float AgcTargetIntensity { get; set; }
        public int PulseDivider { get; set; }
        public int JumboFrameMtu { get; set; }
        public double Current { get; set; }
        public double Gain { get; set; }
        public double GainHs { get; set; }
        public int TriggerSource { get; set; }
        public float LayerMinThickness { get; set; }
        public int FirLength { get; set; }
        public int AverFirLength { get; set; }
        public int DetectionFilter { get; set; }
        public int AverageIntensityFilter { get; set; }
        public int Threshold { get; set; }
        public int MaxPointCount { get; set; }
        public bool HdrEnabled { get; set; }
        public float HdrVLow2 { get; set; } = 114;
        public float HdrVLow3 { get; set; } = 116;
        public float HdrKp1Pos { get; set; } = 10;
        public float HdrKp2Pos { get; set; } = 25;
        public int SensorWidth { get; set; }
        public int XOffset { get; set; }
        public int SensorType { get; set; }
        public bool IsPeakEnabled { get; set; }
        public bool NoiseRemoval { get; set; }
        public double AverageZFilterSize { get; set; }
        public double AverageIntensityFilterSize { get; set; }
        public int MedianZFilterSize { get; set; }
        public int MedianIntensityFilterSize { get; set; }
        public double ResampleLineXResolution { get; set; }
        public int PeakXFilter { get; set; }
        public int OffsetY { get; set; }
        public bool IsThicknessMode { get; set; }
        public double FillGapMax { get; set; }
        public bool IsTrimEdges { get; set; }
        public LayerParameter[] Layers { get; set; }
    }
}
