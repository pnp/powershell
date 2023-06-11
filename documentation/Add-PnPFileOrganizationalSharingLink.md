---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFileOrganizationalSharingLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFileOrganizationalSharingLink
---
  
# Add-PnPFileOrganizationalSharingLink

## SYNOPSIS
Creates an organizational sharing link for a file.

## SYNTAX

```powershell
Add-PnPFileOrganizationalSharingLink -FileUrl <String> -Type <PnP.Core.Model.Security.ShareType> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Creates a new organization sharing link for a file.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPFileOrganizationalSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx"
```

This will create an organization sharing link for `Test.docx` file in the `Shared Documents` library which will be viewable by users in the organization.

### EXAMPLE 2
```powershell
Add-PnPFileOrganizationalSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx" -Type Edit
```

This will create an organization sharing link for `Test.docx` file in the `Shared Documents` library which will be editable by users in the organization.

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

### -FileUrl
The file in the site

```yaml
Type: String
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
