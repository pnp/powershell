---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Grant-PnPAzureADAppSitePermission.html
external help file: PnP.PowerShell.dll-Help.xml
title: Grant-PnPAzureADAppSitePermission
---
  
# Grant-PnPAzureADAppSitePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.FullControl.All

Adds permissions for a given Azure Active Directory application registration.

## SYNTAX

```powershell
Grant-PnPAzureADAppSitePermission -AppId <Guid> -DisplayName <String> -Permissions <Read|Write> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet adds permissions for a given Azure Active Directory application registration in a site collection. It is used in conjunction with the Azure Active Directory SharePoint application permission Sites.Selected. Notice that this cmdlet allows for fewer permissions compared to updating rights through [Set-PnPAzureADAppSitePermission](Set-PnPAzureADAppSitePermission.md). If you wish to i.e. assign FullControl permissions, you need to add read or write permissions through this cmdlet first and then update it to FullControl.

## EXAMPLES

### EXAMPLE 1
```powershell
Grant-PnPAzureADAppSitePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Read
```

Adds permissions for the Azure Active Directory application registration with the specific application id and sets the rights to 'Read' access for the currently connected site collection.

### EXAMPLE 2
```powershell
Grant-PnPAzureADAppSitePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Write -Site https://contoso.sharepoint.com/sites/projects
```

Adds permissions for the Azure Active Directory application registration with the specific application id and sets the rights to 'Write' access for the site collection at the provided URL.

## PARAMETERS

### -AppId
Specify the AppId of the Azure Active Directory application registration to grant permission for.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The display name to set for the application permission you're adding. Only for visual reference purposes, does not need to match the name of the application in Azure Active Directory.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Permissions
Specifies the permissions to set for the Azure Active Directory application registration which can either be Read or Write. Use [Set-PnPAzureADAppSitePermission](Set-PnPAzureADAppSitePermission.md) after initially adding these permissions to update it to Manage or FullControl permissions.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Accepted values: Read, Write
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
Optional url of a site to set the permissions for. Defaults to the current site if not provided.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: Currently connected site
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
