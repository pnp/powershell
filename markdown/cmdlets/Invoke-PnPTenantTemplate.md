---
Module Name: PnP.PowerShell
title: Invoke-PnPTenantTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Invoke-PnPTenantTemplate.html
---
 
# Invoke-PnPTenantTemplate

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Applies a tenant template to the current tenant. You must have the Office 365 Global Admin role to run this cmdlet successfully.

## SYNTAX

### By Path
```powershell
Invoke-PnPTenantTemplate [-Path] <String> [-SequenceId <String>] [-ResourceFolder <String>]
 [-Handlers <Handlers>] [-ExcludeHandlers <Handlers>] [-ExtensibilityHandlers <ExtensibilityHandler[]>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>] [-Parameters <Hashtable>]
 [-OverwriteSystemPropertyBagValues] [-IgnoreDuplicateDataRowErrors] [-ProvisionContentTypesToSubWebs]
 [-ProvisionFieldsToSubWebs] [-ClearNavigation] [-Configuration <ApplyConfigurationPipeBind>]
 [-Connection <PnPConnection>]   
```

### By Object
```powershell
Invoke-PnPTenantTemplate [-Template] <ProvisioningHierarchy> [-SequenceId <String>] [-ResourceFolder <String>]
 [-Handlers <Handlers>] [-ExcludeHandlers <Handlers>] [-ExtensibilityHandlers <ExtensibilityHandler[]>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>] [-Parameters <Hashtable>]
 [-OverwriteSystemPropertyBagValues] [-IgnoreDuplicateDataRowErrors] [-ProvisionContentTypesToSubWebs]
 [-ProvisionFieldsToSubWebs] [-ClearNavigation] [-Configuration <ApplyConfigurationPipeBind>]
 [-Connection <PnPConnection>]   
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Configuration
Specify a JSON configuration file to configure the extraction progress.

```yaml
Type: ApplyConfigurationPipeBind
Parameter Sets: (All)

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

### -ExcludeHandlers
Allows you to run all handlers, excluding the ones specified.

```yaml
Type: Handlers
Parameter Sets: (All)
Accepted values: None, AuditSettings, ComposedLook, CustomActions, ExtensibilityProviders, Features, Fields, Files, Lists, Pages, Publishing, RegionalSettings, SearchSettings, SitePolicy, SupportedUILanguages, TermGroups, Workflows, SiteSecurity, ContentTypes, PropertyBagEntries, PageContents, WebSettings, Navigation, ImageRenditions, ApplicationLifecycleManagement, Tenant, WebApiPermissions, SiteHeader, SiteFooter, Theme, SiteSettings, All

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExtensibilityHandlers
Allows you to specify ExtensibilityHandlers to execute while applying a template

```yaml
Type: ExtensibilityHandler[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Handlers
Allows you to only process a specific part of the template. Notice that this might fail, as some of the handlers require other artifacts in place if they are not part of what your applying.

```yaml
Type: Handlers
Parameter Sets: (All)
Accepted values: None, AuditSettings, ComposedLook, CustomActions, ExtensibilityProviders, Features, Fields, Files, Lists, Pages, Publishing, RegionalSettings, SearchSettings, SitePolicy, SupportedUILanguages, TermGroups, Workflows, SiteSecurity, ContentTypes, PropertyBagEntries, PageContents, WebSettings, Navigation, ImageRenditions, ApplicationLifecycleManagement, Tenant, WebApiPermissions, SiteHeader, SiteFooter, Theme, SiteSettings, All

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreDuplicateDataRowErrors
Ignore duplicate data row errors when the data row in the template already exists.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OverwriteSystemPropertyBagValues
Specify this parameter if you want to overwrite and/or create properties that are known to be system entries (starting with vti_, dlc_, etc.)

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Parameters
Allows you to specify parameters that can be referred to in the tenant template by means of the {parameter:&lt;Key&gt;} token. See examples on how to use this parameter.

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Path to the xml or pnp file containing the tenant template.

```yaml
Type: String
Parameter Sets: By Path

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -ProvisionContentTypesToSubWebs
If set content types will be provisioned if the target web is a subweb.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProvisionFieldsToSubWebs
If set fields will be provisioned if the target web is a subweb.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceFolder
Root folder where resources/files that are being referenced in the template are located. If not specified the same folder as where the tenant template is located will be used.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SequenceId

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Template

```yaml
Type: ProvisioningHierarchy
Parameter Sets: By Object

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TemplateProviderExtensions
Allows you to specify ITemplateProviderExtension to execute while applying a template.

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

