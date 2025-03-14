using System;
using System.Diagnostics;
using System.Management.Automation;
using System.Reflection;
using PnP.Framework.Diagnostics;

namespace PnP.PowerShell.Commands.Utilities.Logging
{
    /// <summary>
    /// Utility class that allows loging messages
    /// </summary>
    internal static class LoggingUtility
    {
        /// <summary>
        /// Keep a reference to the last log time and correlation id to calculate the elapsed time between log entries
        /// </summary>
        private static DateTime? _lastLogTime = null;

        /// <summary>
        /// Keep a reference to the last correlation id to calculate the elapsed time between log entries, but reset counting when the correlation id changes
        /// </summary>
        private static Guid? _lastCorrelationId = null;

        /// <summary>
        /// Logs a debug message to the log stream and writes it to the cmdlet output as well
        /// </summary>
        /// <param name="cmdlet">Cmdlet currently being executed</param>
        /// <param name="message">The message to log</param>
        /// <param name="source">The source from where is being logged. Leave NULL to have it use the cmdlet name automatically.</param>
        /// <param name="correlationId">The correlation of the cmdlet execution</param>
        public static void Debug(Cmdlet cmdlet, string message, string source = null, Guid? correlationId = null)
        {
            Log.Debug(ComposeLogEntry(cmdlet, message, source, correlationId));
            cmdlet?.WriteVerbose(message);
        }

        /// <summary>
        /// Logs a warning message to the log stream and writes it to the cmdlet output as well
        /// </summary>
        /// <param name="cmdlet">Cmdlet currently being executed</param>
        /// <param name="message">The message to log</param>
        /// <param name="source">The source from where is being logged. Leave NULL to have it use the cmdlet name automatically.</param>
        /// <param name="correlationId">The correlation of the cmdlet execution</param>
        public static void Warning(Cmdlet cmdlet, string message, string source = null, Guid? correlationId = null)
        {
            Log.Warning(ComposeLogEntry(cmdlet, message, source, correlationId));
            cmdlet?.WriteWarning(message);
        }

        /// <summary>
        /// Logs an informational message to the log stream and writes it to the cmdlet output as well
        /// </summary>
        /// <param name="cmdlet">Cmdlet currently being executed</param>
        /// <param name="message">The message to log</param>
        /// <param name="source">The source from where is being logged. Leave NULL to have it use the cmdlet name automatically.</param>
        /// <param name="correlationId">The correlation of the cmdlet execution</param>
        public static void Info(Cmdlet cmdlet, string message, string source = null, Guid? correlationId = null)
        {
            Log.Info(ComposeLogEntry(cmdlet, message, source, correlationId));
            cmdlet?.WriteInformation(new InformationRecord(message, DefineCmdletName(cmdlet)));
        }

        /// <summary>
        /// Logs an error message to the log stream and writes it to the cmdlet output as well
        /// </summary>
        /// <param name="cmdlet">Cmdlet currently being executed</param>
        /// <param name="message">The message to log</param>
        /// <param name="source">The source from where is being logged. Leave NULL to have it use the cmdlet name automatically.</param>
        /// <param name="correlationId">The correlation of the cmdlet execution</param>
        public static void Error(Cmdlet cmdlet, string message, string source = null, Guid? correlationId = null)
        {
            Log.Error(ComposeLogEntry(cmdlet, message, source, correlationId));
            cmdlet?.WriteError(new ErrorRecord(new Exception(message), source, ErrorCategory.NotSpecified, null));
        }

        /// <summary>
        /// Defines the cmdlet name based on the CmdletAttribute or the type name of the cmdlet.
        /// </summary>
        /// <param name="cmdlet">Cmdlet to define its name for</param>
        /// <returns>Name of the cmdlet</returns>
        private static string DefineCmdletName(Cmdlet cmdlet)
        {
            if (cmdlet == null)
            {
                return string.Empty;
            }

            if (cmdlet.GetType().GetCustomAttribute(typeof(CmdletAttribute)) is CmdletAttribute cmdletAttribute)
            {
                return $"{cmdletAttribute.VerbName}-{cmdletAttribute.NounName}";
            }
            else
            {
                return cmdlet.GetType().Name;
            }
        }

        /// <summary>
        /// Composes a log entry based on the cmdlet, message, source and correlation id
        /// </summary>
        /// <param name="cmdlet">Cmdlet currently being executed</param>
        /// <param name="message">The message to log</param>
        /// <param name="source">The source from where is being logged. Leave NULL to have it use the cmdlet name automatically.</param>
        /// <param name="correlationId">The correlation of the cmdlet execution</param>
        /// <returns></returns>
        private static LogEntry ComposeLogEntry(Cmdlet cmdlet, string message, string source = null, Guid? correlationId = null)
        {
            if (_lastCorrelationId != correlationId)
            {
                // New cmdlet execution, reset the last log time
                _lastLogTime = null;
                _lastCorrelationId = correlationId;
            }

            var logEntry = new LogEntry
            {
                Message = message,
                CorrelationId = correlationId ?? Guid.Empty,
                EllapsedMilliseconds = _lastLogTime.HasValue ? (long)DateTime.UtcNow.Subtract(_lastLogTime.Value).TotalMilliseconds : 0,
                Source = source ?? DefineCmdletName(cmdlet),
                ThreadId = Environment.CurrentManagedThreadId
            };

            // Keep a reference to the last log time to calculate the elapsed time between log entries
            _lastLogTime = DateTime.UtcNow;

            return logEntry;
        }

        /// <summary>
        /// Tries to remove the listener with the given name from the Trace.Listeners collection.
        /// If the listener is not found, it will be ignored.
        /// </summary>
        /// <param name="listenerName">Name of the trace listener</param>
        public static void RemoveListener(string listenerName)
        {
            try
            {
                var existingListener = Trace.Listeners[listenerName];
                if (existingListener != null)
                {
                    existingListener.Flush();
                    existingListener.Close();
                    Trace.Listeners.Remove(existingListener);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}