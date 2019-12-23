using System;
using System.Text.RegularExpressions;

namespace Core
{
    public static class ConsoleAid
    {
        //MENU
        public static byte GetChoice(string question, string[] possibilities, string stopString, Action draw = null)
        {
            if (draw == null)
            {
                draw = () => { };
            }

            byte choice = 255;

            Console.WriteLine(question);
            PrintLine(question.Length, '-');
            Console.WriteLine();

            for (int i = 0; i < possibilities.GetLength(0); i++)
            {
                Console.WriteLine("{0}) {1}", i + 1, possibilities[i]);
            }

            Console.WriteLine(Environment.NewLine + "{0}) {1}", 0, stopString);

            var cursorTop = Console.CursorTop;
            draw();
            Console.CursorTop = cursorTop;

            choice = ReadKeyByte(0, (byte)possibilities.GetLength(0));

            return choice;
        }

        public static string GetChoicePossibility(string question, string[] possibilities, string stopString, out int choiceInt, Action draw = null)
        {
            if (draw == null)
            {
                draw = () => { };
            }

            Console.WriteLine(question);
            PrintLine(question.Length, '-');
            Console.WriteLine();

            for (int i = 0; i < possibilities.GetLength(0); i++)
            {
                Console.WriteLine("{0}) {1}", i + 1, possibilities[i]);
            }

            Console.WriteLine(Environment.NewLine + "{0}) {1}", 0, stopString);

            var cursorTop = Console.CursorTop;
            draw();
            Console.CursorTop = cursorTop;

            var choice = ReadKeyByte(0, (byte)possibilities.GetLength(0));
            choiceInt = choice;

            return possibilities[choice != 0 ? choice - 1 : choice];
        }

        internal static string ReadText(string title, bool emptyAllowed = false, 
            bool spacesAllowed = false, bool firstCharNumberAllowed = false)
        {
            Console.WriteLine(title);
            var text = Console.ReadLine();
            var feedback = GetFeedbackOfReadText(text, emptyAllowed, spacesAllowed, firstCharNumberAllowed);

            while (!string.IsNullOrEmpty(feedback))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(feedback);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(title);
                text = Console.ReadLine();

                feedback = GetFeedbackOfReadText(text, emptyAllowed, spacesAllowed, firstCharNumberAllowed);
            }

            return text;
        }

        public static string GetFeedbackOfReadText(string text, bool emptyAllowed,
            bool spacesAllowed, bool firstCharNumberAllowed)
        {
            var feedback = string.Empty;
            feedback += $"{(string.IsNullOrEmpty(text) && !emptyAllowed ? "mag niet leeg zijn, " : "")}";
            feedback += $"{(HasStringSpaces(text) && !spacesAllowed ? "mag geen spaties hebben, " : "")}";
            feedback += $"{(StartStringWithNumber(text) && !firstCharNumberAllowed ? "mag niet starten met een getal, " : "")}";

            return feedback;
        }

        public static bool HasStringSpaces(string line)
        {
            return line.Contains(' ');
        }

        public static bool StartStringWithNumber(string line)
        {
            return Regex.IsMatch(line, @"^\d");
        }

        public static void PrintTitle(string title)
        {
            Console.WriteLine(title);
            PrintLine(title.Length, '-');
        }

        // DECIMAL
        public static decimal ReadDecimal(string title, string errorString)
        {
            decimal getal;

            Console.Write(title);

            while (!decimal.TryParse(Console.ReadLine(), out getal))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return getal;
        }

        //INTEGER
        private static bool CheckAllowedValuesInteger(int[] values, int value)
        {
            foreach (var number in values)
            {
                if (number == value)
                {
                    return true;
                }
            }

            return false;
        }
        public static int ReadInteger(string title, string errorString, int[] values)
        {
            int number;

            Console.Write(title);

            while (!int.TryParse(Console.ReadLine(), out number) || !CheckAllowedValuesInteger(values, number))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return number;
        }
        public static int ReadInteger(string title, string errorString, int minValue, int maxValue)
        {
            int number;

            Console.Write(title);

            while (!(int.TryParse(Console.ReadLine(), out number)) || (number < minValue || number > maxValue))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return number;
        }
        public static int ReadInteger(string title, string errorString)
        {
            int getal;

            Console.Write(title);

            while (!(int.TryParse(Console.ReadLine(), out getal)))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return getal;
        }
        public static int ReadKeyInteger(int minValue, int maxValue)
        {
            ConsoleKeyInfo key;
            int number;

            do
            {
                key = Console.ReadKey(true);
                number = (int)(key.KeyChar - (char)48);
            }
            while (number < minValue || number > maxValue);


            return number;
        }

