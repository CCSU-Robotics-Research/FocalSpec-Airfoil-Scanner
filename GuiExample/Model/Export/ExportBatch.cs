// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportBatch.cs" company="FocalSpec Oy">
//   FocalSpec Oy 2016-
// </copyright>
// <summary>
//   Exports profile batches.
// </summary> 
// -------------------------------------------------------------------------------------------------------------------- 

namespace FocalSpec.GuiExample.Model.Export
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using FsApiNet.Model;
    using Camera;
    using Model;

    /// <summary>
    /// Batch export utility.
    /// </summary>
    public class ExportBatch
    {
        private const int SaveIntensity = 1;
        private const int SaveGrayScale = 2;
        private const float NoMeas = FsApi.NoMeasurement - 1;

        /// <summary>
        /// Object indicating ASC files format for possible additional intensity or RGB grayscale value.
        /// </summary>
        private static int _pointCloudAscFormat;

	    /// <summary>
	    /// Calibrated sensor width.
	    /// </summary>
	    private static int _sensorWidth;

        /// <summary>
        /// Appends to point cloud profile to an existing file.
        /// </summary>
        /// <param name="file">The file where the points should be appended to.</param>
        /// <param name="profile">The profile to be exported.</param>
        /// <param name="batchStepLength">The Y distance [mm] between two subsequent profiles in a batch.</param>
        /// <param name="index">Zero-based index of the profile.</param>
        private static void AppendToPointCloudFile(string file, Profile profile, double batchStepLength, int index)
        {
            var str = new StringBuilder();

            if (profile.LayerId < 0)
            {
                List<FsApi.Point> points = profile.Points.ToList();
                foreach (FsApi.Point point in points)
                {
                    str.AppendLine(AppendPointCloudLine(batchStepLength, index, point.X, point.Y, point.Intensity));
                }
            }
            else
            {
                for (int i = 0; i < profile.LineLength; ++i)
                {
                    if (profile.ZValues[i] > NoMeas)
                    {
                        continue;
                    }

                    str.AppendLine(AppendPointCloudLine(batchStepLength, index, (float)(i * profile.XStep), profile.ZValues[i], profile.IntensityValues[i]));
                }
            }

            File.AppendAllText(file, str.ToString());
        }

        private static string AppendPointCloudLine(double batchStepLength, int index, float x, float y, float intensity)
        {
            string line;
            if ((_pointCloudAscFormat & SaveIntensity) == SaveIntensity)
            {
                line = string.Format(CultureInfo.InvariantCulture,
                    "{0:0.000000} {1:0.000000} {2:0.000000} {3:0.000}", x * Defines.ProfileScale,
                    index * batchStepLength, y * Defines.ProfileScale, intensity);
            }
            else
            {
                line = string.Format(CultureInfo.InvariantCulture, "{0:0.000000} {1:0.000000} {2:0.000000}", x * Defines.ProfileScale,
                    index * batchStepLength, y * Defines.ProfileScale);
            }

            if ((_pointCloudAscFormat & SaveGrayScale) == SaveGrayScale)
            {
                // Add GrayScale
                var grayScale = (int)intensity;
                line += string.Format(CultureInfo.InvariantCulture, " {0:0}",
                    Color.FromArgb(0, grayScale, grayScale, grayScale).ToArgb());
            }

            return line;
        }

        /// <summary>
        /// Saves a list of profiles to point cloud file. Overwrites the file if it exists. All dimensions (X,Y,Z) are in [mm].
        /// </summary>
        /// <param name="file">File where export should be saved to. </param>
        /// <param name="profiles">The profiles to be exported. </param>
        /// <param name="batchStepLength">The Y distance [mm] between two subsequent profiles in a batch.</param>
        /// <param name="applicationSettings">Sensor parameters for application use.</param>
        public static void SaveToPointCloudFile(string file, Dictionary<int, List<Profile>> profiles, double batchStepLength, ApplicationSettings applicationSettings)	
        {
            if (profiles == null || profiles.Count == 0) return;

	        _sensorWidth = SensorParameterStore.GetInstance().SensorWidth;
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            if (file.ToLower().EndsWith(".bmp"))
            {
                SaveToBitmapFile(file, profiles, batchStepLength, SensorParameterStore.GetInstance().AveragePixelWidth * 1000);
                return;
            }

            if (file.ToLower().EndsWith(".pcd"))
            {
                SaveToPcdFile(file, profiles, batchStepLength);
                return;
            }

            _pointCloudAscFormat = applicationSettings.PointCloudAscFormat;
            var firstLayerProfiles = profiles.Values.First();
            uint lineStart = 0;
            for (var i = 0; i < 10; i++)
            {
                if (i == firstLayerProfiles.Count) break;

                if (i == 0 || firstLayerProfiles[i].Header.Index < lineStart)
                {
                    lineStart = firstLayerProfiles[i].Header.Index;
                }
            }

            foreach (var layer in profiles.Keys)
            {
                foreach (var profile in profiles[layer])
                {
                    var line = profile.Header.Index - lineStart;
                    AppendToPointCloudFile(file, profile, batchStepLength, (int) line);
                }
            }
        }

        /// <summary>
        /// Save to bitmap file.
        /// </summary>
        /// <param name="file">
        /// The output file.
        /// </param>
        /// <param name="profiles">
        /// The profiles to be exported. Does not support multiple layers.
        /// </param>
        /// <param name="batchStepLength">
        /// The Y distance [mm] between two subsequent profiles in a batch.
        /// </param>
        /// <param name="averagePixelWidth">
        /// Sensor average pixels with in um defined by calibration files.
        /// </param>
        public static void SaveToBitmapFile(string file, Dictionary<int, List<Profile>> profiles, double batchStepLength, double averagePixelWidth)
        {
            if (profiles.Count == 0) return;

            foreach (var layer in profiles.Keys)
            {
                string fileName = Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + 
                                  (layer >= 0 ? (layer + 1).ToString() : "") + Path.GetExtension(file);
                if (File.Exists(fileName))
                    File.Delete(fileName);

                var profileCount = profiles[layer].Count;
                byte[] rawImage = new byte[_sensorWidth * profileCount];
                Array.Clear(rawImage, 0, rawImage.Length);

                uint lineStart = 0;
                for (var i = 0; i < 10; i++)
                {
                    if (i == profileCount) break;

                    if (i == 0 || profiles[layer][i].Header.Index < lineStart)
                    {
                        lineStart = profiles[layer][i].Header.Index;
                    }
                }

                foreach (var profile in profiles[layer])
                {
                    if (profile.LayerId < 0)
                    {
                        foreach (FsApi.Point point in profile.Points)
                        {
                            var column = (int) (0.1 + point.X / averagePixelWidth);
                            if (column >= 0 && column < _sensorWidth)
                            {
                                var line = profile.Header.Index - lineStart;
                                if (line < profiles[layer].Count)
                                {
                                    rawImage[line * _sensorWidth + column] = (byte) point.Intensity;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < profile.LineLength; ++i)
                        {
                            if (profile.ZValues[i] > NoMeas)
                            {
                                continue;
                            }

                            var column = (int) (0.1 + (i * profile.XStep) / averagePixelWidth);
                            if (column >= 0 && column < _sensorWidth)
                            {
                                var line = profile.Header.Index - lineStart;
                                if (line < profiles[layer].Count)
                                {
                                    rawImage[line * _sensorWidth + column] = (byte) profile.IntensityValues[i];
                                }
                            }

                        }
                    }
                }

                var image = new Bitmap(_sensorWidth, profileCount, _sensorWidth, PixelFormat.Format8bppIndexed,
                                       Marshal.UnsafeAddrOfPinnedArrayElement(rawImage, 0));

                // Create gray scale entries and pseudo color for not measured areas
                ColorPalette palette = image.Palette;
                palette.Entries[0] = Color.YellowGreen;
                for (int i = 1; i < palette.Entries.Length; i++)
                {
                    palette.Entries[i] = Color.FromArgb(255, i, i, i);
                }
                image.Palette = palette;

                // set resolution as dots per inch
                image.SetResolution((float)(1000.0 / averagePixelWidth * 25.4), (float)(1000.0 / batchStepLength * 25.4));
                image.Save(fileName, ImageFormat.Bmp);
            }
        }

        /// <summary>
        /// Save to point data to PDC file format.
        /// </summary>
        /// <param name="file">
        /// The output file.
        /// </param>
        /// <param name="profiles">
        /// The profiles to be exported. Does not support multiple layers.
        /// </param>
        /// <param name="batchStepLength">
        /// The Y distance [mm] between two subsequent profiles in a batch.
        /// </param>
        public static void SaveToPcdFile(string file, Dictionary<int, List<Profile>> profiles, double batchStepLength)
        {
            if (profiles.Count == 0) return;

            UInt32 pcdPointsWritten = 0;
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            var firstLayerProfiles = profiles.Values.First();
            uint lineStart = 0;
            for (var i = 0; i < 10; i++)
            {
                if (i == firstLayerProfiles.Count) break;

                if (i == 0 || firstLayerProfiles[i].Header.Index < lineStart)
                {
                    lineStart = firstLayerProfiles[i].Header.Index;
                }
            }

            using (BinaryWriter pcdWriter = new BinaryWriter(File.Open(file, FileMode.Create)))
            {
                pcdWriter.Write(Encoding.Default.GetBytes("# .PCD v.7 - Point Cloud Data file format\r\nVERSION .7\r\nFIELDS x y z rgb\r\nSIZE 4 4 4 4\r\nTYPE F F F F\r\nWIDTH "));
                long pos1 = pcdWriter.BaseStream.Position;
                pcdWriter.Write(Encoding.Default.GetBytes("         \r\nHEIGHT 1\r\nVIEWPOINT 0 0 0 1 0 0 0\r\nPOINTS "));
                long pos2 = pcdWriter.BaseStream.Position;
                pcdWriter.Write(Encoding.Default.GetBytes("         \r\nDATA binary\r\n"));

                foreach (var layer in profiles.Keys)
                {
                    foreach (var profile in profiles[layer])
                    {
                        if (profile.LayerId < 0)
                        {
                            var line = profile.Header.Index - lineStart;
                            List<FsApi.Point> points = profile.Points.ToList();
                            {
                                foreach (FsApi.Point point in points)
                                {
                                    pcdWriter.Write((float) (point.X * Defines.ProfileScale)); // x
                                    pcdWriter.Write((float) (line * batchStepLength)); // z
                                    pcdWriter.Write((float) (point.Y * Defines.ProfileScale)); // y
                                    int intensity = (int) point.Intensity;
                                    Color rgbColor = Color.FromArgb(0, intensity, intensity, intensity);
                                    Int32 color = rgbColor.ToArgb();
                                    pcdWriter.Write(color);
                                    pcdPointsWritten++;
                                }
                            }
                        }
                        else
                        {
                            var line = profile.Header.Index - lineStart;
                            for (int i = 0; i < profile.LineLength; ++i)
                            {
                                if (profile.ZValues[i] > NoMeas)
                                {
                                    continue;
                                }

                                pcdWriter.Write((float) (i * profile.XStep * Defines.ProfileScale)); // x
                                pcdWriter.Write((float) (line * batchStepLength)); // z
                                pcdWriter.Write((float)(profile.ZValues[i] * Defines.ProfileScale)); // y
                                int intensity = (int) (profile.IntensityValues[i]);
                                Color rgbColor = Color.FromArgb(0, intensity, intensity, intensity);
                                Int32 color = rgbColor.ToArgb();
                                pcdWriter.Write(color);
                                pcdPointsWritten++;
                            }
                        }
                    }
                }

                pcdWriter.BaseStream.Position = pos1;
                pcdWriter.Write(Encoding.Default.GetBytes(pcdPointsWritten.ToString()));

                pcdWriter.BaseStream.Position = pos2;
                pcdWriter.Write(Encoding.Default.GetBytes(pcdPointsWritten.ToString()));
            }           
        }
    }
}