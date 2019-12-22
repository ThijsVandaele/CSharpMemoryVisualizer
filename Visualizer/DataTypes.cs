using System.Collections.Generic;
using System.Linq;

namespace Memory
{
    public class DataTypes
    {
        public static readonly string valueTypes = "valueTypes";
        public static readonly string arrayTypes = "arrayTypes";
        public static readonly string customTypes = "customTypes";

        public static IDictionary<string, IList<DataType>> Types => new Dictionary<string, IList<DataType>>
        {
            {
                valueTypes,
                new List<DataType>
                {
                    Int,
                    String,
                    Bool
                }
            },
            {
                arrayTypes,
                new List<DataType>
                {
                    IntArray,
                    StringArray,
                    BoolArray
                }
            },
            {
                customTypes,
                CustomTypes
            }
        };

        public static IList<DataType> CustomTypes = new List<DataType>();

        public static RefType GetCustomType(string name)
        {
            if (!Types[customTypes].Any(x => x.Name == name))
            {
                return null;
            }

           var type = (RefType)Types[customTypes].FirstOrDefault(x => x.Name == name);
            return type;
        }

        public static ValueType Int { get; } = new ValueType(DataTypesR.Int, "0");
        public static ValueType String { get; } = new ValueType(DataTypesR.String, "");
        public static ValueType Bool { get; } = new ValueType(DataTypesR.Bool, "0");
        public static RefType IntArray { get; } = new RefType(DataTypesR.IntArray);
        public static RefType StringArray { get; } = new RefType(DataTypesR.StringArray);
        public static RefType BoolArray { get; } = new RefType(DataTypesR.BoolArray);
    }
}
