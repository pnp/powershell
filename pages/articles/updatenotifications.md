# Update notifications
One time per PowerShell session PnP PowerShell will check for new versions when you execute Connect-PnPOnline.

To turn off this update check set the `PNPPOWERSHELL_UPDATECHECK` environment variable to `false`. Any other value will still continue to check for versions. 

If you have a script and want to turn off the update check, simply set the following as the first line of your script:

```powershell
$env:PNPPOWERSHELL_UPDATECHECK="false"
```



See [Configure PnP PowerShell](configuration.md) for more information on the environment variables you can set.

## When do you receive an update notification

If the major version of your current version is lower than the currently available major version.

If the major versions are the same, but the minor version is lower than the currently available minor version.

### If you are running a nightly build on PowerShell 7

Besides the above rules, you will also receive a notification if the major and minor versions are the same, but the patch level is lower than the currently available version.

