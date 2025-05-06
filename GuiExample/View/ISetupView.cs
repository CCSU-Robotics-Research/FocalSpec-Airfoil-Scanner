namespace FocalSpec.GuiExample.View
{
    interface ISetupView
    {
        string ZCalibrationFilePath { get; set; }

        string XCalibrationFilePath { get; set; }

        bool IsZCalibrationFileSet { get; }

        bool IsXCalibrationFileSet { get; }

        bool ShowModal();
    }
}
