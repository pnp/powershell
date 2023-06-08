---
Module Name: PnP.PowerShell
title: Set-PnPSiteTemplateMetadata
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteTemplateMetadata.html
---
 
# Set-PnPSiteTemplateMetadata

## SYNOPSIS
Sets metadata of a provisioning template

## SYNTAX

```powershell
Set-PnPSiteTemplateMetadata [-Path] <String> [-TemplateDisplayName <String>]
 [-TemplateImagePreviewUrl <String>] [-TemplateProperties <Hashtable>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to modify metadata of a provisioning template.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSiteTemplateMetadata -Path template.xml -TemplateDisplayName "DisplayNameValue"
```

Sets the DisplayName property of a site template in XML format.

### EXAMPLE 2
```powershell
Set-PnPSiteTemplateMetadata -Path template.pnp -TemplateDisplayName "DisplayNameValue"
```

Sets the DisplayName property of a site template in Office Open XML format.

### EXAMPLE 3
```powershell
Set-PnPSiteTemplateMetadata -Path template.xml -TemplateImagePreviewUrl "Full URL of the Image Preview"
```

Sets the Url to the preview image of a site template in XML format.

### EXAMPLE 4
```powershell
Set-PnPSiteTemplateMetadata -Path template.pnp -TemplateImagePreviewUrl "Full URL of the Image Preview"
```

Sets the to the preview image of a site template in Office Open XML format.

### EXAMPLE 5
```powershell
Set-PnPSiteTemplateMetadata -Path template.xml -TemplateProperties @{"Property1" = "Test Value 1"; "Property2"="Test Value 2"}
```

Sets the property 'Property1' to the value 'Test Value 1' of a site template in XML format.

### EXAMPLE 6
```powershell
Set-PnPSiteTemplateMetadata -Path template.pnp -TemplateProperties @{"Property1" = "Test Value 1"; "Property2"="Test Value 2"}
```

Sets the property 'Property1' to the value 'Test Value 1' of a site template in Office Open XML format.

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

### -Path
Path to the xml or pnp file containing the site template.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -TemplateDisplayName
It can be used to specify the DisplayName of the template file that will be updated.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateImagePreviewUrl
It can be used to specify the ImagePreviewUrl of the template file that will be updated.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateProperties
It can be used to specify custom Properties for the template file that will be updated.

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateProviderExtensions
Allows you to specify ITemplateProviderExtension to execute while extracting a template.

```yaml
Type: ITemplateProviderExtension[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

