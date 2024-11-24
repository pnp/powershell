---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPUPABulkImportJob.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPUPABulkImportJob
---

# New-PnPUPABulkImportJob

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Submit up a new user profile bulk import job.

## SYNTAX

### Default (Default)

```
New-PnPUPABulkImportJob [-Folder] <String> [-Path] <String>
 [-UserProfilePropertyMapping] <Hashtable> [-IdProperty] <String>
 [[-IdType] <ImportProfilePropertiesUserIdType>] [-Wait] [-Verbose] [-Connection <PnPConnection>]
 [-WhatIf] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

See https://learn.microsoft.com/sharepoint/dev/solution-guidance/bulk-user-profile-update-api-for-sharepoint-online for information on the API and how the bulk import process works.

## EXAMPLES

### EXAMPLE 1

```powershell
@"
 {
  "value": [
    {
      "IdName": "mikaels@contoso.com",
      "Department": "PnP",
    },
	{
      "IdName": "vesaj@contoso.com",
      "Department": "PnP",
    }
  ]
}
"@ > profiles.json

New-PnPUPABulkImportJob -Folder "Shared Documents" -Path profiles.json -IdProperty "IdName" -UserProfilePropertyMapping @{"Department"="Department"}
```

This will submit a new user profile bulk import job to SharePoint Online using a local file.

### EXAMPLE 2

```powershell
New-PnPUPABulkImportJob -Url "https://{tenant}.sharepoint.com/Shared Documents/profiles.json" -IdProperty "IdName" -UserProfilePropertyMapping @{"Department"="Department"}
```

This will submit a new user profile bulk import job to SharePoint Online using an already uploaded file.

### EXAMPLE 3

```powershell
New-PnPUPABulkImportJob -Url "https://{tenant}.sharepoint.com/sites/userprofilesync/Shared Documents/profiles.json" -IdProperty "IdName" -UserProfilePropertyMapping @{"Department"="Department"} -Wait -Verbose
```

This will submit a new user profile bulk import job to SharePoint Online using an already uploaded file and will wait until the import has finished.

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

### -Folder

Site or server relative URL of the folder to where you want to store the import job file.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Submit up a new user profile bulk import job from local file
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IdProperty

The name of the property identifying the user in your JSON file to update the user profile for

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 3
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IdType

The type of profile identifier (Email/CloudId/PrincipalName). Defaults to Email.

```yaml
Type: ImportProfilePropertiesUserIdType
DefaultValue: Email
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 4
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Email
- CloudId
- PrincipalName
HelpMessage: ''
```

### -Path

The local file path of the JSON file to use for the user profile import

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Submit up a new user profile bulk import job from local file
  Position: 1
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Url

The full url of the JSON file saved in SharePoint Online containing the identities and properties to import into the SharePoint Online User Profiles

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Submit up a new user profile bulk import job job from url
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -UserProfilePropertyMapping

Specify user profile property mapping between the import file and UPA property names, i.e. `@{"JobTitle"="Title"}` where the left side represents the property in the JSON file and the right side the name of the property in the SharePoint Online User Profile Service.

```yaml
Type: Hashtable
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 2
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Verbose

When provided, additional debug statements will be shown while going through the user profile sync steps.

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

### -Wait

Adding this parameter will cause the script to start the user profile sync operation and wait with proceeding with the rest of the script until the user profiles have been imported into the SharePoint Online user profile. It can take a long time for the user profile sync operation to complete. It will check every 30 seconds for the current status of the job, to avoid getting throttled. The check interval is non configurable.

Add `-Verbose` as well to be notified about the progress while waiting.

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

### -WhatIf

Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- wi
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
- [Bulk update custom user profile properties for SharePoint Online](https://learn.microsoft.com/sharepoint/dev/solution-guidance/bulk-user-profile-update-api-for-sharepoint-online)
