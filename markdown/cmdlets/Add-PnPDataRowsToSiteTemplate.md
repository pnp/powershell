---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPDataRowsToSiteTemplate.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPDataRowsToSiteTemplate
---
  
# Add-PnPDataRowsToSiteTemplate

## SYNOPSIS
Adds datarows to a list inside a PnP Provisioning Template

## SYNTAX

```powershell
Add-PnPDataRowsToSiteTemplate [-Path] <String> -List <ListPipeBind> [-Query <String>]
 [-Fields <String[]>] [-IncludeSecurity] [[-TemplateProviderExtensions] <ITemplateProviderExtension[]>]
 [-TokenizeUrls] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Allows to add datarows to list inside a PnP Provisioning Template. The command allows to specify the fields which should be retrieved using `-Fields` option and filter the datarows to be used by using `-Query` option.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPDataRowsToSiteTemplate -Path template.pnp -List 'PnPTestList' -Fields 'Title','Choice'
```

Adds datarows from the provided list to the PnP Provisioning Template at the provided location

### EXAMPLE 2
```powershell
Add-PnPDataRowsToSiteTemplate -Path template.pnp -List 'PnPTestList' -Query '<Query><Where><Geq><FieldRef Name="Modified"/><Value Type="DateTime"><Today OffsetDays="-7" /></Value></Geq></Where></Query>' -Fields 'Title','Choice' -IncludeSecurity
```

Adds datarows from the provided list to the PnP Provisioning Template at the provided location, only the items that have changed since a week ago

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

### -Fields
The fields to retrieve. If not specified all fields will be loaded in the returned list object.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeSecurity
A switch to include ObjectSecurity information.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The list to query

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Filename of the .PNP Open XML site template to read from, optionally including full path.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Query
The CAML query to execute against the list. Defaults to all items.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateProviderExtensions
Allows you to specify ITemplateProviderExtension to execute while loading the template.

```yaml
Type: ITemplateProviderExtension[]
Parameter Sets: (All)

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TokenizeUrls
If set, this switch will try to tokenize the values with web and site related tokens

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


