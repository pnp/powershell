---
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteSensitivityLabel.html
title: Set-PnPSiteSensitivityLabel
Module Name: PnP.PowerShell
applicable: SharePoint Online
---
  
# Set-PnPSiteSensitivityLabel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of InformationProtectionPolicy.Read (delegated), InformationProtectionPolicy.Read.All (application) when resolving a label by name
  * Microsoft Graph API: One of Group.ManageProtection.All (least privileged), Group.ReadWrite.All, Directory.ReadWrite.All (delegated) for a Microsoft 365 Group backed site

Allows placing a Microsoft Purview sensitivity label on the current site

## SYNTAX

```powershell
Set-PnPSiteSensitivityLabel -Identity <String> [-Connection <PnPConnection>] [-Verbose] 
```

## DESCRIPTION
This cmdlet allows for setting a Microsoft Purview sensitivity label on the currently connected to site. When `Identity` is a label name, the cmdlet resolves it through Microsoft Graph and requires InformationProtectionPolicy.Read (delegated) or InformationProtectionPolicy.Read.All (application). Microsoft Learn currently lists the label API as available only in the global service, so provide a label Id in sovereign clouds.

If the site has a Microsoft 365 Group behind it, the label will also be placed on the Microsoft 365 Group and requires Group.ManageProtection.All (least privileged), Group.ReadWrite.All, or Directory.ReadWrite.All delegated permission on Microsoft Graph. The signed-in user must also hold a [supported administrator role](https://learn.microsoft.com/purview/get-started-with-sensitivity-labels#permissions-required-to-create-and-manage-sensitivity-labels). This currently cannot be done using application permissions due to a limitation in Microsoft Graph. If it does not have a Microsoft 365 Group behind it, the cmdlet sets the label on the SharePoint Online site and supports both delegated and app-only logins. If you're looking to set a sensitivity label on a Microsoft 365 Group backed site in an app-only context, you can use [Set-PnPTenantSite -SensitivityLabel](Set-PnPTenantSite.md#-sensitivitylabel) instead.

It may take up to a few minutes for a change to the sensitivity label to become visible in SharePoint Online and Entra ID / Azure Active Directory.  

Use [Get-PnPAvailableSensitivityLabel](Get-PnPAvailableSensitivityLabel.md) to get an overview of the available Microsoft Purview sensitivity labels on the tenant.  

For the classic classification labels, use [Set-PnPSiteClassification](Set-PnPSiteClassification.md) instead.

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

