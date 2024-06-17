---
Module Name: PnP.PowerShell
title: Set-PnPPageTextPart
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPPageTextPart.html
---
 
# Set-PnPPageTextPart

## SYNOPSIS
Sets text part properties.

## SYNTAX

```powershell
Set-PnPPageTextPart -Page <PagePipeBind> -InstanceId <Guid> -Text <String>
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Sets the rendered text in existing client side text component.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPPageTextPart -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82 -Text "MyText"
```

Sets the text of the client side text component.

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

### -InstanceId
The instance id of the text component.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Page
The name of the page.

```yaml
Type: PagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Text
Text to set.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

