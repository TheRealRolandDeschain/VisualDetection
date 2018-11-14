using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualDetection.Model
{
    public class SimulatedMouseModel
    {
        #region Constructor
        public SimulatedMouseModel()
        {
            buttonPressTimerLeft = new Timer();
            coolDownTimerLeft = new Stopwatch();
            buttonPressTimerRight = new Timer();
            coolDownTimerRight = new Stopwatch();
        }
        #endregion

        #region Private Properties
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_WHEEL = 0x0800;
        private Timer buttonPressTimerLeft;
        private Stopwatch coolDownTimerLeft;
        private Timer buttonPressTimerRight;
        private Stopwatch coolDownTimerRight;
        #endregion

        #region Public Properties
        public bool RightButtonPressed { get; set; } = false;
        public bool LeftButtonPressed { get; set; } = false;
        public bool MiddleMouseButtonPressed { get; set; } = false;
        public int CoolDownTime { get; set; }

        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the timers after a option has changed
        /// </summary>
        public void UpdateTimers(int buttonPressTime)
        {
            buttonPressTimerLeft.Interval = buttonPressTime;
            buttonPressTimerRight.Interval = buttonPressTime;
        }

        /// <summary>
        /// Method that releases the virtual mouse buttons
        /// </summary>
        /// <param name="releaseLeft"></param>
        /// <param name="releasRight"></param>
        /// <param name="releaseMiddle"></param>
        public void ReleaseMouseButtons(bool releaseLeft, bool releasRight, bool releaseMiddle)
        {
            uint MouseButtons = 0;
            if (releaseLeft && LeftButtonPressed)
            {
                MouseButtons |= MOUSEEVENTF_LEFTUP;
                LeftButtonPressed = false;
            }
            if (releasRight && RightButtonPressed)
            {
                MouseButtons |= MOUSEEVENTF_RIGHTUP;
                RightButtonPressed = false;
            }
            if (releaseMiddle && MiddleMouseButtonPressed)
            {
                MouseButtons |= MOUSEEVENTF_MIDDLEUP;
                MiddleMouseButtonPressed = false;
            }
            if (MouseButtons != 0)
            {
                mouse_event(MouseButtons, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
            }
        }

        /// <summary>
        /// Method that presses the virtual mouse buttons
        /// </summary>
        /// <param name="pressLeft"></param>
        /// <param name="pressRight"></param>
        /// <param name="pressMiddle"></param>
        public void PressMouseButtons(bool pressLeft, bool pressRight, bool pressMiddle)
        {
            uint MouseButtons = 0;
            if (pressLeft && !LeftButtonPressed)
            {
                MouseButtons |= MOUSEEVENTF_LEFTDOWN;
                LeftButtonPressed = true;
            }
            if (pressRight && !RightButtonPressed)
            {
                MouseButtons |= MOUSEEVENTF_RIGHTDOWN;
                RightButtonPressed = true;
            }
            if (pressMiddle && !MiddleMouseButtonPressed)
            {
                MouseButtons |= MOUSEEVENTF_MIDDLEDOWN;
                MiddleMouseButtonPressed = true;
            }
            if (MouseButtons != 0)
            {
                mouse_event(MouseButtons, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
            }
        }

        /// <summary>
        /// Method that releases the virtual mouse buttons
        /// </summary>
        /// <param name="turnUpdown">specifies the direction true = up, false = down</param>
        /// <param name="turnMovement">specifies the indentes of the move</param>
        public void TurnMouseWheel(bool turnUpdown, int turnMovement)
        {
            int turns = turnMovement * 120;
            if (!turnUpdown) turns *= -1;
            mouse_event(MOUSEEVENTF_WHEEL, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, turns, 0);
        }

        #endregion

        #region Setup for MouseClick Simulation
        // Taken from https://stackoverflow.com/questions/2416748/how-do-you-simulate-mouse-click-in-c
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, int cButtons, uint dwExtraInfo);
        #endregion
    }
}
