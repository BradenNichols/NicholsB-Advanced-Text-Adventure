using System.Runtime.InteropServices;
using System.Text.Json;

namespace Advanced_Text_Adventure
{
    public class Program
    {
        public static SaveData mySave = new();
        static string saveJson;

        static int saveDataVersion = 1; // change this for breaking save data changes

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
            Console.Clear();
            Reader.WriteLine("John Deflector Adventures", 30, ConsoleColor.Blue);
            Thread.Sleep(600);

            List<string> saveChoices = new() { "1) Continue", "2) Return to Title Screen" };

            List<string> choices = new() { "1) How to Play?", "2) New Game" };
            LoadData();

            if (mySave.canLoad && mySave.level > 0 && mySave.saveDataVersion == saveDataVersion)
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
                    Reader.WriteLine("Shoot enemies and buy upgrades in-between levels.", 50, ConsoleColor.Green);
                    Thread.Sleep(1000);

                    Reader.WriteLine("-> Arrow keys to move.", 60);

                    Thread.Sleep(1250);
                    Reader.WriteLine("-> F to Shoot in move direction.", 60);

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
                    Thread.Sleep(750);

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

            if (!useSave && !skipTutorial)
            {
                ResetSave();

                while (true)
                {
                    Battle testBattle = new("Tutorial");
                    testBattle.Start();

                    if (testBattle.outcome == "Win")
                        break;
                }

                mySave.level = 1;
            } else if (useSave)
                PreLevel();

            bool hasWon = false;

            while (true)
            {
                Battle newBattle = new("Level " + mySave.level.ToString());
                newBattle.Start();

                if (newBattle.outcome == "Death")
                {
                    Retry();
                    break;
                }

                mySave.level++;

                if (mySave.level <= 3)
                    mySave.shopPoints++;
                else
                    mySave.shopPoints += 2;

                SaveData();

                if (mySave.level > 10)
                {
                    hasWon = true;
                    break;
                } else
                {
                    bool shouldBreak = PreLevel();

                    if (shouldBreak)
                        break;
                }   
            }

            if (hasWon)
            {
                Reader.WriteLine("You Win!!!!!\n", 45, ConsoleColor.Green);
                Thread.Sleep(2500);
            }
        }

        public static bool PreLevel()
        {
            Console.Clear();
            Reader.WriteLine($"Level {mySave.level} Intermission", 30, ConsoleColor.Blue);

            //List<string> saveChoices = new() { "1) Continue", "2) Return to Title Screen" };

            List<string> choices = new() { "1) Continue Game", $"2) Shop (${mySave.shopPoints})", "3) Title Screen" };

            bool firstChoice = true;
            bool shouldBreak = false;

            while (true)
            {
                if (!firstChoice)
                {
                    Console.Clear();
                    Reader.WriteLine($"Level {mySave.level} Intermission", color: ConsoleColor.Blue);
                }
                else
                    firstChoice = false;

                Reader.WriteLine("\n");

                string result = Reader.ChooseSomething(choices);

                if (result.Contains("1)")) // continue
                {
                    break;
                }
                else if (result.Contains("2)")) // shop
                {
                    Console.Clear();

                    Reader.WriteLine("Pre-Level Shop", color: ConsoleColor.Gray);
                    Reader.WriteLine($"{mySave.shopPoints} Derek Dollars", 30, ConsoleColor.Green);

                    Thread.Sleep(2500);

                    SaveData();
                    choices[1] = $"2) Shop (${mySave.shopPoints})";
                }
                else if (result.Contains("3)")) // title screen
                {
                    TitleScreen();
                    shouldBreak = true;
                    break;
                }
            }

            return shouldBreak;
        }

        static void Retry()
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

        public static void LoadData()
        {
            if (!File.Exists("saveData.json"))
            {
                mySave.canLoad = false;

                SaveData();
                return;
            }

            using StreamReader reader = new("saveData.json");
            saveJson = reader.ReadToEnd();

            mySave = JsonSerializer.Deserialize<SaveData>(saveJson, options);
        }

        public static void SaveData()
        {
            mySave.saveDataVersion = saveDataVersion;

            string serializedData = JsonSerializer.Serialize(mySave, options);
            saveJson = serializedData;

            using StreamWriter writer = new("saveData.json"); // "using" will de-allocate resources when not in use
            writer.Write(serializedData);
            writer.Flush();
        }

        static void ResetSave()
        {
            mySave.level = 0;
            mySave.shopPoints = 0;
            mySave.canLoad = true;

            SaveData();
        }

        public static void PrintSave()
        {
            Reader.Write("-- Save Data --\n", color: ConsoleColor.Gray);
            Thread.Sleep(250);
            Reader.Write($"Level: {mySave.level.ToString()}\n", 55, ConsoleColor.DarkGreen);
            Thread.Sleep(400);
            Reader.Write("Derek Dollars: " + mySave.shopPoints.ToString(), 55, ConsoleColor.DarkGreen);
        }
    }
}
