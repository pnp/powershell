---
Module Name: PnP.PowerShell
title: New-PnPAzureADUserTemporaryAccessPass
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPAzureADUserTemporaryAccessPass.html
---
 
# New-PnPAzureADUserTemporaryAccessPass

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : UserAuthenticationMethod.ReadWrite.All

Creates a temporary access pass to authenticate with for a certain user

## SYNTAX

```powershell
New-PnPAzureADUserTemporaryAccessPass -DisplayName <String> -Description <String> -MailNickname <String> [-Owners <String[]>] [-Members <String[]>] [-IsSecurityEnabled <SwitchParameter>] [-IsMailEnabled <SwitchParameter>] [-Force] 
```

## DESCRIPTION
This cmdlet allows creation of a temporary access pass for a specific user to allow the user to log on once using the generated token. This can be used i.e. when the user needs to sign in to replace the multi factor authentication token.

You can read more on how to enable Temporary Access Pass in Azure Active Directory in [this article](https://learn.microsoft.com/azure/active-directory/authentication/howto-authentication-temporary-access-pass). It is disabled by default on Azure Active Directory.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPAzureADUserTemporaryAccessPass -Identity johndoe@contoso.onmicrosoft.com
```

Creates a temporary access pass for the user with the provided user principal name following the default configuration in Azure Active Directory towards the access pass its lifetime, password length and reusability which can directly be used.

### EXAMPLE 2
```powershell
New-PnPAzureADUserTemporaryAccessPass -Identity 72e2eb87-c124-4bd9-8e01-a447a1752058 -IsUseableOnce:$true
```

Creates a temporary access pass for the user with the provided user Id following the default configuration in Azure Active Directory towards the access pass its lifetime and password length. The token will only be able to be used once and will then immediately expire and can directly be used.

### EXAMPLE 3
```powershell
New-PnPAzureADUserTemporaryAccessPass -Identity johndoe@contoso.onmicrosoft.com -StartDateTime (Get-Date).AddHours(2) -LifeTimeInMinutes 10 -IsUseableOnce:$true
```

Creates a temporary access pass for the user with the provided user principal name which will not become valid for use until 2 hours from now has passed. It will then only be valid for 10 minutes and only can be used once to login after which it will immediately expire, regardless if there are minutes left in the `-LifeTimeInMinutes` parameter.

### EXAMPLE 4
```powershell
Get-PnPAzureADUser -Identity johndoe@contoso.onmicrosoft.com | New-PnPAzureADUserTemporaryAccessPass -StartDateTime (Get-Date).AddMinutes(10) -LifeTimeInMinutes 15 -IsUseableOnce:$false
```

Creates a temporary access pass for the user with the provided user principal name which will not become valid for use until 10 minutes from now has passed. It will then only be valid for 15 minutes and only can be used repeatedly to login while there are minutes left in the `-LifeTimeInMinutes` parameter.

## PARAMETERS

### -Identity
The user principal name, user Id or user instance for which to generate a temporary access pass.

```yaml
Type: AzureADUserPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -StartDateTime
Date and time at which the access pass should become valid. If not provided, the access pass will immediately be valid.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LifeTimeInMinutes
Time in minutes counting from the moment the access pass has become active, how long it will be valid until it will expire and cannot be used anymore. IF not provided, the configured default in Azure Active Directory will be used.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsUseableOnce
Allows defining if the access token can only be used once to log on after which it will directly expire. This takes precedence over the `-LifeTimeInMinutes` option. If not provided, the configured default in Azure Active Directory will be used.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/temporaryaccesspassauthenticationmethod-post)
[Feature description](https://learn.microsoft.com/azure/active-directory/authentication/howto-authentication-temporary-access-pass)