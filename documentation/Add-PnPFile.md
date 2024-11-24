---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPFile.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPFile
---

# Add-PnPFile

## SYNOPSIS

Uploads a file to Web

## SYNTAX

### Upload file

```
Add-PnPFile -Path <String> -Folder <FolderPipeBind> [-NewFileName <String>] [-Checkout]
 [-CheckInComment <String>] [-CheckinType <CheckinType>] [-Approve] [-ApproveComment <String>]
 [-Publish] [-PublishComment <String>] [-UseWebDav] [-Values <Hashtable>]
 [-ContentType <ContentTypePipeBind>] [-Connection <PnPConnection>]
```

### Upload file from stream

```
Add-PnPFile -Folder <FolderPipeBind> -FileName <String> -Stream <Stream> [-Checkout]
 [-CheckInComment <String>] [-CheckinType <CheckinType>] [-Approve] [-ApproveComment <String>]
 [-Publish] [-PublishComment <String>] [-UseWebDav] [-Values <Hashtable>]
 [-ContentType <ContentTypePipeBind>] [-Connection <PnPConnection>]
```

### Create or update file from text

```
Add-PnPFile -Folder <FolderPipeBind> -FileName <String> -Content <text> [-Checkout]
 [-CheckInComment <String>] [-CheckinType <CheckinType>] [-Approve] [-ApproveComment <String>]
 [-Publish] [-PublishComment <String>] [-UseWebDav] [-Values <Hashtable>]
 [-ContentType <ContentTypePipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

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
Add-PnPFile -Path .\sample.doc -Folder "Shared Documents" -Values @{Modified="12/28/2023"}
```

This will upload the file sample.doc to the Shared Documents folder. After uploading it will set the Modified date to 12/28/2023.

### EXAMPLE 4

```powershell
Add-PnPFile -FileName sample.doc -Folder "Shared Documents" -Stream $fileStream -Values @{Modified="12/28/2023"}
```

This will add a file sample.doc with the contents of the stream into the Shared Documents folder. After adding it will set the Modified date to 12/28/2023.

### EXAMPLE 5

```powershell
Add-PnPFile -Path sample.doc -Folder "Shared Documents" -ContentType "Document" -Values @{Modified="12/28/2023"}
```

This will add a file sample.doc to the Shared Documents folder, with a ContentType of 'Documents'. After adding it will set the Modified date to 12/28/2023.

### EXAMPLE 6

```powershell
Add-PnPFile -Path sample.docx -Folder "Documents" -Values @{Modified="12/28/2016"; Created="12/28/2023"; Editor=23}
```

This will add a file sample.docx to the Documents folder and will set the Modified date to 12/28/2016, Created date to 12/28/2023 and the Modified By field to the user with ID 23. To find out about the proper user ID to relate to a specific user, use Get-PnPUser.

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

### -ApproveComment

The comment added to the approval

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

### -CheckInComment

The comment added to the checkin

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

### -CheckinType

Specifies the type of check-in for a file.

```yaml
Type: Enum (Microsoft.SharePoint.Client.CheckinType)
DefaultValue: MinorCheckIn
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

### -Checkout

If versioning is enabled, this will check out the file first if it exists, upload the file, then check it in again

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

### -Content

Content to add to the file to create or overwrite on SharePoint. It will blindly overwrite the contents of the file if it already exists.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: ASTEXT
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ContentType

Use to assign a ContentType to the file

```yaml
Type: ContentTypePipeBind
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

### -FileName

Name for file

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Upload file from stream
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Folder

The destination folder in the site

```yaml
Type: FolderPipeBind
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

### -NewFileName

Filename to give the file on SharePoint

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Upload file
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Path

The local file path

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Upload file
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Publish

Will auto publish the file

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

### -PublishComment

The comment added to the publish action

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

### -Stream

Stream with the file contents

```yaml
Type: Stream
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Upload file from stream
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -UseWebDav



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

### -Values

Use the internal names of the fields when specifying field names.

Single line of text: -Values @{"Title" = "Title New"}

Multiple lines of text: -Values @{"MultiText" = "New text\n\nMore text"}

Rich text: -Values @{"MultiText" = "&lt;strong&gt;New&lt;/strong&gt; text"}

Choice: -Values @{"Choice" = "Value 1"}

Number: -Values @{"Number" = "10"}

Currency: -Values @{"Number" = "10"}

Currency: -Values @{"Currency" = "10"}

Date and Time: -Values @{"DateAndTime" = "04/20/2023 14:16"} (use mm/dd/yyyy)

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
