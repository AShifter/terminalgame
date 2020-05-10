using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace TerminalGame.Input
{
    public static class TextHandler
    {
        /// <summary>
        /// The current result of keyboard input. Characters are appended to InputBuffer until Enter is pressed and InputEnter is fired.
        /// </summary>
        static internal string InputBuffer { get; set; } = "";

        /// <summary>
        /// InputEnter event. Fires when Enter is pressed. Args are type KeyEnterEventArgs, contains Text, the value of InputBuffer before Enter was pressed.
        /// </summary>
        static internal event EventHandler<KeyEnterEventArgs> InputEnter;

        /// <summary>
        /// InputEnter event. Fires when any character is added to the input buffer. Args are type KeyPressEventArgs, contains Text, the value of InputBuffer with the newly appended character, and Char, the newly appened char.
        /// </summary>
        static internal event EventHandler<KeyPressEventArgs> InputChar;

        /// <summary>
        /// InputBackspace event. Fires when Backspace is pressed. Args are type KeyBackspaceEventArgs, contains Text, the value of InputBuffer after Backspace was pressed, and Char, the char that was removed from the buffer.
        /// </summary>
        static internal event EventHandler<KeyBackspaceEventArgs> InputBackspace;

        /// <summary>
        /// Checks each keypress and either adds it to the InputBuffer or fires InputEnter if InputEnter is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static public void GetText(object sender, TextInputEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.Enter:
                    KeyEnterEventArgs argsEnter = new KeyEnterEventArgs();
                    argsEnter.Text = InputBuffer;
                    InputEnter?.Invoke(null, argsEnter);
                    InputBuffer = "";
                break;
                case Keys.Back:
                    if (InputBuffer.Length > 0)
                    {
                        KeyBackspaceEventArgs argsBackspace = new KeyBackspaceEventArgs();
                        argsBackspace.Char = InputBuffer.Substring(InputBuffer.Length - 1);
                        InputBuffer = InputBuffer.Substring(0, InputBuffer.Length - 1);
                        argsBackspace.Text = InputBuffer;
                        InputBackspace?.Invoke(null, argsBackspace);
                    }
                break;
                default:
                    KeyPressEventArgs argsKey = new KeyPressEventArgs();
                    InputBuffer += e.Character;
                    argsKey.Text = InputBuffer;
                    argsKey.Char = e.Character;
                    InputChar?.Invoke(null, argsKey);
                break;
            }
        }
    }

    /// <summary>
    /// Derives from System.EventArgs, single property;
    /// string Text { get; set; } - The contents of the input buffer after Backspace was pressed.
    /// </summary>
    public class KeyBackspaceEventArgs : EventArgs
    {
        public string Text { get; set; }
        public string Char { get; set; }
    }

    /// <summary>
    /// Derives from System.EventArgs, single property;
    /// string Text { get; set; } - The contents of the input buffer at the time Enter was pressed.
    /// </summary>
    public class KeyEnterEventArgs : EventArgs
    {
        public string Text { get; set; }
    }

    /// <summary>
    /// Derives from System.EventArgs, two properties;
    /// string Text { get; set; } - The contents of the input buffer at the time a key is pressed and added to the input buffer.
    /// string NewChar { get; set } - The last key that was pressed and added to the input buffer.
    /// </summary>
    public class KeyPressEventArgs : EventArgs
    {
        public string Text { get; set; }
        public char Char { get; set; }
    }
}
