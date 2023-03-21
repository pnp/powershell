# Disable or Enable telemetry

By default PnP PowerShell will report its usage anonymously to the PnP team. We collection information about the **version of PnP PowerShell**, the **operation system version** and the **cmdlet** executed. Notice that we will *not* include parameters used and we will *not* include any values of parameters. This allows us to get insight in the usage of cmdlets.

To disable telemetry, set the `PNPPOWERSHELL_DISABLETELEMETRY` environment variable to `true`, i.e. by using `$env:PNPPOWERSHELL_DISABLETELEMETRY=$true`. Alternatively, you can create an empty file called `.pnppowershelltelemetry` inside your home directory (`$env:UserProfile` on Windows, `$env:HOME` on Linux) not needing any content inside of the file to disable telemetry.

To query if in a connected PnP PowerShell session the telemetry is enabled, use [Get-PnPPowerShellTelemetryEnabled](../cmdlets/Get-PnPPowerShellTelemetryEnabled.html).
