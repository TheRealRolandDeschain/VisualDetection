using Emgu.CV;
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
            CameraViewMat = new Mat();
            CameraViewGrayScaleMat = new Mat();
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
        public Mat CameraViewGrayScaleMat { get; set; }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods

        // TODO: Make CameraModel a class containing all camera information (resolution, name, etc) as properties!

        #endregion
    }
}
