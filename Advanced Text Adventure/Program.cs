using System.Runtime.InteropServices;

namespace Advanced_Text_Adventure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "deflector: retro";
            Console.SetWindowSize(140, 36);

            Player player = new(name: "Lapis");
            Player.player = player;

            Battle testBattle = new("Tutorial");
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
