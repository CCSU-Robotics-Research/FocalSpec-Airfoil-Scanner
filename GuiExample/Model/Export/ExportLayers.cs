// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportLayers.cs" company="FocalSpec Ltd">
//   FocalSpec Ltd 2016-
// </copyright>
// <summary>
//   Enumerations for selecting exporting layer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FocalSpec.GuiExample.Model.Export
{
    /// <summary>
    /// Layer of a point cloud.
    /// </summary>
    public enum ExportLayer
    {
        /// <summary>
        /// Full data.
        /// </summary>
        All,

        /// <summary>
        /// Layers sorted from top to bottom
        /// </summary>
        Top,

        /// <summary>
        /// Layers sorted from bottom to top.
        /// </summary>
        Bottom,

        /// <summary>
        /// Layers sorted by intensity and from top to bottom.
        /// </summary>
        BrightestAndTop
    }
}