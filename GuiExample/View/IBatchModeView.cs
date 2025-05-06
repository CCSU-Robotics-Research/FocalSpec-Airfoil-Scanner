// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBatchMode.cs" company="FocalSpec Oy">
//   FocalSpec Oy 2016-
// </copyright>
// <summary>
//   Interface definition for the batch mode view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FocalSpec.GuiExample.View
{
    /// <summary>Delegate definition for the OnConfigure event.</summary>
    public delegate void ConfigureHandler();

    /// <summary>Delegate definition for the OnStart event.</summary>
    public delegate void StartHandler();

    /// <summary>Delegate definition for the OnStop event.</summary>
    /// <param name="show">Show/hide batch visualizer.</param>
    public delegate void StopHandler(bool show);

    /// <summary>Delegate definition for the OnClear event.</summary>
    public delegate void ClearHandler();

    /// <summary>Delegate definition for the OnSave event.</summary>
    /// <param name="path">Path to folder where points are saved to. </param>
    public delegate void SaveHandler(string path);

    /// <summary>Delegate definition for the OnPositionBrowsed event.</summary>
    /// <param name="index">Zero-based index of the selected profile. </param>
    public delegate void PositionBrowsedHandler(int index);

    /// <summary>Interface for batch mode view.</summary>
    public interface IBatchModeView
    {
        /// <summary>Event fires when user wants to configure the batch recorder.</summary>
        event ConfigureHandler OnConfigure;

        /// <summary>Event fires when user requests to start recording.</summary>
        event StartHandler OnStart;

        /// <summary>Event fires when user requests to stop recording.</summary>
        event StopHandler OnStop;

        /// <summary>Event fires when user requests to stop recording.</summary>
        event SaveHandler OnSave;

        /// <summary>Event fires when user requests to clear recording.</summary>
        event ClearHandler OnClear;

        /// <summary>Event fires when user browses recorded profiles.</summary>
        event PositionBrowsedHandler OnPositionBrowsed;

        /// <summary>Event fires when user selects to show or hide batch visualizer.</summary>
        event EventHandler OnShowHideBatchVisualizer;

        /// <summary>Sets a value indicating whether the recording related buttons are enabled.</summary>
        bool EnableRecord { set; }

        bool IsConfigured { set; get; }

        /// <summary>Sets a value indicating whether the record saving is enabled.</summary>
        bool EnableSave { set; }

        /// <summary>Sets a value indicating whether the clear recording is enabled.</summary>
        bool EnableClear { set; }

        /// <summary>Sets a value indicating whether the stop recording is enabled.</summary>
        bool EnableStop { set; }

        /// <summary>Sets a value indicating whether the position tracking is enabled.</summary>
        bool EnablePosition { set; }

        /// <summary>Sets a value indicating whether the batch operations are enabled.</summary>
        bool EnableBatch { set; }

	    /// <summary>Sets a value indicating whether the batch configure is enabled.</summary>
	    bool EnableConfigure { set; }

        /// <summary>
        /// Gets or sets a value indicating whether the batch visualizer is visible.
        /// </summary>
        bool IsBatchVisualizerVisible { get; set; }

        /// <summary>Sets the zero-based index of the frame.</summary>
        int Position { set; }

        /// <summary>Sets the maximum position in the trackbar.</summary>
        int MaxPosition { set; }
    }
}