namespace Memory
{
    public class MemoryAddress
    {
        public MemoryAddress(int addressIndex)
        {
            AddressIndex = addressIndex;
        }

        public Variable Variable { get; set; }
        public int AddressIndex { get;}
    }
}
