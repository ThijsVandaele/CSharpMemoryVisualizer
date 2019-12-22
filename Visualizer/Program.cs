using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Memory
{
    class Program
    {
        static Computer computer = new Computer(20);

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.Clear();
            
            int choice = 0;
            string choiceString = string.Empty;
            string name = string.Empty;
            string value = string.Empty;
            int lengte = 0;

            while ((choice = ConsoleAid.GetChoice("Maak een keuze", MainMenu, "Stop", Draw)) != 0)
            {
                Console.Clear();

                switch (choice)
                {
                    case 1: // ValueType
                        choiceString = ConsoleAid.GetChoicePossibility("Welk dataType wil je aanmaken",
                            DataTypes.Types[DataTypes.valueTypes].Select(x => x.Name).ToArray(),
                            "Terug Naar Main Menu", out choice, Draw);

                        switch (choice)
                        {
                            case 1: // int
                                Console.WriteLine("Hoe wil je je int variable noemen?");
                                name = Console.ReadLine();
                                value = ConsoleAid.ReadInteger($"Geef een integere waarde voor {name}.", "Dit is geen integer waarde.").ToString();

                                computer.InitializeValueVariable(DataTypes.Int, name, value);
                                break;

                            case 2: // string
                                Console.WriteLine("Hoe wil je je string variable noemen?");
                                name = Console.ReadLine();
                                Console.WriteLine($"Geef een string waarde voor {name}.");
                                value = Console.ReadLine();

                                computer.InitializeValueVariable(DataTypes.String, name, value);
                                break;

                            case 3: // bool
                                Console.WriteLine("Hoe wil je je bool variable noemen?");
                                name = Console.ReadLine();
                                value = ConsoleAid.ReadInteger($"Geef een bool waarde voor {name}. kies tussen 0 of 1", "Dit is geen bool waarde", 0, 1).ToString();

                                computer.InitializeValueVariable(DataTypes.Bool, name, value);
                                break;

                        }
                        break;

                    case 2: // RefType
                        choiceString = ConsoleAid.GetChoicePossibility("Welk dataType wil je aanmaken",
                            computer.Types.Where(x => x.GetType() == typeof(RefType)).Select(x => x.Name).ToArray(),
                            "Terug Naar Main Menu", out choice, Draw);

                        if(choice == 0)//back to main menu
                        {
                            break;
                        }
                        
                        if(DataTypes.Types[DataTypes.arrayTypes].Any(x => x.Name == choiceString)) // array type
                        {
                            Console.WriteLine($"Hoe wil je je {choiceString} variable noemen?");
                            name = Console.ReadLine();
                            lengte = ConsoleAid.ReadInteger($"Geef een de lengte voor {name}, kies tussen 1 en 5.", "Dit is geen integer waarde.", 1, 5);

                            computer.InitializeArrayVariable(computer.Types.First(x => x.Name == choiceString), name, lengte);
                        }
                        else // custom type
                        {
                            var type = (RefType)DataTypes.CustomTypes.FirstOrDefault(x => x.Name == choiceString);

                            Console.WriteLine($"Hoe wil je je {choiceString} variable noemen?");
                            name = Console.ReadLine();

                            computer.InitializeCustumTypeVariable(type, name);
                        }
                        break;

                    case 3:
                        Console.WriteLine("Hoe heet je nieuw custom data type?");
                        name = Console.ReadLine();
                        var properties = new List<Property>();
                        var maxProperties = 5;
                        var propertyCounter = 1;

                        do
                        {
                            Console.WriteLine($"Property {propertyCounter++}/{maxProperties}.");

                            choiceString = ConsoleAid.GetChoicePossibility("Van welk dataType is je property?",
                            computer.Types.Where(x => x.GetType() == typeof(ValueType)).Select(x => x.Name).ToArray(),
                            "Stop met properties aanmaken", out choice, Draw);

                            if(choice == 0)
                            {
                                break;
                            }

                            Console.WriteLine("Hoe heet je property?");
                            var propertyName = Console.ReadLine();

                            properties.Add(new Property
                            {
                                Name = propertyName,
                                Type = computer.Types.First(x => x.GetType() == typeof(ValueType) && x.Name == choiceString)
                            });

                            Console.Clear();
                        }
                        while (propertyCounter <= maxProperties);

                        computer.CreateCustomType(name, properties);
                        break;
                }

                Console.Clear();
            }            
        }
        
        public static void Draw()
        {
            computer.DrawRam();
            computer.DrawSourceCode();
        }

        static readonly string[] MainMenu =
            {
                "Initialiseer een Value Type",
                "Initialiseer een Reference Type",
                "Maak een nieuw Custom reference Type"
            };
    }
}
