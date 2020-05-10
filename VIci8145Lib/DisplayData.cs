using Vici8145Lib;

namespace VIc8145Lib
{
    public class DisplayData
    {
        public string Sign2Nd { get; set; }
        public string Unit { get; set; }
        public string MainDisplayValue { get; set; }
        public string SecondDisplayValue { get; set; }
        public int BarValue  { get; set; }
        public string Rel { get; set; }
        public string MinMax { get; set; }
        public string Hold { get; set; }
        public EntitiesEnum Entities { get; set; }
        public PrefixEnum Prefixis { get; set; }
        public string Sign { get; set; }
        public string Auto { get; set; }
        public string Unit1 { get; set; }
        public string Unit2 { get; set; }

        public byte[] RawData { get; set; }
        public string Select { get; set; }
        public bool ShowBar { get; set; }
    }
}
