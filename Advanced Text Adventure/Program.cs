using System.Runtime.InteropServices;

namespace Advanced_Text_Adventure
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "deflector: retro";
            Console.SetWindowSize(140, 36);

            Player player = new(name: "Lapis");
            Player.player = player;

            StartGame();

            /*
            Reader.WriteLine("THINK FAST!\n", 10);
            ConsoleKeyInfo keyResult = Reader.ReadTimed(500);

            if (keyResult.Key != ConsoleKey.None)
                Reader.WriteLine("PARRY!", 60, ConsoleColor.Green);
            else
                Reader.WriteLine("EXPLODEEEEEEEEEEEEEEEEEEEEEEEE", 80, ConsoleColor.Red);*/
        }

        public static void StartGame()
        {
            Player.player.Reset();

            Battle testBattle = new("Tutorial");
            testBattle.Start();

            if (testBattle.outcome == "Death")
            {
                Retry();
                return;
            }

            Reader.WriteLine("you win!!!", 20, ConsoleColor.Green);
            Thread.Sleep(2000);

            Retry();
        }

        public static void Retry()
        {
            Reader.WriteLine("\npress enter to retry..", 25);
            Reader.ReadLine();

            StartGame();
        }
    }
}
