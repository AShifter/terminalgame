using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TerminalGame.Terminal
{
    public static class TerminalSettings
    {
        public static Color BackgroundColor { get; set; } = Color.Black;

        public static Color ForegroundColor { get; set; } = Color.White;

        public static Color PromptColor { get; set; } = Color.DarkGray;

        public static string Prompt { get; set; } = "~> ";

        public static SpriteFont Font { get; set; }
    }
}
