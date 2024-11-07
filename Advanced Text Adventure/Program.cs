using System.Runtime.InteropServices;
using System.Text.Json;

namespace Advanced_Text_Adventure
{
    public class Program
    {
        public static SaveData mySave;
        static string saveJson;

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
            Reader.WriteLine("John Deflector Adventures", 30, ConsoleColor.Blue);
            Thread.Sleep(600);

            List<string> saveChoices = new() { "1) Continue", "2) Return to Title Screen" };

            List<string> choices = new() { "1) How to Play?", "2) New Game" };
            bool hasSave = LoadData();

            if (hasSave && mySave.canLoad)
                choices.Add("3) Load Save");

            bool firstChoice = true;

            while (true)
            {
                if (!firstChoice)
                {
                    Console.Clear();
                    Reader.WriteLine("\nJohn Deflector Adventures", color: ConsoleColor.Blue);
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
                } else if (result.Contains("3)"))
                {
                    PrintSave();
                    Thread.Sleep(2000);

                    Reader.WriteLine("\n");
                    string saveResult = Reader.ChooseSomething(saveChoices);

                    if (saveResult.Contains("1)"))
                    {
                        Reader.WriteLine("Shootin' time..", 35, ConsoleColor.Green);
                        Thread.Sleep(500);

                        StartGame(useSave: true);
                        break;
                    }
                }
            }
        }

        public static void StartGame(bool useSave = false, bool skipTutorial = false)
        {
            Player.player.Reset();

            int levelNumber = 1;

            if (!useSave || skipTutorial)
            {
                Battle testBattle = new("Tutorial");
                testBattle.Start();

                if (testBattle.outcome == "Death")
                {
                    Retry();
                    return;
                }
            } else if (useSave)
            {
                levelNumber = mySave.level;
            }

            bool hasWon = false;

            while (true)
            {
                Battle newBattle = new("Level " + levelNumber.ToString());
                newBattle.Start();

                if (newBattle.outcome == "Death")
                {
                    mySave.canLoad = false;
                    SaveData();

                    Retry();
                    break;
                }

                levelNumber++;

                mySave.level = levelNumber;
                mySave.canLoad = true;
                SaveData();

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
            List<string> choices = new() { "1) Retry", "2) Return to Title Screen" };

            Reader.WriteLine("\n");
            string result = Reader.ChooseSomething<string>(choices);

            if (result.Contains("1)"))
                StartGame(skipTutorial: true);
            else
                TitleScreen();
        }

        // File I/O

        static JsonSerializerOptions options = new() { IncludeFields = true, WriteIndented = true };

        public static bool LoadData()
        {
            if (!File.Exists("saveData.json"))
                return false;

            using StreamReader reader = new("saveData.json");
            saveJson = reader.ReadToEnd();

            mySave = JsonSerializer.Deserialize<SaveData>(saveJson, options);
            return true;
        }

        public static void SaveData()
        {
            string serializedData = JsonSerializer.Serialize(mySave, options);
            saveJson = serializedData;

            using StreamWriter writer = new("saveData.json"); // "using" will de-allocate resources when not in use
            writer.Write(serializedData);
            writer.Flush();
        }

        public static void PrintSave()
        {
            Reader.WriteLine("-- Save Data --\n", 45, ConsoleColor.Gray);
            Thread.Sleep(400);
            Reader.Write("Level: " + mySave.level.ToString(), 65, ConsoleColor.DarkGreen);
        }
    }
}
