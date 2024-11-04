using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Text_Adventure
{
    public class Reader
    {
        // ReadTimed Stuff

        const int STD_INPUT_HANDLE = -10;

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CancelIoEx(IntPtr handle, IntPtr lpOverlapped);

        // Choice

        public static string GetNameOfT<T>(T thing)
        {
            string name = "";

           /* if (thing is Entity)
                name = (thing as Entity).name;
            else if (thing is Move)
                name = (thing as Move).name;
            else if (thing is Item)
                name = (thing as Item).name;
            else*/ if (thing is string)
                name = (string)(object)thing;

            return name;
        }

        public static T ChooseSomething<T>(List<T> choices, bool printClass = false, bool canExit = false)
        {
            foreach (T action in choices)
            {
                if (printClass == true)
                    Console.WriteLine(action);
                else
                    Console.WriteLine(GetNameOfT<T>(action));
            }

            if (canExit == true)
                Console.WriteLine("Exit");

            Write("Input: ", color: ConsoleColor.DarkGray);

            while (true)
            {
                string choice = ReadLine();

                if (canExit && choice.ToLower().Contains("exit"))
                    return default;

                foreach (T action in choices)
                {
                    string name = GetNameOfT<T>(action);

                    if (name.ToLower().Contains(choice.ToLower()))
                    {
                        WriteLine("");
                        return action;
                    }
                }

                WriteLine("Not a valid choice. Please pick again.");
                Write("Input: ", color: ConsoleColor.DarkGray);
            }
        }

        // Write
        public static void WriteLine(string line, int typeMS = -1, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            if (typeMS == -1)
                Console.WriteLine(line);
            else
            {
                Console.WriteLine();

                foreach (char letter in line.ToCharArray())
                {
                    Console.Write(letter);
                    Thread.Sleep(typeMS); // MILLISECONDS
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Write(string line, int typeMS = -1, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            if (typeMS == -1)
                Console.Write(line);
            else
            {
                foreach (char letter in line.ToCharArray())
                {
                    Console.Write(letter);
                    Thread.Sleep(typeMS); // MILLISECONDS
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        // Read

        public static void CancelRead()
        {
            var handle = GetStdHandle(STD_INPUT_HANDLE);
            CancelIoEx(handle, IntPtr.Zero);
        }

        public static ConsoleKeyInfo ReadTimed(int timeOut = Timeout.Infinite)
        {
            bool read = false;

            Task.Delay(timeOut).ContinueWith(_ =>
            {
                if (!read) // Timeout => cancel the console read
                    CancelRead();
            });

            try
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                read = true;
                return key;
            }
            catch
            {
                return new ConsoleKeyInfo();
            }
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
