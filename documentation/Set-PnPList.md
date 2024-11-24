---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPList.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPList
---

# Set-PnPList

## SYNOPSIS

Updates list settings.

## SYNTAX

### Default (Default)

```
Set-PnPList -Identity <ListPipeBind> [-EnableContentTypes <Boolean>] [-BreakRoleInheritance]
 [-ResetRoleInheritance] [-CopyRoleAssignments] [-ClearSubScopes] [-Title <String>]
 [-Description <String>] [-Hidden <Boolean>] [-AllowDeletion <Boolean>] [-ForceCheckout <Boolean>]
 [-ListExperience <ListExperience>] [-EnableAttachments <Boolean>] [-EnableFolderCreation <Boolean>]
 [-EnableVersioning <Boolean>] [-EnableMinorVersions <Boolean>] [-MajorVersions <UInt32>]
 [-MinorVersions <UInt32>] [-EnableModeration <Boolean>]
 [-DraftVersionVisibility <DraftVisibilityType>] [-ReadSecurity <ListReadSecurity>]
 [-WriteSecurity <ListWriteSecurity>] [-NoCrawl]
 [-ExemptFromBlockDownloadOfNonViewableFiles <Boolean>] [-DisableGridEditing <Boolean>]
 [-DisableCommenting <Boolean>] [-EnableAutoExpirationVersionTrim <Boolean>]
 [-ExpireVersionsAfterDays <UInt32>] [-DefaultSensitivityLabelForLibrary <SensitivityLabelPipeBind>]
 [-Path <String>] [-OpenDocumentsMode <DocumentLibraryOpenDocumentsInMode>] [-Color <ListColor>]
 [-Icon <ListIcon>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows the configuration of a specific SharePoint Online list to be set.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPList -Identity "Demo List" -EnableContentTypes $true
```

Switches the Enable Content Type switch on the list.

### EXAMPLE 2

```powershell
Set-PnPList -Identity "Demo List" -Hidden $true
```

Hides the list from the SharePoint UI.

### EXAMPLE 3

```powershell
Set-PnPList -Identity "Demo List" -EnableVersioning $true
```

Turns on major versions on a list.

### EXAMPLE 4

```powershell
Set-PnPList -Identity "Demo List" -EnableVersioning $true -MajorVersions 20
```

Turns on major versions on a list and sets the maximum number of major versions to keep at 20.

### EXAMPLE 5

```powershell
Set-PnPList -Identity "Demo Library" -EnableVersioning $true -EnableMinorVersions $true -MajorVersions 20 -MinorVersions 5
```

Turns on major versions on a document library, sets the maximum number of major versions to keep at 20, and sets the maximum number of minor versions to 5.

### EXAMPLE 6

```powershell
Set-PnPList -Identity "Demo List" -EnableAttachments $true
```

Turns on attachments for a list.

### EXAMPLE 7

```powershell
Set-PnPList -Identity "Demo List" -Title "Demo List 2" -Path "Lists/DemoList2"
```

Renames a list, including its URL.

### EXAMPLE 8

```powershell
Set-PnPList -Identity "Demo List" -EnableAutoExpirationVersionTrim $true
```

Enables AutoExpiration file version trim mode on a document library.

### EXAMPLE 9

```powershell
Set-PnPList -Identity "Demo List" -EnableAutoExpirationVersionTrim $false -ExpireVersionsAfterDays 30 -MajorVersions 500
```

Enables ExpireAfter file version trim mode on a document library. MinorVersions parameter is also needed when minor version is enabled.

### EXAMPLE 10

```powershell
Set-PnPList -Identity "Demo List" -EnableAutoExpirationVersionTrim $false -ExpireVersionsAfterDays 0 -MajorVersions 500
```

Enables NoExpiration file version trim mode on a document library. MinorVersions parameter is also needed when minor version is enabled.

### EXAMPLE 11

```powershell
Set-PnPList -Identity "Demo List" -DefaultSensitivityLabelForLibrary "Confidential"
```

Sets the default sensitivity label for a document library to Confidential.

### EXAMPLE 12

```powershell
Set-PnPList -Identity "Demo List" -Color Green -Icon "Plane"
```

Changes the icon of the list to a plane, and the background color of the icon to green.

## PARAMETERS

### -AllowDeletion

Allow or prevent deletion of the list from the SharePoint UI. Set to $true to allow, $false to prevent.

```yaml
Type: Boolean
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

### -BreakRoleInheritance

If used, the security inheritance is broken for this list from its parent, the web in which it resides. Permissions can be added using [Set-PnPListPermission](Set-PnPListPermission.md).

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

### -ClearSubScopes

If used, the unique permissions are cleared from child objects and they can inherit role assignments from this object.

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

### -Color

The background color of the list icon.

```yaml
Type: ListColor
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
- DarkRed
- Red
- Orange
- Green
- DarkGreen
- Teal
- Blue
- NavyBlue
- BluePurple
- DarkBlue
- Lavender
- Pink
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

### -CopyRoleAssignments

If used, the role assignments are copied from the parent web.

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

