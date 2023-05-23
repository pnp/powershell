---
Module Name: PnP.PowerShell
title: Get-PnPSiteTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteTemplate.html
---
 
# Get-PnPSiteTemplate

## SYNOPSIS
Generates a provisioning site template from a web

## SYNTAX

```powershell
Get-PnPSiteTemplate [[-Out] <String>] [[-Schema] <XMLPnPSchemaVersion>] [-IncludeAllTermGroups]
 [-IncludeSiteCollectionTermGroup] [-IncludeSiteGroups] [-IncludeTermGroupsSecurity]
 [-IncludeSearchConfiguration] [-PersistBrandingFiles] [-PersistPublishingFiles]
 [-IncludeNativePublishingFiles] [-IncludeHiddenLists] [-IncludeAllPages] [-SkipVersionCheck]
 [-PersistMultiLanguageResources] [-ResourceFilePrefix <String>] [-Handlers <Handlers>]
 [-ExcludeHandlers <Handlers>] [-ExtensibilityHandlers <ExtensibilityHandler[]>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>] [-ContentTypeGroups <String[]>] [-Force]
 [-NoBaseTemplate] [-Encoding <Encoding>] [-TemplateDisplayName <String>] [-TemplateImagePreviewUrl <String>]
 [-TemplateProperties <Hashtable>] [-OutputInstance] [-ExcludeContentTypesFromSyndication]
 [-ListsToExtract <System.Collections.Generic.List`1[System.String]>]
 [-Configuration <ExtractConfigurationPipeBind>] [-Connection <PnPConnection>] 
  
```

## DESCRIPTION

Allows to generate a provisioning site template from a web.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteTemplate -Out template.pnp
```

Extracts a provisioning template in Office Open XML from the current web.

### EXAMPLE 2
```powershell
Get-PnPSiteTemplate -Out template.xml
```

Extracts a provisioning template in XML format from the current web.

### EXAMPLE 3
```powershell
Get-PnPSiteTemplate -Out template.md
```

Extracts a provisioning template in readable markdown format.

### EXAMPLE 4
```powershell
Get-PnPSiteTemplate -Out template.pnp -Schema V201503
```

Extracts a provisioning template in Office Open XML from the current web and saves it in the V201503 version of the schema.

### EXAMPLE 5
```powershell
Get-PnPSiteTemplate -Out template.pnp -IncludeAllTermGroups
```

Extracts a provisioning template in Office Open XML from the current web and includes all term groups, term sets and terms from the Managed Metadata Service Taxonomy.

### EXAMPLE 6
```powershell
Get-PnPSiteTemplate -Out template.pnp -IncludeSiteCollectionTermGroup
```

Extracts a provisioning template in Office Open XML from the current web and includes the term group currently (if set) assigned to the site collection.

### EXAMPLE 7
```powershell
Get-PnPSiteTemplate -Out template.pnp -PersistBrandingFiles
```

Extracts a provisioning template in Office Open XML from the current web and saves the files that make up the composed look to the same folder as where the template is saved.

### EXAMPLE 8
```powershell
Get-PnPSiteTemplate -Out template.pnp -Handlers Lists, SiteSecurity
```

Extracts a provisioning template in Office Open XML from the current web, but only processes lists and site security when generating the template.

### EXAMPLE 9
```powershell
$handler1 = New-PnPExtensibilityHandlerObject -Assembly Contoso.Core.Handlers -Type Contoso.Core.Handlers.MyExtensibilityHandler1
$handler2 = New-PnPExtensibilityHandlerObject -Assembly Contoso.Core.Handlers -Type Contoso.Core.Handlers.MyExtensibilityHandler2
Get-PnPSiteTemplate -Out NewTemplate.xml -ExtensibilityHandlers $handler1,$handler2
```

This will create two new ExtensibilityHandler objects that are run during extraction of the template

### EXAMPLE 10
Only supported on SP2016, SP2019 and SP Online


```powershell
Get-PnPSiteTemplate -Out template.pnp -PersistMultiLanguageResources
```

Extracts a provisioning template in Office Open XML from the current web, and for supported artifacts it will create a resource file for each supported language (based upon the language settings of the current web). The generated resource files will be named after the value specified in the Out parameter. For instance if the Out parameter is specified as -Out 'template.xml' the generated resource file will be called 'template.en-US.resx'.

### EXAMPLE 11
Only supported on SP2016, SP2019 and SP Online


```powershell
Get-PnPSiteTemplate -Out template.pnp -PersistMultiLanguageResources -ResourceFilePrefix MyResources
```

Extracts a provisioning template in Office Open XML from the current web, and for supported artifacts it will create a resource file for each supported language (based upon the language settings of the current web). The generated resource files will be named 'MyResources.en-US.resx' etc.

### EXAMPLE 12
```powershell
$template = Get-PnPSiteTemplate -OutputInstance
```

Extracts an instance of a provisioning template object from the current web. This syntax cannot be used together with the -Out parameter, but it can be used together with any other supported parameters.

### EXAMPLE 13
```powershell
Get-PnPSiteTemplate -Out template.pnp -ContentTypeGroups "Group A","Group B"
```

Extracts a provisioning template in Office Open XML from the current web, but only processes content types from the to given content type groups.

### EXAMPLE 14
```powershell
Get-PnPSiteTemplate -Out template.pnp -ExcludeContentTypesFromSyndication
```

Extracts a provisioning template in Office Open XML from the current web, excluding content types provisioned through content type syndication (content type hub), in order to prevent provisioning errors if the target also provision the content type using syndication.

### EXAMPLE 15
```powershell
Get-PnPSiteTemplate -Out template.pnp -ListsToExtract "Title of List One","95c4efd6-08f4-4c67-94ae-49d696ba1298","Title of List Three"
```

