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
        private Stopwatch buttonPressTimer;
        private Stopwatch coolDownTimer;
        private bool useAsToggleSwitchEnabled;
        private MouseButtonsimulationMajorOptions majorOptionsSelected;
        private MBSimulationMiddleMouseOption middleMouseButtonOptions;
        private UseMouseWheelEnabled mouseWheelOptions;
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
            ButtonPressTime = GenDefInt.DefaultButtonPressTime;
            CoolDownTime = GenDefInt.DefaultCoolDownTime;
            buttonPressTimer = new Stopwatch();
            coolDownTimer = new Stopwatch();
        }

        /// <summary>
        /// Simulates the MouseClicks as ToggleSwitches
        /// </summary>
        private void SimulateMouseClickAsToggleSwitch()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
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
            System.Windows.MessageBox.Show(UseAsToggleSwitchEnabled.ToString());
        }
        #endregion

        #region Setup for MouseClick Simulation
        // Taken from https://stackoverflow.com/questions/2416748/how-do-you-simulate-mouse-click-in-c
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_WHEEL = 0x0800;
        #endregion
    }
}
