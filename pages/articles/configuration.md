# Configure PnP PowerShell

## Environment Variables

There are currently two settings that can be set to control the behavior of PnP PowerShell by means of setting environment variables.

### Disable or Enable version checks

Setting the environment variable `PNPPOWERSHELL_UPDATECHECK` to `false` or `off`, i.e. by using `$env:PNPPOWERSHELL_UPDATECHECK=off`, will disable the version check which is occurring when executing `Connect-PnPOnline`. Setting it to any other value will enable it. Notice that this version check will only occur once during a PowerShell session. If you close PowerShell and reopen it, the version will be checked again.

### Disable or Enable telemetry

By default PnP PowerShell will report its usage anonymously to the PnP team. We collection information about the **version of PnP PowerShell**, the **operation system version** and the **cmdlet** executed. Notice that we will *not* include parameters used and we will *not* include any values of parameters. This allows us to get insight in the usage of cmdlets.

To disable telemetry, set the `PNPPOWERSHELL_DISABLETELEMETRY` environment variable to `true`, i.e. by using `$env:PNPPOWERSHELL_DISABLETELEMETRY=$true`. Alternatively, you can create an empty file called `.pnppowershelltelemetry` inside your home directory (`$env:UserProfile` on Windows, `$env:HOME` on Linux) not needing any content inside of the file to disable telemetry.

To query if in a connected PnP PowerShell session the telemetry is enabled, use [Get-PnPPowerShellTelemetryEnabled](../cmdlets/Get-PnPPowerShellTelemetryEnabled.html).