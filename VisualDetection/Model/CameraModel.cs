using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace VisualDetection.Model
{
    public sealed class CameraModel
    {
        #region Constructors
        private CameraModel()
        {

        }
        #endregion

        #region Singleton
        private static volatile CameraModel instance;
        private static object syncRoot = new object();
        public static CameraModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CameraModel();
                    }
                }

                return instance;
            }
        }
        #endregion

        #region Private Properties
        #endregion

        #region Public Properties
        public Mat CameraViewMat { get; set; }
        public Mat CamerViewGrayScaleMat { get; set; }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        /// <summary>
        /// Get BitmapSource from image matrix
        /// </summary>
        public BitmapSource MatToBitmapSource(Mat image)
        {
            using (Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap
                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }

        public Mat MatToGrayscale(Mat imageMat)
        {
            var imageGray = new Mat();
            CvInvoke.CvtColor(imageMat, imageGray, ColorConversion.Bgr2Gray);
            return imageGray;
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
