---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAvailableSensitivityLabel.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAvailableSensitivityLabel
---
  
# Get-PnPAvailableSensitivityLabel

## SYNOPSIS
Gets the Microsoft Purview sensitivity labels that are available within the tenant

## SYNTAX

```powershell
Get-PnPAvailableSensitivityLabel [-Identity <Guid>] [-User <AzureADUserPipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows retrieval of the available Microsoft Purview sensitivity labels in the currently connected tenant. You can retrieve all the labels, a specific label or all the labels available to a specific user. When connected with a delegate token, it will return the Microsoft Purview sensitivity labels for the user you logged on with. When connecting with an application token, it will return all available Microsoft Purview sensitivity labels on the tenant.

For retrieval of the available classic Site Classification, use [Get-PnPAvailableSiteClassification](Get-PnPAvailableSiteClassification.html) instead.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAvailableSensitivityLabel
```

Returns all the Microsoft Purview sensitivity labels that exist on the tenant

### EXAMPLE 2
```powershell
Get-PnPAvailableSensitivityLabel -User johndoe@tenant.onmicrosoft.com
```

Returns all Microsoft Purview sensitivity labels which are available to the provided user

### EXAMPLE 3
```powershell
Get-PnPAvailableSensitivityLabel -Identity 47e66706-8627-4979-89f1-fa7afeba2884
```

Returns a specific Microsoft Purview sensitivity label by its id

## PARAMETERS

### -Identity
The Id of the Microsoft Purview sensitivity label to retrieve

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
The UPN, Id or instance of an Azure AD user for which you would like to retrieve the Microsoft Purview sensitivity labels available to this user

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/informationprotectionpolicy-list-labels)