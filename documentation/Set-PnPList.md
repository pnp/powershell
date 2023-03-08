---
Module Name: PnP.PowerShell
title: Set-PnPList
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPList.html
---
 
# Set-PnPList

## SYNOPSIS
Updates list settings

## SYNTAX

```powershell
Set-PnPList -Identity <ListPipeBind> [-EnableContentTypes <Boolean>] [-BreakRoleInheritance]
 [-ResetRoleInheritance] [-CopyRoleAssignments] [-ClearSubscopes] [-Title <String>] [-Description <String>]
 [-Hidden <Boolean>] [-ForceCheckout <Boolean>] [-ListExperience <ListExperience>]
 [-EnableAttachments <Boolean>] [-EnableFolderCreation <Boolean>] [-EnableVersioning <Boolean>]
 [-EnableMinorVersions <Boolean>] [-MajorVersions <UInt32>] [-MinorVersions <UInt32>]
 [-EnableModeration <Boolean>] [-DraftVersionVisibility <DraftVisibilityType>] [-ReadSecurity <ListReadSecurity>] [-WriteSecurity <ListWriteSecurity>]
 [-EnableAutoExpirationVersionTrim <Boolean>][-ExpireVersionsAfterDays <UInt32>]
 [-NoCrawl] [-ExemptFromBlockDownloadOfNonViewableFiles <Boolean>] [-DisableGridEditing <Boolean>] [-DefaultSensitivityLabelForLibrary <SensitivityLabelPipeBind>]
 [-Path <String>] [-Connection <PnPConnection>] [<CommonParameters>]

```

## DESCRIPTION
Allows the configuration of a specific SharePoint Online list to be set.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPList -Identity "Demo List" -EnableContentTypes $true
```

Switches the Enable Content Type switch on the list

### EXAMPLE 2
```powershell
Set-PnPList -Identity "Demo List" -Hidden $true
```

Hides the list from the SharePoint UI.

### EXAMPLE 3
```powershell
Set-PnPList -Identity "Demo List" -EnableVersioning $true
```

Turns on major versions on a list

### EXAMPLE 4
```powershell
Set-PnPList -Identity "Demo List" -EnableVersioning $true -MajorVersions 20
```

Turns on major versions on a list and sets the maximum number of Major Versions to keep to 20.

### EXAMPLE 5
```powershell
Set-PnPList -Identity "Demo Library" -EnableVersioning $true -EnableMinorVersions $true -MajorVersions 20 -MinorVersions 5
```

Turns on major versions on a document library and sets the maximum number of Major versions to keep to 20 and sets the maximum of Minor versions to 5.

### EXAMPLE 6
```powershell
Set-PnPList -Identity "Demo List" -EnableAttachments $true
```

Turns on attachments on a list

### EXAMPLE 7
```powershell
Set-PnPList -Identity "Demo List" -Title "Demo List 2" -Path "Lists/DemoList2"
```

Rename a list, including its' URL.

### EXAMPLE 8
```powershell
Set-PnPList -Identity "Demo List" -EnableAutoExpirationVersionTrim $true
```

Enable AutoExpiration file version trim mode on a doccument library.

### EXAMPLE 9
```powershell
Set-PnPList -Identity "Demo List" -EnableAutoExpirationVersionTrim $false -ExpireVersionsAfterDays 30 -MajorVerions 500
```

Enable ExpireAfter file version trim mode on a doccument library. MinorVersions is also needed when minor version is enabled.

### EXAMPLE 10
```powershell
Set-PnPList -Identity "Demo List" -EnableAutoExpirationVersionTrim $false -ExpireVersionsAfterDays 0 -MajorVerions 500
```

Enable NoExpiration file version trim mode on a doccument library. MinorVersions is also needed when minor version is enabled.
=======
Set-PnPList -Identity "Demo List" -DefaultSensitivityLabelForLibrary "Confidential"
```

Sets the default sensitivity label for a document library to Confidential.

## PARAMETERS

### -BreakRoleInheritance
If used the security inheritance is broken for this list from its parent, the web in which it resides. Permissions can be added using [Set-PnPListPermission](Set-PnPListPermission.html).

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResetRoleInheritance
If used the security inheritance is reset for this list meaning it will not copy the permissions from its parent, but will start with an empty list of permissions. Permissions can be added using [Set-PnPListPermission](Set-PnPListPermission.html).

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClearSubscopes
If used the unique permissions are cleared from child objects and they can inherit role assignments from this object

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

### -CopyRoleAssignments
If used the roles are copied from the parent web

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The description of the list

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultSensitivityLabelForLibrary
The instance, Id or name of the sensitivity label to set as the default for the library. If $null is provided, the default label will be removed.

```yaml
Type: SensitivityLabelPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableAttachments
Enable or disable attachments. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableContentTypes
Set to $true to enable content types, set to $false to disable content types

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableFolderCreation
Enable or disable folder creation. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableMinorVersions
Enable or disable minor versions versioning. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableModeration
Enable or disable whether content approval is enabled for the list. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DraftVersionVisibility
Specify which users should be able to view drafts in this list.

```yaml
Type: DraftVisibilityType
Parameter Sets: (All)
Accepted values: Approver, Author, Reader
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableVersioning
Enable or disable versioning. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ForceCheckout
Enable or disable force checkout. Set to $true to enable, $false to disable.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Hidden
Hide the list from the SharePoint UI. Set to $true to hide, $false to show.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListExperience
Set the list experience: Auto, NewExperience or ClassicExperience

```yaml
Type: ListExperience
Parameter Sets: (All)
Accepted values: Auto, NewExperience, ClassicExperience

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MajorVersions
Maximum major versions to keep

```yaml
Type: UInt32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MinorVersions
Maximum major versions for which to keep minor versions

```yaml
Type: UInt32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReadSecurity
Sets the read security for the list

```yaml
Type: ListReadSecurity
Parameter Sets: (All)
Accepted values: AllUsersReadAccess, AllUsersReadAccessOnItemsTheyCreate
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WriteSecurity
Sets the write security for the list

```yaml
Type: ListWriteSecurity
Parameter Sets: (All)
Accepted values: WriteAllItems, WriteOnlyMyItems, WriteNoItems
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the list

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoCrawl
Switch parameter if this list should be indexed in search. 

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExemptFromBlockDownloadOfNonViewableFiles
Allows to configure access capabilities for unmanaged devices for the list. If set to $true, the list will be accessible by unmanaged devices as well. For more information, see [SharePoint and OneDrive unmanaged device access controls for administrators](https://learn.microsoft.com/sharepoint/control-access-from-unmanaged-devices).

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableGridEditing
Enable or disable whether edit grid editing is enabled for the list. Set to $true to disable, $false to enable.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The new URL path of the list. The parent folder must exist and be in the same site/web. I.e. lists\newname.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableAutoExpirationVersionTrim
Enable or disable AutoExpiration version trim for the document library. Set to $true to enable, $false to disable.

Parameter ExpireVersionsAfterDays is required when EnableAutoExpirationVersionTrim is false. Set ExpireVersionsAfterDays to 0 for NoExpiration, set it to greater or equal 30 for ExpireAfter.

Parameter MajorVersions is required when EnableAutoExpirationVersionTrim is false.
Parameter MinorVersions is required when EnableAutoExpirationVersionTrim is false and minor version is enabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExpireVersionsAfterDays
Work with parameter EnableAutoExpirationVersionTrim. Please see description in EnableAutoExpirationVersionTrim.

```yaml
Type: UInt32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```
## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)