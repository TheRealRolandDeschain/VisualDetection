namespace VisualDetection.Util
{

    /// <summary>
    /// Radio buttons of the CameraView
    /// </summary>
    public enum CameraViewRadioButtons
    {
        OriginalImage,
        GrayScaleImage,
        ImageWithDetectedFeaturess
    }

    /// <summary>
    /// Radiobuttons for Major Options concerning the Mouse Button Simulation
    /// </summary>
    public enum MouseButtonsimulationMajorOptions
    {
        UseStandard,
        OnlyUseRightMB,
        OnlyUseLeftMB,
        InverLeftRightEnabled,
        UseMiddleMouseButtonEnabled,
        UseMouseWheelEnabled,
    }

    /// <summary>
    /// Radiobuttons for MiddleMouseButton Options concerning the Mouse Button Simulation
    /// </summary>
    public enum MBSimulationMiddleMouseOption
    {
        UseInsteadOfRight,
        UseInsteadOfLeft,
        UseInsteadOfBoth,
    }

    /// <summary>
    /// Radiobuttons for MouseWheel Options concerning the Mouse Button Simulation
    /// </summary>
    public enum UseMouseWheelEnabled
    {
        InvertUpDownDisabled,
        InvertUpDownEnabled,
    }
}
