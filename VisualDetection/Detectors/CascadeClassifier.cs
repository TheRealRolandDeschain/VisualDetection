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
        public static double? Detect(bool showFeatures, double faceScale, int faceMinNeigbours, Size faceMinSize, Size faceMaxSize,
            double eyesScale, int eyesMinNeigbours, Size eyesMinSize, Size eyesMaxSize,
            CascadeClassifier eye, CascadeClassifier face, MCvScalar faceRectColor, MCvScalar eyeRectColor)
        {
            Rectangle[] eyesDetected;
            

            Rectangle[] facesDetected = face.DetectMultiScale(
                CameraModel.Instance.CameraViewGrayScaleMat,
                faceScale,
                faceMinNeigbours,
                faceMinSize, 
                faceMaxSize
                );
            if (facesDetected.Length < 1) return null;

            using (Mat faceRegion = new Mat(CameraModel.Instance.CameraViewGrayScaleMat, facesDetected[0]))
            {
                eyesDetected = eye.DetectMultiScale(
                    faceRegion,
                    eyesScale,
                    eyesMinNeigbours,
                    eyesMinSize,
                    eyesMaxSize
                    );
                if (eyesDetected.Length != 2) return null;
                eyesDetected[0].Offset(facesDetected[0].X, facesDetected[0].Y);
                eyesDetected[1].Offset(facesDetected[0].X, facesDetected[0].Y);
            }
            Point[] eyesDetectedPoints = { new Point((eyesDetected[0].X + eyesDetected[0].Width / 2), (eyesDetected[0].Y + eyesDetected[0].Height / 2)),
                new Point((eyesDetected[1].X + eyesDetected[1].Width / 2), (eyesDetected[1].Y + eyesDetected[1].Height / 2))};

            if (showFeatures)
            {
                CvInvoke.Rectangle(CameraModel.Instance.CameraViewMat, facesDetected[0], faceRectColor, 4);
                CvInvoke.Rectangle(CameraModel.Instance.CameraViewMat, eyesDetected[0], eyeRectColor, 4);
                CvInvoke.Rectangle(CameraModel.Instance.CameraViewMat, eyesDetected[1], eyeRectColor, 4);
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
