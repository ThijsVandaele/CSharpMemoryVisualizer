using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Visualizer
{
    class Program
    {
        static readonly Computer computer = new Computer(20);

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

            while ((choice = ConsoleAid.GetChoice("Maak een keuze", MainMenu.ToArray(), "Stop", Draw)) != 0)
            {
                Console.Clear();
                if(computer.LastVariableChanged != null)
                {
                    computer.LastVariableChanged.LastChanged = false;
                    computer.LastVariableChanged = null;
                }

                try
                {
                    switch (choice)
                    {
                        case 1: // ValueType
                            choiceString = ConsoleAid.GetChoicePossibility("Welk dataType wil je aanmaken",
                                DataTypes.Types[DataTypes.valueTypes].Select(x => x.Name).ToArray(),
                                "Terug Naar Main Menu", out choice, Draw);

                            switch (choice)
                            {
                                case 1: // int
                                    name = ConsoleAid.ReadText("Hoe wil je je int variable noemen ?");
                                    value = ConsoleAid.ReadInteger($"Geef een integere waarde voor {name}.", "Dit is geen integer waarde.").ToString();

                                    computer.InitializeValueVariable(DataTypes.Int, name, value);
                                    break;

                                case 2: // string
                                    name = ConsoleAid.ReadText("Hoe wil je je string variable noemen?");
                                    value = ConsoleAid.ReadText($"Geef een string waarde voor {name}.", true, true, true);

                                    computer.InitializeValueVariable(DataTypes.String, name, value);
                                    break;

                                case 3: // bool
                                    name = ConsoleAid.ReadText("Hoe wil je je bool variable noemen?");
                                    value = ConsoleAid.ReadInteger($"Geef een bool waarde voor {name}. kies tussen 0 of 1", "Dit is geen bool waarde", 0, 1).ToString();

                                    computer.InitializeValueVariable(DataTypes.Bool, name, value);
                                    break;

                            }
                            break;

                        case 2: // RefType
                            choiceString = ConsoleAid.GetChoicePossibility("Welk dataType wil je aanmaken",
                                computer.Types.Where(x => x.GetType() == typeof(RefType)).Select(x => x.Name).ToArray(),
                                "Terug Naar Main Menu", out choice, Draw);

                            if (choice == 0)//back to main menu
                            {
                                break;
                            }

                            if (DataTypes.Types[DataTypes.arrayTypes].Any(x => x.Name == choiceString)) // array type
                            {
                                name = ConsoleAid.ReadText($"Hoe wil je je {choiceString} variable noemen?");
                                lengte = ConsoleAid.ReadInteger($"Geef een de lengte voor {name}, kies tussen 1 en 5.", "Dit is geen integer waarde.", 1, 5);

                                computer.InitializeArrayVariable(computer.Types.First(x => x.Name == choiceString), name, lengte);
                            }
                            else // custom type
                            {
                                var type = (RefType)DataTypes.CustomTypes.FirstOrDefault(x => x.Name == choiceString);

                                name = ConsoleAid.ReadText($"Hoe wil je je {choiceString} variable noemen?");

                                computer.InitializeCustumTypeVariable(type, name);
                            }
                            break;

                        case 3:
                            name = ConsoleAid.ReadText("Hoe heet je nieuw custom data type?");
                            var properties = new List<Property>();
                            var maxProperties = 5;
                            var propertyCounter = 1;

                            do
                            {
                                Console.WriteLine($"Property {propertyCounter++}/{maxProperties}.");

                                choiceString = ConsoleAid.GetChoicePossibility("Van welk dataType is je property?",
                                computer.Types.Where(x => x.GetType() == typeof(ValueType)).Select(x => x.Name).ToArray(),
                                "Stop met properties aanmaken", out choice, Draw);

                                if (choice == 0)
                                {
                                    break;
                                }

                                var propertyName = ConsoleAid.ReadText("Hoe heet je property?");

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

                        case 4:
                            if (!computer.Variables.Any())
                            {
                                throw new Exception("Nog geen variabelen aangemaakt");
                            }

                            var variableNames = computer.Variables.Select(x => x.Name).ToArray();
                            choiceString = ConsoleAid.GetChoicePossibility("Van welke variable wil je de waarde wijzigen",
                                variableNames, "Stop", out choice, Draw);

                            if(choice == 0)
                            {
                                break;
                            }
                            choice--;
                            var variable = computer.Variables[choice];

                            if(variable.Type is ValueType)
                            {
                                switch (variable.Type.Name)
                                {
                                    case "int": // int
                                        value = ConsoleAid.ReadInteger($"Geef een integere waarde voor {name}.", "Dit is geen integer waarde.").ToString();
                                        break;

                                    case "string": // string
                                        value = ConsoleAid.ReadText($"Geef een string waarde voor {name}.", true, true, true);
                                        break;

                                    case "bool": // bool
                                        value = ConsoleAid.ReadInteger($"Geef een bool waarde voor {name}. kies tussen 0 of 1", "Dit is geen bool waarde", 0, 1).ToString();
                                        break;

                                }
                            }
                            else if (variable.Type is RefType)
                            {
                                Console.Clear();
                                Console.WriteLine($"Je hebt gekozen om '{variable.Name}' te wijzigen.");

                                variableNames = variable.Variables.Select(x => x.Name).ToArray();
                                choiceString = ConsoleAid.GetChoicePossibility("Van welke variable wil je de waarde wijzigen",
                                    variableNames, "Stop", out choice, Draw);

                                if(choice == 0)
                                {
                                    break;
                                }

                                choice--;
                                variable = variable.Variables[choice];

                                switch (variable.Type.Name)
                                {
                                    case "int": // int
                                        value = ConsoleAid.ReadInteger($"Geef een integere waarde voor {name}.", "Dit is geen integer waarde.").ToString();
                                        break;

                                    case "string": // string
                                        value = ConsoleAid.ReadText($"Geef een string waarde voor {name}.", true, true, true);
                                        break;

                                    case "bool": // bool
                                        value = ConsoleAid.ReadInteger($"Geef een bool waarde voor {name}. kies tussen 0 of 1", "Dit is geen bool waarde", 0, 1).ToString();
                                        break;
                                }
                            }

                            computer.SetValueOfVariable(variable, value);

                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Druk op ENTER om verder te gaan.");
                    Console.ReadLine();
                }
                

                Console.Clear();
            }            
        }
        
        public static void Draw()
        {
            computer.DrawRam();
            computer.DrawSourceCode();
        }

        static readonly List<string> MainMenu = new List<string>
        {
            "Initialiseer een Value Type",
            "Initialiseer een Reference Type",
            "Maak een nieuw Custom reference Type",
            "Verander waarde van variable"
        };
    }
}
