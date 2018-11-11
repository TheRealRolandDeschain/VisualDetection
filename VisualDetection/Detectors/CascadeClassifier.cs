using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualDetection.Model;

namespace VisualDetection.Detectors
{
    public static class CascadeClassifierClass
    {

        public static double? Detect(bool showFeatures)
        {
            CameraModel cm = CameraModel.Instance;

            Rectangle[] eyesDetected;
            

            Rectangle[] facesDetected = cm.cascadeOptions.Face.DetectMultiScale(
                CameraModel.Instance.CameraViewGrayScaleMat,
                cm.cascadeOptions.FaceScale,
                cm.cascadeOptions.FaceMinNeigbours,
                cm.cascadeOptions.FaceMinSize, 
                cm.cascadeOptions.FaceMaxSize
                );
            if (facesDetected.Length < 1) return null;

            using (Mat faceRegion = new Mat(CameraModel.Instance.CameraViewGrayScaleMat, facesDetected[0]))
            {
                eyesDetected = cm.cascadeOptions.Eye.DetectMultiScale(
                    faceRegion,
                    cm.cascadeOptions.EyesScale,
                    cm.cascadeOptions.EyesMinNeigbours,
                    cm.cascadeOptions.EyesMinSize,
                    cm.cascadeOptions.EyesMaxSize
                    );
                if (eyesDetected.Length != 2) return null;
                eyesDetected[0].Offset(facesDetected[0].X, facesDetected[0].Y);
                eyesDetected[1].Offset(facesDetected[0].X, facesDetected[0].Y);
            }
            Point[] eyesDetectedPoints = { new Point((eyesDetected[0].X + eyesDetected[0].Width / 2), (eyesDetected[0].Y + eyesDetected[0].Height / 2)),
                new Point((eyesDetected[1].X + eyesDetected[1].Width / 2), (eyesDetected[1].Y + eyesDetected[1].Height / 2))};

            if (showFeatures)
            {
                CvInvoke.Rectangle(CameraModel.Instance.CameraViewMat, facesDetected[0], cm.generalOptions.FaceRectColorScalar, 4);
                CvInvoke.Rectangle(CameraModel.Instance.CameraViewMat, eyesDetected[0], cm.generalOptions.EyesRectColorScalar, 4);
                CvInvoke.Rectangle(CameraModel.Instance.CameraViewMat, eyesDetected[1], cm.generalOptions.EyesRectColorScalar, 4);
            }
            return CalcAngle(eyesDetectedPoints);
        }


        /// <summary>
        /// calculates the angle between the eyes in deg
        /// </summary>
        /// <param name="eyesDetectedPoints"></param>
        /// <returns></returns>
        public static double? CalcAngle(Point[] eyesDetectedPoints)
        {
            return Math.Atan((double)(eyesDetectedPoints[1].Y - eyesDetectedPoints[0].Y) / (double)(eyesDetectedPoints[1].X - eyesDetectedPoints[0].X)) * 180 / Math.PI;
        }
    }
}
