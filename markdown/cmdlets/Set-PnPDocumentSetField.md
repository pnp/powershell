---
Module Name: PnP.PowerShell
title: Set-PnPDocumentSetField
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPDocumentSetField.html
---
 
# Set-PnPDocumentSetField

## SYNOPSIS
Sets a site column from the available content types to a document set

## SYNTAX

```powershell
Set-PnPDocumentSetField -DocumentSet <DocumentSetPipeBind> -Field <FieldPipeBind> [-SetSharedField]
 [-SetWelcomePageField] [-RemoveSharedField] [-RemoveWelcomePageField] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to set a site column from the available content types to a document set.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPDocumentSetField -Field "Test Field" -DocumentSet "Test Document Set" -SetSharedField -SetWelcomePageField
```

This will set the field, available in one of the available content types, as a Shared Field and as a Welcome Page Field.

### EXAMPLE 2
```powershell
Set-PnPDocumentSetField -Field "Test Field" -DocumentSet "Test Document Set" -RemoveSharedField -RemoveWelcomePageField
```

This will remove the field, available in one of the available content types, as a Shared Field and as a Welcome Page Field.

## PARAMETERS

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

### -DocumentSet
The document set in which to set the field. Either specify a name, a document set template object, an id, or a content type object

```yaml
Type: DocumentSetPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Field
The field to set. The field needs to be available in one of the available content types. Either specify a name, an id or a field object

```yaml
Type: FieldPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveSharedField
Removes the field as a Shared Field

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveWelcomePageField
Removes the field as a Welcome Page Field

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SetSharedField
Set the field as a Shared Field

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SetWelcomePageField
Set the field as a Welcome Page field

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

