# Disable or Enable telemetry

By default PnP PowerShell will report its usage anonymously to the PnP team. We collection information about the **version of PnP PowerShell**, the **operation system version** and the **cmdlet** executed. Notice that we will *not* include parameters used and we will *not* include any values of parameters. We will also *not* be able to trace the execution back to the specific tenant it ran on, the organization it was used for or the person it was run by. Having telemtry in place allows us to get insight in the usage of cmdlets and thereby prioritize work towards the most popular cmdlets.

To query if in a connected PnP PowerShell session the telemetry is enabled, use [Get-PnPPowerShellTelemetryEnabled](../cmdlets/Get-PnPPowerShellTelemetryEnabled.html).

If you wish to control telemetry to be sent, you can use one of the below options.

## By using PnP PowerShell
You can disable telemetry to be sent by using [Disable-PnPPowerShellTelemetry](../cmdlets/Disable-PnPPowerShellTelemetry.html).
You can enable telemetry to be sent by using [Enable-PnPPowerShellTelemetry](../cmdlets/Enable-PnPPowerShellTelemetry.html).

## By setting an environment variable
To disable telemetry, set the `PNPPOWERSHELL_DISABLETELEMETRY` environment variable to `true`, i.e. by using `$env:PNPPOWERSHELL_DISABLETELEMETRY=$true`. Remove the entry again or set it to `false` to enable telemetry to be sent again. 

## By adding a file in your user profile folder
Alternatively, you can create an empty file called `.pnppowershelltelemetry` inside your home directory (`$env:UserProfile` on Windows, `$env:HOME` on Linux) not needing any content inside of the file to disable telemetry. Remove the file again to enable telemetry to be sent.