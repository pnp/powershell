---
Module Name: PnP.PowerShell
title: Set-PnPContentType
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPContentType.html
---
 
# Set-PnPContentType

## SYNOPSIS

Updates a content type in a web or a list

## SYNTAX

```powershell
Set-PnPContentType [-Identity] <ContentTypePipeBind> [-List] <ListPipeBind> [-InSiteHierarchy] <SwitchParameter>
[-UpdateChildren] <SwitchParameter> [-Name] <String> [-Description] <String> [-Group] <String>
[-Hidden] <String> [-ReadOnly] <String> [-Sealed] <String>
 [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
```

## DESCRIPTION

Allows modification of the settings of a content type in a list or site.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPContentType -Identity "Project Document" -UpdateChildren -Name "Project Documentation" -Description "Documentation for projects"
```

This will update a content type called "Project Document" in the current web and rename it to "Project Documentation" and change its description to "Documentation for projects"

### EXAMPLE 2

```powershell
Set-PnPContentType -Identity "Project Document" -UpdateChildren -Group "Custom Content Types" -Hidden
```

This will update a content type called "Project Document" in the current web, make it hidden and change its group to "Custom Content Types".

### EXAMPLE 3

```powershell
Set-PnPContentType -Identity "Project Document" -List "Projects" -Name "Project Documentation" -Description "Documentation for projects"
```

This will update a content type called "Project Document" in the list called "Projects" in the current web and rename it to "Project Documentation" and change its description to "Documentation for projects".

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

### -Identity

The name or ID of the content type to update

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -List

The list in which the content type to be updated resides.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InSiteHierarchy

Search site hierarchy for content types

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UpdateChildren

Specify if you want to update the child content types

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name

The updated name of the content type.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description

The updated description of the content type.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group

The updated group to which the content type belongs.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Hidden

Specify if you want to hide the content type.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReadOnly

Specify if you want to set the content type as read only.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Sealed

Specify if you want to seal the content type.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while updating the content type.

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
