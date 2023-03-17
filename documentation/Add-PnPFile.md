---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFile.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFile
---
  
# Add-PnPFile

## SYNOPSIS
Uploads a file to Web

## SYNTAX

### Upload file
```powershell
Add-PnPFile -Path <String> -Folder <FolderPipeBind> [-NewFileName <String>] [-Checkout] [-CheckInComment <String>] [-CheckinType <CheckinType>]
 [-Approve] [-ApproveComment <String>] [-Publish] [-PublishComment <String>] [-UseWebDav] [-Values <Hashtable>]
 [-ContentType <ContentTypePipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Upload file from stream
```powershell
Add-PnPFile -Folder <FolderPipeBind> -FileName <String> -Stream <Stream> [-Checkout] [-CheckInComment <String>] [-CheckinType <CheckinType>]
 [-Approve] [-ApproveComment <String>] [-Publish] [-PublishComment <String>] [-UseWebDav] [-Values <Hashtable>]
 [-ContentType <ContentTypePipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Create or update file from text
```powershell
Add-PnPFile -Folder <FolderPipeBind> -FileName <String> -Content <text> [-Checkout] [-CheckInComment <String>] [-CheckinType <CheckinType>]
 [-Approve] [-ApproveComment <String>] [-Publish] [-PublishComment <String>] [-UseWebDav] [-Values <Hashtable>]
 [-ContentType <ContentTypePipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet uploads a local file, file from a stream or plain text to the specified folder.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPFile -Path c:\temp\company.master -Folder "_catalogs/masterpage"
```

This will upload the file company.master to the masterpage catalog

### EXAMPLE 2
```powershell
Add-PnPFile -Path .\displaytemplate.html -Folder "_catalogs/masterpage/display templates/test"
```

This will upload the file displaytemplate.html to the test folder in the display templates folder. If the test folder does not exist it will create it.

### EXAMPLE 3
```powershell
Add-PnPFile -Path .\sample.doc -Folder "Shared Documents" -Values @{Modified="1/1/2016"}
```

This will upload the file sample.doc to the Shared Documents folder. After uploading it will set the Modified date to 1/1/2016.

### EXAMPLE 4
```powershell
Add-PnPFile -FileName sample.doc -Folder "Shared Documents" -Stream $fileStream -Values @{Modified="1/1/2016"}
```

This will add a file sample.doc with the contents of the stream into the Shared Documents folder. After adding it will set the Modified date to 1/1/2016.

### EXAMPLE 5
```powershell
Add-PnPFile -Path sample.doc -Folder "Shared Documents" -ContentType "Document" -Values @{Modified="1/1/2016"}
```

This will add a file sample.doc to the Shared Documents folder, with a ContentType of 'Documents'. After adding it will set the Modified date to 1/1/2016.

### EXAMPLE 6
```powershell
Add-PnPFile -Path sample.docx -Folder "Documents" -Values @{Modified="1/1/2016"; Created="1/1/2017"; Editor=23}
```

This will add a file sample.docx to the Documents folder and will set the Modified date to 1/1/2016, Created date to 1/1/2017 and the Modified By field to the user with ID 23. To find out about the proper user ID to relate to a specific user, use Get-PnPUser.

### EXAMPLE 7
```powershell
Add-PnPFile -Path sample.docx -Folder "Documents" -NewFileName "differentname.docx"
```

This will upload a local file sample.docx to the Documents folder giving it the filename differentname.docx on SharePoint

### EXAMPLE 8
```powershell
Add-PnPFile -FileName sample.txt -Folder "Shared Documents" -Content '{ "Test": "Value" }'
```

This will create a file sample.docx in the Documents library inserting the provided plain text into it. If a similarly file already exists at this location, its contents will be overwritten.

## PARAMETERS

### -Approve
Will auto approve the uploaded file

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApproveComment
The comment added to the approval

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CheckInComment
The comment added to the checkin

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CheckinType
Specifies the type of check-in for a file.

```yaml
Type: Enum (Microsoft.SharePoint.Client.CheckinType)
Parameter Sets: (All)

Required: False
Position: Named
Default value: MinorCheckIn
Accept pipeline input: False
Accept wildcard characters: False
```

### -Checkout
If versioning is enabled, this will check out the file first if it exists, upload the file, then check it in again

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

### -ContentType
Use to assign a ContentType to the file

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileName
Name for file

```yaml
Type: String
Parameter Sets: Upload file from stream

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Folder
The destination folder in the site

```yaml
Type: FolderPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NewFileName
Filename to give the file on SharePoint

```yaml
Type: String
Parameter Sets: Upload file

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The local file path

```yaml
Type: String
Parameter Sets: Upload file

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Publish
Will auto publish the file

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PublishComment
The comment added to the publish action

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Stream
Stream with the file contents

```yaml
Type: Stream
Parameter Sets: Upload file from stream

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseWebDav

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Content
Content to add to the file to create or overwrite on SharePoint. It will blindly overwrite the contents of the file if it already exists.

```yaml
Type: String
Parameter Sets: ASTEXT

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values
Use the internal names of the fields when specifying field names.

Single line of text: -Values @{"Title" = "Title New"}

Multiple lines of text: -Values @{"MultiText" = "New text\n\nMore text"}

Rich text: -Values @{"MultiText" = "&lt;strong&gt;New&lt;/strong&gt; text"}

Choice: -Values @{"Choice" = "Value 1"}

Number: -Values @{"Number" = "10"}

Currency: -Values @{"Number" = "10"}

Currency: -Values @{"Currency" = "10"}

Date and Time: -Values @{"DateAndTime" = "03/10/2015 14:16"}

Lookup (id of lookup value): -Values @{"Lookup" = "2"}

Multi value lookup (id of lookup values as array 1): -Values @{"MultiLookupField" = "1","2"}

Multi value lookup (id of lookup values as array 2): -Values @{"MultiLookupField" = 1,2}

Multi value lookup (id of lookup values as string): -Values @{"MultiLookupField" = "1,2"}

Yes/No: -Values @{"YesNo" = $false}

Person/Group (id of user/group in Site User Info List or email of the user, separate multiple values with a comma): -Values @{"Person" = "user1@domain.com","21"}

Managed Metadata (single value with path to term): -Values @{"MetadataField" = "CORPORATE|DEPARTMENTS|FINANCE"}

Managed Metadata (single value with id of term): -Values @{"MetadataField" = "fe40a95b-2144-4fa2-b82a-0b3d0299d818"} with Id of term

Managed Metadata (multiple values with paths to terms): -Values @{"MetadataField" = "CORPORATE|DEPARTMENTS|FINANCE","CORPORATE|DEPARTMENTS|HR"}

Managed Metadata (multiple values with ids of terms): -Values @{"MetadataField" = "fe40a95b-2144-4fa2-b82a-0b3d0299d818","52d88107-c2a8-4bf0-adfa-04bc2305b593"}

Hyperlink or Picture: -Values @{"Hyperlink" = "https://github.com/OfficeDev/, OfficePnP"}

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)