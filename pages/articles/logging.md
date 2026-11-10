# Logging and tracing

PnP PowerShell can keep a trace log of what it does: the start and end of every cmdlet, the Microsoft Graph and REST requests it sends, the [retries after throttling](throttling.md), and the warnings and errors it runs into. The trace log also holds messages that `-Verbose` does not show, such as a permission scope missing from the access token. It is the first thing to turn on when a cmdlet does not behave as expected.

## Starting the trace log

[Start-PnPTraceLog](../cmdlets/Start-PnPTraceLog.md) writes the log to one or more targets:

```powershell
# Keep the entries in memory, to read them as objects with Get-PnPTraceLog
Start-PnPTraceLog -WriteToLogStream -Level Debug

# Append the entries to a file
Start-PnPTraceLog -Path ./pnp.log -Level Debug

# Write the entries to the console
Start-PnPTraceLog -WriteToConsole
```

The targets can be combined. Starting a target that is already running replaces it, which for `-WriteToLogStream` discards the entries kept so far.

`-Level` sets which entries are written, for all targets at once:

| Level | Writes |
|---|---|
| `Debug` | everything, including each cmdlet as it was called and each Microsoft Graph and REST request sent |
| `Information` | retries and other progress messages, plus warnings and errors (the default) |
| `Warning` | warnings and errors |
| `Error` | errors only |

The trace log is kept for the whole PowerShell process. It is not affected by `Connect-PnPOnline` or `Disconnect-PnPOnline`, and entries from `ForEach-Object -Parallel` script blocks end up in the same log.

## Reading the trace log

[Get-PnPTraceLog](../cmdlets/Get-PnPTraceLog.md) returns the entries kept in memory as objects with, among others, a `TimeStamp`, `Source`, `Level` and `Message`:

```powershell
Get-PnPTraceLog | Where-Object Level -eq Error
```

A log file is plain text with the fields of each entry separated by tabs, and can be read with `Get-Content` while it is still being written. [Clear-PnPTraceLog](../cmdlets/Clear-PnPTraceLog.md) empties the entries kept in memory.

## Adding your own entries

[Write-PnPTraceLog](../cmdlets/Write-PnPTraceLog.md) adds entries from your script, so they appear in order with those of PnP PowerShell:

```powershell
Write-PnPTraceLog -Message "Processing site $siteUrl" -Source "SiteInventory" -Level Information
```

Besides the trace log, an entry is also written to the matching PowerShell stream: a `Warning` entry shows as a warning, and an `Error` entry is written as an error, which stops a script running with `$ErrorActionPreference = 'Stop'`.

## Stopping the trace log

[Stop-PnPTraceLog](../cmdlets/Stop-PnPTraceLog.md) stops every target unless told otherwise. To stop writing to a file and the console, but keep the entries in memory available to `Get-PnPTraceLog`:

```powershell
Stop-PnPTraceLog -StopLogStreamLogging:$false
```

## The trace log and -Verbose

Entries written at the `Debug` level by the cmdlets themselves also appear when a cmdlet runs with `-Verbose`, whether the trace log is started or not. The other entries, which include the requests sent, the retries and the permission checks, appear only in the trace log.

## Secrets in the trace log

At the `Debug` level, each cmdlet is logged as it was typed. A client secret or password typed into the command appears in the log as is, while one passed in a variable appears as the variable name. The access tokens PnP PowerShell acquires are not logged. Treat a debug log as sensitive, and pass secrets in variables.