### -DefaultSensitivityLabelForLibrary

The instance, Id or name of the sensitivity label to set as the default for the library. If $null is provided, the default label will be removed.

```yaml
Type: SensitivityLabelPipeBind
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

### -Description

The description of the list.

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

### -DisableCommenting

Enable or disable whether commenting is enabled for the list. Set to $true to disable, $false to enable.

```yaml
Type: Boolean
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

### -DisableGridEditing

Enable or disable whether edit grid editing is enabled for the list. Set to $true to disable, $false to enable.

```yaml
Type: Boolean
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

### -DraftVersionVisibility

Specify which users should be able to view drafts in this list.

```yaml
Type: DraftVisibilityType
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
- Approver
- Author
- Reader
HelpMessage: ''
```

### -EnableAttachments

Enable or disable attachments. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
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

### -EnableAutoExpirationVersionTrim

Enable or disable AutoExpiration version trim for the document library. Set to $true to enable, $false to disable.

Parameter ExpireVersionsAfterDays is required when EnableAutoExpirationVersionTrim is false. Set ExpireVersionsAfterDays to 0 for NoExpiration, set it to greater or equal 30 for ExpireAfter.

Parameter MajorVersions is required when EnableAutoExpirationVersionTrim is false.
Parameter MinorVersions is required when EnableAutoExpirationVersionTrim is false and minor version is enabled.

```yaml
Type: Boolean
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

### -EnableClassicAudienceTargeting

Enable classic audience targeting in a SharePoint list.

```yaml
Type: Boolean
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

Set to $true to enable content types, set to $false to disable content types.

```yaml
Type: Boolean
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

### -EnableFolderCreation

Enable or disable folder creation. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
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

### -EnableMinorVersions

Enable or disable minor versions versioning. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
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

### -EnableModeration

Enable or disable whether content approval is enabled for the list. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
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

### -EnableModernAudienceTargeting

Enable modern audience targeting in a SharePoint list. Please make sure the following feature ModernAudienceTargeting with ID "bc13eaf7-67c7-4f85-a80f-a4b0dae5e5bd" is activated first on the site by using Enable-PnPFeature.

```yaml
Type: Boolean
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

Enable or disable versioning. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
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

### -ExemptFromBlockDownloadOfNonViewableFiles

Allows to configure access capabilities for un-managed devices for the list. If set to $true, the list will be accessible by un-managed devices as well. For more information, see [SharePoint and OneDrive un-managed device access controls for administrators](https://learn.microsoft.com/sharepoint/control-access-from-unmanaged-devices).

```yaml
Type: Boolean
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

### -ExpireVersionsAfterDays

Works with parameter EnableAutoExpirationVersionTrim. Please see description in EnableAutoExpirationVersionTrim.

```yaml
Type: UInt32
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

### -ForceCheckout

Enable or disable force checkout. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
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

Hide the list from the SharePoint UI. Set to $true to hide, $false to show.

```yaml
Type: Boolean
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

### -Icon

The icon of the list.

```yaml
Type: ListIcon
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
- Bug
- Calendar
- Target
- Clipboard
- Plane
- Rocket
- ColorPalette
- Lightbulb
- Cube
- Beaker
- Robot
- PiggyBank
- Playlist
- Hospital
- Bank
- MapPin
- CoffeCup
- ShoppingCart
- BirthdayCake
HelpMessage: ''
```

### -Identity

The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
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

### -ListExperience

Set the list experience: Auto, NewExperience or ClassicExperience

```yaml
Type: ListExperience
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
- Auto
- NewExperience
- ClassicExperience
HelpMessage: ''
```

### -MajorVersions

Maximum major versions to keep.

```yaml
Type: UInt32
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

### -MinorVersions

Maximum major versions for which to keep minor versions.

```yaml
Type: UInt32
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

### -NoCrawl

Switch parameter to specify whether this list should be excluded from search indexing.

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

### -OpenDocumentsMode

Allows configuring the "Opening Documents in the Browser" advanced setting on document libraries. Set to `ClientApplication` to have documents opened in the locally installed Word, PowerPoint, or Excel client, or set to `Browser` to have documents opened in the browser. It is not possible to set it to "Use the server default mode".

```yaml
Type: DocumentLibraryOpenDocumentsInMode
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
- ClientApplication
- Browser
HelpMessage: ''
```

### -Path

The new URL path of the list. The parent folder must exist and be in the same site/web. I.e. lists\newname.

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

### -ReadSecurity

Sets the read security for the list.

```yaml
Type: ListReadSecurity
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
- AllUsersReadAccess
- AllUsersReadAccessOnItemsTheyCreate
HelpMessage: ''
```

### -ResetRoleInheritance

If used, the security inheritance is reset for this list, meaning it will not copy the permissions from its parent but will start with an empty list of permissions. Permissions can be added using [Set-PnPListPermission](Set-PnPListPermission.md).

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

### -Title

The title of the list.

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

### -WriteSecurity

Sets the write security for the list.

```yaml
Type: ListWriteSecurity
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
- WriteAllItems
- WriteOnlyMyItems
- WriteNoItems
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
