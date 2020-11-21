# Update notifications
One time per PowerShell session PnP PowerShell will check for new versions when you execute Connect-PnPOnline.

You can turn this behavior off in 2 ways:

Set the `PNPPOWERSHELL_UPDATECHECK` environment variable to `Off`. Any other value will still continue to check for versions. 

or

Use `-NoVersionCheck` parameter on Connect-PnPOnline. 

## When do you receive an update notification

If the major version of your current version is lower than the currently available major version.

If the major versions are the same, but the minor version is lower than the currently avialable minor version.

If you are running a nightly build on PowerShell 7:

If major and minor versions are the same, but the patch level is lower than the currently available patch version.