Extracts a provisioning template in Office Open XML from the current web, including only the lists specified by title or ID.

### EXAMPLE 16
```powershell
Get-PnPSiteTemplate -Out template.xml -Handlers Fields, ContentTypes, SupportedUILanguages -PersistMultiLanguageResources
```

Extracts a provisioning template in XML format from the current web including the fields, content types and supported ui languages.
It will create a resource file for each supported language. The generated resource files will be named 'template.en-US.resx' etc.
It is mandatory to include the "SupportedUILanguages" for these handlers as otherwise the resource files will not be created.

### EXAMPLE 17
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com/sites/yourContentCenter"

Get-PnPSiteTemplate -Out MyModels.pnp -Handlers SyntexModels
```

Export all Syntex Document Understanding models in a Content Center into a single PnP template. Note that only unstructured document processing models can be imported from a PnP template.

## PARAMETERS

### -Configuration
Specify a JSON configuration file to configure the extraction progress.

```yaml
Type: ExtractConfigurationPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -ContentTypeGroups
Allows you to specify from which content type group(s) the content types should be included into the template.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Encoding
The encoding type of the XML file, Unicode is default

```yaml
Type: Encoding
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeContentTypesFromSyndication
Specify whether or not content types issued from a content hub should be exported. By default, these content types are included.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeHandlers
Allows you to run all handlers, excluding the ones specified.

```yaml
Type: Handlers
Parameter Sets: (All)
Accepted values: All, ApplicationLifecycleManagement, AuditSettings, ComposedLook, ContentTypes, CustomActions, ExtensibilityProviders, Features, Fields, Files, ImageRenditions, Lists, Navigation, None, PageContents, Pages, PropertyBagEntries, Publishing, RegionalSettings, SearchSettings, SiteFooter, SiteHeader, SitePolicy, SiteSecurity, SiteSettings, SupportedUILanguages, SyntexModels, Tenant, TermGroups, Theme, WebApiPermissions, WebSettings, Workflows

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExtensibilityHandlers
Allows you to specify ExtensibilityHandlers to execute while extracting a template.

```yaml
Type: ExtensibilityHandler[]
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

### -Handlers
Allows you to only process a specific type of artifact in the site. Notice that this might result in a non-working template, as some of the handlers require other artifacts in place if they are not part of what your extracting. For possible values for this parameter visit https://learn.microsoft.com/dotnet/api/officedevpnp.core.framework.provisioning.model.handlers

```yaml
Type: Handlers
Parameter Sets: (All)
Accepted values: All, ApplicationLifecycleManagement, AuditSettings, ComposedLook, ContentTypes, CustomActions, ExtensibilityProviders, Features, Fields, Files, ImageRenditions, Lists, Navigation, None, PageContents, Pages, PropertyBagEntries, Publishing, RegionalSettings, SearchSettings, SiteFooter, SiteHeader, SitePolicy, SiteSecurity, SiteSettings, SupportedUILanguages, SyntexModels, Tenant, TermGroups, Theme, WebApiPermissions, WebSettings, Workflows

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeAllPages
If specified all site pages will be included

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: IncludeAllClientSidePages

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeAllTermGroups
If specified, all term groups will be included. Overrides IncludeSiteCollectionTermGroup.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeHiddenLists
If specified hidden lists will be included in the template

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeNativePublishingFiles
If specified, out of the box / native publishing files will be saved.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeSearchConfiguration
If specified the template will contain the current search configuration of the site.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeSiteCollectionTermGroup
If specified, all the site collection term groups will be included. Overridden by IncludeAllTermGroups.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeSiteGroups
If specified all site groups will be included.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeTermGroupsSecurity
If specified all the managers and contributors of term groups will be included.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListsToExtract
Specify the lists to extract, either providing their ID or their Title.

```yaml
Type: System.Collections.Generic.List`1[System.String]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoBaseTemplate
{{ Fill NoBaseTemplate Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Out
Filename to write to, optionally including full path. The format of the file is based upon the extension you specify. 
- .xml will generate an XML file
- .pnp will generate a PnP Provisioning Package, which is a file that contains all artifacts in a single archive (files, images, etc.)
- .md will generate a user readable markdown report. This is work in progress and will be extended in the future.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputInstance
Returns the template as an in-memory object, which is an instance of the SiteTemplate type of the PnP Core Component. It cannot be used together with the -Out parameter.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PersistBrandingFiles
If specified the files used for masterpages, sitelogo, alternate CSS and the files that make up the composed look will be saved.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PersistMultiLanguageResources
If specified, resource values for applicable artifacts will be persisted to a resource file

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PersistPublishingFiles
If specified the files used for the publishing feature will be saved.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceFilePrefix
If specified, resource files will be saved with the specified prefix instead of using the template name specified. If no template name is specified the files will be called PnP-Resources.&lt;language&gt;.resx. See examples for more info.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Schema
The schema of the output to use, defaults to the latest schema

```yaml
Type: XMLPnPSchemaVersion
Parameter Sets: (All)
Accepted values: LATEST, V201503, V201505, V201508, V201512, V201605, V201705, V201801, V201805, V201807, V201903, V201909, V202002

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipVersionCheck
During extraction the version of the server will be checked for certain actions. If you specify this switch, this check will be skipped.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateDisplayName
It can be used to specify the DisplayName of the template file that will be extracted.

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
It can be used to specify the ImagePreviewUrl of the template file that will be extracted.

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
It can be used to specify custom Properties for the template file that will be extracted.

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



### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Encoding documentation](https://learn.microsoft.com/dotnet/api/system.text.encoding?view=net-6.0)
