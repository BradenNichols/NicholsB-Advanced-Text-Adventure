using System.Runtime.InteropServices;

namespace Advanced_Text_Adventure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Reader.WriteLine("THINK FAST!\n", 20);
            ConsoleKeyInfo keyResult = Reader.ReadTimed(500);

            if (keyResult.Key != ConsoleKey.None)
                Reader.WriteLine("PARRY!", 60, ConsoleColor.Green);
            else
                Reader.WriteLine("EXPLODEEEEEEEEEEEEEEEEEEEEEEEE", 80, ConsoleColor.Red);
        }
    }
}
