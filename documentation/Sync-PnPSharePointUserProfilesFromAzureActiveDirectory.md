---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Sync-PnPSharePointUserProfilesFromAzureActiveDirectory.html
external help file: PnP.PowerShell.dll-Help.xml
title: Sync-PnPSharePointUserProfilesFromAzureActiveDirectory
---
  
# Sync-PnPSharePointUserProfilesFromAzureActiveDirectory

## SYNOPSIS
**Required Permissions**

* SharePoint: Sites.FullControl.All, TermStore.ReadWrite.All, User.ReadWrite.All
* Microsoft Graph: User.Read
* ACS: No longer needed
  
Synchronizes user profiles from Entra ID into the SharePoint Online User Profiles

## SYNTAX

### Upload file
```powershell
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping <Hashtable> [-IdType <Enum>] [-Users <Array>] [-Folder <String>] [-Wait] [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet allows synchronizing user profiles from Entra ID to their SharePoint Online User Profile equivalents. Note that certain properties are already synced by default. A list of these can be found here: https://learn.microsoft.com/sharepoint/user-profile-sync#properties-that-are-synced-into-sharepoint-user-profiles

For other properties not listed on this page, you can use this cmdlet to synchronize them. You can provide the property name(s) in Entra ID and specify its equivalent property in SharePoint Online for the values to be mapped to.

Note that SharePoint Online User Profile properties you wish to sync to *must* have the checkbox unchecked for "Allow users to edit values for this property" in the user profile property in the SharePoint User Profile service application. It also *must* have "User can override" checked under Policy Settings of the user profile property in the SharePoint User Profile service application.

When running this cmdlet, it will upload a file named `userprofilesyncdata-<timestamp>-<guid>.json` to the document library of the SharePoint Online site you are connected to. From there an asynchronous process will be started that processes the JSON file and updates the user profiles on the SharePoint Online side. The time before this process starts varies. Once that process is done and only if something failed, you will find a new folder created in the same document library of which the folder name starts with the same name as the filename. It will contain a .log file in which you can find the results of it trying to update the user profiles in SharePoint Online which were specified in the JSON file. If all the user profile properties have been updated successfully, it will not create such a folder and log file.

You can also query the import job status using `Get-PnPUPABulkImportStatus -JobId <jobid>`. The jobid will be returned upon running this cmdlet and can be fed into this cmdlet to get the actual status. It will show `State: Submitted` after running this cmdlet and before processing it and `State: Succeeded` once its done and was successful or `State: Error` if it failed. It will also return full details on the file it will use to update the user profiles and the location of the log file once its done processing and only if it failed. For documentation on all the possible states it can be in, see https://learn.microsoft.com/sharepoint/dev/solution-guidance/bulk-user-profile-update-api-for-sharepoint-online#parameters-2. 

When not providing -Users, it will fetch all the users and the properties defined in the mapping from Entra ID itself. You can also opt to query for a subset of Entra ID users to update using i.e. `Get-PnPAzureAdUser` and feed the outcome of that to the -Users parameter. In this case you must ensure that the user objects you supply contain the properties you wish to sync towards SharePoint Online.

When not providing -Folder, it will assume a document library named "Shared Documents" is present within the site collection you're currently connected to. In case you are not using an English site collection, this name may be different and localized. In that case use the -Folder parameter passing in the localized name of the document library you wish to upload the mapping file to.

**Required Permissions**

It is no longer needed to use ACS permissions for this cmdlet to work. The following permissions, granted through an Entra ID application registration, should suffice. It can also be used using a Managed Identity within Azure using the same permissions.

* SharePoint: Sites.FullControl.All, TermStore.ReadWrite.All, User.ReadWrite.All
* Microsoft Graph: User.Read

## EXAMPLES

### EXAMPLE 1
```powershell
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping @{"HomePhone"="phone";"CustomProperty"="DisplayName"}
```

This will retrieve all users in Entra ID and take its phone property to update in the HomePhone field in the SharePoint Online user profiles for each of these users. Similarly it will set the SharePoint Online User Profile property named CustomProperty to the value of the DisplayName as set in Entra ID on the user object. It will upload the JSON file with the instructions for the update to the 'Shared Documents' library of the site currently connected to.

### EXAMPLE 2
```powershell
$users = Get-PnPAzureADUser -Filter "jobTitle eq 'IT Administrator'"
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping @{"CostCenter"="extension_b0b5aaa58a0a4287acd826c5b8330e48_CostCenter"} -Users $users
```

This will update the CostCenter SharePoint Online User Profile property with the value of the property extension_b0b5aaa58a0a4287acd826c5b8330e48_CostCenter coming from Entra ID for the users getting returned by the Get-PnPAzureADUser query. It will upload the JSON file with the instructions for the update to the 'Shared Documents' library of the site currently connected to.

### EXAMPLE 3
```powershell
$delta = Get-PnPAzureADUser -Delta -DeltaToken $delta.DeltaToken
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping @{"CostCenter"="extension_b0b5aaa58a0a4287acd826c5b8330e48_CostCenter"} -Users $delta.Users
```

This will retrieve all the users from Entra ID and includes a DeltaToken in the response. Using the DeltaToken you can retrieve only those users which have had changes done to their attributes since the DeltaToken was given out. This makes it ideal to use with the profile sync as this way you only will sync those users that have had changes to their profiles. Only for those users this will update the CostCenter SharePoint Online User Profile property with the value of the property extension_b0b5aaa58a0a4287acd826c5b8330e48_CostCenter coming from Entra ID. It will upload the JSON file with the instructions for the update to the 'Shared Documents' library of the site currently connected to.

### EXAMPLE 4
```powershell
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping @{"CostCenter"="extension_b0b5aaa58a0a4287acd826c5b8330e48_CostCenter"} -Folder "User Profile Sync"
```

This will retrieve all users in Entra ID and take its extension property named CostCenter to update in the CostCenter field in the SharePoint Online user profiles for each of these users. It will upload the JSON file with the instructions for the update to a library named 'User Profile Sync' in the site currently connected to.

### EXAMPLE 5
```powershell
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping @{"CostCenter"="extension_b0b5aaa58a0a4287acd826c5b8330e48_CostCenter"} -Folder "User Profile Sync\Jobs" -Wait -Verbose
```

This will retrieve all users in Entra ID and take its extension property named CostCenter to update in the CostCenter field in the SharePoint Online user profiles for each of these users. It will upload the JSON file with the instructions for the update to the folder Jobs inside a library named 'User Profile Sync' in the site currently connected to. It will wait with continuing the execution of the remainder of your script until the synchronization process has either completed or failed. It will output verbose logging to provide input on its status while executing. Notice that it may very well take 10 minutes or more for the synchronization to complete.

## PARAMETERS

### -Users
Through this parameter you can pass in users coming forward from a query through Get-PnPAzureADUser that need to have their SharePoint Online User profiles updated

```yaml
Type: System.Collections.Generic.List`1[PnP.Framework.Graph.Model.User]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Folder
The site relative name of the folder or document library to upload the JSON files containing the user profiles to be updated. I.e. 'Shared Documents' to upload it to the root of the default Documents library in a Team site. If you want to specify a folder inside the document library, you can use i.e. 'Shared Documents\Somefolder'. If you are not using a site collection in the English language, be sure to provide this parameter passing in the localized name of your library instead.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: "Shared Documents"
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserProfilePropertyMapping
A hashtable containing the SharePoint Online User Profile property to update as the key and as the value the Entra ID user profile property from which the value should be copied. It is possible to copy one Entra ID user profile property to multiple SharePoint Online User Profile property fields. It is also possible to provide multiple mappings at once. For SharePoint Online, please be sure to take the actual property name as shown on the User Profile property page at https://tenant-admin.sharepoint.com/_layouts/15/TenantProfileAdmin/MgrProperty.aspx?ProfileType=User&ApplicationID=00000000%2D0000%2D0000%2D0000%2D000000000000 and not its display name as these can easily be mixed up.

I.e. @{"SharePointUserProfileProperty1"="AzureADUserProperty1";"SharePointUserProfileProperty2"="AzureADUserProperty2"}

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IdType
The type of profile identifier (Email/CloudId/PrincipalName). Defaults to CloudId. Ensure that if you use this in combination with `-Users` that all of the user objects you're passing in are having their Mail property populated when choosing IdType Email, Id property for IdType CloudId or UserPrincipalName for IdType PrincipalName.

```yaml
Type: ImportProfilePropertiesUserIdType
Parameter Sets: (All)
Accepted values: Email, CloudId, PrincipalName

Required: False
Default value: CloudId
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Will retrieve the users from Entra ID and create and upload the mappings JSON file, but will not invoke a request to SharePoint Online to queue the import process. This way you can examine the mappings JSON file on SharePoint Online first to ensure the mappings are being done correctly.

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
