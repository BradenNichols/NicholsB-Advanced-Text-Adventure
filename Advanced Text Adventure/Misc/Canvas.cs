using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure.Misc
{
    public static class Canvas
    {
        public static int width = 80;
        public static int height = 32;

        public static int baseWidth = 3;
        public static int baseHeight = 1;

        public static void Draw()
        {
            Console.Clear();
            Console.CursorVisible = false;

            // Top

            for (int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(i + baseWidth, baseHeight);
                Reader.Write("▄");
            }

            // Bottom

            for (int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(i + baseWidth, height + baseHeight);
                Reader.Write("▄");
            }

            // Right

            Console.SetCursorPosition(baseWidth + width, baseHeight); // top right
            Reader.Write("▄");

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(baseWidth + width, baseHeight + i + 1); 
                Reader.Write("█");
            }

            // Left

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(baseWidth, baseHeight + i + 1);
                Reader.Write("█");
            }

            // Special

            Console.SetCursorPosition(baseWidth + width + 20, baseHeight + (height / 2));
            Reader.Write("Deflector: Retro", -1, ConsoleColor.Blue);
            Console.SetCursorPosition(baseWidth + width + 13, baseHeight + (height / 2) + 1);
            Reader.Write("the besterest game of all time", -1, ConsoleColor.DarkBlue);
        }
    }
}
