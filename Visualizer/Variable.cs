using System.Collections.Generic;

namespace Memory
{
    public class Variable
    {
        public DataType Type { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public IList<Variable> Variables { get; set; } = new List<Variable>();
    }
}
