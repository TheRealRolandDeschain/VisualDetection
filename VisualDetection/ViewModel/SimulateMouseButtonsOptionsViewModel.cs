using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualDetection.Util;

namespace VisualDetection.ViewModel
{
    public class SimulateMouseButtonsOptionsViewModel : ViewModelBase
    {
        #region Constructor
        public SimulateMouseButtonsOptionsViewModel()
        {
            SetDefaultValues();
        }
        #endregion

        #region Private Properties
        private bool onlyUseRightMB;
        private bool onlyUseLeftMB;
        private bool inverLeftRightEnabled;
        private bool useAsToggleSwitchEnabled;
        private bool useMiddleMouseButtonEnabled;
        private bool useInsteadOfRight;
        private bool useInsteadOfLeft;
        private bool useInsteadOfBoth;
        private bool useMouseWheelEnabled;
        private bool invertUpDownEnabled;
        private int buttonPressTime;
        #endregion

        #region Public Properties
        /// <summary>
        /// The time for the simulated button press
        /// </summary>
        public int ButtonPressTime
        {
            get { return buttonPressTime; }
            set
            {
                if (buttonPressTime != value)
                {
                    SetProperty(ref buttonPressTime, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool OnlyUseRightMB
        {
            get { return onlyUseRightMB; }
            set
            {
                if (onlyUseRightMB != value)
                {
                    SetProperty(ref onlyUseRightMB, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool OnlyUseLeftMB
        {
            get { return onlyUseLeftMB; }
            set
            {
                if (onlyUseLeftMB != value)
                {
                    SetProperty(ref onlyUseLeftMB, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool InverLeftRightEnabled
        {
            get { return inverLeftRightEnabled; }
            set
            {
                if (inverLeftRightEnabled != value)
                {
                    SetProperty(ref inverLeftRightEnabled, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UseAsToggleSwitchEnabled
        {
            get { return useAsToggleSwitchEnabled; }
            set
            {
                if (useAsToggleSwitchEnabled != value)
                {
                    SetProperty(ref useAsToggleSwitchEnabled, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UseMiddleMouseButtonEnabled
        {
            get { return useMiddleMouseButtonEnabled; }
            set
            {
                if (useMiddleMouseButtonEnabled != value)
                {
                    SetProperty(ref useMiddleMouseButtonEnabled, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UseInsteadOfRight
        {
            get { return useInsteadOfRight; }
            set
            {
                if (useInsteadOfRight != value)
                {
                    SetProperty(ref useInsteadOfRight, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UseInsteadOfLeft
        {
            get { return useInsteadOfLeft; }
            set
            {
                if (useInsteadOfLeft != value)
                {
                    SetProperty(ref useInsteadOfLeft, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UseInsteadOfBoth
        {
            get { return useInsteadOfBoth; }
            set
            {
                if (useInsteadOfBoth != value)
                {
                    SetProperty(ref useInsteadOfBoth, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UseMouseWheelEnabled
        {
            get { return useMouseWheelEnabled; }
            set
            {
                if (useMouseWheelEnabled != value)
                {
                    SetProperty(ref useMouseWheelEnabled, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool InvertUpDownEnabled
        {
            get { return invertUpDownEnabled; }
            set
            {
                if (invertUpDownEnabled != value)
                {
                    SetProperty(ref invertUpDownEnabled, value);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the default values
        /// </summary>
        private void SetDefaultValues()
        {
            ButtonPressTime = GenDefInt.DefaultButtonPressTime;
        }

        /// <summary>
        /// Handles the Checkboxes based on new changes
        /// </summary>
        private void HandleCheckboxes()
        {

        }
        #endregion

        #region Public Methods
        #endregion
    }
}
