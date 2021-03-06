﻿using VIc8145Lib;

namespace Vici8145Lib
{
    public class Vici8145
    {
        
       public bool Openport(string port)
        {
            return Serial.OpenPort(port);
        }

        public void SendCommand(NonRespondingCommands cmd)
        {
            Serial.WriteNonrespondingCommand(cmd);
        }
        public DisplayData GetData(DisplayData dispData, RespondingCommands cmd)
        {
            return Serial.UpdateDisplayData(dispData, cmd);
        }

        public void ClosePort()
        {
            Serial.ClosePort();
        }
    }
}
