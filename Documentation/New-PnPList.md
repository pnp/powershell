---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/new-pnplist
schema: 2.0.0
title: New-PnPList
---

# New-PnPList

## SYNOPSIS
Creates a new list

## SYNTAX

```
New-PnPList -Title <String> -Template <ListTemplateType> [-Url <String>] [-Hidden] [-EnableVersioning]
 [-EnableContentTypes] [-OnQuickLaunch] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPList -Title Announcements -Template Announcements
```

Create a new announcements list

### EXAMPLE 2
```powershell
New-PnPList -Title "Demo List" -Url "lists/DemoList" -Template Announcements
```

Create an announcements list with a title that is different from the url

### EXAMPLE 3
```powershell
New-PnPList -Title HiddenList -Template GenericList -Hidden
```

Create a new custom list and hides it from the SharePoint UI

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableContentTypes
Switch parameter if content types should be enabled on this list

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableVersioning
Switch parameter if versioning should be enabled

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Hidden
Switch parameter if list should be hidden from the SharePoint UI

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OnQuickLaunch
Switch parameter if this list should be visible on the QuickLaunch

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Template
The type of list to create.

```yaml
Type: ListTemplateType
Parameter Sets: (All)
Aliases:
Accepted values: NoListTemplate, GenericList, DocumentLibrary, Survey, Links, Announcements, Contacts, Events, Tasks, DiscussionBoard, PictureLibrary, DataSources, WebTemplateCatalog, UserInformation, WebPartCatalog, ListTemplateCatalog, XMLForm, MasterPageCatalog, NoCodeWorkflows, WorkflowProcess, WebPageLibrary, CustomGrid, SolutionCatalog, NoCodePublic, ThemeCatalog, DesignCatalog, AppDataCatalog, AppFilesCatalog, DataConnectionLibrary, WorkflowHistory, GanttTasks, HelpLibrary, AccessRequest, PromotedLinks, TasksWithTimelineAndHierarchy, MaintenanceLogs, Meetings, Agenda, MeetingUser, Decision, MeetingObjective, TextBox, ThingsToBring, HomePageLibrary, Posts, Comments, Categories, Facility, Whereabouts, CallTrack, Circulation, Timecard, Holidays, IMEDic, ExternalList, MySiteDocumentLibrary, IssueTracking, AdminTasks, HealthRules, HealthReports, DeveloperSiteDraftApps, ContentCenterModelLibrary, ContentCenterPrimeLibrary, ContentCenterSampleLibrary, AccessApp, AlchemyMobileForm, AlchemyApprovalWorkflow, SharingLinks, HashtagStore, RecipesTable, FormulasTable, WebTemplateExtensionsList, ItemReferenceCollection, ItemReferenceReference, ItemReferenceReferenceCollection, InvalidType

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The Title of the list

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
If set, will override the url of the list.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)