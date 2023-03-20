# Connecting with PnP PowerShell

PnP PowerShell offers many ways to connect to an environment. This page provides guidance on the various options you have and how they can be used against which environment(s).


## Connect using credentials

In order to connect with credentials you have two options: 
### Connect by using the PnP Management Shell Multi-Tenant Azure AD Application

You will have to consent / register the PnP Management Shell Multi-Tenant Azure AD Application in your own tenant:

```powershell
Register-PnPManagementShellAccess
```

This will launch a device login flow that will ask you to consent to the application. Notice that is only required -once- per tenant. You will need to have appropriate access rights to be able to consent applications in your Azure AD.

After that you can authenticate using

```powershell
Connect-PnPOnline [tenant].sharepoint.com -Credentials (Get-Credential)
```

or in case the account you would like to use has MFA or any other authentication provider configured for it, instead use:

```powershell
Connect-PnPOnline [tenant].sharepoint.com -Interactive
```

### Connect by using your own Azure AD Application

You will have to create your own Azure AD Application registration, or you can create one:

```powershell
Register-PnPAzureADApp -ApplicationName "YourApplicationName" -Tenant [tenant].onmicrosoft.com -Interactive
```

This will launch a authentication dialog where you need to authenticate. After closing this window the cmdlet will continue to register a new application with a set of default permissions. By default a certificate will be generated and stored in the current folder, named after the application you want to create. You can specify your own certificate by using the `-CertificatePath` parameter and optional `-CertificatePassword` parameter.

You can add permissions by using the `-GraphApplicationPermissions`, `-GraphDelegatePermissions`, `-SharePointApplicationPermissions` or `-SharePointDelegatePermissions` parameters. The cmdlet will output the Azure AppId/client id, the name and location of the certificates created (if any) and the thumbprint of the certificate. It is possible to add the certificate created to the certificate management store in Windows by adding the `-Store` parameter.

Note if you are using Credential Based Authentication, you will need to make a change to the app registration manifest file. Go to the app registration, select Manifest under the Manage section, then change the "allowPublicClient" property to true and click save.

```powershell
Connect-PnPOnline [tenant].sharepoint.com -Credentials (Get-Credential) -ClientId [clientid]
```

## Connect interactively using WebLogin supporting MFA

One of the easiest methods to use. However, notice that this connection method will have its limitation as we will utility cookie based authentication. For instance, we will not be able to make calls to the Microsoft Graph behind the scenes. 

```powershell
Connect-PnPOnline [tenant].sharepoint.com -UseWebLogin
```

## Connect using a ClientId and PFX certificate stored on your local machine

Allows using an Azure Active Directory app registration from your own Azure Active Directory with a certificate to connect. The private key certificate, typically the .pfx file, should be accessible on your local machine. 

The following will generate an Azure AD Application registration and create a certificate containing a public and private key.

```powershell
Register-PnPAzureADApp -ApplicationName "PnPPowerShell" -Tenant tenant.onmicrosoft.com -Password (ConvertTo-SecureString -String "password" -AsPlainText -Force)
```

You will be asked to authenticate. After that the cmdlet will generate two files, PnPPowerShell.pfx and PnPPowerShell.cer and a new Azure AD Application will be registered with the specified name. The public key/CER file will be uploaded and registered with the newly create application registration. You will have to use the .pfx file to connect. Notice that the `Register-PnPAzureADApp` cmdlet only have to be executed once per tenant/application.

```powershell
Connect-PnPOnline [tenant].sharepoint.com -ClientId [clientid] -Tenant tenant.onmicrosoft.com -CertificatePath '.\PnPPowerShell.pfx' -CertificatePassword (ConvertTo-SecureString -AsPlainText -Force "password")
```

## Connect using a ClientId and PFX certificate stored in the Windows Certificate Management Store

Allows using an Azure Active Directory app registration from your own Azure Active Directory with a certificate to connect. The private key certificate, typically the .pfx file, should be accessible on your local machine in the Certificate Management Store.

The following will generate an Azure AD Application registration and create a certificate containing a public and private key which will be stored for the current user in the Windows Certificate Management Store.
```powershell
$password = ConvertTo-SecureString -String "password" -AsPlainText -Force
Register-PnPAzureADApp -ApplicationName "PnPPowerShell" -Tenant [tenant].onmicrosoft.com -Store CurrentUser
```

You will be asked to authenticate. After that the cmdlet will generate a certificate and will store it in the Windows Certificate Management Store and a new Azure AD Application will be registered with the specified name. The public key of the certificate file will be uploaded and registered with the newly create application registration. Notice that the `Register-PnPAzureADApp` cmdlet only have to be executed once per tenant/application. The output of the cmdlet contains the thumbprint to use.

```PowerShell
Connect-PnPOnline [tenant].sharepoint.com -ClientId [clientid] -Tenant [tenant].onmicrosoft.com -Thumbprint $thumbprint
```

## Connect using a ClientId and PFX certificate being Base64 encoded

In some scenarios it might be easier to have the PFX file being encoded as a string using Base64 as opposed to having to store the physical PFX file somewhere. If you have the PFX encoded using Base64 encoding, you can connect using:

```PowerShell
Connect-PnPOnline [tenant].sharepoint.com -ClientId [clientid] -Tenant [tenant].onmicrosoft.com -CertificateBase64Encoded $encodedPfx
```

If you wish to convert a PFX file to its Base64 encoded equivallent, you can use:

```PowerShell
$bytes = Get-Content '.\PnPPowerShell.pfx' -AsByteStream
$encodedPfx = [System.Convert]::ToBase64String($bytes)
```

## Connect to a National Cloud Deployment (GCC/Germany/China)

If you are on a National Cloud instance, read the [authentication](authentication.md) article for more information
