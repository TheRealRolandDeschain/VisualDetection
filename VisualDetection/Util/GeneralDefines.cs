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
    }

    public static class GenDefInt
    {
        public const int FaceMinNeigboursDefault = 10;
        public const int EyesMinNeigboursDefault = 10;
        public const int FaceMinSizeValue = 20;
        public const int FaceMaxSizeValue = 500;
        public const int EyesMinSizeValue = 10;
        public const int EyesMaxSizeValue = 250;
    }

    public static class GenDefDouble
    {
        public const double FaceScaleDefault = 1.1;
        public const double EyesScaleDefault = 1.1;

    }
        public static class GenDefList
    {

    }
}
