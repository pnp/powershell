---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPListFoldersToSiteTemplate.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPListFoldersToSiteTemplate
---
  
# Add-PnPListFoldersToSiteTemplate

## SYNOPSIS
Adds folders to a list in a PnP Provisioning Template

## SYNTAX

```powershell
Add-PnPListFoldersToSiteTemplate [-Path] <String> [-List] <ListPipeBind> [-Recursive]
 [-IncludeSecurity] [[-TemplateProviderExtensions] <ITemplateProviderExtension[]>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add folders to a list in a PnP Provisioning Template.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPListFoldersToSiteTemplate -Path template.pnp -List 'PnPTestList'
```

Adds top level folders from a list to an existing template and returns an in-memory PnP Site Template

### EXAMPLE 2
```powershell
Add-PnPListFoldersToSiteTemplate -Path template.pnp -List 'PnPTestList' -Recursive
```

Adds all folders from a list to an existing template and returns an in-memory PnP Site Template

### EXAMPLE 3
```powershell
Add-PnPListFoldersToSiteTemplate -Path template.pnp -List 'PnPTestList' -Recursive -IncludeSecurity
```

Adds all folders from a list with unique permissions to an in-memory PnP Site Template

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
Position: 2
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

### -Recursive
A switch parameter to include all folders in the list, or just top level folders.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Alias: Recurse
Position: 4
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
Position: 6
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


