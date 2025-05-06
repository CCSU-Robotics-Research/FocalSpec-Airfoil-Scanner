// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportImage.cs" company="FocalSpec Oy">
//   FocalSpec Oy 2018-
// </copyright>
// <summary>
//   Exports images.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FocalSpec.GuiExample.Model.Export
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// Export image utility.
    /// </summary>
    public class ExportImage
    {
        /// <summary>
        /// Save to picture file. Currently only PNG supported.
        /// </summary>
        /// <param name="file">The output file.</param>
        /// <param name="image">The bitmap image to be exported.</param>
        public static void SaveToPictureFile(string file, Image image)
        {
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                image.Save(file, ImageFormat.Png);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}