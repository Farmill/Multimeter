using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIc8145Lib;
using Vici8145Lib;

namespace Multimeter
{
    class Program
    {
        static void Main(string[] args)
        {
            var lib = new Vici8145();
            lib.Openport("COM10");
            while (true)
            {
                DisplayData b = lib.GetData(null, RespondingCommands.MainDisplayValue);
                 b = lib.GetData(b, RespondingCommands.SecondDisplayValue);
                 b = lib.GetData(b, RespondingCommands.AnalogeBarValue);

                var hexdisp = $"{b.RawData[0]:X2} {b.RawData[1]:X2} {b.RawData[2]:X2} {b.RawData[3]:X2} {b.RawData[4]:X2} {b.RawData[5]:X2} {b.RawData[6]:X2} {b.RawData[7]:X2} {b.RawData[8]:X2} {b.RawData[9]:X2} {b.RawData[10]:X2} {b.RawData[11]:X2}";
                Console.WriteLine(b.MainDisplayValue + " " + b.Unit + " " + b.Unit1 +  " " + b.Select + " " + b.SecondDisplayValue + " " + b.Rel  + " " + b.Hold  + " " + b.MinMax );
                Console.WriteLine(hexdisp);
                Console.ReadKey();
            }
            //Console.ReadKey();
        }


    }
}

