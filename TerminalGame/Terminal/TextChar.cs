using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TerminalGame.Terminal
{
    public class TextChar
    {
        public TextChar(string character, Color fcolor, Color bgcolor)
        {
            Character = character;
            ForegroundColor = fcolor;
            BackgroundColor = bgcolor;
        }
        /// <summary>
        /// The current text character.
        /// </summary>
        public string Character { get; set; }

        /// <summary>
        /// The Foreground Color of the terminal (text color for this character).
        /// </summary>
        public Color ForegroundColor { get; set; }

        /// <summary>
        /// The Background Color of the terminal (color of the area behind this character).
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// The Format Byte (BUIb), WIP
        /// </summary>
        public byte FormatByte { get; set; }

        /// <summary>
        /// The Bounding Box of this character - represented by the colored area around the character, used internally for autoscrolling.
        /// </summary>
        public Rectangle Bounding { get; set; }

        public static List<TextChar> ListFromString(string input, Color fcolor, Color bgcolor)
        {
            List<TextChar> output = new List<TextChar>();
            for (int i = 0; i < input.Length; i++)
            {
                output.Add(new TextChar(input[i].ToString(), fcolor, bgcolor));
            }
            return output;
        }
        public static string GetText(List<TextChar> list)
        {
            string output = "";
            for (int i = 0; i < list.Count; i++)
            {
                output += list[i].Character;
            }
            return output;
        }

        public static void DrawFromList(List<TextChar> list, Vector2 position)
        {
            for (int i = 0; i < list.Count; i++)
            {
                DrawSegment(list[i], new Vector2(position.X, position.Y), i);
            }
        }

        public static void DrawSegment(TextChar character, Vector2 position, int i)
        {
            character.Bounding = new Rectangle(new Point((int)position.X + i * 8, (int)position.Y), new Point(8, 16));
            GameClasses.spriteBatch.Draw(GameClasses.content.Load<Texture2D>("Textures/Pixel"), character.Bounding, character.BackgroundColor);
            GameClasses.spriteBatch.DrawString(TerminalSettings.Font, character.Character, new Vector2(position.X + i * 8, position.Y), character.ForegroundColor);
        }
    }
}
