using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.Utilities
{
    public class CmdletMessageWriter
    {
        private BasePSCmdlet Cmdlet { get; set; }
        private Queue<Message> Queue { get; set; }
        private object LockToken { get; set; }
        public bool Finished { get; set; }
        public CmdletMessageWriter(BasePSCmdlet cmdlet)
        {
            this.Cmdlet = cmdlet;
            this.LockToken = new object();
            this.Queue = new Queue<Message>();
            this.Finished = false;
        }

        public void Stop()
        {
            this.Finished = true;
        }

        public void Start()
        {
            while (!Finished || Queue.Count > 0)
            {
                while (Queue.Count > 0)
                {
                    var message = Queue.Dequeue();
                    if (message.Formatted)
                    {
                        WriteFormattedMessage(Cmdlet, message);
                    }
                    else
                    {
                        switch (message.Type)
                        {
                            case MessageType.Message:
                                {
                                    Cmdlet.Host.UI.WriteLine(message.Text);
                                    break;
                                }
                            case MessageType.Warning:
                                {
                                    Cmdlet.Host.UI.WriteWarningLine(message.Text);
                                    break;
                                }
                            case MessageType.Verbose:
                                {
                                    Cmdlet.Host.UI.WriteVerboseLine(message.Text);
                                    break;
                                }
                        }

                    }
                    break;
                }
            }

            Thread.Sleep(100);
        }

        public void LogWarning(string message, bool formatted = true)
        {
            lock (LockToken)
            {

                Queue.Enqueue(new Message() { Formatted = formatted, Text = message, Type = MessageType.Warning });
            }
        }

        public void WriteMessage(string message, bool formatted = true)
        {
            lock (LockToken)
            {
                Queue.Enqueue(new Message() { Formatted = formatted, Text = message, Type = MessageType.Message });
            }
        }

        public void LogDebug(string message)
        {
            if (Cmdlet.MyInvocation.BoundParameters.ContainsKey("Verbose"))
            {
                lock (LockToken)
                {
                    Queue.Enqueue(new Message() { Formatted = false, Text = message, Type = MessageType.Verbose });
                }
            }
        }

        internal class Message
        {
            public string Text { get; set; }
            public bool Formatted { get; set; }
            public MessageType Type { get; set; }
        }

        internal enum MessageType
        {
            Message,
            Warning,
            Verbose
        }

        private static List<string> WordWrap(string text, int maxLineLength)
        {
            var list = new List<string>();

            int currentIndex;
            var lastWrap = 0;
            var whitespace = new[] { ' ', '\r', '\n', '\t' };
            do
            {
                currentIndex = lastWrap + maxLineLength > text.Length ? text.Length : (text.LastIndexOfAny(new[] { ' ', ',', '.', '?', '!', ':', ';', '-', '\n', '\r', '\t' }, Math.Min(text.Length - 1, lastWrap + maxLineLength)) + 1);
                if (currentIndex <= lastWrap)
                    currentIndex = Math.Min(lastWrap + maxLineLength, text.Length);
                list.Add(text.Substring(lastWrap, currentIndex - lastWrap).Trim(whitespace));
                lastWrap = currentIndex;
            } while (currentIndex < text.Length);

            return list;
        }

        internal static void WriteFormattedWarning(BasePSCmdlet cmdlet, string message)
        {
            WriteFormattedMessage(cmdlet, new Message { Text = message, Type = MessageType.Warning, Formatted = true });
        }

        internal static void WriteFormattedMessage(BasePSCmdlet cmdlet, Message message)
        {
            if (cmdlet.Host.Name == "ConsoleHost" && cmdlet.Host.UI.RawUI.MaxWindowSize.Width > 8)
            {
                var messageLines = new List<string>();
                messageLines.AddRange(message.Text.Split(new[] { '\n' }));
                var wrappedText = new List<string>();
                foreach (var messageLine in messageLines.Select(l => l == "\n" ? " \n" : l))
                {
                    wrappedText.AddRange(WordWrap(messageLine, cmdlet.Host.UI.RawUI.MaxWindowSize.Width - 8));
                }

                var notificationColor = "\x1B[7m";
                var resetColor = "\x1B[0m";

                var outMessage = string.Empty;

                foreach (var wrappedLine in wrappedText)
                {
                    var lineToAdd = string.Empty;
                    if (wrappedLine == "")
                    {
                        lineToAdd = "\x00A0".PadRight(cmdlet.Host.UI.RawUI.MaxWindowSize.Width - 8);
                    }
                    else
                    {
                        lineToAdd = wrappedLine.PadRight(cmdlet.Host.UI.RawUI.MaxWindowSize.Width - 8);
                    }
                    outMessage += $" {lineToAdd}\n";
                }
                switch (message.Type)
                {
                    case MessageType.Message:
                        {
                            cmdlet.WriteObject($"{notificationColor}\n{outMessage}{resetColor}\n");
                            break;
                        }
                    case MessageType.Warning:
                        {
                            cmdlet.LogWarning($"{notificationColor}\n{outMessage}{resetColor}\n");
                            break;
                        }
                    case MessageType.Verbose:
                        {
                            cmdlet.LogDebug(outMessage);
                            break;
                        }
                }
            }
            else
            {
                switch (message.Type)
                {
                    case MessageType.Message:
                        {
                            cmdlet.WriteObject(message.Text);
                            break;
                        }
                    case MessageType.Warning:
                        {
                            cmdlet.LogWarning(message.Text);
                            break;
                        }
                    case MessageType.Verbose:
                        {
                            cmdlet.LogDebug(message.Text);
                            break;
                        }
                }
            }
        }
    }
}