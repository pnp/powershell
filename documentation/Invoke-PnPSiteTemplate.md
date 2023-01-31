---
Module Name: PnP.PowerShell
title: Invoke-PnPSiteTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Invoke-PnPSiteTemplate.html
---
 
# Invoke-PnPSiteTemplate

## SYNOPSIS
Applies a site template to a web

## SYNTAX

### Path
```powershell
Invoke-PnPSiteTemplate [-Path] <String> [-TemplateId <String>] [-ResourceFolder <String>]
 [-OverwriteSystemPropertyBagValues] [-IgnoreDuplicateDataRowErrors] [-ProvisionContentTypesToSubWebs]
 [-ProvisionFieldsToSubWebs] [-ClearNavigation] [-Parameters <Hashtable>] [-Handlers <Handlers>]
 [-ExcludeHandlers <Handlers>] [-ExtensibilityHandlers <ExtensibilityHandler[]>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>] 
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### Instance
```powershell
Invoke-PnPSiteTemplate [-TemplateId <String>] [-ResourceFolder <String>]
 [-OverwriteSystemPropertyBagValues] [-IgnoreDuplicateDataRowErrors] [-ProvisionContentTypesToSubWebs]
 [-ProvisionFieldsToSubWebs] [-ClearNavigation] [-Parameters <Hashtable>] [-Handlers <Handlers>]
 [-ExcludeHandlers <Handlers>] [-ExtensibilityHandlers <ExtensibilityHandler[]>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>] [-InputInstance <SiteTemplate>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

Allows to apply a site template on a web.

## EXAMPLES

### EXAMPLE 1
```powershell
Invoke-PnPSiteTemplate -Path template.xml
```

Applies a site template in XML format to the current web.

### EXAMPLE 2
```powershell
Invoke-PnPSiteTemplate -Path template.xml -ResourceFolder c:\provisioning\resources
```

Applies a site template in XML format to the current web. Any resources like files that are referenced in the template will be retrieved from the folder as specified with the ResourceFolder parameter.

### EXAMPLE 3
```powershell
Invoke-PnPSiteTemplate -Path template.xml -Parameters @{"ListTitle"="Projects";"parameter2"="a second value"}
```

Applies a site template in XML format to the current web. It will populate the parameter in the template the values as specified and in the template you can refer to those values with the {parameter:<key>} token.

For instance with the example above, specifying {parameter:ListTitle} in your template will translate to 'Projects' when applying the template. These tokens can be used in most string values in a template.

### EXAMPLE 4
```powershell
Invoke-PnPSiteTemplate -Path template.xml -Handlers Lists, SiteSecurity
```

Applies a site template in XML format to the current web. It will only apply the lists and site security part of the template.

### EXAMPLE 5
```powershell
Invoke-PnPSiteTemplate -Path template.pnp
```

Applies a site template from a pnp package to the current web.

### EXAMPLE 6
```powershell
Invoke-PnPSiteTemplate -Path "https://tenant.sharepoint.com/sites/templatestorage/Documents/template.pnp"
```

Applies a site template from a pnp package stored in a library to the current web.

### EXAMPLE 7
```powershell
$handler1 = New-PnPExtensibilityHandlerObject -Assembly Contoso.Core.Handlers -Type Contoso.Core.Handlers.MyExtensibilityHandler1
$handler2 = New-PnPExtensibilityHandlerObject -Assembly Contoso.Core.Handlers -Type Contoso.Core.Handlers.MyExtensibilityHandler2
Invoke-PnPSiteTemplate -Path NewTemplate.xml -ExtensibilityHandlers $handler1,$handler2
```

This will create two new ExtensibilityHandler objects that are run while provisioning the template

### EXAMPLE 8
```powershell
Invoke-PnPSiteTemplate -Path .\ -InputInstance $template
```

Applies a site template from an in-memory instance of a SiteTemplate type of the PnP Core Component, reading the supporting files, if any, from the current (.\) path. The syntax can be used together with any other supported parameters.

### EXAMPLE 9
```powershell
Invoke-PnPSiteTemplate -Path .\template.xml -TemplateId "MyTemplate"
```

Applies the SiteTemplate with the ID "MyTemplate" located in the template definition file template.xml.

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
Accepted values: All, ApplicationLifecycleManagement, AuditSettings, ComposedLook, ContentTypes, CustomActions, ExtensibilityProviders, Features, Fields, Files, ImageRenditions, Lists, Navigation, None, PageContents, Pages, PropertyBagEntries, Publishing, RegionalSettings, SearchSettings, SiteFooter, SiteHeader, SitePolicy, SiteSecurity, SiteSettings, SupportedUILanguages, SyntexModels, Tenant, TermGroups, Theme, WebApiPermissions, WebSettings, Workflows

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExtensibilityHandlers
Allows you to specify ExtensbilityHandlers to execute while applying a template

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
Allows you to only process a specific part of the template. Notice that this might fail, as some of the handlers require other artifacts in place if they are not part of what your applying. Visit https://learn.microsoft.com/dotnet/api/officedevpnp.core.framework.provisioning.model.handlers for possible values.

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

### -InputInstance
Allows you to provide an in-memory instance of the SiteTemplate type of the PnP Core Component. When using this parameter, the -Path parameter refers to the path of any supporting file for the template.

```yaml
Type: SiteTemplate
Parameter Sets: Instance

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
Allows you to specify parameters that can be referred to in the template by means of the {parameter:&lt;Key&gt;} token. See examples on how to use this parameter.

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
Path to the xml or pnp file containing the provisioning template.

```yaml
Type: String
Parameter Sets: Path

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
Root folder where resources/files that are being referenced in the template are located. If not specified the same folder as where the provisioning template is located will be used.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateId
ID of the template to use from the xml file containing the provisioning template. If not specified and multiple SiteTemplate elements exist, the last one will be used.

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

