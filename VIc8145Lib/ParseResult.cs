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
        private static int adjustDecPos2;
        private static bool generator;
        private static bool isAC;
        private static bool isOhm;

        public static void ParseUnits(byte[] toParse, DisplayData dispData)
        {
            temperature = false;
            resistance = false;
            adjustDecPos = adjustDecPos2 = adjustDecPos2 = 0;
            var mode = (toParse[1] & 0b0111_1000) >> 3;
            var postfix = "";

            dispData.Select = ParseSelect(toParse[1] & 3, mode, dispData, toParse);
            if (dispData.Select == "AC")
            {
                isAC = true;
            }

            if (dispData.Entities == EntitiesEnum.Resistance)
            {
                isOhm = true;
            }

        }

        private static string ParseSelect(int select, int mode, DisplayData d, byte[] rawdata)
        {
            generator = false;
            resistance = false;
            isAC = false;
            d.ShowBar = true;
            d.Unit2 = "";
            switch (select)
            {
                case 00:
                    switch (mode)
                    {
                        case 0xE:
                            d.Unit1 = d.Unit = "V";
                            d.Entities = EntitiesEnum.Voltage;
                            return "DC";
                        case 0xF:
                            d.Unit1 = d.Unit = "V";
                            d.Entities = EntitiesEnum.Voltage;
                            return "AC";
                        case 0xD:
                            d.Unit1 = d.Unit = "mV";
                            d.Entities = EntitiesEnum.Voltage;
                            return "DC";
                        case 0x6:
                            d.Unit1 = d.Unit = "mA";
                            d.Entities = EntitiesEnum.Current;
                            return "DC";
                        case 0x5:
                            d.Unit = "A";
                            d.Entities = EntitiesEnum.Current;
                            return "DC";
                        case 0xC:
                            d.Unit1 = d.Unit = "Ω";
                            d.Unit2 = "Ω";
                            resistance = true;
                            d.Entities = EntitiesEnum.Resistance;
                            return "";
                        case 0x9:
                            if (rawdata[2] != 0x98)
                            {
                                d.Unit1 = d.Unit = "nF";
                            }
                            else
                            {
                                d.Unit1 = d.Unit = "µF";
                            }
                            d.ShowBar = false;
                            d.Entities = EntitiesEnum.Capacity;

                            return "Capacitor";
                        case 0x8:
                            d.Unit = "°C";
                            d.Unit1 = "°F";
                            d.Entities = EntitiesEnum.Temp;
                            temperature = true;
                            d.ShowBar = false;
                            adjustDecPos = adjustDecPos2 = 1;
                            return "Temp";
                        case 0xA:
                            d.Entities = EntitiesEnum.Frequency;
                            d.Unit = "Hz";
                            d.Unit1 = "% duty";
                            d.ShowBar = false;
                            return "";
                        case 0x4:
                            //adjustDecPos =adjustDecPos2= 1;
                            d.Unit = "Hz";
                            d.Unit1 = "Duty";
                            generator = true;
                            d.Entities = EntitiesEnum.Generator;
                            return "Generator";
                        case 0xb:
                            d.Unit = "V";
                            d.Unit1 = "";
                            d.Entities = EntitiesEnum.Diode;
                            return "Diode";
                    }

                    break;
                case 1:
                    switch (mode)
                    {
                        case 0xE:
                            d.Unit = "V";
                            d.Unit1 = "Hz";
                            return "AC+DC";
                        case 0xF:
                            d.Unit = "V";
                            d.Unit1 = "Hz";
                            return "AC";
                        case 0xD:
                            adjustDecPos = adjustDecPos2 = 1;
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
                            d.Unit = ">>";
                            return "";
                        case 0x8:
                            d.Unit = "°C";
                            d.Unit1 = "°F";
                            temperature = true;
                            adjustDecPos = adjustDecPos2 = 1;
                            d.ShowBar = false;
                            return "Temp Ext";
                        case 0xA:
                            d.Unit = "MHz";
                            d.Unit1 = "";
                            adjustDecPos = adjustDecPos2 = -2;
                            return "Hi";
                    }

                    break;
                case 2:
                    switch (mode)
                    {
                        case 0xE:
                            d.Unit1 = "Hz";
                            d.Unit = "dBm";
                            return "";
                        case 0xf:
                            d.Unit1 = "Hz";
                            d.Unit = "dBm";
                            return "AC";
                        case 0xD:
                            d.Unit1 = "Hz";
                            d.Unit = "dBm";
                            if (rawdata[2] == 0xc8)
                            {
                                adjustDecPos = 10;
                            }
                            else
                            {
                                adjustDecPos = 2;
                            }
                            adjustDecPos2 = 1;
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
            if (data[1] == 0xe8)
            {
                adjustDecPos += 1;
            }
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

            if (generator)
            {
                res = AddDecimalPoint(data[2]);
            }
            return res;
        }

        private static string AddDecimalPoint(byte res)
        {
            var result = "";

            switch (res)
            {

                case 0x80:
                    result = "0.50000";
                    break;
                case 0x88:
                    result = "1.0000";
                    break;
                case 0x90:
                    result = "2.0000";
                    break;
                case 0x98:
                    result = "10.000";
                    break;
                case 0xA0:
                    result = "20.000";
                    break;
                case 0xA8:
                    result = "60.240";
                    break;
                case 0xB0:
                    result = "74.630";
                    break;
                case 0xB8:
                    result = "100.00";
                    break;
                case 0xC0:
                    result = "151.50";
                    break;
                case 0xC8:
                    result = "200.00";
                    break;
                case 0xD0:
                    result = "303.00";
                    break;
                case 0xD8:
                    result = "606.10";
                    break;
                case 0xE0:
                    result = "1.2500";
                    break;
                case 0xE8:
                    result = "1.6660";
                    break;
                case 0xF0:
                    result = "2.5000";
                    break;
                case 0xF8:
                    result = "5.0000";
                    break;
            }

            return result;
        }

        private static int GetRange(byte[] data)
        {
            int range = (data[2] & 0b0011_1000) >> 3;
            //if (temperature)
            //{
            //    range++;                // Correct if temp. measurement
            //}

            if (resistance || generator)
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
            if (isAC)
            {
                return " ";
            }
            switch ((result[4] & 0b01110000) >> 4)
            {
                default:
                    return "+";
                case 4:
                case 0:
                    return " ";
                case 5:
                case 1:
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

                case 2:
                    return PrefixEnum.Mega;

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

            if (data[1] == 0xA0)
            {
                adjustDecPos2 -= 1;
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

                if (res.Length == decpos + adjustDecPos2)
                {
                    res += ".";
                }
            }

            if (res == ".")
                res = "";

            displayData.Sign2nd = res == "" ? "" : ParserSign(data);
            if (displayData.Entities == EntitiesEnum.Diode)
            {
                if (Convert.ToDecimal(displayData.MainDisplayValue) > 2)
                {
                    res = "Open";
                }
                else if (Convert.ToDecimal(displayData.MainDisplayValue) < 0.2M)
                {
                    res = "SHRT";
                }
                else
                {
                    res = "----";
                }

                displayData.Sign2nd = "";
            }
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
