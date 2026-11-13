---
Module Name: PnP.PowerShell
title: Remove-PnPSiteUserInvitations
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteUserInvitations.html
---
 
# Remove-PnPSiteUserInvitations

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Searches against all stored sharing links and removes an email invite.

## SYNTAX

```powershell
Remove-PnPSiteUserInvitations [[-Site] <SitePipeBind>] [-EmailAddress] <string> [-Connection <PnPConnection>]
```

## DESCRIPTION
Searches against all stored sharing links on a Site and removes an email invites. If the site parameter is omitted the current site will be searched.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSiteUserInvitations -Site "https://contoso.sharepoint.com/sites/ContosoWeb1/" -EmailAddress someone@example.com
```

This example removes the email invite stored in the ContosoWeb1 site for the user with email address someone@example.com.

## PARAMETERS

### -Site
Specifies the URL of the site collection.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

