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
Synchronizes user profiles from Azure Active Directory into the SharePoint Online User Profiles

## SYNTAX

### Upload file
```powershell
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping <Hashtable> [-Users <Array>] [-Folder <String>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet allows synchronizing user profiles from Azure Active Directory to their SharePoint Online User Profile equivallents. Note that certain properties are already synced by default. A list of these can be found here: https://docs.microsoft.com/sharepoint/user-profile-sync#properties-that-are-synced-into-sharepoint-user-profiles

For other properties not listed on this page, you can use this cmdlet to synchronize them. You can provide the property name(s) in Azure Active Directory and specify its equivallent property in SharePoint Online for the values to be mapped to.

Note that SharePoint Online User Profile properties you wish to sync to *must* have the checkbox unchecked for "Allow users to edit values for this property" in the user profile property in the SharePoint User Profile service application. It also *must* have "User can override" checked under Policy Settings of the user profile property in the SharePoint User Profile service application.

When running this cmdlet, it will upload a file named `userprofilesyncdata-<timestamp>-<guid>.json` to the documents library of the SharePoint Online site you are connected to. From there an asynchronous process will be started that processed the JSON file and updates the user profiles on the SharePoint Online side. The time before this process starts varies. Once that process is done and only if something failed, you will find a new folder created in the same document library of which the folder name starts with the same name as the filename. It will contain a .log file in which you can find the results of it trying to update the user profiles in SharePoint Online which were specified in the JSON file. If all the user profile properties have been updated successfully, it will not create such a folder and log file.

You can also query the import job status using `Get-PnPUPABulkImportStatus -JobId <jobid>`. The jobid will be returned upon running this cmdlet and can be fed into this cmdlet to get the actual status. It will show `State: Submitted` after running this cmdlet and before processing it and `State: Succeeded` once its done and was successful or `State: Error` if it failed. It will also return full details on the file it will use to update the user profiles and the location of the log file once its done processing and only if it failed. For documentation on all the possible states it can be in, see https://docs.microsoft.com/sharepoint/dev/solution-guidance/bulk-user-profile-update-api-for-sharepoint-online#parameters-2. 

When not providing -Users, it will fetch all the users and the properties defined in the mapping from Azure Active Directory itself. You can also opt to query for a subset of Azure Active Directory users to update using i.e. `Get-PnPAzureAdUser` and feed the outcome of that to the -Users parameter. In this case you must ensure that the user objects you supply contain the properties you wish to sync towards SharePoint Online.

**Required Permissions**

In order to be able to run this cmdlet you need to have [User.Read.All] and [Sites.FullControl.All] permissions on SharePoint and [User.Read.All] permissions on Microsoft Graph so it will be able to read the users directly from Azure Active Directory and upload the JSON file to SharePoint Online. It also needs to have the Tenant Full Control ACS permission through https://tenant-admin.sharepoint.com/_layouts/appinv.aspx for it to be allowed to kick off the import user profile process:

`
<AppPermissionRequests AllowAppOnlyPolicy="true">
  <AppPermissionRequest Scope="http://sharepoint/content/tenant" Right="FullControl" />
</AppPermissionRequests>
`

## EXAMPLES

### EXAMPLE 1
```powershell
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping @{"HomePhone"="phone";"CustomProperty"="DisplayName"}
```

This will retrieve all users in Azure Active Directory and take its phone property to update in the HomePhone field in the SharePoint Online user profiles for each of these users. Similarly it will set the SharePoint Online User Profile property named CustomProperty to the value of the DisplayName as set in Azure Active Directory on the user object. It will upload the JSON file with the instructions for the update to the 'Shared Documents' library of the site currently connected to.

### EXAMPLE 2
```powershell
$users = Get-PnPAzureADUser -Filter "jobTitle eq 'IT Administrator'"
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping @{"CostCenter"="ext_123456"} -Users $users
```

This will update the CostCenter SharePoint Online User Profile property with the value of the property ext_123456 coming from Azure Active Directory for the users getting returned by the Get-PnPAzureADUser query. It will upload the JSON file with the instructions for the update to the 'Shared Documents' library of the site currently connected to.

### EXAMPLE 3
```powershell
$delta = Get-PnPAzureADUser -Delta -DeltaToken $delta.DeltaToken
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping @{"CostCenter"="ext_123456"} -Users $delta.Users
```

This will retrieve all the users from Azure Active Directory and includes a DeltaToken in the response. Using the DeltaToken you can retrieve only those users which have had changes done to their attributes since the DeltaToken was given out. This makes it ideal to use with the profile sync as this way you only will sync those users that have had changes to their profiles. Only for those users this will update the CostCenter SharePoint Online User Profile property with the value of the property ext_123456 coming from Azure Active Directory. It will upload the JSON file with the instructions for the update to the 'Shared Documents' library of the site currently connected to.

### EXAMPLE 4
```powershell
Sync-PnPSharePointUserProfilesFromAzureActiveDirectory -UserProfilePropertyMapping @{"CostCenter"="ext_123456"} -Folder "User Profile Sync"
```

This will retrieve all users in Azure Active Directory and take its phone property to update in the HomePhone field in the SharePoint Online user profiles for each of these users. Similarly it will set the SharePoint Online User Profile property named CustomProperty to the value of the DisplayName as set in Azure Active Directory on the user object. It will upload the JSON file with the instructions for the update to a library named 'User Profile Sync' in the site currently connected to.

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
The site relative name of the folder or document library to upload the JSON files containing the user profiles to be updated. I.e. 'Shared Documents' to upload it to the root of the default Documents library in a Team site.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserProfilePropertyMapping
A hashtable containing the SharePoint Online User Profile property to update as the key and as the value the Azure Active Directory user profile property from which the value should be copied. It is possible to copy one Azure Active Directory user profile property to multiple SharePoint Online User Profile property fields. It is also possible to provide multiple mappings at once. For SharePoint Online, please be sure to take the actual property name as shown on the User Profile property page at https://tenant-admin.sharepoint.com/_layouts/15/TenantProfileAdmin/MgrProperty.aspx?ProfileType=User&ApplicationID=00000000%2D0000%2D0000%2D0000%2D000000000000 and not its display name as these can easily be mixed up.

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

### -WhatIf
Will retrieve the users from Azure Active Directory and create and upload the mappings JSON file, but will not invoke a request to SharePoint Online to queue the import process. This way you can examine the mappings JSON file on SharePoint Online first to ensure the mappings are being done correctly.

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
