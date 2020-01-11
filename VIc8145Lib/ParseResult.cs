using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vici8145Lib;

namespace VIc8145Lib
{
    class ParseResult
    {
        private static bool temperature = false;
        private static bool resistance;
        private static int adjustDecPos;

        public static string ParseUnits(byte[] toParse, DisplayData dispData)
        {
            temperature = false;
            resistance = false;
            adjustDecPos = 0;
            var mode = (toParse[1] & 0b0111_1000) >> 3;
            var postfix = "";

            dispData.Select = ParseSelect(toParse[1] & 3, mode, dispData);
            return $"";

            switch (mode)
            {
                case 0xE:
                case 0xF:
                    return "V";

                case 0xD:
                    adjustDecPos = 1;
                    return "mV";
                case 0x6:
                    return "mA";
                case 0x7:
                    return "A";
                case 0xC:
                    resistance = true;
                    return "Ω";
                case 0xB:
                    return "Diode";
                case 0x8:
                    dispData.Unit1 = "°C";
                    dispData.Unit2 = "°F";
                    temperature = true;
                    return "temp";
                case 0x9:
                    return "Cap";
                case 0xA:
                    return "Hz";
                case 0x4:
                    return "Generator";
                default:
                    return "Unknown";
            }
        }

        private static string ParseSelect(int select, int mode, DisplayData d)
        {
            d.Unit2 = "";
            switch (select)
            {
                case 00:
                    switch (mode)
                    {
                        case 0xE:
                            d.Unit = "V";
                            return "DC";
                        case 0xF:
                            d.Unit = "V";
                            return "AC";
                        case 0xD:
                            d.Unit = "mV";
                            return "DC";
                        case 0x6:
                            d.Unit = "mA";
                            return "DC";
                        case 0x5:
                            d.Unit = "A";
                            return "DC";
                        case 0xC:
                            d.Unit = "Ω";
                            return "";
                        case 0x9:
                            d.Unit = "Cap";
                            return "F";
                        case 0x8:
                            d.Unit = "°C";
                            d.Unit1 = "°F";
                            temperature = true;
                            return "Temp";
                        case 0xA:
                            d.Unit = "Hz";
                            d.Unit1 = "";
                            return "";

                    }

                    break;
                case 1:
                    switch (mode)
                    {
                        case 0xE:
                            d.Unit= "V";
                            d.Unit1 = "Hz";
                            return "AC+DC";
                        case 0xF:
                            d.Unit = "V";
                            d.Unit1 = "Hz";
                            return "AC";
                        case 0xD:
                            d.Unit = "mV";
                            d.Unit1 = "Hz";
                            return "AC";
                        case 0x6:
                            d.Unit = "mA";
                            return "AC";
                        case 0x5:
                            d.Unit = "A";
                            return "AC";
                        case 0xC:
                            d.Unit = ">>>";
                            return "";
                        case 0x8:
                            d.Unit = "°C";
                            d.Unit1 = "°F";
                            temperature = true;
                            return "Temp Ext";
                        case 0xA:
                            d.Unit = "MHz";
                            d.Unit1 = "";
                            return "Hi";
                    }

                    break;
                case 2:
                    switch (mode)
                    {
                        case 0xE:
                            d.Unit1 = "Hz";
                            return "dBm";
                        case 0xf:
                            d.Unit1 = "Hz";
                            d.Unit = "dBm";
                            return "AC";
                        case 0xD:
                            d.Unit1 = "Hz";
                            d.Unit = "dBm";
                            return "";
                        case 0x6:
                            d.Unit = "mA";
                            d.Unit1 = "mA AC";
                            return "AC+DC";
                        case 0x5:
                            d.Unit = "A";
                            d.Unit1 = "A AC";
                            return "AC+DC";
                        case 0xC:
                            d.Unit = "Hi";
                            return "";
                        case 0xA:
                            d.Unit = "RPM";
                            d.Unit1 = "";
                            return "";

                    }

                    break;
                case 3:
                    switch (mode)
                    {

                        case 0x6:
                            d.Unit1 = "Hz";
                            d.Unit = "mA";
                            return "AC";

                        case 0x5:
                            d.Unit1 = "Hz";
                            d.Unit = "A";
                            return "AC";
                    }

                    break;
            }

            return "";
        }


