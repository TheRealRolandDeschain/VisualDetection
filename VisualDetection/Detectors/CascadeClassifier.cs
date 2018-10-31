using Emgu.CV;
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
        public static void Detect(CascadeClassifier eye, CascadeClassifier face = null)
        {
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();
            CameraModel.Instance.CameraViewDetectedFeaturesMat = CameraModel.Instance.CameraViewMat.Clone();
            MCvScalar faceRectColor = new MCvScalar(0, 0, 255);
            MCvScalar eyeRectColor = new MCvScalar(255, 0, 0);

            //normalizes brightness and increases contrast of the image
            CvInvoke.EqualizeHist(CameraModel.Instance.CameraViewGrayScaleMat, CameraModel.Instance.CameraViewGrayScaleMat);

            //Detect the faces from the gray scale image and store the locations as rectangle
            //The first dimensional is the channel
            //The second dimension is the index of the rectangle in the specific channel
            Rectangle[] facesDetected = face.DetectMultiScale(
                CameraModel.Instance.CameraViewGrayScaleMat,
                1.1,
                10,
                new Size(20, 20));

            faces.AddRange(facesDetected);

            foreach (Rectangle f in facesDetected)
            {
                //Get the region of interest on the faces
                using (Mat faceRegion = new Mat(CameraModel.Instance.CameraViewGrayScaleMat, f))
                {
                    Rectangle[] eyesDetected = eye.DetectMultiScale(
                        faceRegion,
                        1.1,
                        10,
                        new Size(20, 20));

                    foreach (Rectangle e in eyesDetected)
                    {
                        Rectangle eyeRect = e;
                        eyeRect.Offset(f.X, f.Y);
                        eyes.Add(eyeRect);
                    }
                }
            }

            foreach(var foundface in faces)
            {
                CvInvoke.Rectangle(CameraModel.Instance.CameraViewDetectedFeaturesMat, foundface, faceRectColor);
            }
            foreach(var foundeye in eyes)
            {
                CvInvoke.Rectangle(CameraModel.Instance.CameraViewDetectedFeaturesMat, foundeye, eyeRectColor);
            }

        }
    }
}
