# Using PnP PowerShell in Azure Functions

In this article we will setup an Azure Function to use PnP PowerShell

## Create the function app

As the UI in https://portal.azure.com changes every now and then, but the principles stay the same, follow the following steps:

1. Create a new Function App and navigate there when ready.
1. Make sure you select the option to run PowerShell V3 functions, based upon PowerShell 7.

## Make PnP PowerShell available to all functions in the app

1. Navigate to `App files` which is located the left side menu of the function app under the `Functions` header.
1. In the dropdown presented, select `requirements.psd1`. You'll notice that the function app wants to provide the Azure cmdlets. If you do not need those, consider removing the `Az` entry presented.
1. Add a new entry or replace the whole contents of the file with:
 
   ```powershell
   @{
       'PnP.PowerShell' = '0.3.9-nightly'
   }
   ```
1. The version that will be installed will be the specified nightly build.
1. The moment we release a full 1.0 release you can use wildcards too:

    ```powershell
    @{
        'PnP.PowerShell' = '1.*'
     }
    ```
1. This will then automatically download any minor version of the major 1 release when available. Notice that you cannot use wildcards to specify a nightly build.

If you decide to remove the Az cmdlets, save the `requirements.psd1` file and edit the `profile.psd` file. Mark out the following block in the file as follows:

```powershell
# if ($env:MSI_SECRET) {
#     Disable-AzContextAutosave -Scope Process | Out-Null     
#     Connect-AzAccount -Identity
# }
```

Save the file.

## Create your credentials
1. Navigate to `Configuration` under `Settings` and create a new Application Setting. 
1. Enter `tenant_user` and enter the username you want to authenticate with as the user
1. Enter `tenant_pwd` and enter the password you want to use for that user

## Create the function

Create a new function and replace the function code with following example:

````powershell
using namespace System.Net

# Input bindings are passed in via param block.
param($Request, $TriggerMetadata)

# Write to the Azure Functions log stream.
Write-Host "PowerShell HTTP trigger function processed a request."

$script = {
    $securePassword = ConvertTo-SecureString $env:tenant_pwd -AsPlainText -Force
    $credentials = New-Object PSCredential ($env:tenant_user, $securePassword)

    Connect-PnPOnline -Url https://yourtenant.sharepoint.com/sites/demo -Credentials $credentials

    $web = Get-PnPWeb;
    $web.Title
}

$webTitle = Start-ThreadJob -Script $script | Receive-Job -Wait

$body = "The title of the web is $($webTitle)"

# Associate values to output bindings by calling 'Push-OutputBinding'.
Push-OutputBinding -Name Response -Value ([HttpResponseContext]@{
        StatusCode = [HttpStatusCode]::OK
        Body = $body
    })
````

In the example above we are retrieving the username and password from the settings as environment variables. We then create a new credentials object which we pass in to the `Connect-PnPOnline` cmdlet. After connecting to SharePoint we output the title of the web through the function.

Notice that we run the script in a separate thread/job. We do this because of possible conflicts between assemblies of already loaded PowerShell modules and PnP PowerShell (for instance, the Az cmdlets that get loaded by default use some of the same assemblies as PnP PowerShell but in different versions which can cause conflicts). By running the script in a separate thread we will not have these conflicts. If PnP PowerShell is the only module currently being used and loaded in your Azure Function you don't need the Start-ThreadJob construct and you can simply write the script as usual.
