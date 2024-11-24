---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Invoke-PnPTenantTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Invoke-PnPTenantTemplate
---

# Invoke-PnPTenantTemplate

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Applies a tenant template to the current tenant. You must have the Office 365 Global Admin role to run this cmdlet successfully.

## SYNTAX

### By Path

```
Invoke-PnPTenantTemplate [-Path] <String> [-SequenceId <String>] [-ResourceFolder <String>]
 [-Handlers <Handlers>] [-ExcludeHandlers <Handlers>]
 [-ExtensibilityHandlers <ExtensibilityHandler[]>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>] [-Parameters <Hashtable>]
 [-OverwriteSystemPropertyBagValues] [-IgnoreDuplicateDataRowErrors]
 [-ProvisionContentTypesToSubWebs] [-ProvisionFieldsToSubWebs] [-ClearNavigation]
 [-Configuration <ApplyConfigurationPipeBind>] [-Connection <PnPConnection>]
```

### By Object

```
Invoke-PnPTenantTemplate [-Template] <ProvisioningHierarchy> [-SequenceId <String>]
 [-ResourceFolder <String>] [-Handlers <Handlers>] [-ExcludeHandlers <Handlers>]
 [-ExtensibilityHandlers <ExtensibilityHandler[]>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>] [-Parameters <Hashtable>]
 [-OverwriteSystemPropertyBagValues] [-IgnoreDuplicateDataRowErrors]
 [-ProvisionContentTypesToSubWebs] [-ProvisionFieldsToSubWebs] [-ClearNavigation]
 [-Configuration <ApplyConfigurationPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to apply a tenant template on current tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Invoke-PnPTenantTemplate -Path myfile.pnp
```

Will read the tenant template from the filesystem and will apply the sequences in the template

### EXAMPLE 2

```powershell
Invoke-PnPTenantTemplate -Path myfile.pnp -SequenceId "mysequence"
```

Will read the tenant template from the filesystem and will apply the specified sequence in the template

### EXAMPLE 3

```powershell
Invoke-PnPTenantTemplate -Path myfile.pnp -Parameters @{"ListTitle"="Projects";"parameter2"="a second value"}
```

Applies a tenant template to the current tenant. It will populate the parameter in the template the values as specified and in the template you can refer to those values with the {parameter:<key>} token.

For instance with the example above, specifying {parameter:ListTitle} in your template will translate to 'Projects' when applying the template. These tokens can be used in most string values in a template.

## PARAMETERS

### -ClearNavigation

Override the RemoveExistingNodes attribute in the Navigation elements of the template. If you specify this value the navigation nodes will always be removed before adding the nodes in the template

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Configuration

Specify a JSON configuration file to configure the extraction progress.

```yaml
Type: ApplyConfigurationPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ExcludeHandlers

Allows you to run all handlers, excluding the ones specified.

```yaml
Type: Handlers
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- None
- AuditSettings
- ComposedLook
- CustomActions
- ExtensibilityProviders
- Features
- Fields
- Files
- Lists
- Pages
- Publishing
- RegionalSettings
- SearchSettings
- SitePolicy
- SupportedUILanguages
- TermGroups
- Workflows
- SiteSecurity
- ContentTypes
- PropertyBagEntries
- PageContents
- WebSettings
- Navigation
- ImageRenditions
- ApplicationLifecycleManagement
- Tenant
- WebApiPermissions
- SiteHeader
- SiteFooter
- Theme
- SiteSettings
- All
HelpMessage: ''
```

### -ExtensibilityHandlers

Allows you to specify ExtensibilityHandlers to execute while applying a template

```yaml
Type: ExtensibilityHandler[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Handlers

Allows you to only process a specific part of the template. Notice that this might fail, as some of the handlers require other artifacts in place if they are not part of what your applying.

```yaml
Type: Handlers
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- None
- AuditSettings
- ComposedLook
- CustomActions
- ExtensibilityProviders
- Features
- Fields
- Files
- Lists
- Pages
- Publishing
- RegionalSettings
- SearchSettings
- SitePolicy
- SupportedUILanguages
- TermGroups
- Workflows
- SiteSecurity
- ContentTypes
- PropertyBagEntries
- PageContents
- WebSettings
- Navigation
- ImageRenditions
- ApplicationLifecycleManagement
- Tenant
- WebApiPermissions
- SiteHeader
- SiteFooter
- Theme
- SiteSettings
- All
HelpMessage: ''
```

### -IgnoreDuplicateDataRowErrors

Ignore duplicate data row errors when the data row in the template already exists.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OverwriteSystemPropertyBagValues

Specify this parameter if you want to overwrite and/or create properties that are known to be system entries (starting with vti_, dlc_, etc.)

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Parameters

Allows you to specify parameters that can be referred to in the tenant template by means of the {parameter:&lt;Key&gt;} token. See examples on how to use this parameter.

```yaml
Type: Hashtable
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Path

Path to the xml or pnp file containing the tenant template.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Path
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ProvisionContentTypesToSubWebs

If set content types will be provisioned if the target web is a subweb.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ProvisionFieldsToSubWebs

If set fields will be provisioned if the target web is a subweb.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ResourceFolder

Root folder where resources/files that are being referenced in the template are located. If not specified the same folder as where the tenant template is located will be used.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SequenceId



```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Template



```yaml
Type: ProvisioningHierarchy
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Object
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TemplateProviderExtensions

Allows you to specify ITemplateProviderExtension to execute while applying a template.

```yaml
Type: ITemplateProviderExtension[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
