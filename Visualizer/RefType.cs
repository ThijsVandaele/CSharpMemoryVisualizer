using System.Collections.Generic;

namespace Visualizer
{
    public class RefType : DataType
    {
        public RefType(string name, string defaultValue = "NULL") 
            : base(name, defaultValue)
        {
        }

        public IList<Property> Properties { get; set; } = new List<Property>();
    }
}
