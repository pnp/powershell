---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantPronounsSettings.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPTenantPronounsSettings
---
  
# Set-PnPTenantPronounsSettings

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : PeopleSettings.ReadWrite.All
  
Allows allowing configuring the tenant-wide pronouns settings

## SYNTAX

```powershell
Set-PnPTenantPronounsSettings [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows allowing or disallowing users configuring their own desired pronouns in their user profile.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTenantPronounsSettings -IsEnabledInOrganization:$true
```

Enables allowing users to configure their own desired pronouns in their user profile.

### EXAMPLE 2
```powershell
Set-PnPTenantPronounsSettings -IsEnabledInOrganization:$false
```

Disables users from configuring their own desired pronouns in their user profile.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsEnabledInOrganization
Provide $true to allow end users to set their desired pronounce, provide $false to prevent end users from setting their desired pronouns.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/peopleadminsettings-list-pronouns)