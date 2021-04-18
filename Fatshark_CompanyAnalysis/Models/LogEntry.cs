using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatshark_CompanyAnalysis.Models
{
    public class LogEntry
    {
        public LogEntry(string message, LogType type = LogType.Info)
        {
            Message = message;
            Type = type;
            Time = DateTime.Now.ToString("t");
        }
        public LogType Type { get; set; }
        public string Time { get; set; }
        public string Message { get; set; }
    }

    public enum LogType
    {
        Debug,
        Info,
        Error
    }
}
