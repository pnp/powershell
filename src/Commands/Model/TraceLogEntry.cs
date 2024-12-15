using System;

namespace PnP.PowerShell.Model
{
     public class TraceLogEntry
        {
            public DateTime TimeStamp;
            public string Category;
            // public string Three;
            public string Level;
            public string Message;
            // public string Six;
            // public string Seven;
            public TraceLogEntry(string[] values)
            {
                TimeStamp = DateTime.Parse(values[0].Split(" : ")[1]);
                Category = values[1].Trim('[', ']');
                // Three = values[2];
                Level = values[3].Trim('[', ']');
                Message = values[4];
                // Six = values[5];
                // Seven = values[6];
            }
        }
}