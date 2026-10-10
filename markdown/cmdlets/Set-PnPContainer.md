---
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPContainer.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPContainer
schema: 2.0.0
Module Name: PnP.PowerShell
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
---
 
# Set-PnPContainer

## SYNOPSIS

**Required Permissions**

* For all parameters except those below: SharePoint Embedded Administrator or Global Administrator role is required
* For `-Name`, `-Description`, `-OcrEnabled`, `-ItemVersioningEnabled` and `-ItemMajorVersionLimit`: Microsoft Graph API : FileStorageContainer.Selected, plus permission on the Container Type for the application you connect with, or FileStorageContainer.Manage.All on behalf of a SharePoint Embedded Administrator

Sets or updates one or more properties of a Container in SharePoint Embedded.

## SYNTAX

### Information barriers mode (Default)
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Sensitivity label
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> -SensitivityLabel <String> [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Remove sensitivity label
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> -RemoveLabel [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Restrict content org wide search
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> -RestrictContentOrgWideSearch <Boolean> [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Block download policy
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> [-BlockDownloadPolicy <Boolean>] [-ExcludeBlockDownloadPolicyContainerOwners <Boolean>] [-ReadOnlyForBlockDownloadPolicy <Boolean>] [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Restricted access control
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> [-EnableRestrictedAccessControl <Boolean>] [-RestrictedAccessControlGroups <Guid[]>] [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Add restricted access control groups
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> -RestrictedAccessControlGroupsToAdd <Guid[]> [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Remove restricted access control groups
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> -RestrictedAccessControlGroupsToRemove <Guid[]> [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Clear restricted access control
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> -ClearRestrictedAccessControl [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Conditional access
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> [-ConditionalAccessPolicy <SPOConditionalAccessPolicyType>] [-LimitedAccessFileType <SPOLimitedAccessFileType>] [-AllowEditing <Boolean>] [-ReadOnlyForUnmanagedDevices <Boolean>] [-AuthenticationContextName <String>] [-Force] [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Sharing domain restriction
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> -SharingDomainRestrictionMode <SharingDomainRestrictionModes> [-SharingAllowedDomainList <String>] [-SharingBlockedDomainList <String>] [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Principal owner transfer
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> -CurrentPrincipalOwner <String> -NewPrincipalOwner <String> [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Add information barrier segments
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> -AddInformationSegment <Guid[]> [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Remove information barrier segments
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> -RemoveInformationSegment <Guid[]> [-InformationBarriersMode <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

### Container properties
```powershell
Set-PnPContainer [-Identity] <ContainerPipeBind> [-Name <String>] [-Description <String>] [-OcrEnabled <Boolean>] [-ItemVersioningEnabled <Boolean>] [-ItemMajorVersionLimit <Int32>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Changes only the properties for the parameters that are passed in, on the active Container identified by `-Identity`. Each parameter set changes one group of properties; `-InformationBarriersMode` can be combined with any of them, except the Container properties parameter set. For the settings changed through the SharePoint Online admin API, the Container is always read from the server first, so a Container object passed in is not modified and its values are not written back.

Some parameters reset related properties:
- `-ConditionalAccessPolicy` other than `AllowLimitedAccess` turns off `ReadOnlyForUnmanagedDevices`; other than `AuthenticationContext` it clears the authentication context name; and `AllowFullAccess` allows editing.
- `-BlockDownloadPolicy` resets `ExcludeBlockDownloadPolicyContainerOwners` and `ReadOnlyForBlockDownloadPolicy` to the values passed in, or to `$false`.

The name, description, optical character recognition and versioning settings are changed through Microsoft Graph, which needs different permissions but no access to the SharePoint Online Admin Center, so the application owning the Container can change them. For these, specify the Container by its id or its api url. Changing them also activates an inactive Container. They cannot be combined with `-InformationBarriersMode`. All other settings are changed through the SharePoint Online admin API.

The cmdlet fails for an archived Container. Transferring the principal owner is only supported for Containers that are owned by a user. Always wait for a principal owner transfer to finish before running the cmdlet again, as running it concurrently or too early can lead to incomplete or invalid ownership changes.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPContainer -Identity "https://contoso.sharepoint.com/contentstorage/CSP_33a63968-abae-49a3-a255-f83d0ab2260a" -BlockDownloadPolicy $true
```

Turns on the block download policy for the Container.

### EXAMPLE 2
```powershell
Set-PnPContainer -Identity "https://contoso.sharepoint.com/contentstorage/CSP_33a63968-abae-49a3-a255-f83d0ab2260a" -ConditionalAccessPolicy AllowLimitedAccess -LimitedAccessFileType OfficeOnlineFilesOnly
```

Gives limited, web-only access to the content of the Container, in which only Office files can be previewed in the browser.

### EXAMPLE 3
```powershell
Set-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -SensitivityLabel ab310e93-9f19-43f2-bc19-bf3386dc0956
```

Sets a sensitivity label on the Container.

### EXAMPLE 4
```powershell
Set-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -RemoveLabel
```

Removes the sensitivity label from the Container.

### EXAMPLE 5
```powershell
Set-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -SharingDomainRestrictionMode AllowList -SharingAllowedDomainList "contoso.com fabrikam.com"
```

Only allows sharing with external users from the contoso.com and fabrikam.com domains.

### EXAMPLE 6
```powershell
Set-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -EnableRestrictedAccessControl $true -RestrictedAccessControlGroups 5e3f9c1a-7d2b-4c8e-9f1a-2b3c4d5e6f70
```

Restricts access to the Container to the members of the specified group.

### EXAMPLE 7
```powershell
Set-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -AddInformationSegment a17efb47-e3c9-4d85-a188-1cd59c83de32
```

Adds the specified information barrier segment to the Container.

### EXAMPLE 8
```powershell
Set-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -Name "Contoso Legal" -OcrEnabled $true
```

Renames the Container and enables optical character recognition on its documents.

## PARAMETERS

### -AddInformationSegment

Adds information barrier segments to the Container. Only available for tenants with Microsoft 365 Information Barriers enabled.

```yaml
Type: Guid[]
Parameter Sets: Add information barrier segments

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowEditing

Whether users can edit Office files in the browser and copy and paste their contents out of the browser window, when the conditional access policy is `AllowLimitedAccess`. Requires `-ConditionalAccessPolicy AllowLimitedAccess`; when that is not passed, you are asked to set the policy to `AllowLimitedAccess`.

```yaml
Type: Boolean
Parameter Sets: Conditional access

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AuthenticationContextName

The name of the Conditional Access authentication context. Can only be used with `-ConditionalAccessPolicy AuthenticationContext`. When omitted, the current name is kept.

```yaml
Type: String
Parameter Sets: Conditional access

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockDownloadPolicy

Blocks downloading files from the Container, without needing Microsoft Entra Conditional Access policies. Users get browser-only access: they can't download, print or sync files, nor open them in apps, including the Office desktop apps.

```yaml
Type: Boolean
Parameter Sets: Block download policy

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClearRestrictedAccessControl

Clears the list of groups given access to the Container through the restricted access control policy.

```yaml
Type: SwitchParameter
Parameter Sets: Clear restricted access control

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConditionalAccessPolicy

The Conditional Access policy of the Container:
- AllowFullAccess: full access from desktop apps, mobile apps and the web.
- AllowLimitedAccess: limited, web-only access.
- BlockAccess: blocks access.
- AuthenticationContext: requires the authentication context given with `-AuthenticationContextName`.

```yaml
Type: SPOConditionalAccessPolicyType
Parameter Sets: Conditional access
Accepted values: AllowFullAccess, AllowLimitedAccess, BlockAccess, AuthenticationContext

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

### -CurrentPrincipalOwner

The current principal owner of the Container.

```yaml
Type: String
Parameter Sets: Principal owner transfer

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description

The new description of the Container. An empty string removes the description. Changed through Microsoft Graph.

```yaml
Type: String
Parameter Sets: Container properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableRestrictedAccessControl

Restricts access to the Container to the members of the groups given with `-RestrictedAccessControlGroups`. The restricted access control groups can only be changed while it is enabled.

```yaml
Type: Boolean
Parameter Sets: Restricted access control

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeBlockDownloadPolicyContainerOwners

Excludes the owners of the Container from the block download policy. Can only be used with `-BlockDownloadPolicy $true`.

```yaml
Type: Boolean
Parameter Sets: Block download policy

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force

Sets the conditional access policy to `AllowLimitedAccess` without asking for confirmation, when `-LimitedAccessFileType`, `-AllowEditing` or `-ReadOnlyForUnmanagedDevices` is used without `-ConditionalAccessPolicy` on a Container with another policy.

```yaml
Type: SwitchParameter
Parameter Sets: Conditional access

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

The url, the api url or the id of the Container. With `-Name`, `-Description`, `-OcrEnabled`, `-ItemVersioningEnabled` or `-ItemMajorVersionLimit`, only the id or the api url.

```yaml
Type: ContainerPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -InformationBarriersMode

The information barriers mode of the Container.

```yaml
Type: String
Parameter Sets: Information barriers mode, Sensitivity label, Remove sensitivity label, Restrict content org wide search, Block download policy, Restricted access control, Add restricted access control groups, Remove restricted access control groups, Clear restricted access control, Conditional access, Sharing domain restriction, Principal owner transfer, Add information barrier segments, Remove information barrier segments

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ItemMajorVersionLimit

The maximum number of major versions kept of each file in the Container. Changed through Microsoft Graph.

```yaml
Type: Int32
Parameter Sets: Container properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ItemVersioningEnabled

Whether versioning is enabled for the files in the Container. Changed through Microsoft Graph.

```yaml
Type: Boolean
Parameter Sets: Container properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LimitedAccessFileType

The files users can access when the conditional access policy is `AllowLimitedAccess`:
- OfficeOnlineFilesOnly: users can only preview Office files in the browser. More secure, but may hinder productivity.
- WebPreviewableFiles: users can preview Office files and other file types, such as PDF files and images, in the browser.
- OtherFiles: users can download files that can't be previewed, such as .zip and .exe files. Less secure.

Requires `-ConditionalAccessPolicy AllowLimitedAccess`; when that is not passed, you are asked to set the policy to `AllowLimitedAccess`.

```yaml
Type: SPOLimitedAccessFileType
Parameter Sets: Conditional access
Accepted values: OfficeOnlineFilesOnly, WebPreviewableFiles, OtherFiles

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name

The new display name of the Container. Changed through Microsoft Graph.

```yaml
Type: String
Parameter Sets: Container properties
Aliases: DisplayName

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NewPrincipalOwner

The user to transfer the principal ownership of the Container to.

```yaml
Type: String
Parameter Sets: Principal owner transfer

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OcrEnabled

Whether optical character recognition is performed on new and updated documents in the Container, so their text can be searched. Turning it off leaves existing recognised text in place. Changed through Microsoft Graph.

```yaml
Type: Boolean
Parameter Sets: Container properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReadOnlyForBlockDownloadPolicy

Makes the Container read-only under the block download policy. Can only be used with `-BlockDownloadPolicy $true`.

```yaml
Type: Boolean
Parameter Sets: Block download policy

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReadOnlyForUnmanagedDevices

Gives unmanaged devices read-only access to the Container. Requires `-ConditionalAccessPolicy AllowLimitedAccess`; when that is not passed, you are asked to set the policy to `AllowLimitedAccess`.

```yaml
Type: Boolean
Parameter Sets: Conditional access

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveInformationSegment

Removes information barrier segments from the Container. Only available for tenants with Microsoft 365 Information Barriers enabled.

```yaml
Type: Guid[]
Parameter Sets: Remove information barrier segments

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveLabel

Removes the sensitivity label from the Container.

```yaml
Type: SwitchParameter
Parameter Sets: Remove sensitivity label

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictContentOrgWideSearch

Whether the content of the Container is excluded from organization wide search.

```yaml
Type: Boolean
Parameter Sets: Restrict content org wide search

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictedAccessControlGroups

The ids of the groups whose members can access the Container under the restricted access control policy. Cannot be used with `-EnableRestrictedAccessControl $false`.

```yaml
Type: Guid[]
Parameter Sets: Restricted access control

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictedAccessControlGroupsToAdd

The ids of groups to add to the restricted access control policy, granting their members access.

```yaml
Type: Guid[]
Parameter Sets: Add restricted access control groups

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictedAccessControlGroupsToRemove

The ids of groups to remove from the restricted access control policy, revoking their members' access.

```yaml
Type: Guid[]
Parameter Sets: Remove restricted access control groups

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SensitivityLabel

The id of the sensitivity label to assign to the Container.

```yaml
Type: String
Parameter Sets: Sensitivity label

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingAllowedDomainList

The email domains external users must belong to for content to be shared with them, separated by spaces, such as "contoso.com fabrikam.com". Requires `-SharingDomainRestrictionMode AllowList`. When the tenant also uses an allow list, the domains must be on it.

```yaml
Type: String
Parameter Sets: Sharing domain restriction

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingBlockedDomainList

The email domains of external users content cannot be shared with, separated by spaces, such as "contoso.com fabrikam.com". Requires `-SharingDomainRestrictionMode BlockList`.

```yaml
Type: String
Parameter Sets: Sharing domain restriction

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingDomainRestrictionMode

Restricts sharing with external users by their email domain:
- None: does not restrict sharing by domain, and removes the domain lists.
- AllowList: only allows sharing with external users from the domains in `-SharingAllowedDomainList`.
- BlockList: allows sharing with external users from every domain except those in `-SharingBlockedDomainList`.

A Container cannot use a block list when the tenant uses an allow list.

```yaml
Type: SharingDomainRestrictionModes
Parameter Sets: Sharing domain restriction
Accepted values: None, AllowList, BlockList

Required: True
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
[Manage containers with PowerShell](https://learn.microsoft.com/sharepoint/dev/embedded/admin/manage-containers-powershell)

