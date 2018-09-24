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
    public static class SURFDetector
    {
        #region Public Methods
        public static void CalculateSURFFeatures()
        {
            //int k = 2;
            //double uniquenessThreshold = 0.8;
            double hessianThresh = 300;
            VectorOfKeyPoint modelKeyPoints = new VectorOfKeyPoint();
            VectorOfKeyPoint observedKeyPoints = new VectorOfKeyPoint();
            SURF SURFDetector = new SURF(hessianThresh);
            SURFDetector.DetectAndCompute(CameraModel.Instance.CameraViewGrayScaleMat, null, modelKeyPoints, CameraModel.Instance.CameraViewDetectedFeaturesMat, false);
            Features2DToolbox.DrawKeypoints(CameraModel.Instance.CameraViewMat, modelKeyPoints, CameraModel.Instance.CameraViewDetectedFeaturesMat, new Bgr(255,255,255));
        }
        #endregion
    }
}
