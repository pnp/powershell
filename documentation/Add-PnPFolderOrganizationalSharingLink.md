---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFolderOrganizationalSharingLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFolderOrganizationalSharingLink
---
  
# Add-PnPFolderOrganizationalSharingLink

## SYNOPSIS
Creates an organizational sharing link for a folder.

## SYNTAX

```powershell
Add-PnPFolderOrganizationalSharingLink -Folder <FolderPipeBind> -Type <PnP.Core.Model.Security.ShareType> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Creates a new organization sharing link for a folder.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPFolderOrganizationalSharingLink -Folder "/sites/demo/Shared Documents/Test"
```

This will create an organization sharing link for `Test` folder in the `Shared Documents` library which will be viewable by users in the organization.

### EXAMPLE 2
```powershell
Add-PnPFolderOrganizationalSharingLink -Folder "/sites/demo/Shared Documents/Test" -Type Edit
```

This will create an organization sharing link for `Test` folder in the `Shared Documents` library which will be editable by users in the organization.

## PARAMETERS

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

### -Folder
The folder in the site

```yaml
Type: FolderPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShareType
The type of sharing that you want to, i.e do you want to enable people in your organization to view the shared content or also edit the content?

Supported values are `View` and `Edit`

```yaml
Type: PnP.Core.Model.Security.ShareType
Parameter Sets: (All)

Required: False
Position: Named
Default value: View
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
