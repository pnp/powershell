---
Module Name: PnP.PowerShell
title: Test-PnPSiteTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Test-PnPSiteTemplate.html
---

# Test-PnPSiteTemplate

## SYNOPSIS
Validates a PnP site template without applying it.

## SYNTAX

### By Path
```powershell
Test-PnPSiteTemplate [-Path] <String> [-TemplateId <String>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>]
```

### By Stream
```powershell
Test-PnPSiteTemplate [-Stream] <Stream> [-TemplateId <String>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>]
```

### By XML
```powershell
Test-PnPSiteTemplate [-Xml] <String> [-TemplateProviderExtensions <ITemplateProviderExtension[]>]
```

### By Template
```powershell
Test-PnPSiteTemplate [-Template] <ProvisioningTemplate>
```

## DESCRIPTION

Validates the provisioning schema and the internal structure of a PnP site template without applying it or making a request to SharePoint.

The cmdlet checks for missing or duplicate content type, field, list and binding identifiers. It warns when field, content type or term set references are not defined in the template because those dependencies must already exist on the target site or in its term store. Hub associations are also reported as external dependencies. When validating XML or package input, the cmdlet warns about provisioning elements that have been removed from the latest schema. When the template has a file connector, the cmdlet also checks referenced files, localizations, directories, app packages, site scripts and relative site logos. Missing optional localizations and unresolved directory or logo resources are warnings.

Each result contains `IsValid`, `ResourcesValidated` and a collection of structured issues with a code, severity, message and location. `ResourcesValidated` is `$false` when an in-memory template has no connector. The cmdlet does not compare the template with a site, so it cannot confirm whether warned-about content types, fields, term sets or hub associations exist there.

## EXAMPLES

### EXAMPLE 1
```powershell
Test-PnPSiteTemplate -Path ./template.pnp
```

Validates every template found in a PnP site template package and returns one validation result for each template.

### EXAMPLE 2
```powershell
Read-PnPSiteTemplate -Path ./template.xml | Test-PnPSiteTemplate
```

Validates an in-memory provisioning template received through the pipeline.

### EXAMPLE 3
```powershell
$result = Test-PnPSiteTemplate -Path ./template.xml
if ($result | Where-Object { -not $_.IsValid }) {
    $result.Issues | Format-Table Code, Severity, Location, Message
    throw "The site template is invalid."
}
```

Stops a script when the template contains an error-severity validation issue.

### EXAMPLE 4
```powershell
Test-PnPSiteTemplate -Path ./templates.pnp -TemplateId TeamSite
```

Validates only the template with ID `TeamSite` from a package containing multiple templates.

## PARAMETERS

### -Path
Path to an XML site template or a PnP site template package.

```yaml
Type: String
Parameter Sets: By Path

Required: True
Position: 0
Default value: None
Aliases: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Stream
Stream containing an XML site template or a PnP site template package.

```yaml
Type: Stream
Parameter Sets: By Stream

Required: True
Position: 0
Default value: None
Aliases: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Template
In-memory provisioning template to validate.

```yaml
Type: ProvisioningTemplate
Parameter Sets: By Template

Required: True
Position: 0
Default value: None
Aliases: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TemplateId
ID of the template to validate when an XML file or package contains multiple templates.

```yaml
Type: String
Parameter Sets: By Path, By Stream

Required: False
Position: Named
Default value: None
Aliases: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateProviderExtensions
Template provider extensions to execute while loading the template.

```yaml
Type: ITemplateProviderExtension[]
Parameter Sets: By Path, By Stream, By XML

Required: False
Position: Named
Default value: None
Aliases: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Xml
XML text containing a provisioning template.

```yaml
Type: String
Parameter Sets: By XML

Required: True
Position: 0
Default value: None
Aliases: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Read-PnPSiteTemplate](https://pnp.github.io/powershell/cmdlets/Read-PnPSiteTemplate.html)

[Invoke-PnPSiteTemplate](https://pnp.github.io/powershell/cmdlets/Invoke-PnPSiteTemplate.html)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)