        //BYTE
        private static bool CheckAllowedValuesByte(byte[] values, byte value)
        {
            foreach (var number in values)
            {
                if (number == value)
                {
                    return true;
                }
            }

            return false;
        }
        public static byte ReadKeyByte(string title, byte[] values)
        {
            ConsoleKeyInfo key;
            byte number;

            Console.Write(title);

            do
            {
                key = Console.ReadKey(true);
                number = (byte)(key.KeyChar - (char)48);
            }
            while (!CheckAllowedValuesByte(values, number));


            return number;
        }
        public static byte ReadKeyByte(byte minValue, byte maxValue)
        {
            ConsoleKeyInfo key;
            byte number;

            do
            {
                key = Console.ReadKey(true);
                number = (byte)(key.KeyChar - (char)48);
            }
            while (number < minValue || number > maxValue);


            return number;
        }
        public static int ReadByte(string title, string errorString, byte[] values)
        {
            byte number;

            Console.Write(title);

            while (!(byte.TryParse(Console.ReadLine(), out number)) || !CheckAllowedValuesByte(values, number))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return number;
        }
        public static byte ReadByte(string title, string errorString, byte minValue, byte maxValue)
        {
            byte number;

            Console.Write(title);

            while (!(byte.TryParse(Console.ReadLine(), out number)) || (number < minValue || number > maxValue))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return number;
        }
        public static byte ReadByte(string title, string errorString)
        {
            byte getal;

            Console.Write(title);

            while (!(byte.TryParse(Console.ReadLine(), out getal)))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return getal;
        }
        public static byte ReadByte(string title)
        {
            byte getal;

            Console.WriteLine(title);

            while (!byte.TryParse(Console.ReadLine(), out getal)) ;

            return getal;
        }

        //FLOAT
        private static bool CheckAllowedValuesFloat(float[] values, float value)
        {
            foreach (var number in values)
            {
                if (number == value)
                {
                    return true;
                }
            }

            return false;
        }
        public static float ReadFloat(string title, string errorString, float[] values)
        {
            float number;

            Console.Write(title);

            while (!(float.TryParse(Console.ReadLine(), out number)) || !CheckAllowedValuesFloat(values, number))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return number;
        }
        public static float ReadFloat(string title, string errorString, float minValue, float maxValue)
        {
            float number;

            Console.Write(title);

            while (!(float.TryParse(Console.ReadLine(), out number)) || (number < minValue || number > maxValue))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return number;
        }
        public static float ReadFloat(string title, string errorString)
        {
            float getal;

            Console.Write(title);

            while (!(float.TryParse(Console.ReadLine(), out getal)))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return getal;
        }
        public static float ReadFloat(float numberIfFail)
        {
            float getal;

            while (!(float.TryParse(Console.ReadLine(), out getal)))
            {
                getal = numberIfFail;
                break;
            }

            return getal;
        }

        //CHAR
        private static bool CheckAllowedValuesChar(char[] values, char value)
        {
            foreach (var number in values)
            {
                if (number == value)
                {
                    return true;
                }
            }

            return false;
        }
        public static char ReadChar(string title, string errorString, char[] values)
        {
            char character;

            Console.Write(title);

            while (!(char.TryParse(Console.ReadLine(), out character)) || !CheckAllowedValuesChar(values, character))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return character;
        }
        public static char ReadKeyChar(string title, string errorString, char[] values)
        {
            ConsoleKeyInfo key;
            char character;

            Console.Write(title);

            do
            {
                key = Console.ReadKey(true);
                character = key.KeyChar;
            }
            while (!CheckAllowedValuesChar(values, character));

            return character;
        }
        public static char ReadChar(string title, string errorString, char minValue, char maxValue)
        {
            char number;

            Console.Write(title);

            while (!(char.TryParse(Console.ReadLine(), out number)) || (number < minValue || number > maxValue))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return number;
        }
        public static char ReadChar(string title, string errorString)
        {
            char getal;

            Console.Write(title);

            while (!(char.TryParse(Console.ReadLine(), out getal)))
            {
                Console.WriteLine(errorString);
                Console.Write(title);
            }

            return getal;
        }
        public static void SetBackgroundColor(byte number)
        {
            switch (number)
            {
                case 1:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case 3:
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case 4:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case 5:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case 6:
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case 7:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case 8:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case 9:
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 10:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }

        //without Text
        public static void PrintRectangle(int row, int col, int spaceFromLeft, int startPlaceFromTop, char charachter = ' ')
        {
            for (int i = 0; i < row; i++)
            {
                Console.SetCursorPosition(spaceFromLeft, startPlaceFromTop++);

                PrintLine(col, ' ');
            }
        }

        public static void PrintLine(int length, char charachter = ' ')
        {
            Print(length, charachter);

            Console.WriteLine();
        }

        public static void Print(int length, char charachter = ' ')
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write(charachter);
            }
        }

        //with text
        public static void PrintRectangleText(int row, int col, int spaceFromLeft, int startPlaceFromTop, string text, char character = ' ')
        {
            for (int i = 0; i < row; i++)
            {
                Console.SetCursorPosition(spaceFromLeft, startPlaceFromTop++);

                if (i == row / 2)
                {
                    PrintLineText(col, text, character);
                }
                else
                {
                    PrintLine(col, character);
                }
            }
        }

        public static void PrintLineText(int length, string text, char charachter = ' ')
        {
            PrintText(length, text, charachter);

            Console.WriteLine();
        }

        public static void PrintText(int length, string text, char charachter = ' ')
        {
            int amountSpaces = (length / 2) - (text.Length / 2);

            //SpacesBegin
            for (int i = 0; i < amountSpaces; i++)
            {
                Console.Write(charachter);
            }

            //text
            Console.Write(text);

            //SpacesEnd
            amountSpaces = length - (text.Length + amountSpaces);
            for (int i = 0; i < amountSpaces; i++)
            {
                Console.Write(charachter);
            }
        }
    }
}
