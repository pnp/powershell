using System.Collections.Generic;
using System.Diagnostics;

namespace PnP.PowerShell.Commands.Utilities.Logging
{
    /// <summary>
    /// Tracel listener that captures log entries in a list in memory so they can be retrieved in PowerShell without having to write them to a file
    /// </summary>
    public class LogStreamListener : TraceListener
    {
        /// <summary>
        /// The default name of the log stream listener. This is used to identify the listener in the Trace.Listeners collection.
        /// </summary>
        public const string DefaultListenerName = "PNPPOWERSHELLLOGSTREAMTRACELISTENER";

        /// <summary>
        /// The list of log entries captured by this listener. This is used to retrieve the log entries in PowerShell.
        /// </summary>
        public List<TraceLogEntry> Entries { get; } = [];

        /// <summary>
        /// Receives a message that is being logged
        /// </summary>
        /// <param name="message">Message that is being logged</param>
        public override void Write(string message)
        {
            // Ignore the message, we only care about WriteLine as these partial messages are not complete log entries
        }

        /// <summary>
        /// Receives a message that is being logged
        /// </summary>
        /// <param name="message">Message that is being logged</param>
        public override void WriteLine(string message)
        {
            // Check if we received a message
            if (string.IsNullOrWhiteSpace(message)) return;

            // Split the message into parts. The message is tab separated.
            var items = message.Split('\t');

            // Ensure we have at least 7 items in the message
            if (items.Length < 7) return;

            // Try to parse the message into a TraceLogEntry. If it fails, ignore the message.
            Entries.Add(new TraceLogEntry(items));
        }
    }
}