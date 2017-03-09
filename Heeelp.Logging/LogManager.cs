using System;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.DataContracts;
using System.Configuration;

namespace Heeelp.Core.Logging
{
    public class LogManager
    {
        private static TelemetryClient telemetry;
        private static string traceName;

        public LogManager()
        {
            telemetry = new TelemetryClient();
            traceName = ConfigurationManager.AppSettings["TraceName"].ToString();
        }



        public static void Info(object message)
        {
            Info(message, null);
        }

        public static void Info(object message, Exception exception)
        {
            if (exception != null)
            {
                telemetry.TrackException(exception);
            }
            var msg = new Dictionary<string, string> { { "message", message.ToString() } };
            telemetry.TrackTrace(traceName, SeverityLevel.Information, msg);
        }

        public static void Warn(object message)
        {
            Warn(message, null);
        }

        public static void Warn(object message, Exception exception)
        {
            if (exception != null)
            {
                telemetry.TrackException(exception);
            }
            var msg = new Dictionary<string, string> { { "message", message.ToString() } };
            telemetry.TrackTrace(traceName, SeverityLevel.Warning, msg);
        }

        public static void Debug(object message)
        {
            Debug(message, null);
        }

        public static void Debug(object message, Exception exception)
        {
            if (exception != null)
            {
                telemetry.TrackException(exception);
            }
            var msg = new Dictionary<string, string> { { "message", message.ToString() } };
            telemetry.TrackTrace(traceName, SeverityLevel.Information, msg);
        }

        public static void Error(object message)
        {
            Error(message, null);
        }

        public static void Error(object message, Exception exception)
        {
            if (exception != null)
            {
                telemetry.TrackException(exception);
            }
            var msg = new Dictionary<string, string> { { "message", message.ToString() } };
            telemetry.TrackTrace(traceName, SeverityLevel.Error, msg);
        }

        public static void Fatal(object message)
        {
            Fatal(message, null);
        }

        public static void Fatal(object message, Exception exception)
        {
            if (exception != null)
            {
                telemetry.TrackException(exception);
            }
            var msg = new Dictionary<string, string> { { "message", message.ToString() } };
            telemetry.TrackTrace(traceName, SeverityLevel.Critical, msg);
        }
    }
}
