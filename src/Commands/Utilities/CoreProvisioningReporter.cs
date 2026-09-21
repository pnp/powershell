using PnP.Core.Provisioning.ObjectHandlers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Turns the progress and message callbacks of the PnP.Core.Provisioning engine into PowerShell
    /// progress records and warnings, the same way the PnP Framework based provisioning cmdlets present them.
    ///
    /// The engine is async and calls back on a thread pool thread, while PowerShell allows WriteProgress
    /// and WriteWarning only from the pipeline thread. Callbacks are therefore queued, and
    /// <see cref="Run{T}(Func{Task{T}})"/> replays them on the pipeline thread while the engine runs.
    /// Call the engine through that method rather than blocking on the task yourself.
    /// </summary>
    internal sealed class CoreProvisioningReporter
    {
        private readonly BlockingCollection<Action> pendingWrites = new();
        private readonly HashSet<string> warningsShown = new(StringComparer.Ordinal);
        private readonly Action<ProgressRecord> writeProgress;
        private readonly Action<string> logWarning;
        private readonly string activity;

        /// <summary>
        /// Creates a reporter for one run of the engine.
        /// </summary>
        /// <param name="activity">The activity shown on the main progress record</param>
        /// <param name="writeProgress">Writes a progress record, normally the cmdlet's WriteProgress</param>
        /// <param name="logWarning">Reports a warning, normally the cmdlet's LogWarning</param>
        internal CoreProvisioningReporter(string activity, Action<ProgressRecord> writeProgress, Action<string> logWarning)
        {
            this.activity = string.IsNullOrWhiteSpace(activity) ? "Processing" : activity;
            this.writeProgress = writeProgress;
            this.logWarning = logWarning;
        }

        /// <summary>
        /// The delegate to hand to the extract or apply configuration for overall progress.
        /// </summary>
        internal ProvisioningProgressDelegate ProgressDelegate => (message, step, total) =>
        {
            if (string.IsNullOrWhiteSpace(message) || total == 0)
            {
                return;
            }

            var record = new ProgressRecord(0, activity, message)
            {
                PercentComplete = Percentage(step, total),
                RecordType = ProgressRecordType.Processing
            };
            pendingWrites.Add(() => writeProgress(record));
        };

        /// <summary>
        /// The delegate to hand to the extract or apply configuration for warnings and per handler progress.
        /// A warning is shown once, as a handler may report the same problem for every artefact it touches.
        /// </summary>
        internal ProvisioningMessagesDelegate MessagesDelegate => (message, type) =>
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            switch (type)
            {
                case ProvisioningMessageType.Warning:
                case ProvisioningMessageType.Error:
                    pendingWrites.Add(() =>
                    {
                        if (warningsShown.Add(message))
                        {
                            logWarning(message);
                        }
                    });
                    break;

                case ProvisioningMessageType.Progress:
                    var progressRecord = BuildSubProgressRecord(message);
                    pendingWrites.Add(() => writeProgress(progressRecord));
                    break;

                case ProvisioningMessageType.Completed:
                    var completedRecord = new ProgressRecord(1, message, " ") { RecordType = ProgressRecordType.Completed };
                    pendingWrites.Add(() => writeProgress(completedRecord));
                    break;
            }
        };

        /// <summary>
        /// Runs an engine operation which returns a result, writing the progress and warnings it reports
        /// on the calling thread while it runs.
        /// </summary>
        /// <param name="operation">The engine call to run</param>
        /// <returns>What the engine call returned</returns>
        internal T Run<T>(Func<Task<T>> operation)
        {
            var task = Task.Run(operation);
            try
            {
                while (!task.IsCompleted)
                {
                    if (pendingWrites.TryTake(out var write, 100))
                    {
                        write();
                    }
                }
                while (pendingWrites.TryTake(out var remaining, 0))
                {
                    remaining();
                }

                return task.GetAwaiter().GetResult();
            }
            finally
            {
                Complete();
            }
        }

        /// <summary>
        /// Runs an engine operation which returns nothing, writing the progress and warnings it reports
        /// on the calling thread while it runs.
        /// </summary>
        /// <param name="operation">The engine call to run</param>
        internal void Run(Func<Task> operation)
        {
            Run(async () =>
            {
                await operation().ConfigureAwait(false);
                return true;
            });
        }

        private void Complete()
        {
            writeProgress(new ProgressRecord(1, " ", " ") { RecordType = ProgressRecordType.Completed });
            writeProgress(new ProgressRecord(0, activity, " ") { RecordType = ProgressRecordType.Completed });
        }

        private static ProgressRecord BuildSubProgressRecord(string message)
        {
            var parts = message.Split('|');
            if (parts.Length == 4
                && double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var current)
                && double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
                && total > 0)
            {
                return new ProgressRecord(1,
                    string.IsNullOrWhiteSpace(parts[0]) ? "-" : parts[0],
                    string.IsNullOrWhiteSpace(parts[1]) ? "-" : parts[1])
                {
                    PercentComplete = Percentage(current, total),
                    RecordType = ProgressRecordType.Processing
                };
            }

            return new ProgressRecord(1, "Processing", message) { RecordType = ProgressRecordType.Processing };
        }

        private static int Percentage(double step, double total)
        {
            var percentage = Convert.ToInt32(100 / total * step);
            return Math.Clamp(percentage, 0, 100);
        }
    }
}
