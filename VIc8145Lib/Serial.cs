using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using Vici8145Lib;

namespace VIc8145Lib
{
    public static class Serial
    {

        private static AutoResetEvent _messageReceived;
        private static int _maxWait;
        private static readonly byte[] Bfr = new byte[100];
        private static SerialPort _port;
        private static readonly byte[] NonRespondingCommands = {0xa0, 0xa1, 0xa3, 0xa4, 0xa5, 0xa7, 0xa8, 0xa9, 0xaA, 0xab, 0xaD};
        private static readonly byte[] RespondingCommands    = {0x89, 0x8a, 0x8b};

        
        public static List<string> GetComports() {
            var result = new List<string>();

            return result;
        }

        public static void ClosePort()
        {
            _port.Close();
        }

        public static bool OpenPort(string portId, int timeout = 5000)
        {
            _port = new SerialPort(portId, 9600, Parity.None, 8, StopBits.One);
            _maxWait = timeout;
            _port.Open();
            _messageReceived = new AutoResetEvent(false);
            _port.DataReceived += PortOnDataReceived;
            return _port.IsOpen;
        }

        private static void PortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort)sender;
            byte read;
            var index = 0;
            do
            {
              Bfr[index++] = read = (byte)sp.ReadByte();
            } while (read != 0x0a && index < 90);

            if (index > 90)
                index = 0;
            Bfr[index] = 0;

            _messageReceived.Set();
        }

        public static void WriteNonrespondingCommand(NonRespondingCommands cmd)
        {
            _port.Write(NonRespondingCommands, (int)cmd, 1);
        }

        public static byte[] WriteRespondingCommand(RespondingCommands cmd)
        {
            Bfr[0] = 0;
            _port.Write(RespondingCommands, (int)cmd, 1);
            _messageReceived.WaitOne(_maxWait);
            return Bfr[0] != 0 ? Bfr : null;
        }

        public static DisplayData UpdateDisplayData(DisplayData displayData, RespondingCommands cmd)
        {
            if (displayData == null)
            {
                displayData = new DisplayData();
            }

            var result = WriteRespondingCommand(cmd);
            switch (cmd)
            {
                case Vici8145Lib.RespondingCommands.MainDisplayValue:
                    displayData = ParseResult.ParseMainDisplayData(displayData, result);
                    break;
                case Vici8145Lib.RespondingCommands.SecondDisplayValue:
                    displayData = ParseResult.ParseSecondDisplayData(displayData, result);
                    break;
                case Vici8145Lib.RespondingCommands.AnalogeBarValue:
                    displayData = ParseResult.ParseBarDisplayData(displayData, result);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cmd), cmd, null);
            }

            displayData.RawData = result;
            return displayData;


        }
    }
}

