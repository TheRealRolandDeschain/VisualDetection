using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Drawing;
using VisualDetection.Util;
using VisualDetection.Model;
using Emgu.CV;

namespace VisualDetection.ViewModel
{
    public class CascadeDetectorOptionsViewModel : ViewModelBase
    {
        #region Constructors
        /// <summary>
        /// standard constructor
        /// </summary>
        public CascadeDetectorOptionsViewModel()
        {
            HaarCascadeFacePathString = GenDefString.HaarCascadePathStringEmpty;
            HaarCascadeEyePathString = GenDefString.HaarCascadePathStringEmpty;
            Face = new CascadeClassifier();
            Eye = new CascadeClassifier();
            CameraModel.Instance.cascadeOptions = this;
            LoadDefaultValues();
        }
        #endregion

        #region Private Properties
        private RelayCommand loadHaarCascadeFaceClicked;
        private RelayCommand loadHaarCascadeEyeClicked;
        private string haarCascadeFacePathString;
        private string haarCascadeFaceNameString;
        private string haarCascadeEyePathString;
        private string haarCascadeEyeNameString;
        private bool validFaceClassifierLoaded;
        private bool validEyeClassifierLoaded;
        private double faceScale;
        private int faceMinNeigbours;
        private double eyesScale;
        private int eyesMinNeigbours;
        private int faceMinSizeValue;
        private int faceMaxSizeValue;
        private int eyesMinSizeValue;
        private int eyesMaxSizeValue;
        #endregion

        #region Public Properties
        public CascadeClassifier Face { get; set; }
        public CascadeClassifier Eye { get; set; }
        public Size FaceMinSize { get; set; }
        public Size FaceMaxSize { get; set; }
        public Size EyesMinSize { get; set; }
        public Size EyesMaxSize { get; set; }


        /// <summary>
        /// The button to load a HaarCascade for face detection was clicked
        /// </summary>
        public ICommand LoadHaarCascadeFaceClicked
        {
            get
            {
                return loadHaarCascadeFaceClicked ?? (loadHaarCascadeFaceClicked = new RelayCommand(command => LoadHaarCascadeFace()));
            }
        }

        /// <summary>
        /// The button to load a HaarCascade for face detection was clicked
        /// </summary>
        public ICommand LoadHaarCascadeEyeClicked
        {
            get
            {
                return loadHaarCascadeEyeClicked ?? (loadHaarCascadeEyeClicked = new RelayCommand(command => LoadHaarCascadeEye()));
            }
        }

        /// <summary>
        /// The path of face detection cascade
        /// </summary>
        public string HaarCascadeFacePathString
        {
            get { return haarCascadeFacePathString; }
            set
            {
                if (haarCascadeFacePathString != value)
                {
                    HaarCascadeFaceNameString = Path.GetFileName(value);
                    SetProperty(ref haarCascadeFacePathString, value);
                }
            }
        }

        /// <summary>
        /// The content of the textbox for face detection cascade
        /// </summary>
        public string HaarCascadeFaceNameString
        {
            get { return haarCascadeFaceNameString; }
            set
            {
                if (haarCascadeFaceNameString != value)
                {
                    SetProperty(ref haarCascadeFaceNameString, value);
                }
            }
        }

        /// <summary>
        /// The content of the textbox for the path of eye detection cascade
        /// </summary>
        public string HaarCascadeEyePathString
        {
            get { return haarCascadeEyePathString; }
            set
            {
                if (haarCascadeEyePathString != value)
                {
                    HaarCascadeEyeNameString = Path.GetFileName(value);
                    SetProperty(ref haarCascadeEyePathString, value);
                }
            }
        }

        /// <summary>
        /// The content of the textbox for eye detection cascade
        /// </summary>
        public string HaarCascadeEyeNameString
        {
            get { return haarCascadeEyeNameString; }
            set
            {
                if (haarCascadeEyeNameString != value)
                {
                    SetProperty(ref haarCascadeEyeNameString, value);
                }
            }
        }

        /// <summary>
        /// The value of the scale used of the face detection
        /// </summary>
        public double FaceScale
        {
            get { return faceScale; }
            set
            {
                if (value > 5 || value <= 1)
                {
                    System.Windows.MessageBox.Show(GenDefString.ValueMustBeBetweenOneAndFive);
                    return;
                }
                if (faceScale != value)
                {
                    SetProperty(ref faceScale, value);
                }
            }
        }

        /// <summary>
        /// The value of the minimum neighbours for face detection
        /// </summary>
        public int FaceMinNeigbours
        {
            get { return faceMinNeigbours; }
            set
            {
                if (faceMinNeigbours != value)
                {
                    SetProperty(ref faceMinNeigbours, value);
                }
            }
        }

        /// <summary>
        /// The value of the scale used of the eyes detection
        /// </summary>
        public double EyesScale
        {
            get { return eyesScale; }
            set
            {
                if (value > 5 || value <= 1)
                {
                    System.Windows.MessageBox.Show(GenDefString.ValueMustBeBetweenOneAndFive);
                    return;
                }
                if (eyesScale != value)
                {
                    SetProperty(ref eyesScale, value);
                }
            }
        }

