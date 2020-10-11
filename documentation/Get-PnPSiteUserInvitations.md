---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpsiteuserinvitations
schema: 2.0.0
title: Get-PnPSiteUserInvitations
---

# Get-PnPSiteUserInvitations

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Searches against all stored sharing links and retrieves the email invites

## SYNTAX

```powershell
Get-PnPSiteUserInvitations [-Site] <SpoSitePipeBind> [-EmailAddress] <string>
   [<CommonParameters>]
```

## DESCRIPTION
Searches against all stored sharing links on a Site and retrieves the email invites. If the site parameter is omitted the current site will be searched.
## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPiteUserInvitations -Site https://contoso.sharepoint.com/sites/ContosoWeb1/ -EmailAddress someone@example.com
```

This example retrieves email invites stored in the ContosoWeb1 site to the user with email address someone@example.com.

## PARAMETERS

### -Site
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EmailAddress
Email Address of the user.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)