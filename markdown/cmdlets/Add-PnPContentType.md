---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPContentType.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPContentType
---
  
# Add-PnPContentType

## SYNOPSIS
Adds a new content type.

## SYNTAX

```powershell
Add-PnPContentType -Name <String> [-ContentTypeId <String>] [-Description <String>] [-Group <String>]
 [-ParentContentType <ContentType>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Adds a new content type to a site. To create a content type in the modern Content Type Gallery, first connect to the content type hub site for the tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPContentType -Name "Project Document" -Description "Use for Contoso projects" -Group "Contoso Content Types" -ParentContentType $ct
```

This will add a new content type based on the parent content type stored in the $ct variable.

### EXAMPLE 2
```powershell
Add-PnPContentType -Name "Project Document" -Description "Use for Contoso projects" -Group "Contoso Content Types" -ParentContentType (Get-PnPContentType -Identity 0x0101) -DocumentTemplate "/_cts/Project Document/template.docx"
```

This will add a new content type based on the standard document content type and assigns the document template template.docx to it.

### EXAMPLE 3
```powershell
Add-PnPContentType -Name "Project Item" -Description "Use for Contoso projects" -Group "Contoso Content Types"
```

This will add a new content type based on the item content type. 

### EXAMPLE 4
```powershell
Add-PnPContentType -Name "Project Item"
```

This will add a new content type based on the item content type to a group "Custom Content Types".

### EXAMPLE 5
```powershell
Add-PnPContentType -Name "Project Document" -Description "Use for Contoso projects" -Group "Contoso Content Types" -ContentTypeId 0x010100CD5BDB7DDE03324794E155CE37E4B6BB
```

This will add a new content type to a group "Contoso Content Types". The content type will be based on the standard document content type, and with the specified content type id. Mind the content type id format: https://learn.microsoft.com/en-us/previous-versions/office/developer/sharepoint-2010/aa543822(v=office.14)

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

### -ContentTypeId
If specified, in the format of 0x0100233af432334r434343f32f3, will create a content type with the specific ID.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
Specifies the description of the new content type.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
Specifies the group of the new content type.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Specify the name of the new content type.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ParentContentType
Specifies the parent of the new content type.

```yaml
Type: ContentType
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DocumentTemplate
Allows providing a server relative path to a file which should be used as the document template for this content type.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


