using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading;

namespace PnP.PowerShell.Commands.Utilities
{
    public class CmdletMessageWriter
    {
        private PSCmdlet Cmdlet { get; set; }
        private Queue<Message> Queue { get; set; }
        private object LockToken { get; set; }
        public bool Finished { get; set; }
        public CmdletMessageWriter(PSCmdlet cmdlet)
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
                    if (!message.IsError)
                    {
                        if (message.Formatted)
                        {
                            WriteFormattedWarning(Cmdlet, message.Text);
                        }
                        else
                        {
                            Cmdlet.Host.UI.WriteLine(message.Text);
                        }
                    }
                    else
                    {
                        Cmdlet.Host.UI.WriteErrorLine(message.Text);
                    }
                }

                Thread.Sleep(100);
            }
        }

        public void WriteMessage(string message, bool formatted = true)
        {
            lock (LockToken)
            {
                Queue.Enqueue(new Message() { Formatted = formatted, Text = message });
            }
        }

        public void WriteError(string message)
        {
            lock (LockToken)
            {
                Queue.Enqueue(new Message() { Formatted = false, Text = message, IsError = true });
            }
        }

        private class Message
        {
            public string Text { get; set; }
            public bool Formatted { get; set; }
            public bool IsError { get; set; }
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

        internal static void WriteFormattedWarning(PSCmdlet cmdlet, string message)
        {
            if (cmdlet.Host.Name == "ConsoleHost")
            {
                var messageLines = new List<string>();
                messageLines.AddRange(message.Split(new[] { '\n' }));
                var wrappedText = new List<string>();
                foreach (var messageLine in messageLines.Select(l => l == "\n" ? " \n" : l))
                {
                    wrappedText.AddRange(WordWrap(messageLine, cmdlet.Host.UI.RawUI.MaxWindowSize.Width - 4));
                }

                var notificationColor = "\x1B[7m";
                var resetColor = "\x1B[0m";

                var outMessage = string.Empty;
               
                foreach (var wrappedLine in wrappedText)
                {
                    if (wrappedLine == "")
                    {
                        outMessage += $" \x00A0\n".PadRight(cmdlet.Host.UI.RawUI.MaxWindowSize.Width - 4);
                    } else
                    {
                        var lineToAdd = wrappedLine.PadRight(cmdlet.Host.UI.RawUI.MaxWindowSize.Width - 4);
                        outMessage += $" {lineToAdd}\n";
                    }
                }
                cmdlet.Host.UI.WriteWarningLine($"{notificationColor}\n{outMessage}{resetColor}\n");
                //cmdlet.Host.UI.WriteLine($"{notificationColor}\n{outMessage}{resetColor}\n");
            }
            else
            {
                cmdlet.WriteWarning(message);
            }
        }
    }
}