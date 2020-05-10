using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TerminalGame.Terminal
{
    public class TerminalController : DrawableGameComponent
    {
        // Class derives from DrawableGameComponent, so we'll somewhere to keep Game
        Game game;
        
        // The TerminalHistory list - this is where all of the terminal's text goes after it's drawn
        public static List<List<TextChar>> TerminalHistory = new List<List<TextChar>>();
        
        // HistoryScroll - the number of pixels the text in the terminal is scrolled (a proxy for the position of the terminal)
        public static int HistoryScroll = 0;

        // Internal stuff that I don't really care enough to explain but it's pretty self-explanitory
        internal static string TextBuffer = "";
        internal static List<TextChar> InputBuffer = new List<TextChar>();
        internal static Rectangle ScrollCollision;
        internal static Rectangle WrapCollision;
        internal static List<TextChar> Prompt;
        KeyboardState previous;

        /// <summary>
        /// The TerminalController is the basis for the terminal system. It watches keyboard input (from TextHandler), writes to the viewport, and sends commands to the CommandParser to get executed. Inherits DrawableGameComponent.
        /// </summary>
        /// <param name="game"></param>
        public TerminalController(Game _game) : base(_game)
        {
            this.game = _game;
            game.Window.TextInput += Input.TextHandler.GetText;
            Input.TextHandler.InputEnter += EnterHit;
            Input.TextHandler.InputChar += InputHit;
            Input.TextHandler.InputBackspace += BackspaceHit;
            previous = Keyboard.GetState();
            Prompt = TextChar.ListFromString(TerminalSettings.Prompt, TerminalSettings.PromptColor, Color.Black);
            ScrollCollision = new Rectangle(new Point(0, GameClasses.graphics.PreferredBackBufferHeight - 17), new Point(GameClasses.graphics.PreferredBackBufferWidth, 4800));
            WrapCollision = new Rectangle(new Point(GameClasses.graphics.PreferredBackBufferWidth - 9, 0), new Point(4800, GameClasses.graphics.PreferredBackBufferHeight));
        }

        /// <summary>
        /// Fires when Enter is hit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void EnterHit(object sender, Input.KeyEnterEventArgs e)
        {
            if (InputBuffer.Count > 0) { InputBuffer.RemoveAt(InputBuffer.Count - 1); }
            string Log = TerminalSettings.Prompt + TextChar.GetText(InputBuffer);
            TerminalHistory.Add(new List<TextChar>());
            Prompt.ForEach(item => TerminalHistory[TerminalHistory.Count - 1].Add(item));
            InputBuffer.ForEach(item => TerminalHistory[TerminalHistory.Count - 1].Add(item));
            if (TextBuffer != "") { CommandParser.ParseCommand(TextBuffer); }
            InputBuffer.Clear();
            TextBuffer = "";
            WriteLine("");
        }

        /// <summary>
        /// Fires when a key is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void InputHit(object sender, Input.KeyPressEventArgs e)
        {
            TextBuffer += e.Char;
            if (InputBuffer.Count > 0) { InputBuffer.RemoveAt(InputBuffer.Count - 1); }
            InputBuffer.Add(new TextChar(e.Char.ToString(), Color.White, Color.Black));
            InputBuffer.Add(new TextChar("_", Color.LightGray, Color.Black));
        }

        /// <summary>
        /// Fires when Backspace is hit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void BackspaceHit(object sender, Input.KeyBackspaceEventArgs e)
        {
            InputBuffer.Remove(InputBuffer[InputBuffer.Count - 1]);
            TextBuffer = TextBuffer.Remove(TextBuffer.Length - 1);
        }

        public static void WriteLine(string text, Color? fcolor = null, Color? bgcolor = null)
        {
            if (fcolor != null && bgcolor != null)
            {
                TerminalHistory.Add(TextChar.ListFromString(text, (Color)fcolor, (Color)bgcolor));
            }
            else
            {
                TerminalHistory.Add(TextChar.ListFromString(text, Color.White, Color.Black));
            }
        }

        public static void Write(string text, Color? fcolor = null, Color? bgcolor = null)
        {
            if (fcolor != null && bgcolor != null)
            {
                TextChar.ListFromString(text, (Color)fcolor, (Color)bgcolor).ForEach(item => TerminalHistory[TerminalHistory.Count - 1].Add(item));
            }
            else
            {
                TextChar.ListFromString(text, Color.White, Color.Black).ForEach(item => TerminalHistory[TerminalHistory.Count - 1].Add(item));
            }
        }

        /// <summary>
        /// MonoGame Update.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.PageDown) && !previous.IsKeyDown(Keys.PageDown))
            {
                HistoryScroll -= 16;
            }
            if (state.IsKeyDown(Keys.PageUp) && !previous.IsKeyDown(Keys.PageUp))
            {
                HistoryScroll += 16;
            }
            previous = state;

            if (ScrollCollision.Contains(Prompt[0].Bounding))
            {
                HistoryScroll -= 16;
            }

            if (InputBuffer.Count > 0 && WrapCollision.Contains(InputBuffer[InputBuffer.Count - 1].Bounding))
            {
                WriteLine("Wrap!");
                InputBuffer.Remove(InputBuffer[InputBuffer.Count - 1]);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// MonoGame Draw.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            GameClasses.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            for (int i = 0; i < TerminalHistory.Count; i++)
            {
                TextChar.DrawFromList(TerminalHistory[i], new Vector2(0, HistoryScroll + i * 16));
            }

            TextChar.DrawFromList(Prompt, new Vector2(0, HistoryScroll + TerminalHistory.Count * 16));
            TextChar.DrawFromList(InputBuffer, new Vector2(TerminalSettings.Prompt.Length * 8, HistoryScroll + TerminalHistory.Count * 16));

            GameClasses.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}