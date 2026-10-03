---
Module Name: PnP.PowerShell
title: Set-PnPListRecordDeclaration
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPListRecordDeclaration.html
---
 
# Set-PnPListRecordDeclaration

## SYNOPSIS
Updates record declaration settings of a list.

## SYNTAX

```powershell
Set-PnPListRecordDeclaration -List <ListPipeBind> [-ManualRecordDeclaration <EcmListManualRecordDeclaration>]
 [-AutoRecordDeclaration <Boolean>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
The RecordDeclaration parameter supports 3 values:

* AlwaysAllowManualDeclaration
* NeverAllowManualDeclaration
* UseSiteCollectionDefaults


## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPListRecordDeclaration -List "Documents" -ManualRecordDeclaration NeverAllowManualDeclaration
```

Sets the manual record declaration to never allow.

### EXAMPLE 2
```powershell
Set-PnPListRecordDeclaration -List "Documents" -AutoRecordDeclaration $true
```

Turns on auto record declaration for the list.

## PARAMETERS

### -AutoRecordDeclaration
Turns on or off auto record declaration on the list.

```yaml
Type: Boolean
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

### -List
The list to set the manual record declaration settings for. Specify title, list id, or list object.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ManualRecordDeclaration
Defines the manual record declaration setting for the lists.

```yaml
Type: EcmListManualRecordDeclaration
Parameter Sets: (All)
Accepted values: UseSiteCollectionDefaults, AlwaysAllowManualDeclaration, NeverAllowManualDeclaration

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

