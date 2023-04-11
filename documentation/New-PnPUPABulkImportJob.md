---
Module Name: PnP.PowerShell
title: New-PnPUPABulkImportJob
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPUPABulkImportJob.html
---
 
# New-PnPUPABulkImportJob

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Submit up a new user profile bulk import job.

## SYNTAX

```powershell
New-PnPUPABulkImportJob [-Folder] <String> [-Path] <String> [-UserProfilePropertyMapping] <Hashtable>
 [-IdProperty] <String> [[-IdType] <ImportProfilePropertiesUserIdType>] [-Wait] [-Verbose] [-Connection <PnPConnection>]
 
```

```powershell
New-PnPUPABulkImportJob -Url <String> [-UserProfilePropertyMapping] <Hashtable>
 [-IdProperty] <String> [[-IdType] <ImportProfilePropertiesUserIdType>] [-Wait] [-Verbose] [-Connection <PnPConnection>]
 
```

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

### -Folder
Site or server relative URL of the folder to where you want to store the import job file.

```yaml
Type: String
Parameter Sets: Submit up a new user profile bulk import job from local file

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IdProperty
The name of the property identifying the user in your JSON file to update the user profile for

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IdType
The type of profile identifier (Email/CloudId/PrincipalName). Defaults to Email.

```yaml
Type: ImportProfilePropertiesUserIdType
Parameter Sets: (All)
Accepted values: Email, CloudId, PrincipalName

Required: False
Position: 4
Default value: Email
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The local file path of the JSON file to use for the user profile import

```yaml
Type: String
Parameter Sets: Submit up a new user profile bulk import job from local file

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The full url of the JSON file saved in SharePoint Online containing the identities and properties to import into the SharePoint Online User Profiles

```yaml
Type: String
Parameter Sets: Submit up a new user profile bulk import job job from url

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserProfilePropertyMapping
Specify user profile property mapping between the import file and UPA property names, i.e. `@{"JobTitle"="Title"}` where the left side represents the property in the JSON file and the right side the name of the property in the SharePoint Online User Profile Service.

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: True
Position: 2
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

### -Wait
Adding this parameter will cause the script to start the user profile sync operation and wait with proceeding with the rest of the script until the user profiles have been imported into the SharePoint Online user profile. It can take a long time for the user profile sync operation to complete. It will check every 30 seconds for the current status of the job, to avoid getting throttled. The check interval is non configurable.

Add `-Verbose` as well to be notified about the progress while waiting.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while going through the user profile sync steps.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Bulk update custom user profile properties for SharePoint Online](https://learn.microsoft.com/sharepoint/dev/solution-guidance/bulk-user-profile-update-api-for-sharepoint-online)
