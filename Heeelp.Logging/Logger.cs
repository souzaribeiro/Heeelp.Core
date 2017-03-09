using System;
using System.Diagnostics;
using System.Text;

namespace Heeelp.Core.Logging
{
    public static class Logger
    {
        private const string APPNAME = "Heeelp";
        private const string LOGNAME = "Application";

        public static void LogEvent(EventSource source, EventType type, string message)
        {
            var sourceName = APPNAME + "-" + source.ToString();

            if (!EventLog.SourceExists(sourceName))
                EventLog.CreateEventSource(sourceName, LOGNAME);

            EventLog.WriteEntry(sourceName, message, (EventLogEntryType)Convert.ToInt32(type));
        }

        public static void LogEvent(EventSource source, EventType type, string message, Exception ex)
        {
            var sb = new StringBuilder(message);
            sb.AppendLine(); sb.AppendLine();
            sb.AppendLine("Exception Details: ");
            sb.AppendLine(); sb.AppendLine();
            sb.AppendLine(ex.ToString());

            LogEvent(source, type, sb.ToString());
        }
    }
}