        public static DisplayData ParseMainDisplayData(DisplayData displayData, byte[] data)
        {
            //displayData.Unit = 
                ParseUnits(data, displayData);
            displayData.Prefixis = ParsePrefix(data);
            displayData.Hold = ParseHold(data);
            displayData.Rel = ParseRel(data);
            displayData.Sign = ParserSign(data);
            displayData.MainDisplayValue = ParseMainDisplayValue(data);
            displayData.MinMax = ParseMinMax(data);
            displayData.Auto = IsAutoRange(data[2]);
            return displayData;
        }

        private static string ParseMinMax(byte[] data)
        {
            switch ((data[3] & 0b0111_1000) >> 3)
            {
                default:
                    return "";
                case 8:
                    return "Max";
                case 9:
                    return "Min";
                case 0xA:
                    return "Max Min";
                case 0x0b:
                    return "Avg";
            }
        }

        private static bool ParseRel(byte[] data)
        {
            return (data[3] & 0b0000_0010) != 0;
        }

        private static string ParseMainDisplayValue(byte[] data)
        {
            int decpos = GetRange(data) + adjustDecPos;
            var res = string.Empty;
            for (int i = 5; i < 10; i++)
            {
                switch (data[i])
                {
                    case 0x3f:
                        res += " ";
                        break;
                    case 0x3E:
                        res += "L";
                        break;
                    default:
                        res += (data[i] - 0x30).ToString();
                        break;
                }
                if (res.Length == decpos)
                {
                    res += ".";
                }
            }
            return res;
        }

        private static int GetRange(byte[] data)
        {
            int range = (data[2] & 0b0011_1000) >> 3;
            if (temperature)
            {
                range++;                // Correct if temp. measurement
            }

            if (resistance)
            {
                switch (data[1] & 3)
                {
                    case 0:
                    case 1:
                        if (((data[2] & 0x38) >> 3) == 0)
                        {
                            range = 3;
                        }
                        else if (((data[2] & 0x38) >> 3) < 4)
                        {

                        }
                        else
                        {
                            range = 1;
                        }
                        break;

                    case 2:
                        range = 4;
                        break;
                }

                return range;
            }
            return range + 1;
        }

        private static string ParserSign(byte[] result)
        {
            switch ((result[4] & 0b01110000) >> 4)
            {
                default:
                    return " ";
                case 4:
                    return " ";
                case 5:
                    return "-";
            }
        }

        public static PrefixEnum ParsePrefix(byte[] result)
        {
            switch (result[1] & 3)
            {
                case 0:
                case 1:
                    if (((result[2] & 0x38) >> 3) == 0)
                    {
                        return PrefixEnum.None;
                    }
                    else if (((result[2] & 0x38) >> 3) < 4)
                    {
                        return PrefixEnum.Kilo;
                    }
                    else
                    {

                        return PrefixEnum.Mega;
                    }
                    break;

                case 2:

                    return PrefixEnum.Mega;
                    break;

                default:
                    return PrefixEnum.None;
            }
        }

        private static string ParseHold(byte[] result)
        {

            switch (result[4] & 0x3)
            {
                default:
                    return "";
                case 1:
                    return "A-H";
                case 2:
                    return "PH%";
                case 3:
                    return "PH-";
            }
        }

        public static DisplayData ParseSecondDisplayData(DisplayData displayData, byte[] data)
        {
            int decpos = GetRange(data);
            if (temperature)
            {
                decpos++;
            }


            var res = string.Empty;
            for (int i = 5; i < 10; i++)
            {
                switch (data[i])
                {
                    case 0x3f:
                        break;
                    case 0x3E:
                        res += "L";
                        break;
                    default:
                        res += (data[i] - 0x30).ToString();
                        break;
                }

                if (res.Length == decpos)
                {
                    res += ".";
                }
            }

            if (res == ".")
                res = "";

            displayData.SecondDisplayValue = res;
            return displayData;
        }

        public static string IsAutoRange(byte b)
        {
            var res = b & 0x40;

            return res > 0 ? "AUTO" : "";
        }

        public static DisplayData ParseBarDisplayData(DisplayData displayData, byte[] result)
        {
            displayData.BarValue = result[6] - 0x7f;
            return displayData;
        }
    }
}
