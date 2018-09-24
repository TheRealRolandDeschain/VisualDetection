using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

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

        /// <summary>
        /// Get BitmapSource from image matrix
        /// </summary>
        public static BitmapSource MatToBitmapSource(Mat image)
        {
            using (Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap
                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }
        #endregion

        #region DLL
        /// <summary>
        /// Delete a GDI object
        /// </summary>
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);
        #endregion
    }
}
