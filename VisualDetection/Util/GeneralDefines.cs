using System.Collections.Generic;
using System.Windows.Media;

namespace VisualDetection.Util
{
    public static class GenDefString
    {
        public const string StartCaptureButtonString = "Start Capture";
        public const string StopCaptureButtonString = "Stop Capture";
        public const string DefaultImagePathString = "..\\Icons\\DefaultImage.png";
        public const string HaarCascadePathStringEmpty = "Please load a valid XML-File!";
        public const string HaarCascadeOfdDefaultExt = "xml";
        public const string HaarCascadeOfdFilter = "XML Files|*.xml";
        public const string InvalidCascadeClassifierXMLLoaded = "The file you tried to load was invalid. Please try another one. ";

        //Warnings
        public const string ValueMustBeBetweenOneAndFive = "Invalid Value! \n The value must be bigger than 1 and lower than 5. ";
        public const string HaarCascadeOfdTitle = "Please select a valid XML file!";

        //OutputOptionTitles
        public const string SimulateMouseButtonTitle = "Simulate Mouse Buttons...";
        public const string SimulateKeyPressTitle = "Simulate Key Press...";
        public const string SimulateKeyPressSequenceTitle = "Simulate Key Press Sequence...";
        public const string OpenExternalSoftwareTitle = "Open External Software...";
        public const string CallWindowsStandardFunctionTitle = "Call Windows Standard Function...";
    }

    public static class GenDefInt
    {
        //GeneraloptionsDefault
        public const int DefaultDetectorTypeIndex = 0;
        public const int DefaultIdleAfterFrameCalculation = 0;

        //CascadeClassifierOptionsDefault
        public const int FaceMinNeigboursDefault = 10;
        public const int EyesMinNeigboursDefault = 10;
        public const int FaceMinSizeValue = 200;
        public const int FaceMaxSizeValue = 500;
        public const int EyesMinSizeValue = 5;
        public const int EyesMaxSizeValue = 250;

        //OutputOptionsDefault
        public const int DefaultOutpuOptionSelectedIndex = 0;
        public const int DefaultTriggerAngle = 5;
        public const int DefaultNrOfPositiveFramesNeeded = 5;
        public const int DefaultNrOfUndefinedFramesAllowed = 3;
        public const int DefaultButtonPressTime = 100;
    }

    public static class GenDefDouble
    {
        public const double FaceScaleDefault = 1.1;
        public const double EyesScaleDefault = 1.1;

    }

    public static class GenDefList
    {
        public static readonly List<string> AvailableDetecorTypes = new List<string>(){
                "Cascade Detector"
            };
    }

    public static class GenDefBool
    {
        public const bool DefaultUseEqualizeHist = true;
    }

    public static class GenDefColors
    {
        public static readonly Color DefaultFaceColor = Colors.Red;
        public static readonly Color DefaultEyeColor = Colors.Blue;

    }
}
