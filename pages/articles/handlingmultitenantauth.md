# How to handle authenticating to multiple tenants

One question that may come up when working with multiple tenants is how to handle authenticating to multiple tenants, especially since we now require you to register your own Entra ID Application Registration. This is a common scenario when you have a separate development, test and production tenant setup. Or if you are working with multiple customers' tenants. This can be challenging because each tenant requires its own application registration, having it's own application ID / Client ID and may have different authentication requirements, such as different identity providers or different authentication mechanisms.

In this article, we will try to inspire you on some methods you could apply to handle authenticating to multiple tenants in more convient ways.

## How could this be a challenge?

One might wonder why this could even be a challenge. After all, you could just [create an application registration](registerapplication.md) in each tenant and use the corresponding application ID / Client ID when authenticating.

Let's suppose you do so and say we have created these:

Client ID | Tenant | Purpose
--- | --- | ---
`12345678-1234-1234-1234-123456789012` | contosodev.onmicrosoft.com | Development Tenant
`23456789-2345-2345-2345-234567890123` | contosotest.onmicrosoft.com | Test Tenant
`34567890-3456-3456-3456-345678901234` | contoso.onmicrosoft.com | Production Tenant

You now have multiple application registrations, each with its own application ID / Client ID. This means you need to keep track of all these application IDs / Client IDs and use the correct one when authenticating. This can be cumbersome and error-prone, especially if you have many tenants. When connecting to one of these tenants, you will need to connect like this:

```powershell
Connect-PnPOnline contosodev.sharepoint.com -Interactive -ClientId 12345678-1234-1234-1234-123456789012
Connect-PnPOnline contosotest.sharepoint.com -Interactive -ClientId 23456789-2345-2345-2345-234567890123
Connect-PnPOnline contoso.sharepoint.com -Interactive -ClientId 34567890-3456-3456-3456-345678901234
```

This could lead to mistakes, such as using the wrong application ID / Client ID when connecting to a tenant. This could lead to authentication errors, which might lead to confusion and frustration.

## Idea 1: Create your own function to encapsulate the connection logic

One option is to create your on functions which allow you to connect a specific environment and have the function pass in the required parameters for connecting to it, such as the ClientId and the Certificate to use.

You could easily establish this by opening PowerShell 7 and executing:

### Windows 
```powershell
notepad $profile
```
### Linux/Mac
```powershell
vi $profile
```

Add to your profile functions, such as:

```powershell
Function Connect-PnPDevTenant
{
	Connect-PnPOnline https://contosodev.sharepoint.com -ClientId 12345678-1234-1234-1234-123456789012
}

Function Connect-PnPProdTenant
{
	Connect-PnPOnline https://contoso.sharepoint.com -ClientId 34567890-3456-3456-3456-345678901234
}
```

Save the file and start a new PowerShell 7 session to have the updated profile being loaded into the session.

Now you can simply connect to your tenant by executing:

```powershell
Connect-PnPDevTenant
```

And it will execute the cmdlet as configured in your profile instead.

## Idea 2: Create your own multi tenant application registration

When you [create an Entra ID Application registration](registerapplication.md), either manually or using PnP PowerShell, you can configure or switch it to be used on multiple tenants.

If the Entra ID Application registration has already been created:
1. Go to [https://entra.microsoft.com](https://entra.microsoft.com)
2. In the menu on the left, expand Identity > Applications and click on App registrations
3. Locate the Entra ID Application registration you wish to switch to become a multi tenant application registration
4. Go to Authentication
5. Under "Supported account types", select "Accounts in any organizational directory (Any Microsoft Entra ID tenant - Multitenant)" and click on Save at the bottom

You can now use the Client ID of this application with any tenant. Restrictions may apply, based on the specific configuration of the tenants you wish to use it against.

## Idea 3: Utilize the credential manager to store your client IDs

See [this article](credentialmanagement.md) on how to use the Credential Manager with PnP PowerShell.

## Idea 4: Use persisted logins

See [this article](persistedlogin.md) on how to use the persisted login feature that has been added in PnP PowerShell version 3.

## Other ideas?

If you have any other idea to easily work with multiple tenants, feel free to edit this article, add your ideas, and submit it through a Pull Request.
