// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportCsv.cs" company="FocalSpec Ltd">
// FocalSpec Ltd 2016-
// </copyright>
// <summary>
// Exports profiles into CSV files. 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using FocalSpec.FsApiNet.Model;
using FocalSpec.GuiExample.Model.Camera;

namespace FocalSpec.GuiExample.Model.Export
{
    /// <summary>
    /// Export data in CSV format.
    /// </summary>
    public static class ExportCsv
    {
        /// <summary>
        /// Exports a profile to a file.
        /// </summary>
        /// <param name="file">Export file name.</param>
        /// <param name="profiles">Profiles to export.</param>
        /// <param name="layer">Selected layer.</param>
        public static void Export(string file, List<Profile> profiles, int layer)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("X [mm];Z [mm];Intensity");

            foreach (var profile in profiles)
            {
                if (profile == null || layer >= 0 && profile.LayerId != layer) continue;

                if (profile.LayerId < 0)
                {
                    foreach (FsApi.Point point in profile.Points)
                    {
                        str.AppendLine(string.Format("{0:0.000000};{1:0.000000};{2}", point.X * Defines.ProfileScale, point.Y * Defines.ProfileScale,
                            point.Intensity));
                    }
                }
                else
                {
                    for (int i = 0; i < profile.LineLength; ++i)
                    {
                        if (profile.ZValues[i] > FsApi.NoMeasurement - 1)
                        {
                            continue;
                        }

                        str.AppendLine(string.Format("{0:0.000000};{1:0.000000};{2}", (float)(i * profile.XStep * Defines.ProfileScale), (profile.ZValues[i] * Defines.ProfileScale), profile.IntensityValues[i]));
                    }
                }
            }

            File.WriteAllText(file, str.ToString());
        }
    }
}