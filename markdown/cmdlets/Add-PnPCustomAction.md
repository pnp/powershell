---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPCustomAction.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPCustomAction
---
  
# Add-PnPCustomAction

## SYNOPSIS
Adds a custom action

## SYNTAX

### Default
```powershell
Add-PnPCustomAction -Name <String> -Title <String> -Description <String> -Group <String> -Location <String>
 [-Sequence <Int32>] [-Url <String>] [-ImageUrl <String>] [-CommandUIExtension <String>]
 [-RegistrationId <String>] [-Rights <PermissionKind[]>] [-RegistrationType <UserCustomActionRegistrationType>]
 [-Scope <CustomActionScope>] [-Connection <PnPConnection>] 
```

### Client Side Component Id
```powershell
Add-PnPCustomAction -Name <String> -Title <String> -Location <String> [-Sequence <Int32>]
 [-RegistrationId <String>] [-RegistrationType <UserCustomActionRegistrationType>] [-Scope <CustomActionScope>]
 -ClientSideComponentId <Guid> [-ClientSideComponentProperties <String>]
 [-ClientSideHostProperties <String>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Adds a user custom action to a web or sitecollection.

## EXAMPLES

### EXAMPLE 1
```powershell
$cUIExtn = "<CommandUIExtension><CommandUIDefinitions><CommandUIDefinition Location=""Ribbon.List.Share.Controls._children""><Button Id=""Ribbon.List.Share.GetItemsCountButton"" Alt=""Get list items count"" Sequence=""11"" Command=""Invoke_GetItemsCountButtonRequest"" LabelText=""Get Items Count"" TemplateAlias=""o1"" Image32by32=""_layouts/15/images/placeholder32x32.png"" Image16by16=""_layouts/15/images/placeholder16x16.png"" /></CommandUIDefinition></CommandUIDefinitions><CommandUIHandlers><CommandUIHandler Command=""Invoke_GetItemsCountButtonRequest"" CommandAction=""javascript: alert('Total items in this list: '+ ctx.TotalListItems);"" EnabledScript=""javascript: function checkEnable() { return (true);} checkEnable();""/></CommandUIHandlers></CommandUIExtension>"

Add-PnPCustomAction -Name 'GetItemsCount' -Title 'Invoke GetItemsCount Action' -Description 'Adds custom action to custom list ribbon' -Group 'SiteActions' -Location 'CommandUI.Ribbon' -CommandUIExtension $cUIExtn
```

Adds a new custom action to the custom list template, and sets the Title, Name and other fields with the specified values. On click it shows the number of items in that list. Notice: escape quotes in CommandUIExtension.

### EXAMPLE 2
```powershell
Add-PnPCustomAction -Title "CollabFooter" -Name "CollabFooter" -Location "ClientSideExtension.ApplicationCustomizer" -ClientSideComponentId c0ab3b94-8609-40cf-861e-2a1759170b43 -ClientSideComponentProperties "{`"sourceTermSet`":`"PnP-CollabFooter-SharedLinks`",`"personalItemsStorageProperty`":`"PnP-CollabFooter-MyLinks`"}"
```

Adds a new application customizer to the site. This requires that an SPFX solution has been deployed containing the application customizer specified. Be sure to run Install-PnPApp before trying this cmdlet on a site.

## PARAMETERS

### -ClientSideComponentId
The Client Side Component Id of the custom action

```yaml
Type: Guid
Parameter Sets: Client Side Component Id

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSideComponentProperties
The Client Side Component Properties of the custom action. Specify values as a json string : "{Property1 : 'Value1', Property2: 'Value2'}"

```yaml
Type: String
Parameter Sets: Client Side Component Id

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSideHostProperties
The Client Side Host Properties of the custom action. Specify values as a json string : "{'preAllocatedApplicationCustomizerTopHeight': '50', 'preAllocatedApplicationCustomizerBottomHeight': '50'}"

```yaml
Type: String
Parameter Sets: Client Side Component Id

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CommandUIExtension
XML fragment that determines user interface properties of the custom action

```yaml
Type: String
Parameter Sets: Default

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

### -Description
The description of the custom action

```yaml
Type: String
Parameter Sets: Default

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
The group where this custom action needs to be added like 'SiteActions'

```yaml
Type: String
Parameter Sets: Default

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ImageUrl
The URL of the image associated with the custom action

```yaml
Type: String
Parameter Sets: Default

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Location
The actual location where this custom action need to be added like 'CommandUI.Ribbon'

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name of the custom action

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RegistrationId
The identifier of the object associated with the custom action.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RegistrationType
Specifies the type of object associated with the custom action

```yaml
Type: UserCustomActionRegistrationType
Parameter Sets: (All)
Accepted values: None, List, ContentType, ProgId, FileType

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Rights
A string array that contain the permissions needed for the custom action

```yaml
Type: PermissionKind[]
Parameter Sets: Default
Accepted values: EmptyMask, ViewListItems, AddListItems, EditListItems, DeleteListItems, ApproveItems, OpenItems, ViewVersions, DeleteVersions, CancelCheckout, ManagePersonalViews, ManageLists, ViewFormPages, AnonymousSearchAccessList, Open, ViewPages, AddAndCustomizePages, ApplyThemeAndBorder, ApplyStyleSheets, ViewUsageData, CreateSSCSite, ManageSubwebs, CreateGroups, ManagePermissions, BrowseDirectories, BrowseUserInfo, AddDelPrivateWebParts, UpdatePersonalWebParts, ManageWeb, AnonymousSearchAccessWebLists, UseClientIntegration, UseRemoteAPIs, ManageAlerts, CreateAlerts, EditMyUserInfo, EnumeratePermissions, FullMask

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
The scope of the CustomAction to add to. Either Web or Site; defaults to Web. 'All' is not valid for this command.

```yaml
Type: CustomActionScope
Parameter Sets: (All)
Accepted values: Web, Site, All

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Sequence
Sequence of this CustomAction being injected. Use when you have a specific sequence with which to have multiple CustomActions being added to the page.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the custom action

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The URL, URI or ECMAScript (JScript, JavaScript) function associated with the action

```yaml
Type: String
Parameter Sets: Default

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[UserCustomAction](https://learn.microsoft.com/previous-versions/office/sharepoint-server/ee539583(v=office.15))
[BasePermissions](https://learn.microsoft.com/previous-versions/office/sharepoint-server/ee543321(v=office.15))