﻿
using System;
using System.Diagnostics;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Stop, "PnPTraceLog")]
    public class StopTraceLog : PSCmdlet
    {
        private const string FileListenername = "PNPPOWERSHELLFILETRACELISTENER";
        private const string ConsoleListenername = "PNPPOWERSHELLCONSOLETRACELISTENER";
        protected override void ProcessRecord()
        {
            Trace.Flush();
            RemoveListener(ConsoleListenername);
            RemoveListener(FileListenername);
        }

        private void RemoveListener(string listenerName)
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