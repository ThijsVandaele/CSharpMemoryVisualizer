using System.Collections.Generic;

namespace Visualizer
{
    public class Variable
    {
        public DataType Type { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public IList<Variable> Variables { get; set; } = new List<Variable>();
        public Variable Parent { get; set; }
        public bool LastChanged { get; set; }
    }
}
