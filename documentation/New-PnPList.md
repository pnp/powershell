---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPList.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPList
---

# New-PnPList

## SYNOPSIS

Creates a new list

## SYNTAX

### Default (Default)

```
New-PnPList -Title <String> -Template <ListTemplateType> [-Url <String>] [-Hidden]
 [-EnableVersioning] [-EnableContentTypes] [-OnQuickLaunch] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to create a new list.

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

### -EnableContentTypes

Switch parameter if content types should be enabled on this list

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

### -EnableVersioning

Switch parameter if versioning should be enabled

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

### -Hidden

Switch parameter if list should be hidden from the SharePoint UI

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

### -OnQuickLaunch

Switch parameter if this list should be visible on the QuickLaunch

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

### -Template

The type of list to create.

```yaml
Type: ListTemplateType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- NoListTemplate
- GenericList
- DocumentLibrary
- Survey
- Links
- Announcements
- Contacts
- Events
- Tasks
- DiscussionBoard
- PictureLibrary
- DataSources
- WebTemplateCatalog
- UserInformation
- WebPartCatalog
- ListTemplateCatalog
- XMLForm
- MasterPageCatalog
- NoCodeWorkflows
- WorkflowProcess
- WebPageLibrary
- CustomGrid
- SolutionCatalog
- NoCodePublic
- ThemeCatalog
- DesignCatalog
- AppDataCatalog
- AppFilesCatalog
- DataConnectionLibrary
- WorkflowHistory
- GanttTasks
- HelpLibrary
- AccessRequest
- PromotedLinks
- TasksWithTimelineAndHierarchy
- MaintenanceLogs
- Meetings
- Agenda
- MeetingUser
- Decision
- MeetingObjective
- TextBox
- ThingsToBring
- HomePageLibrary
- Posts
- Comments
- Categories
- Facility
- Whereabouts
- CallTrack
- Circulation
- Timecard
- Holidays
- IMEDic
- ExternalList
- MySiteDocumentLibrary
- IssueTracking
- AdminTasks
- HealthRules
- HealthReports
- DeveloperSiteDraftApps
- ContentCenterModelLibrary
- ContentCenterPrimeLibrary
- ContentCenterSampleLibrary
- AccessApp
- AlchemyMobileForm
- AlchemyApprovalWorkflow
- SharingLinks
- HashtagStore
- RecipesTable
- FormulasTable
- WebTemplateExtensionsList
- ItemReferenceCollection
- ItemReferenceReference
- ItemReferenceReferenceCollection
- InvalidType
HelpMessage: ''
```

### -Title

The Title of the list

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Url

If set, will override the url of the list.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
