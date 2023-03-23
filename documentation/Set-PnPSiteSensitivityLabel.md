---
Module Name: PnP.PowerShell
title: Set-PnPSiteSensitivityLabel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteSensitivityLabel.html
---
 
# Set-PnPSiteSensitivityLabel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Delegate token of Group.ReadWrite.All, Directory.ReadWrite.All (see description below)

Allows placing a Microsoft Purview sensitivity label on the current site

## SYNTAX

```powershell
Set-PnPSiteSensitivityLabel -Identity <String> [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet allows for setting a Microsoft Purview sensitivity label on the currently connected to site. If the site has a Microsoft 365 Group behind it, the label will be placed on the Microsoft 365 Group and will require either Group.ReadWrite.All or Directory.ReadWrite.All delegate permissions on Microsoft Graph. This currently cannot be done using App Only permissions due to a limitation in Microsoft Graph. If it does not have a Microsoft 365 Group behind it, it will set the label on the SharePoint Online site and will not require Microsoft Graph permissions and will work with both delegate as well as app only logins.

It may take up to a few minutes for a change to the sensitivity label to become visible in SharePoint Online and Azure Active Directory.

Use [Get-PnPAvailableSensitivityLabel](Get-PnPAvailableSensitivityLabel.html) to get an overview of the available Microsoft Purview sensitivity labels on the tenant.

For the classic classification labels, use [Set-PnPSiteClassification](Set-PnPSiteClassification.html) instead.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSiteSensitivityLabel -Identity "Top Secret"
```

Sets the Microsoft Purview sensitivity label with the name "Top Secret" on the current site

### EXAMPLE 2
```powershell
Set-PnPSiteSensitivityLabel -Identity a1888df2-84c2-4379-8d53-7091dd630ca7
```

Sets the Microsoft Purview sensitivity label with the Id a1888df2-84c2-4379-8d53-7091dd630ca7 on the current site

## PARAMETERS

### -Identity
Id or name of the Microsoft Purview sensitivity label to apply

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: True
Accept pipeline input: True
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

### -Verbose
When provided, additional debug statements will be shown while going through the execution of this cmdlet.

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-update?view=graph-rest-beta&tabs=http#example-2-apply-sensitivity-label-to-a-microsoft-365-group)