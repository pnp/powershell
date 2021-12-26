# Using PnP PowerShell in Azure Functions

In this article we will setup an Azure Function to use PnP PowerShell

> [!Important]
> Notice that the Azure Function scripts in this article run in a separate thread/job. We do this because of possible conflicts between assemblies of already loaded PowerShell modules and PnP PowerShell (for instance, the Az cmdlets that get loaded by default use some of the same assemblies as PnP PowerShell but in different versions which can cause conflicts). By running the script in a separate thread we will not have these conflicts. If PnP PowerShell is the only module currently being used and loaded in your Azure Function you don't need the Start-ThreadJob construct and you can simply write the script as usual.

## Create the function app

As the UI in https://portal.azure.com changes every now and then, but the principles stay the same, follow the following steps:

1. Create a new Function App and navigate there when ready.
1. Make sure you select the option to run PowerShell V3 functions, based upon PowerShell 7.

## Make PnP PowerShell available to all functions in the app

1. Navigate to `App files` which is located the left side menu of the function app under the `Functions` header.
1. In the dropdown presented, select `requirements.psd1`. You'll notice that the function app wants to provide the Azure cmdlets. If you do not need those, keep the `Az` entry presented commented out.
1. Add a new entry or replace the whole contents of the file with:
 
   ```powershell
   @{
       'PnP.PowerShell' = '1.9.0'
   }
   ```
   The version that will be installed will be the specified specific build, which is generally recommended. You build and test your Azure Function against this specific PnP PowerShell version. Future releases may work differently and cause issues, therefore it is generally recommended to specify a specific version here.
   
   If, for some reason, you would like to ensure it is always using the latest available PnP PowerShell version, you can also specify a wildcard in the version:

    ```powershell
    @{
        'PnP.PowerShell' = '1.*'
     }
    ```
   This will then automatically download any minor version of the major 1 release when available. Notice that you cannot use wildcards to specify a nightly build.

1. Save the `requirements.psd1` file 

1. If you decide to keep the Az cmdlets commented out, save and edit the `profile.psd` file. Mark out the following block in the file as follows, if not already done:

   ```powershell
   # if ($env:MSI_SECRET) {
   #     Disable-AzContextAutosave -Scope Process | Out-Null     
   #     Connect-AzAccount -Identity
   # }
   ```

   Save the `profile.psd` file.

## Decide how you want to authenticate in your PowerShell Function

### By using Credentials

#### Create your credentials
1. Navigate to `Configuration` under `Settings` and create a new Application Setting. 
1. Enter `tenant_user` and enter the username you want to authenticate with as the user
1. Enter `tenant_pwd` and enter the password you want to use for that user

#### Create the function

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

### By using a certificate

#### Create your certificate

In this following example we create a new Azure AD Application registration which creates your certificates. You can of course do all this work manually too with your own certificates.

```powershell
$password = Read-Host -Prompt "Enter certificate password" -AsSecureString
Register-PnPAzureADApp -ApplicationName "MyDemoApp" -Tenant [yourtenant.onmicrosoft.com] -CertificatePassword $password -Interactive
```

You will be asked to authenticate. Log in using an account that has the permissions to create an app registration in your Azure Active Directory. After logging in, the following actions will automatically be taken:

- An app registration will be created using the provided name
- A self signed certificate will be generated which includes a pfx and a cer file (private/public key pair)
- The public key of the certificate (cer) will be configured as a valid certificate to authenticate with for the app registration
- A basic set of permissions will be assigned to the app registration. These can freely be changed at will at a later time.
- Admin consent will be given to the given set of permissions

Make a note of the clientid shown and proceed with the steps in the following section.

#### Apply your certificate

Once you have an Azure Active Directory application set up and the public key certificate uploaded to its registration, proceed with configuring the Azure Function to make use of the private key of this certificate pair:

1. In your function app, navigate to `TLS/SSL Settings` and switch to the `Private Key Certificates (.pfx)` section.
1. Click `Upload Certificate` and select the "MyDemoApp.pfx" file that has been created for you. Enter the password you used in the script above.
1 After the certificate has been uploaded, copy the thumbprint value shown.
1 Navigate to `Configuration` and add a new Application Setting
1. Call the setting `WEBSITE_LOAD_CERTIFICATES` and set the thumbprint as a value. To make all the certificates you uploaded available use `*` as the value. See https://docs.microsoft.com/en-gb/azure/app-service/configure-ssl-certificate-in-code for more information.
1. Save the settings

#### Create the Azure Function

