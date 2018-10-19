using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.XFeatures2D;
using VisualDetection.Model;

namespace VisualDetection.Detectors
{
    //SURF Detector is not used for face detection, but for feature detection/matching
    //We leave it in the project for possible later use
    public static class SURFDetector
    {

        //int k = 2;
        //double uniquenessThreshold = 0.8;
        private static double hessianThresh = 300;
        private static VectorOfKeyPoint modelKeyPoints = new VectorOfKeyPoint();
        private static VectorOfKeyPoint observedKeyPoints = new VectorOfKeyPoint();
        private static SURF Surf = new SURF(hessianThresh);
        private static Bgr color = new Bgr(255, 255, 255);

        #region Public Methods
        public static void CalculateSURFFeatures()
        {
            Surf.DetectAndCompute(CameraModel.Instance.CameraViewGrayScaleMat, null, modelKeyPoints, CameraModel.Instance.CameraViewDetectedFeaturesMat, false);
            Features2DToolbox.DrawKeypoints(CameraModel.Instance.CameraViewMat, modelKeyPoints, CameraModel.Instance.CameraViewDetectedFeaturesMat, color);
        }
        #endregion
    }
}
