using System;
using VisualDetection.Interfaces;
using VisualDetection.Model;
using VisualDetection.Util;

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
        private uint buttonPressTime;
        private uint coolDownTime;
        private uint mouseWheelSpeedTime;
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
        public uint ButtonPressTime
        {
            get { return buttonPressTime; }
            set
            {
                if (buttonPressTime != value)
                {
                    SetProperty(ref buttonPressTime, value);
                    mouse.UpdateTimers();
                }
            }
        }

        /// <summary>
        /// The amount of ms between two mousewheel actions 
        /// </summary>
        public uint MouseWheelSpeedTime
        {
            get { return mouseWheelSpeedTime; }
            set
            {
                if (mouseWheelSpeedTime != value)
                {
                    SetProperty(ref mouseWheelSpeedTime, value);
                    mouse.UpdateTimers();
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
        public uint CoolDownTime
        {
            get { return coolDownTime; }
            set
            {
                if (coolDownTime != value)
                {
                    SetProperty(ref coolDownTime, value);
                    if (mouse != null) mouse.CoolDownTime = value;
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
            mouse = new SimulatedMouseModel(this);
            OptionTitle = GenDefString.SimulateMouseButtonTitle;
            CoolDownTime = GenDefInt.DefaultCoolDownTime;
            ButtonPressTime = GenDefInt.DefaultButtonPressTime;
            MouseWheelSpeedTime = GenDefInt.DefaultMouseWheelSpeedTime;
            MouseWheelIndent = GenDefInt.DefaultMouseWheelIndent;

        }

        /// <summary>
        /// Method that handles the standard options
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private void HandleMiddleMouseButtonClick(bool left, bool right)
        {
            switch (MiddleMouseButtonOptions)
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
        ///  Method that handles the mousewheel options
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private void HandlesMouseWheelTurn(bool left, bool right)
        {
            if (left == right)
            {
                if (UseAsToggleSwitchEnabled)
                {
                    mouse.StopMouseWheel();
                    return;
                }
                else return;
            }

            switch (MouseWheelOptions)
            {
                case UseMouseWheelEnabled.InvertUpDownDisabled:
                    mouse.TurnMouseWheel(left);
                    break;
                case UseMouseWheelEnabled.InvertUpDownEnabled:
                    mouse.TurnMouseWheel(right);
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
                    mouse.PressMouseButtons(triggers.LeftTriggerActive, triggers.RightTriggerActive, false);
                    break;
                case MouseButtonsimulationMajorOptions.OnlyUseRightMB:
                    mouse.PressMouseButtons(false, triggers.LeftTriggerActive || triggers.RightTriggerActive, false);
                    break;
                case MouseButtonsimulationMajorOptions.OnlyUseLeftMB:
                    mouse.PressMouseButtons(triggers.LeftTriggerActive || triggers.RightTriggerActive, false, false);
                    break;
                case MouseButtonsimulationMajorOptions.InverLeftRightEnabled:
                    mouse.PressMouseButtons(triggers.RightTriggerActive, triggers.LeftTriggerActive, false);
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
