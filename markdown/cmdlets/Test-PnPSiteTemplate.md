---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Test-PnPSiteTemplate.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Test-PnPSiteTemplate
Module Name: PnP.PowerShell
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

Validates a PnP site template without applying it and without making a request to SharePoint. Use it to catch structural problems in a template, for example in a pull request or a release pipeline, before it ever reaches a site.

One result is returned per template found in the source. Every finding is a structured issue carrying a `Code`, `Severity`, `Message` and `Location`, at one of three severities.

| Severity | Meaning |
|---|---|
| `Error` | The template is broken and should not be applied, for example a duplicate identifier, an unreadable package, an unsupported schema, or a referenced file that is not present. This is the only severity that sets `IsValid` to `$false`. |
| `Warning` | The template can be applied but may not behave as intended, for example an older provisioning schema, or an attribute that has been removed from the latest schema. |
| `Information` | Something the target site or term store has to provide, or something that could not be checked in advance. Out of the box content types and site columns are never declared inside a template, so these appear on almost every template and are grouped into a single issue per location. |

How much can be checked depends on what the source provides, which the result states explicitly rather than leaving you to guess.

| Property | Meaning |
|---|---|
| `IsValid` | `$false` only when an `Error` severity issue was found. |
| `ResourcesChecked` | `$true` when the template carried a file connector, so referenced files, localizations, directories, data row attachments, workflow definitions, publishing design packages, document set default documents, app packages, site scripts and relative site logos could be resolved. `$false` for an in-memory template without a connector, meaning none of those were looked at. |
| `SchemaChecked` | `$true` when the source XML was available, so the schema version and removed-element checks could run. `$false` for a template received through the pipeline. |
| `SchemaVersion` | The provisioning schema namespace found in the source, when available. |

The provisioning schema versions 2019/03 through 2022/09 are recognised. Anything else is reported as an `UnsupportedSchema` error, because the provisioning engine falls back to the latest deserializer for a namespace it does not know, which quietly produces an empty template rather than failing.

Server relative paths and absolute URLs are resolved at the moment the template is applied, so they are not looked for among the template's own files. A path counts as tokenized when it starts with a token or names a parameter, and one that cannot be found is then reported as `UnverifiedResourcePath` at `Information` severity rather than as missing, because only the target site can expand it. A source that merely contains `_api` or a brace in a folder name is checked normally.

A resource reference that is present but empty is an error in its own right, because the provisioning engine reads every one of them without checking first. This includes a design package or a document set default document whose source path is blank, which is what `Get-PnPSiteTemplate` produces for default documents when it runs without `-PersistBrandingFiles`.

`XInclude` references are resolved before anything else, so a template that lives in an included fragment is validated and can be selected with `-TemplateId` like any other. Each `href` is read from the root of the source, matching how the provisioning engine resolves it, and a fragment pulled in by an include is not validated a second time on its own.

When the source is a package, templates stored in a folder inside it are validated as well as those at its root, and an issue in one member names that member in its `Location`. Passing `-TemplateId` reports only the issues belonging to the template that was asked for.

The cmdlet never compares the template against a site, so it cannot confirm whether the content types, fields, term sets or hub sites it reports as dependencies actually exist there.

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

Validates an in-memory provisioning template received through the pipeline. `SchemaChecked` is `$false` on the result because the source XML is no longer available, so the schema version and removed-element checks are skipped.

### EXAMPLE 3
```powershell
$result = Test-PnPSiteTemplate -Path ./template.xml
if ($result | Where-Object { -not $_.IsValid }) {
    $result.Issues | Format-Table Code, Severity, Location, Message
    throw "The site template is invalid."
}
```

Stops a script when the template contains an error-severity validation issue. Use `Where-Object` rather than `$result.IsValid`, because a source holding several templates returns one result for each.

### EXAMPLE 4
```powershell
Test-PnPSiteTemplate -Path ./template.pnp |
    Select-Object -ExpandProperty Issues |
    Where-Object Severity -ne Information |
    Format-Table Severity, Code, Location, Message
```

Shows only the errors and warnings, hiding the informational dependencies that a template is expected to have on its target site.

### EXAMPLE 5
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

