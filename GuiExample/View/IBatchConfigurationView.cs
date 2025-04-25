// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBatchConfiguration.cs" company="FocalSpec Oy">
//   FocalSpec Oy 2016-
// </copyright>
// <summary>
//   Interface definition for the batch configuration dialog.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FocalSpec.GuiExample.View
{
    using Model.BatchMode;

    /// <summary>
    /// Interface definition for the batch configuration dialog.
    /// </summary>
    public interface IBatchConfigurationView
    {
        /// <summary>
        /// Displays this dialog.
        /// </summary>
        /// <param name="batchConfiguration">The batch configuration shown as the initial state. </param>
        /// <param name="newBatchConfiguration">New batch configuration, which is updated, when user makes changes. </param>
        /// <returns>True, if user closed the dialog by pressing ok.</returns>
        bool Display(BatchConfiguration batchConfiguration, out BatchConfiguration newBatchConfiguration);
    }
}