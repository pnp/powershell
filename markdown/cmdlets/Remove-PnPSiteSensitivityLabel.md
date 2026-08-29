---
Module Name: PnP.PowerShell
schema: 2.0.0
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPSiteSensitivityLabel
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteSensitivityLabel.html
---
 
# Remove-PnPSiteSensitivityLabel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Group.ManageProtection.All (least privileged), Group.ReadWrite.All, Directory.ReadWrite.All (delegated) for a Microsoft 365 Group backed site

Removes the Microsoft Purview sensitivity label from the current site.

## SYNTAX

```powershell
Remove-PnPSiteSensitivityLabel [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION

Removes the Microsoft Purview sensitivity label from the current site. If the site is backed by a Microsoft 365 Group, the label is also removed from the group through Microsoft Graph. Updating the group requires Group.ManageProtection.All (least privileged), Group.ReadWrite.All, or Directory.ReadWrite.All delegated permission, together with a [supported administrator role](https://learn.microsoft.com/purview/get-started-with-sensitivity-labels#permissions-required-to-create-and-manage-sensitivity-labels). The operation is not supported with application permissions. Removing the label from a site that is not group backed does not require Microsoft Graph permissions.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPSiteSensitivityLabel
```

Removes the Microsoft Purview sensitivity label from the current site and its associated Microsoft 365 Group, if present.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying `-ReturnConnection` on `Connect-PnPOnline` or by executing `Get-PnPConnection`.

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
When provided, additional debug statements will be shown while the cmdlet executes.

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

[Update group - Microsoft Graph](https://learn.microsoft.com/graph/api/group-update?view=graph-rest-beta)

