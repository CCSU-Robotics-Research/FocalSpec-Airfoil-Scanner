// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewMode.cs" company="FocalSpec Oy">
//   FocalSpec Oy 2016-
// </copyright>
// <summary>
//   Defines the view mode enumerations.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
  
namespace FocalSpec.GuiExample.Model
{
    /// <summary>
    /// Values that represent view modes.
    /// </summary>
    public enum ViewMode  
    {
        /// <summary>
        /// View is in peak data real-time mode.
        /// </summary>
        RealTime,

        /// <summary>
        /// View is in batch mode.
        /// </summary>
        Batch,

        /// <summary>
        /// View is in recording mode. In that mode, profiles should be updated to view as received.
        /// </summary>
        Recording,

	    /// <summary>
	    /// View is in raw image mode.
	    /// </summary>
		RawImage
    }
}