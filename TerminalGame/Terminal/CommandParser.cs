using Microsoft.Xna.Framework;

namespace TerminalGame.Terminal
{
    public class CommandParser
    {
        public static void ParseCommand(string command)
        {
            if (command.ToLower() == "test")
            {
                TerminalController.WriteLine("Test Command!", Color.Black, Color.Teal);
            }
            else if (command.ToLower() == "clear")
            {
                TerminalController.HistoryScroll = -16;
                TerminalController.TerminalHistory.Clear();
            }
            else if (command.ToLower() == "palette")
            {
                TerminalController.WriteLine("Color Palette;");
                TerminalController.WriteLine("     ", Color.White, Color.Black);
                TerminalController.Write("     ", Color.White, Color.DarkGray);
                TerminalController.Write("     ", Color.White, Color.DarkRed);
                TerminalController.Write("     ", Color.White, Color.DarkMagenta);
                TerminalController.Write("     ", Color.White, Color.DarkBlue);
                TerminalController.Write("     ", Color.White, Color.Teal);
                TerminalController.Write("     ", Color.White, Color.DarkGreen);
                TerminalController.Write("     ", Color.White, Color.Gold);
                TerminalController.WriteLine("  0  ", Color.White, Color.Black);
                TerminalController.Write("  1  ", Color.White, Color.DarkGray);
                TerminalController.Write("  2  ", Color.White, Color.DarkRed);
                TerminalController.Write("  3  ", Color.White, Color.DarkMagenta);
                TerminalController.Write("  4  ", Color.White, Color.DarkBlue);
                TerminalController.Write("  5  ", Color.White, Color.Teal);
                TerminalController.Write("  6  ", Color.White, Color.DarkGreen);
                TerminalController.Write("  7  ", Color.White, Color.Gold);
                TerminalController.WriteLine("     ", Color.White, Color.Black);
                TerminalController.Write("     ", Color.White, Color.DarkGray);
                TerminalController.Write("     ", Color.White, Color.DarkRed);
                TerminalController.Write("     ", Color.White, Color.DarkMagenta);
                TerminalController.Write("     ", Color.White, Color.DarkBlue);
                TerminalController.Write("     ", Color.White, Color.Teal);
                TerminalController.Write("     ", Color.White, Color.DarkGreen);
                TerminalController.Write("     ", Color.White, Color.Gold);

                TerminalController.WriteLine("     ", Color.Black, Color.LightGray);
                TerminalController.Write("     ", Color.Black, Color.White);
                TerminalController.Write("     ", Color.Black, Color.Red);
                TerminalController.Write("     ", Color.Black, Color.Magenta);
                TerminalController.Write("     ", Color.Black, Color.Blue);
                TerminalController.Write("     ", Color.Black, Color.Cyan);
                TerminalController.Write("     ", Color.Black, Color.Lime);
                TerminalController.Write("     ", Color.Black, Color.Yellow);
                TerminalController.WriteLine("  8  ", Color.Black, Color.LightGray);
                TerminalController.Write("  9  ", Color.Black, Color.White);
                TerminalController.Write("  A  ", Color.Black, Color.Red);
                TerminalController.Write("  B  ", Color.Black, Color.Magenta);
                TerminalController.Write("  C  ", Color.Black, Color.Blue);
                TerminalController.Write("  D  ", Color.Black, Color.Cyan);
                TerminalController.Write("  E  ", Color.Black, Color.Lime);
                TerminalController.Write("  F  ", Color.Black, Color.Yellow);
                TerminalController.WriteLine("     ", Color.Black, Color.LightGray);
                TerminalController.Write("     ", Color.Black, Color.White);
                TerminalController.Write("     ", Color.Black, Color.Red);
                TerminalController.Write("     ", Color.Black, Color.Magenta);
                TerminalController.Write("     ", Color.Black, Color.Blue);
                TerminalController.Write("     ", Color.Black, Color.Cyan);
                TerminalController.Write("     ", Color.Black, Color.Lime);
                TerminalController.Write("     ", Color.Black, Color.Yellow);
            }
            else if (command.ToLower() == "version")
            {
                TerminalController.WriteLine("[AShifter's Terminal Thing Version 0.2a]", Color.Black, Color.Lime);
            }
            else if (command.ToLower() == "help")
            {
                TerminalController.WriteLine("[Terminal Help]", Color.Gold, Color.Black);
                TerminalController.WriteLine("help", Color.Teal, Color.Black);
                TerminalController.Write(" - Display this text.");
                TerminalController.WriteLine("test", Color.Teal, Color.Black);
                TerminalController.Write(" - Test Command.");
                TerminalController.WriteLine("palette", Color.Teal, Color.Black);
                TerminalController.Write(" - Display the current 16 color palette.");
                TerminalController.WriteLine("clear", Color.Teal, Color.Black);
                TerminalController.Write(" - Clear the terminal.");
                TerminalController.WriteLine("shutdown", Color.Teal, Color.Black);
                TerminalController.Write(" - Exit the game.");
            }
            else if (command.ToLower() == "shutdown")
            {
                Game1.self.Exit();
            }
            else
            {
                TerminalController.WriteLine("Command Not Found", Color.Red, Color.Black);
            }
        }
    }
}