Create a new function and replace the function code with the following example:

```powershell
using namespace System.Net

# Input bindings are passed in via param block.
param($Request, $TriggerMetadata)

# Write to the Azure Functions log stream.
Write-Host "PowerShell HTTP trigger function processed a request."

$script = {
    Connect-PnPOnline -Url https://yourtenant.sharepoint.com/sites/demo -ClientId [the clientid created earlier] -Thumbprint [the thumbprint you copied] -Tenant [yourtenant.onmicrosoft.com]

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
```

### By using a Managed Identity

Yet another option is to use a [managed identity in Azure](https://docs.microsoft.com/azure/active-directory/managed-identities-azure-resources/overview) to allow your Azure Function to connect to Microsoft Graph using PnP PowerShell. Using this method, you specifically grant permissions for your Azure Function to access these permissions, without having any client secret or certificate pair that potentially could fall into wrong hands. This makes this option the most secure option by far. There is a downside to this approach, however. At present, only permissions can be granted to the Microsoft Graph and not to the SharePoint APIs, which effectively means that most of the PnP PowerShell cmdlets will not work. Only those solely and directly communicating with the Microsoft Graph, will be authorized to work, such as, but not limited to: `Get-PnPAzureAdUser`, `Get-PnPMicrosoft365Group`, `Get-PnPTeamsTeam`.

#### Enabling the managed identity

1. In your Azure Function, in the left menu, go to Identity
1. Ensure you are on the System assigned tab and flip the switch for Status to On
1. Click the save button and confirm your action in the dialog box that will be shown

A new entry will now automatically be created in your Azure Active Directory for this app having the same name as your Azure Function and the Object (principal) ID shown on this page. Take notice of the Object (principal) ID. We will need it in the next section to assign permissions to.

#### Assigning Microsoft Graph permissions to the managed identity

Next step is to assign permissions to this managed identity so it is authorized to access the Microsoft Graph.

1. Ensure you're having the Azure PowerShell management module installed on your environment. You can install it using:

   ```powershell
   Install-Module AzureAD
   ```
   
1. Connect to the Azure instance where your Azure Function runs and of which you want to use the Microsoft Graph through PnP PowerShell

    ```powershell
    Connect-AzureAD -TenandId <contoso>.onmicrosoft.com
    ```
    
1. Retrieve the Azure AD Service Principal instance for the Microsoft Graph. It should always be AppId 00000003-0000-0000-c000-000000000000.

   ```powershell
   $graphServicePrincipal = Get-AzureADServicePrincipal -SearchString "Microsoft Graph" | Select-Object -First 1
   ```
   
1. Using the following PowerShell cmdlet you can list all the possible Microsoft Graph permissions you can give your Azure Function through the Managed Identity. This list will be long. Notice that we are specifically querying for application permissions. Delegate permissions cannot be utilized using a Managed Identity.

   ```powershell
   $graphServicePrincipal.AppRoles | Where-Object { $_.AllowedMemberTypes -eq "Application" }
   ```
   
1. Pick a permission which you would like to grant your Azure Function to have towards the Microsoft Graph, i.e. `Group.Read.All`.

   ```powershell
   $appRole = $graphServicePrincipal.AppRoles | Where-Object { $_.AllowedMemberTypes -eq "Application" -and $_.Value -eq "Group.Read.All" }
   ```
   
1. Now assign this permission to the Azure Active Directory app registration that has been created automatically by enabling the managed identity on the Azure Function in the steps above:

   ```powershell
   $managedIdentityId = "<Object (principal) ID of the Azure Function generated in the previous section>"
   New-AzureAdServiceAppRoleAssignment -ObjectId $managedIdentityId -PrincipalId $managedIdentityId -ResourceId $graphServicePrincipal.ObjectId -Id $appRole.Id
   
   ```

#### Create the Azure Function

Create a new function and replace the function code with the following example:

```powershell
using namespace System.Net

param($Request, $TriggerMetadata)

Connect-PnPOnline -ManagedIdentity
Get-PnPMicrosoft365Group

Push-OutputBinding -Name Response -Value ([HttpResponseContext]@{
    StatusCode = [HttpStatusCode]::OK
})

```

Notice the super clean and simple `Connect-PnPOnline`. No identifiers whatsoever need to be provided. Nothing that could fall into wrong hands, no client secret or certificate that could expire. Based on the permissions assigned to the managed identity, it will be able to authenticate and authorize access to the Microsoft Graph APIs used behind the cmdlet to fetch the data.
