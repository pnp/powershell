---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Export-PnPTermGroupToXml.html
external help file: PnP.PowerShell.dll-Help.xml
title: Export-PnPTermGroupToXml
---
  
# Export-PnPTermGroupToXml

## SYNOPSIS
Exports a taxonomy TermGroup to either the output or to an XML file.

## SYNTAX

```powershell
Export-PnPTermGroupToXml [-Identity <TermGroupPipeBind>] [-Out <String>] [-FullTemplate] [-Encoding <Encoding>]
 [-Force] [-Connection <PnPConnection>]   
```

## DESCRIPTION

Allows to export a taxonomy TermGroup to either the output or to an XML file.

## EXAMPLES

### EXAMPLE 1
```powershell
Export-PnPTermGroupToXml
```

Exports all term groups in the default site collection term store to the standard output

### EXAMPLE 2
```powershell
Export-PnPTermGroupToXml -Out output.xml
```

Exports all term groups in the default site collection term store to the file 'output.xml' in the current folder

### EXAMPLE 3
```powershell
Export-PnPTermGroupToXml -Out c:\output.xml -Identity "Test Group"
```

Exports the term group with the specified name to the file 'output.xml' located in the root folder of the C: drive.

### EXAMPLE 4
```powershell
$termgroup = Get-PnPTermGroup -Identity Test
$termgroup | Export-PnPTermGroupToXml -Out c:\output.xml
```

Retrieves a termgroup and subsequently exports that term group to a the file named 'output.xml'

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

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

### -Encoding
Defaults to Unicode

```yaml
Type: Encoding
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Overwrites the output file if it exists.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FullTemplate
If specified, a full provisioning template structure will be returned

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID or name of the termgroup

```yaml
Type: TermGroupPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Out
File to export the data to.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


