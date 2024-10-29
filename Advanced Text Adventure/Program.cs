using System.Runtime.InteropServices;

namespace Advanced_Text_Adventure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestEnemy enemy = new(name: "ukoG");
            Friendly player = new(name: "Lapis");

            Battle testBattle = new(new Friendly[] { player }, new Enemy[] { enemy });
            testBattle.Start();

            /*
            Reader.WriteLine("THINK FAST!\n", 10);
            ConsoleKeyInfo keyResult = Reader.ReadTimed(500);

            if (keyResult.Key != ConsoleKey.None)
                Reader.WriteLine("PARRY!", 60, ConsoleColor.Green);
            else
                Reader.WriteLine("EXPLODEEEEEEEEEEEEEEEEEEEEEEEE", 80, ConsoleColor.Red);*/
        }
    }
}
