using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualDetection.Model;

namespace VisualDetection.ViewModel
{
    public class OutputViewModel : ViewModelBase
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public OutputViewModel()
        {
            CameraModel.Instance.output = this;
        }
        #endregion

        #region Private Properties
        private double? eyeAngle;
        private int frameCalculationTime;
        #endregion

        #region Public Properties

        /// <summary>
        /// The angle between the eyes showing the tilt of the head
        /// </summary>
        public double? EyeAngle
        {
            get { return eyeAngle; }
            set
            {
                if (eyeAngle != value)
                {
                    SetProperty(ref eyeAngle, value);
                }
            }
        }

        /// <summary>
        /// The time needed for the calculation of the last frame
        /// </summary>
        public int FrameCalculationTime
        {
            get { return frameCalculationTime; }
            set
            {
                if (frameCalculationTime != value)
                {
                    SetProperty(ref frameCalculationTime, value);
                }
            }
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion
    }
}
