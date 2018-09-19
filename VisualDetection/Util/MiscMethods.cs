using Emgu.CV;
using Emgu.CV.CvEnum;

namespace VisualDetection.Util
{
    public static class MiscMethods
    {

        #region Public Methods
        /// <summary>
        /// Converts a Bgr Mat into a Grayscale Mat
        /// </summary>
        /// <param name="imageMat"></param>
        /// <returns></returns>
        public static Mat MatToGrayscale(Mat imageMat)
        {
            var imageGray = new Mat();
            CvInvoke.CvtColor(imageMat, imageGray, ColorConversion.Bgr2Gray);
            return imageGray;
        }
        #endregion
    }
}
