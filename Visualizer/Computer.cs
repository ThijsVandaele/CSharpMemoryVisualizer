using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Memory
{
    public class Computer
    {
        public Computer(int amountMemoryAddresses)
        {
            if (amountMemoryAddresses < 1)
            {
                throw new InvalidOperationException("amount of memory addresses should be more than 1");
            }

            NextAvailableStack = 0;
            NextAvailableHeap = (amountMemoryAddresses / 2) - 1;

            Ram = new Ram(amountMemoryAddresses);

            foreach(var key in DataTypes.Types)
            {
                foreach(var value in key.Value)
                {
                    Types.Add(value);
                }
            }
            Console.WriteLine();
        }

        private Ram Ram { get; set; }

        public IList<Variable> Variables { get; set; } = new List<Variable>();
        public IList<DataType> Types { get; set; } = new List<DataType>();
        private IList<string> SourceCode { get; set; } = new List<string>();

        private int NextAvailableStack { get; set; }
        private int NextAvailableHeap { get; set; }

        public void DeclareValueVariable(DataType dataType, string name)
        {
            if(!(dataType is ValueType type))
            {
                throw new ArgumentException($"{nameof(dataType)} needs to be a value type");
            }

            if(!Types.Any(x => x.Name == type.Name))
            {
                throw new InvalidOperationException($"{dataType.Name} is not in the list of available types.");
            }

            if(Variables.Any(x => x.Name == name))
            {
                throw new InvalidOperationException($"Variable with name {name} already exists");
            }

            var variable = new Variable
            {
                Name = name,
                Type = dataType,
                Value = dataType.Defaultvalue
            };

            Ram.SetVariable(NextAvailableStack, variable);

            Variables.Add(variable);

            NextAvailableStack++;
        }

        public void InitializeValueVariable(DataType dataType, string name, string value)
        {
            if (!(dataType is ValueType type))
            {
                throw new ArgumentException($"{nameof(dataType)} needs to be a value type");
            }

            if (!Types.Any(x => x.Name == type.Name))
            {
                throw new InvalidOperationException($"{dataType.Name} is not in the list of available types.");
            }

            if (Variables.Any(x => x.Name == name))
            {
                throw new InvalidOperationException($"Variable with name {name} already exists");
            }

            var variable = new Variable
            {
                Name = name,
                Type = dataType,
                Value = value == string.Empty ? dataType.Defaultvalue : value
            };

            Ram.SetVariable(NextAvailableStack, variable);

            Variables.Add(variable);

            NextAvailableStack++;

            SourceCode.Add($"{dataType.Name} {name} = {value};");
        }

        public void InitializeArrayVariable(DataType dataType, string name, int length)
        {
            if (!(dataType is RefType type))
            {
                throw new ArgumentException($"{nameof(dataType)} needs to be a reference type");
            }
            
            if (!DataTypes.Types[DataTypes.arrayTypes].Any(x => x.Name == dataType.Name))
            {
                throw new InvalidOperationException($"{dataType.Name} is in an array type.");
            }

            if (!Types.Any(x => x.Name == type.Name))
            {
                throw new InvalidOperationException($"{dataType.Name} is not in the list of available types.");
            }

            if (Variables.Any(x => x.Name == name))
            {
                throw new InvalidOperationException($"Variable with name {name} already exists");
            }

            var variable = new Variable
            {
                Name = name,
                Type = dataType,
                Value = NextAvailableHeap.ToString()
            };

            var valueType = GetValueTypeFromArray(dataType.Name);

            for(int i = 0; i < length; i++)
            {
                var variableOnHeap = new Variable
                {
                    Name = $"{name}[{i}]",
                    Type = valueType,
                    Value = valueType.Defaultvalue
                };

                variable.Variables.Add(variableOnHeap);

                Ram.SetVariable(NextAvailableHeap, variableOnHeap);
                NextAvailableHeap++;
            }

            Ram.SetVariable(NextAvailableStack, variable);

            Variables.Add(variable);

            NextAvailableStack++;

            SourceCode.Add($"{dataType.Name} {name} = new {valueType.Name}[{length}];");
        }

        public void SetValueOfVariable(Variable variable, string value)
        {
            variable.Value = value;
        }

        public void CreateCustomType(string name, List<Property> properties)
        {
            if (Variables.Any(x => x.Name == name))
            {
                throw new InvalidOperationException($"Variable with name {name} already exists");
            }

            var newCustomType = new RefType(name)
            {
                Properties = properties
            };

            DataTypes.Types[DataTypes.customTypes].Add(newCustomType);
            Types.Add(newCustomType);
        }

        public void InitializeCustumTypeVariable(RefType customType, string name)
        {

            if (!(customType is RefType type))
            {
                throw new ArgumentException($"{nameof(customType)} needs to be a reference type");
            }

            if (!DataTypes.Types[DataTypes.customTypes].Any(x => x.Name == customType.Name))
            {
                throw new InvalidOperationException($"{customType.Name} is in an array type.");
            }

            if (!Types.Any(x => x.Name == type.Name))
            {
                throw new InvalidOperationException($"{customType.Name} is not in the list of available types.");
            }

            if (Variables.Any(x => x.Name == name))
            {
                throw new InvalidOperationException($"Variable with name {name} already exists");
            }

            var variable = new Variable
            {
                Name = name,
                Type = customType,
                Value = NextAvailableHeap.ToString()
            };
            
            foreach(var property in customType.Properties)
            {
                var variableOnHeap = new Variable
                {
                    Name = $"{name}.{property.Name}",
                    Type = property.Type,
                    Value = property.Type.Defaultvalue
                };

                variable.Variables.Add(variableOnHeap);

                Ram.SetVariable(NextAvailableHeap, variableOnHeap);
                NextAvailableHeap++;
            }

            Ram.SetVariable(NextAvailableStack, variable);

            Variables.Add(variable);

            NextAvailableStack++;

            SourceCode.Add($"{customType.Name} {name} = new {customType.Name}();");
        }
        
        public void DrawRam()
        {
            Ram.Draw();
        }

        private const int colStart = 50;
        private const int rowStart = 0;

        public void DrawSourceCode()
        {
            Console.CursorTop = rowStart;
            Console.BackgroundColor = ConsoleColor.Gray;

            foreach (var statement in SourceCode)
            {
                Console.CursorLeft = colStart;
                ConsoleAid.PrintLineText(50, statement);
            }

            Console.BackgroundColor = ConsoleColor.White;
        }

        private ValueType GetValueTypeFromArray(string type)
        {
            switch (type)
            {
                case "int[]":
                    return DataTypes.Int;

                case "string[]":
                    return DataTypes.String;

                case "bool[]":
                    return DataTypes.Bool;

                default:
                    throw new InvalidOperationException($"{type} not found. while retrieving the value type from an array type");
            }
        }
    }
}
