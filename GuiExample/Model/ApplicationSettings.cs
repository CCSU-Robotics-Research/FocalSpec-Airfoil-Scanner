using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using FocalSpec.GuiExample.Model.Camera;

namespace FocalSpec.GuiExample.Model
{
    public static class Utils
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicallyInstalledSystemMemory(out long totalMemoryInKilobytes);
    }

    /// <summary>
    /// Stores application settings that can be changed at run-time. Singleton instance.
    /// </summary>
    public sealed class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets the last used recipe.
        /// </summary>
        public string LastRecipe { get; set; }

        /// <summary>
        /// Gets or sets the IPv4 address of the sensor. If null, auto-ip shall be used.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Backing-field of <code>ZCalibrationFile</code>.
        /// </summary>
        private string _zCalibrationFile;

        /// <summary>
        /// Gets or sets the Z calibration file.
        /// </summary>
        public string ZCalibrationFile
        {
            get => _zCalibrationFile;
            set
            {
                if (value != null)
                {
                    // Due to nature of the optics, we need to calculate average pixel width [mm].
                    GetCalibrationProperties(value, 0, Defines.DefaultSensorWidth - 1, out _, out _, out var avgGain);
                    // if gain was not set calibration file was not found from expected location.
                    if (Math.Abs(avgGain) < double.Epsilon)
                        return;
                    SensorParameterStore.GetInstance().AveragePixelHeight = avgGain;
                }
                _zCalibrationFile = value;
            }
        }

        /// <summary>
        /// Backing-field of <code>XCalibrationFile</code>.
        /// </summary>
        private string _xCalibrationFile;

        /// <summary>
        /// Gets or sets the X calibration file.
        /// </summary>
        public string XCalibrationFile
        {
            get => _xCalibrationFile;
            set
            {
                if (value != null)
                {
                    // Due to nature of the optics, we need to calculate actual min. X [mm], max. X [mm] and average pixel width [mm] from the calibration data of the sensor at hand.
                    GetCalibrationProperties(value, 0, Defines.DefaultSensorWidth - 1, out var min, out var max, out var avgGain);
                    // if gain was not set calibration file was not found from expected location.
                    if (Math.Abs(avgGain) < double.Epsilon)
                        return;

                    OpticalProfileMinX = min;
                    OpticalProfileMaxX = max;
                    SensorParameterStore.GetInstance().AveragePixelWidth = avgGain;
                }

                _xCalibrationFile = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the window in [mm].
        /// </summary>
        public int UiWindowHeight { get; set; }

        /// <summary>
        /// Gets or sets the point cloud ASC file additional attributes.
        /// 0: x, y, z coordinates
        /// 1: coordinates + intensity in float format.
        /// 2: coordinates + gray scale value in RGB format. 
        /// 3: coordinates, + intensity + gray scale. 
        /// </summary>
        public int PointCloudAscFormat { get; set; }

        public int ExpectedCameraCount { get; set; }

        private Dictionary<string, string> _cameraIds = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the camera id string containing MAC and logical names.
        /// </summary>
        public string CameraIdString { get; set; }

        /// <summary>
        /// Gets or sets the save rate used in ASC file saving. 
        /// </summary>
        [JsonIgnore]
        public int SaveRate { get; set; }

        /// <summary>
        /// Gets or sets the camera id string format.
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, string> CameraIds
        {
            get
            {
                _cameraIds.Clear();
                if (string.IsNullOrEmpty(CameraIdString))
                    return _cameraIds;

                string[] items = CameraIdString.TrimEnd(';').Split(';');
                foreach (string item in items)
                {
                    string[] keyValue = item.Split('=');
                    _cameraIds.Add(keyValue[0], keyValue[1]);
                }
                return _cameraIds; 
            }
            set
            {
                _cameraIds = value;
                CameraIdString = string.Join(";", _cameraIds.Select(x => x.Key + "=" + x.Value));
            }
        }

		/// <summary>
		/// Gets the flag indicating whether the Z calibration data is set or not.
		/// </summary>
		[JsonIgnore]
        public bool IsZCalibrationDataSet => !string.IsNullOrEmpty(ZCalibrationFile);

        /// <summary>
        /// Gets the flag indicating whether the X calibration data is set or not.
        /// </summary>
        [JsonIgnore]
        public bool IsXCalibrationDataSet => !string.IsNullOrEmpty(XCalibrationFile);

        /// <summary>
        /// Gets the min. x of the optical profile in [mm].
        /// </summary>
        [JsonIgnore]
        public double OpticalProfileMinX { get; set; }

        /// <summary>
        /// Gets the min. x of the optical profile in [mm].
        /// </summary>
        [JsonIgnore]
        public double OpticalProfileMaxX { get; set; }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        private static volatile ApplicationSettings _instance;

        /// <summary>
        /// Protects from concurrent object instantiation.
        /// </summary>
        private static readonly object InstanceLock = new object();

        /// <summary>
        /// Assigns default settings.
        /// </summary>
        private ApplicationSettings()
        {
            IpAddress = Defines.DefaultIpAddress;
            ZCalibrationFile = Defines.DefaultZCalibrationFile;
            XCalibrationFile = Defines.DefaultXCalibrationFile;
            UiWindowHeight = Defines.DefaultUiWindowSize;
            PointCloudAscFormat = Defines.DefaultPointCloudAscFormat;
	        SaveRate = Defines.DefaultSaveRate;
            ExpectedCameraCount = Defines.DefaultExpectedCameraCount;
        }

        /// <summary>
        /// Calculates min. and max. calibrated values from the calibration data given.
        /// </summary>
        /// <param name="calibrationFile">Full path to the calibration data.</param>
        /// <param name="varMin">Calibration polynomial min. parameter value.</param>
        /// <param name="varMax">Calibration polynomial max. parameter value.</param>
        /// <param name="calibMin">Min. calibrated value found.</param>
        /// <param name="calibMax">Max. calibrated value found.</param>
        /// <param name="avgGain">Average gain value among polynomials.</param>
        private void GetCalibrationProperties(string calibrationFile, int varMin, int varMax, out double calibMin, out double calibMax, out double avgGain)
        {
            calibMin = double.MaxValue;
            calibMax = double.MinValue;
            avgGain = 0.0;
            string[] lines;
            try
            {
                lines = File.ReadAllLines(calibrationFile);
            }
            catch
            {
                return;
            }
         
            var gain = new List<double>();

            foreach (string line in lines)
            {
                if (line.Contains(";"))
                {
                    List<double> coeffs = line.Split(';').Skip(1).Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToList();

                    gain.Add(coeffs[1]);
				    double minCandidate = Math.Min(Horner(coeffs, varMin), Horner(coeffs, varMax));
                    double maxCandidate = Math.Max(Horner(coeffs, varMin), Horner(coeffs, varMax));

                    calibMin = minCandidate < calibMin ? minCandidate : calibMin;
                    calibMax = maxCandidate > calibMax ? maxCandidate : calibMax;
                }
            }

            avgGain = gain.Average();
        }

        /// <summary>
        /// Horner polynomial evaluation routine at given x.
        /// </summary>
        /// <param name="coeffs">Polynomial coefficients: 0 degree, 1st degree, ..., nth degree.</param>
        /// <param name="x">Parameter value.</param>
        /// <returns>Value of the polynomial at x.</returns>
        private double Horner(List<double> coeffs, double x)
        {
            double s = 0.0f;

            for (int i = coeffs.Count-1; i >= 0; i--)
            {
                s = s * x + coeffs[i];
            }

	        return s;
        }

        /// <summary>
        /// Saves settings to file.
        /// </summary>
        public void SaveToFile()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            try
            {
                File.WriteAllText(Defines.ApplicationSettingsFile, json);
            }
            catch (Exception)
            {
                // If application not having write access to installation folder, common application data folder is used. 
                string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string path = appDataDir + Defines.FsAppDataDir; 
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                                         
                path += Defines.ApplicationSettingsFile;
                File.WriteAllText(path, json);
            }
        }

        /// <summary>
        /// Loads settings from file.
        /// </summary>
        /// <returns>New MainViewSettings object initialized from the read settings.</returns>
        public static ApplicationSettings LoadFromFile()
        {
            if (_instance != null) return _instance;

            string json = "";
            bool exists = File.Exists(Defines.ApplicationSettingsFile);
            if (exists)
                json = File.ReadAllText(Defines.ApplicationSettingsFile);
            else
            {
                string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string path = appDataDir + Defines.FsAppDataDir + Defines.ApplicationSettingsFile;

                exists = File.Exists(path);
                if (exists)
                    json = File.ReadAllText(path);
            }

            lock (InstanceLock)
            {
                _instance = exists ? JsonConvert.DeserializeObject<ApplicationSettings>(json) : new ApplicationSettings();
            }

            return _instance;
        }
    }
}
