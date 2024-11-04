using System.Runtime.InteropServices;

namespace Advanced_Text_Adventure
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Deflector: Retro";
            Console.SetWindowSize(140, 36);

            Player player = new(name: "Lapis");
            Player.player = player;

            // Title

            TitleScreen();

            /*
            Reader.WriteLine("THINK FAST!\n", 10);
            ConsoleKeyInfo keyResult = Reader.ReadTimed(500);

            if (keyResult.Key != ConsoleKey.None)
                Reader.WriteLine("PARRY!", 60, ConsoleColor.Green);
            else
                Reader.WriteLine("EXPLODEEEEEEEEEEEEEEEEEEEEEEEE", 80, ConsoleColor.Red);*/
        }

        public static void TitleScreen()
        {
            Reader.WriteLine("Deflector: Retro", 55, ConsoleColor.Blue);
            Thread.Sleep(750);

            List<string> choices = new();
            choices.Add("1) How to Play?");
            choices.Add("2) New Game");

            if (true)
                choices.Add("3) Load Save");

            bool firstChoice = true;

            while (true)
            {
                if (!firstChoice)
                {
                    Console.Clear();
                    Reader.WriteLine("\nDeflector: Retro", color: ConsoleColor.Blue);
                }
                else
                    firstChoice = false;

                Reader.WriteLine("\n");

                string result = Reader.ChooseSomething(choices);

                if (result.Contains("1)"))
                {
                    Reader.WriteLine("Deflect enemies to their death.", 50, ConsoleColor.Green);
                    Thread.Sleep(1000);

                    Reader.WriteLine("-> Arrow keys to move.", 60);

                    Thread.Sleep(1250);
                    Reader.WriteLine("-> F to Deflect: move to change direction of deflect.", 60);

                    Thread.Sleep(2500);

                    Reader.WriteLine("\nPress enter to return to title screen.", color: ConsoleColor.DarkGray);
                    Reader.ReadLine();
                } else if (result.Contains("2)"))
                {
                    Reader.WriteLine("deflecting time...", 35, ConsoleColor.Green);
                    Thread.Sleep(500);

                    StartGame();
                    break;
                }
            }
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
            Thread.Sleep(1500);

            Retry();
        }

        public static void Retry()
        {
            Reader.WriteLine("\npress enter to retry..\n", 25);
            Reader.ReadLine();

            StartGame();
        }
    }
}
