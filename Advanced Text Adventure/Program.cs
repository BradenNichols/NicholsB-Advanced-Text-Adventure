using System.Runtime.InteropServices;

namespace Advanced_Text_Adventure
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "John Deflector Adventures";
            Console.SetWindowSize(140, 36);

            Player player = new(name: "Lapis");
            Player.player = player;

            // Title
            TitleScreen();
        }

        public static void TitleScreen()
        {
            Reader.WriteLine("John Deflector Adventures", 45, ConsoleColor.Blue);
            Thread.Sleep(600);

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
                    Reader.WriteLine("Shootin' time..", 35, ConsoleColor.Green);
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

            int levelNumber = 1;
            bool hasWon = false;

            while (true)
            {
                Battle newBattle = new("Level " + levelNumber.ToString());
                newBattle.Start();

                if (newBattle.outcome == "Death")
                {
                    Retry();
                    break;
                }

                levelNumber++;

                if (levelNumber > 10)
                {
                    hasWon = true;
                    break;
                }
            }

            if (hasWon)
            {
                Reader.WriteLine("You Win!!!!!", 45, ConsoleColor.Green);
                Thread.Sleep(2500);
            }
        }

        public static void Retry()
        {
            Reader.WriteLine("\nPress enter to retry..\n", 25);
            Reader.ReadLine();

            StartGame();
        }
    }
}
