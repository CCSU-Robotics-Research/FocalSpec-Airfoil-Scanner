using System;

namespace FocalSpecSDK
{
    /// <summary>
    /// Defines methods to operate the FocalSpec LCI 1600 scanner.
    /// </summary>
    public interface ILci1600Scanner
    {
        /// <summary>
        /// Opens the API and initializes resources.
        /// </summary>
        /// <returns>True if successful, otherwise false.</returns>
        bool Open();

        /// <summary>
        /// Connects to the scanner with the given camera ID.
        /// </summary>
        /// <param name="cameraId">The ID of the camera to connect.</param>
        /// <returns>True if connected successfully, otherwise false.</returns>
        bool Connect(string cameraId);

        /// <summary>
        /// Loads calibration files for Z and X dimensions.
        /// </summary>
        /// <param name="zCalibrationPath">Path to the Z calibration file.</param>
        /// <param name="xCalibrationPath">Path to the X calibration file.</param>
        /// <returns>True if loaded successfully, otherwise false.</returns>
        bool LoadCalibration(string zCalibrationPath, string xCalibrationPath);

        /// <summary>
        /// Loads a recipe file for configuring sensor parameters.
        /// </summary>
        /// <param name="recipePath">The file path to the recipe.</param>
        /// <returns>True if loaded successfully, otherwise false.</returns>
        bool LoadRecipe(string recipePath);

        /// <summary>
        /// Starts data acquisition.
        /// </summary>
        /// <returns>True if acquisition started, otherwise false.</returns>
        bool StartGrabbing();

        /// <summary>
        /// Stops data acquisition.
        /// </summary>
        /// <returns>True if stopped successfully, otherwise false.</returns>
        bool StopGrabbing();

        /// <summary>
        /// Sets a callback to handle scan line data for a specific layer.
        /// </summary>
        /// <param name="layer">The layer index for which to set the callback.</param>
        /// <param name="callback">An action to handle the received scan line data.</param>
        void SetLineCallback(int layer, Action<ScanLineData> callback);

        /// <summary>
        /// Sets a sensor parameter.
        /// </summary>
        /// <param name="parameter">The parameter to set.</param>
        /// <param name="value">The value to assign to the parameter.</param>
        /// <returns>True if the parameter was set successfully, otherwise false.</returns>
        bool SetParameter(SensorParameter parameter, object value);

        /// <summary>
        /// Retrieves the value of a sensor parameter.
        /// </summary>
        /// <param name="parameter">The parameter to retrieve.</param>
        /// <returns>The current value of the parameter.</returns>
        object GetParameter(SensorParameter parameter);

        /// <summary>
        /// Disconnects from the scanner and releases resources.
        /// </summary>
        /// <returns>True if disconnected successfully, otherwise false.</returns>
        bool Disconnect();

        /// <summary>
        /// Gets a value indicating whether the scanner is currently connected.
        /// </summary>
        bool IsConnected { get; }
    }

    /// <summary>
    /// Represents the data of a single scan line received from the scanner.
    /// </summary>
    public class ScanLineData
    {
        public float[] ZValues { get; set; }
        public float[] IntensityValues { get; set; }
        public int LineLength { get; set; }
        public double XStep { get; set; }
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Defines common sensor parameters.
    /// You can extend this enum to include all required parameters as defined in the SDK.
    /// </summary>
    public enum SensorParameter
    {
        PeakEnabled,
        ZCalibrationFile,
        XCalibrationFile,
        PeakXUnit,
        PeakYUnit,
        LoadRecipe,
        // Extend with additional parameters as needed...
    }

    
    /// <summary>
    /// A sample implementation of the ILci1600Scanner interface using the FsApiNet.dll wrapper.
    /// </summary>
    public class Lci1600ScannerImpl : ILci1600Scanner
        {
            // Field to hold our sensor wrapper instance.
            // This is a hypothetical class provided by FsApiNet.dll.
            private Sensor _sensor;

            // Holds the ID for the connected sensor.
            private string _sensorId;

            public bool IsConnected { get; private set; }

