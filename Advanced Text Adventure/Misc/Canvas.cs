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

            Console.SetCursorPosition(baseWidth + width + 16, baseHeight + 13);
            Reader.Write("John Deflector Adventures", -1, ConsoleColor.Blue);

            int length = Battle.activeBattle.levelName.Length - 1;

            Console.SetCursorPosition(baseWidth + width + (19 + length), baseHeight + 14);
            Reader.Write(Battle.activeBattle.levelName, -1, ConsoleColor.DarkGreen);

            // Controls

            Console.SetCursorPosition(baseWidth + width + 7, baseHeight + 17);
            Reader.Write("Controls:", -1, ConsoleColor.DarkGray);

            Console.SetCursorPosition(baseWidth + width + 7, baseHeight + 18);
            Reader.Write("-> Arrow keys to move.", -1, ConsoleColor.Gray);

            Console.SetCursorPosition(baseWidth + width + 7, baseHeight + 19);
            Reader.Write("-> F to Shoot.", -1, ConsoleColor.Gray);
        }
    }
}
