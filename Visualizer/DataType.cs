namespace Visualizer
{
    public abstract class DataType
    {
        public string Name { get; private set; }

        public string Defaultvalue { get; private set; }

        public DataType(string name, string defaultValue)
        {
            Name = name;
            Defaultvalue = defaultValue;
        }

        public override bool Equals(object obj)
        {
            DataType other = (DataType)obj;

            return Name == other.Name;
        }
    }
}
