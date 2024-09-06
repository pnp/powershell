# Set a default Client ID

As of September 9th, 2024, it is no longer possible to use PnP PowerShell with -Interactive without providing your own Entra ID App Registration by passing in -ClientId as well. To avoid having to add -ClientId on every connect, you can also perform the below task to set the default ClientId for your environment. This avoids you having to update all of your scripts to include -ClientId in the Connect-PnPonline statements.  

## By setting an environment variable

You can set an environment variable on your machine or in your profile to default to the ClientId you configure in it. The name of the environment variable should be either: `ENTRAID_APP_ID`, or `ENTRAID_CLIENT_ID`, or `AZURE_CLIENT_ID`. You only need one of these, not all of them. They will be used in the order shown, i.e. if you set a value for `AZURE_CLIENT_ID` and another one for `ENTRAID_APP_ID`, the `ENTRAID_APP_ID` entry will be used and the other will be ignored.  

As the value for the environment variable, set the GUID of the Client Id / App Id from Entra ID of [your own App Registration](authentication.md#setting-up-access-to-your-own-entra-id-app-for-interactive-login).  