            /// <summary>
            /// Initializes the API by creating the sensor wrapper instance.
            /// </summary>
            public bool Open()
            {
                try
                {
                    // Example: initialize the sensor API
                    _sensor = new Sensor();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error opening sensor API: " + ex.Message);
                    return false;
                }
            }

            /// <summary>
            /// Connects to a sensor using its unique identifier.
            /// </summary>
            public bool Connect(string cameraId)
            {
                try
                {
                    _sensorId = cameraId;
                    // Example: Use the wrapper's Connect method.
                    // The second parameter could be an optional IP address (null for auto-IP).
                    bool result = _sensor.Connect(cameraId, null);
                    IsConnected = result;
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to sensor: " + ex.Message);
                    return false;
                }
            }

            /// <summary>
            /// Loads the calibration files (Z and X) into the sensor.
            /// </summary>
            public bool LoadCalibration(string zCalibrationPath, string xCalibrationPath)
            {
                try
                {
                    // Set the calibration files using parameters defined in the SDK.
                    bool statusZ = _sensor.SetParameter(_sensorId, SensorParameter.ZCalibrationFile, zCalibrationPath);
                    bool statusX = _sensor.SetParameter(_sensorId, SensorParameter.XCalibrationFile, xCalibrationPath);
                    return statusZ && statusX;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading calibration files: " + ex.Message);
                    return false;
                }
            }

            /// <summary>
            /// Loads a recipe file to configure sensor settings.
            /// </summary>
            public bool LoadRecipe(string recipePath)
            {
                try
                {
                    return _sensor.SetParameter(_sensorId, SensorParameter.LoadRecipe, recipePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading recipe: " + ex.Message);
                    return false;
                }
            }

            /// <summary>
            /// Starts the data acquisition process.
            /// </summary>
            public bool StartGrabbing()
            {
                try
                {
                    return _sensor.StartGrabbing(_sensorId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error starting acquisition: " + ex.Message);
                    return false;
                }
            }

            /// <summary>
            /// Stops the data acquisition process.
            /// </summary>
            public bool StopGrabbing()
            {
                try
                {
                    return _sensor.StopGrabbing(_sensorId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error stopping acquisition: " + ex.Message);
                    return false;
                }
            }

            /// <summary>
            /// Registers a callback to receive scan line data for a specified layer.
            /// </summary>
            public void SetLineCallback(int layer, Action<ScanLineData> callback)
            {
                // Assume that the Sensor class exposes a method for setting a line callback.
                // The SDK documentation (Section 2.1) shows the C-style callback signature.
                // Here we wrap that in a lambda to convert the parameters into our ScanLineData.
                _sensor.SetLineCallback(_sensorId, layer,
                    (string id, int layerIndex, float[] zValues, float[] intensityValues, int lineLength, double xStep, object header) =>
                    {
                        var data = new ScanLineData
                        {
                            ZValues = zValues,
                            IntensityValues = intensityValues,
                            LineLength = lineLength,
                            XStep = xStep,
                            // If the header doesn’t provide a timestamp, we use the current time.
                            Timestamp = DateTime.Now
                        };
                        callback?.Invoke(data);
                    });
            }

            /// <summary>
            /// Sets a sensor parameter.
            /// </summary>
            public bool SetParameter(SensorParameter parameter, object value)
            {
                try
                {
                    return _sensor.SetParameter(_sensorId, parameter, value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error setting parameter: " + ex.Message);
                    return false;
                }
            }

            /// <summary>
            /// Retrieves the current value of a sensor parameter.
            /// </summary>
            public object GetParameter(SensorParameter parameter)
            {
                try
                {
                    return _sensor.GetParameter(_sensorId, parameter);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error getting parameter: " + ex.Message);
                    return null;
                }
            }

            /// <summary>
            /// Disconnects from the sensor and cleans up resources.
            /// </summary>
            public bool Disconnect()
            {
                try
                {
                    bool result = _sensor.Disconnect(_sensorId);
                    IsConnected = false;
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error disconnecting: " + ex.Message);
                    return false;
                }
            }
        }
    }
}