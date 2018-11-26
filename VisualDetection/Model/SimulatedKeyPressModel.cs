using System;
using System.Runtime.InteropServices;
using VisualDetection.Util;

namespace VisualDetection.Model
{
    public class SimulatedKeyPressModel
    {

        /// <summary>
        /// Method to simulate a Keystroke 
        /// (taken from https://www.chriswirz.com/software/using-the-windows-api-to-simulate-keyboard-input-in-c-sharp)
        /// </summary>
        /// <param name="keyCode"></param>
        public static void KeyPress(KeyCode keyCode)
        {
            INPUT input = new INPUT
            {
                type = SendInputEventType.InputKeyboard,
                mkhi = new MOUSEANDKEYBOARDINPUT
                {
                    ki = new KEYBOARDINPUT
                    {
                        wVk = (ushort)keyCode,
                        wScan = 0,
                        dwFlags = 0, // if nothing, key down
                        time = 0,
                        dwExtraInfo = IntPtr.Zero,
                    }
                }
            };

            INPUT input2 = new INPUT
            {
                type = SendInputEventType.InputKeyboard,
                mkhi = new MOUSEANDKEYBOARDINPUT
                {
                    ki = new KEYBOARDINPUT
                    {
                        wVk = (ushort)keyCode,
                        wScan = 0,
                        dwFlags = 2, // key up
                        time = 0,
                        dwExtraInfo = IntPtr.Zero,
                    }
                }
            };

            INPUT[] inputs = new INPUT[] { input, input2 }; // Combined, it's a keystroke
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }


        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);
    }
}
