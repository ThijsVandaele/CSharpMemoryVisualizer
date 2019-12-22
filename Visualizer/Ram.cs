using Core;
using System;

namespace Memory
{
    public class Ram
    {
        public Ram(int amountMemoryAddresses)
        {
            MemoryAddresses = new MemoryAddress[amountMemoryAddresses];

            for(int i = 0; i < amountMemoryAddresses; i++)
            {
                MemoryAddresses[i] = new MemoryAddress(i);
            }
        }
        
        private MemoryAddress[] MemoryAddresses { get; set; }

        public MemoryAddress GetMemoryAddress(int address)
        {
            MemoryAddress memoryAddress;

            try
            {
                memoryAddress = MemoryAddresses[address];
            }
            catch
            {
                throw new IndexOutOfRangeException($"Couldn't get the memory addres {address}");
            }

            return memoryAddress;
        }

        public void SetVariable(int addressIndex, Variable variable)
        {
            var memoryAddress = GetMemoryAddress(addressIndex);
            MemoryAddresses[addressIndex].Variable = variable;
        }

        private const int typeAndNameVarialbeLineLength = 40;
        private readonly int valueLineLength = 25 + addressIndexLineLength;
        private const int addressIndexLineLength = 5;
        private const int colStart = 90;
        private const int rowStart = 0;
        private const ConsoleColor lineColor = ConsoleColor.Black;

        public void Draw()
        {
            Console.CursorLeft = colStart;
            Console.CursorTop = rowStart;

            foreach (var address in MemoryAddresses)
            {
                var indexColor = address.AddressIndex >= (MemoryAddresses.Length / 2) - 1 ? ConsoleColor.DarkYellow : ConsoleColor.Blue;
                string variableTypeName = string.Empty;
                string variableValue = string.Empty;

                if (address.Variable != null)
                {
                    variableTypeName = $"({address.Variable.Type.Name}) {address.Variable.Name}";
                    variableValue = address.Variable.Type == DataTypes.String 
                        ? $"\"{address.Variable.Value}\"" 
                        : address.Variable.Value;
                    variableValue += $"{(address.Variable.Type is RefType ? " (ref)" : string.Empty)}";
                }

                Console.ForegroundColor = lineColor;
                ConsoleAid.Print(typeAndNameVarialbeLineLength);
                ConsoleAid.PrintLine(valueLineLength, '-');
                Console.CursorLeft = colStart;

                Console.ForegroundColor = ConsoleColor.Green;
                ConsoleAid.PrintText(typeAndNameVarialbeLineLength, variableTypeName);

                Console.ForegroundColor = lineColor;
                Console.Write("|");
                Console.ForegroundColor = indexColor;
                ConsoleAid.PrintText(addressIndexLineLength, address.AddressIndex.ToString());

                Console.ForegroundColor = lineColor;
                Console.Write("|");
                Console.ForegroundColor = ConsoleColor.Green;
                ConsoleAid.PrintText(valueLineLength - addressIndexLineLength - 3, variableValue);
                Console.ForegroundColor = lineColor;
                Console.WriteLine("|");
                Console.CursorLeft = colStart;
            }

            ConsoleAid.Print(typeAndNameVarialbeLineLength);
            ConsoleAid.PrintLine(valueLineLength, '-');
        }
    }
}
