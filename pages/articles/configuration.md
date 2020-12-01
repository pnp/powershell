# Configure PnP PowerShell

## Environment Variables

There are currently two ways to make changes to the way PnP PowerShell behaves by means of setting environment variables

### Disable or Enable version checks

Setting the environment variable `PNPPOWERSHELL_UPDATECHECK` to `false` will disable the version check which is occuring when executing `Connect-PnPOnline`. Notice that this version check will only occur once during a PowerShell session. If you close PowerShell and reopen it, the version will be checked again.

### Disable or Enable telemetry

By default PnP PowerShell will report its usage to the PnP Team. We collection information about the **version of PnP PowerShell** and the **cmdlet** executed. Notice that we will *not* include parameters used and we will *not* include any values of parameters.

To disable telemetry, set the `PNPPOWERSHELL_DISABLETELEMETRY` environment variable to `true`.