        /// <summary>
        /// The content of the textbox for the path of eye detection cascade
        /// </summary>
        public int EyesMinNeigbours
        {
            get { return eyesMinNeigbours; }
            set
            {
                if (eyesMinNeigbours != value)
                {
                    SetProperty(ref eyesMinNeigbours, value);
                }
            }
        }

        /// <summary>
        /// The value of the minimum size of the detected rectangle for face detection
        /// </summary>
        public int FaceMinSizeValue
        {
            get { return faceMinSizeValue; }
            set
            {
                if (faceMinSizeValue != value)
                {
                    FaceMinSize = new Size(value, value);
                    SetProperty(ref faceMinSizeValue, value);
                }
            }
        }

        /// <summary>
        /// The value of the maximum size of the detected rectangle for face detection
        /// </summary>
        public int FaceMaxSizeValue
        {
            get { return faceMaxSizeValue; }
            set
            {
                if (faceMaxSizeValue != value)
                {
                    FaceMaxSize = new Size(value, value);
                    SetProperty(ref faceMaxSizeValue, value);
                }
            }
        }

        /// <summary>
        /// The value of the minimum size of the detected rectangle for eyes detection
        /// </summary>
        public int EyesMinSizeValue
        {
            get { return eyesMinSizeValue; }
            set
            {
                if (eyesMinSizeValue != value)
                {
                    EyesMinSize = new Size(value, value);
                    SetProperty(ref eyesMinSizeValue, value);
                }
            }
        }

        /// <summary>
        /// The value of the maximum size of the detected rectangle for eyes detection
        /// </summary>
        public int EyesMaxSizeValue
        {
            get { return eyesMaxSizeValue; }
            set
            {
                if (eyesMaxSizeValue != value)
                {
                    EyesMaxSize = new Size(value, value);
                    SetProperty(ref eyesMaxSizeValue, value);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Load a XML file for face detection
        /// </summary>
        private void LoadHaarCascadeFace()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = GenDefString.HaarCascadeOfdTitle,
                DefaultExt = GenDefString.HaarCascadeOfdDefaultExt,
                Filter = GenDefString.HaarCascadeOfdFilter,
                Multiselect = false
            };
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    HaarCascadeFacePathString = Path.GetFullPath(ofd.FileName);
                    FileStorage fs = new FileStorage(HaarCascadeFacePathString, FileStorage.Mode.Read);
                    FileNode fn = fs.GetFirstTopLevelNode();
                    validFaceClassifierLoaded = Face.Read(fn);
                }
                catch (Exception e)
                {
                    HaarCascadeFacePathString = GenDefString.HaarCascadePathStringEmpty;
                    validFaceClassifierLoaded = false;
                    System.Windows.MessageBox.Show(GenDefString.InvalidCascadeClassifierXMLLoaded + "\n error: " + e.Message);
                }
            }
        }

        /// <summary>
        /// Load a XML file for eye detection
        /// </summary>
        private void LoadHaarCascadeEye()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = GenDefString.HaarCascadeOfdTitle,
                DefaultExt = GenDefString.HaarCascadeOfdDefaultExt,
                Filter = GenDefString.HaarCascadeOfdFilter,
                Multiselect = false
            };
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    HaarCascadeEyePathString = Path.GetFullPath(ofd.FileName);
                    FileStorage fs = new FileStorage(HaarCascadeEyePathString, FileStorage.Mode.Read);
                    FileNode fn = fs.GetFirstTopLevelNode();
                    validEyeClassifierLoaded = Eye.Read(fn);
                }
                catch (Exception e)
                {
                    HaarCascadeEyePathString = GenDefString.HaarCascadePathStringEmpty;
                    validEyeClassifierLoaded = false;
                    System.Windows.MessageBox.Show(GenDefString.InvalidCascadeClassifierXMLLoaded + "\n error: " + e.Message);
                }
            }
        }

        /// <summary>
        /// Loads the default values for the cascade classifier
        /// </summary>
        private void LoadDefaultValues()
        {
            validFaceClassifierLoaded = false;
            validEyeClassifierLoaded = false;
            FaceScale = GenDefDouble.FaceScaleDefault;
            FaceMinNeigbours = GenDefInt.FaceMinNeigboursDefault;
            EyesScale = GenDefDouble.EyesScaleDefault;
            EyesMinNeigbours = GenDefInt.EyesMinNeigboursDefault;
            FaceMinSizeValue = GenDefInt.FaceMinSizeValue;
            FaceMaxSizeValue = GenDefInt.FaceMaxSizeValue;
            EyesMinSizeValue = GenDefInt.EyesMinSizeValue;
            EyesMaxSizeValue = GenDefInt.EyesMaxSizeValue;
            FaceMinSize = new Size(FaceMinSizeValue, FaceMinSizeValue);
            FaceMaxSize = new Size(FaceMaxSizeValue, FaceMaxSizeValue);
            EyesMinSize = new Size(EyesMinSizeValue, EyesMinSizeValue);
            EyesMaxSize = new Size(EyesMaxSizeValue, EyesMaxSizeValue);
        }
        #endregion

        #region Public Methods
        #endregion

    }
}
