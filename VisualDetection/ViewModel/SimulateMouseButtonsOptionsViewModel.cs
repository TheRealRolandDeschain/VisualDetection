using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualDetection.Util;
using VisualDetection.Interfaces;
using VisualDetection.Model;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace VisualDetection.ViewModel
{
    public class SimulateMouseButtonsOptionsViewModel : ViewModelBase, IOutputOption
    {
        #region Constructor
        public SimulateMouseButtonsOptionsViewModel()
        {
            SetDefaultValues();
        }
        #endregion

        #region Private Properties
        private int buttonPressTime;
        private int coolDownTime;
        private int mouseWheelSpeedTime;
        private int mouseWheelIndent;
        private bool useAsToggleSwitchEnabled;
        private MouseButtonsimulationMajorOptions majorOptionsSelected;
        private MBSimulationMiddleMouseOption middleMouseButtonOptions;
        private UseMouseWheelEnabled mouseWheelOptions;
        private SimulatedMouseModel mouse;
        #endregion

        #region Public Properties
        /// <summary>
        /// Title of this Option
        /// </summary>
        public string OptionTitle { get; private set; }

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
                    mouse.UpdateTimers(value);
                }
            }
        }

        /// <summary>
        /// The amount of ms between two mousewheel actions 
        /// </summary>
        public int MouseWheelSpeedTime
        {
            get { return mouseWheelSpeedTime; }
            set
            {
                if (mouseWheelSpeedTime != value)
                {
                    SetProperty(ref mouseWheelSpeedTime, value);
                }
            }
        }

        /// <summary>
        /// The amount of turn indents for the mousewheel
        /// </summary>
        public int MouseWheelIndent
        {
            get { return mouseWheelIndent; }
            set
            {
                if (mouseWheelIndent != value)
                {
                    SetProperty(ref mouseWheelIndent, value);
                }
            }
        }

        /// <summary>
        /// The minimum time between two simulated button press
        /// </summary>
        public int CoolDownTime
        {
            get { return coolDownTime; }
            set
            {
                if (coolDownTime != value)
                {
                    SetProperty(ref coolDownTime, value);
                    if(mouse != null) mouse.CoolDownTime = value;
                }
            }
        }

        /// <summary>
        /// Defines if Simulation is used as Toggle Switch
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
        /// Major Options
        /// </summary>
        public MouseButtonsimulationMajorOptions MajorOptionsSelected
        {
            get { return majorOptionsSelected; }
            set
            {
                if (majorOptionsSelected != value)
                {
                    SetProperty(ref majorOptionsSelected, value);
                    mouse.ReleaseMouseButtons(true, true, true);
                }
            }
        }

        /// <summary>
        /// Middle Mouse Button Options
        /// </summary>
        public MBSimulationMiddleMouseOption MiddleMouseButtonOptions
        {
            get { return middleMouseButtonOptions; }
            set
            {
                if (middleMouseButtonOptions != value)
                {
                    SetProperty(ref middleMouseButtonOptions, value);
                    mouse.ReleaseMouseButtons(true, true, true);
                }
            }
        }

        /// <summary>
        /// Mouse Wheel Options
        /// </summary>
        public UseMouseWheelEnabled MouseWheelOptions
        {
            get { return mouseWheelOptions; }
            set
            {
                if (mouseWheelOptions != value)
                {
                    SetProperty(ref mouseWheelOptions, value);
                    mouse.ReleaseMouseButtons(true, true, true);
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
            OptionTitle = GenDefString.SimulateMouseButtonTitle;
            CoolDownTime = GenDefInt.DefaultCoolDownTime;
            MouseWheelSpeedTime = GenDefInt.DefaultMouseWheelSpeedTime;
            MouseWheelIndent = GenDefInt.DefaultMouseWheelIndent;
            mouse = new SimulatedMouseModel();
        }

        /// <summary>
        /// Method that handles the standard options
        /// </summary>
        private void HandleStandardClick(bool left, bool right)
        {
            mouse.PressMouseButtons(left, right, false);
        }

        /// <summary>
        /// Method that handles the standard options
        /// </summary>
        private void HandleMiddleMouseButtonClick(bool left, bool right)
        {
            switch(MiddleMouseButtonOptions)
            {
                case MBSimulationMiddleMouseOption.UseInsteadOfBoth:
                    mouse.PressMouseButtons(false, false, left || right);
                    break;
                case MBSimulationMiddleMouseOption.UseInsteadOfLeft:
                    mouse.PressMouseButtons(false, right, left);
                    break;
                case MBSimulationMiddleMouseOption.UseInsteadOfRight:
                    mouse.PressMouseButtons(left, false, right);
                    break;
            }
        }

        /// <summary>
        /// Method that handles the mousewheel options
        /// </summary>
        private void HandlesMouseWheelTurn(bool left, bool right)
        {
            if (left == right) return;

            switch(MouseWheelOptions)
            {
                case UseMouseWheelEnabled.InvertUpDownDisabled:
                    mouse.TurnMouseWheel(left, MouseWheelIndent);
                    break;
                case UseMouseWheelEnabled.InvertUpDownEnabled:
                    mouse.TurnMouseWheel(left, MouseWheelIndent);
                    break;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Trigger Status changed Event was raised
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnTriggerOnTriggerStatusChanged(object source, EventArgs e)
        {
            var triggers = (source as OutputViewModel);
            switch (MajorOptionsSelected)
            { 
                case MouseButtonsimulationMajorOptions.UseStandard:
                    HandleStandardClick(triggers.LeftTriggerActive, triggers.RightTriggerActive);
                    break;
                case MouseButtonsimulationMajorOptions.OnlyUseRightMB:
                    HandleStandardClick(false, triggers.LeftTriggerActive || triggers.RightTriggerActive);
                    break;
                case MouseButtonsimulationMajorOptions.OnlyUseLeftMB:
                    HandleStandardClick(triggers.LeftTriggerActive || triggers.RightTriggerActive, false);
                    break;
                case MouseButtonsimulationMajorOptions.InverLeftRightEnabled:
                    HandleStandardClick(triggers.RightTriggerActive, triggers.LeftTriggerActive);
                    break;
                case MouseButtonsimulationMajorOptions.UseMiddleMouseButtonEnabled:
                    HandleMiddleMouseButtonClick(triggers.LeftTriggerActive, triggers.RightTriggerActive);
                    break;
                case MouseButtonsimulationMajorOptions.UseMouseWheelEnabled:
                    HandlesMouseWheelTurn(triggers.LeftTriggerActive, triggers.RightTriggerActive);
                    break;
            }
        }
        #endregion
    }
}
