---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTeamsTab.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTeamsTab
---
  
# Add-PnPTeamsTab

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Adds a tab to an existing Channel. Note that the `-ContentUrl` is a 'dynamic' parameter and will only be valid for tab types that support it.
Also, some `-Type` values are not configurable due to Microsoft Graph API limitations (i.e.: PowerBI).

## SYNTAX

```powershell
Add-PnPTeamsTab -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -DisplayName <String>
 -Type <TeamTabType> -ContentUrl <String> 
```

## DESCRIPTION

Allows to add a tab to an existing Channel. By using `ContentUrl` option you may specify the content of the tab.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTeamsTab -Team "My Team" -Channel "My Channel" -DisplayName "My Tab Name" -Type WebSite -ContentUrl "https://aka.ms/m365pnp"
```

Adds a web site tab to the specified channel.

### EXAMPLE 2
```powershell
Add-PnPTeamsTab -Team "My Team" -Channel "My Channel" -DisplayName "My Tab Name" -Type PDF -ContentUrl "https://contoso.sharepoint.com/sites/Marketing/Shared Documents/General/MyFile.pdf" -EntityId "null"
```

Adds the specified PDF file from the "Documents" library as a tab.

### EXAMPLE 3
```powershell
Add-PnPTeamsTab -Team "My Team" -Channel "My Channel" -DisplayName "My Tab Name" -Type SharePointPageAndList -WebSiteUrl "https://contoso.sharepoint.com/sites/Marketing/SitePages/Home.aspx"
```

Adds the specified SharePoint page as a tab in Teams. Note that the ContentUrl will automatically be generated and cannot be specified, and the `-WebsiteUrl` parameter is only available from version `2.x` onwards.

### EXAMPLE 4
```powershell
Add-PnPTeamsTab -Team "My Team" -Channel "My Channel" -DisplayName "My Excel Tab" -Type Excel -ContentUrl "https://contoso.sharepoint.com/sites/Marketing/Shared Documents/My Excel File.csv" -EntityId 6
```

Adds the "My Excel file.csv" with ID 6 as a tab from the Marketing site.

### EXAMPLE 5
```powershell
$PlannerPlan = Get-PnPPlannerPlan -Group $groupId -Identity $PlannerName
if(-not $PlannerPlan)
{
    $PlannerPlan = New-PnPPlannerPlan -Group $groupId -Title $PlannerName
}
$bucket = Add-PnPPlannerBucket -Group $groupId -Plan $PlannerPlan.Id -Name "Tasks"
Add-PnPPlannerTask -Group $groupId -Plan $PlannerPlan.Id -Bucket $bucket.Id -Title "plannertaskA"

$teamsChannel = Get-PnPTeamsChannel -Team $groupId -Identity "General"

$tenant = "contoso.onmicrosoft.com"

$teamsTab = Add-PnPTeamsTab -Team $groupId -Channel $teamsChannel -DisplayName "My Tab Name" -Type Planner -ContentUrl "https://tasks.office.com/$tenant/Home/PlannerFrame?page=7&planId=$($PlannerPlan.Id)"

```

Gets the existing Planner if one exists in the Group, otherwise creates a new. Adds a new bucket and creates a few new Tasks. Finally creates a Tab in the channel named "My Tab Name" 


## PARAMETERS

### -Channel
Specify the channel id or name of the team to retrieve.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ContentUrl
The Url to use to render the content inside the tab within Microsoft Teams.

When using Type SharePointPageAndList, the ContentUrl will automatically be generated and cannot be specified.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebsiteUrl
The Url to use when the user clicks on the dropdown of the tab in Microsoft Teams and clicks on "Go to website".

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
Specify the tab type

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Team
Specify the group id, mailNickname or display name of the team to use.

```yaml
Type: TeamsTeamPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Type
Specify the tab type

```yaml
Type: TeamTabType
Parameter Sets: (All)
Accepted values: WebSite, DocumentLibrary, Wiki, Planner, MicrosoftStream, MicrosoftForms, Word, Excel, PowerPoint, PDF, OneNote, PowerBI, SharePointPageAndList, Custom

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveUrl
Specifies the URL to be called by Teams client when a Tab is removed using the Teams Client.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EntityId
Specifies the Identifier for the entity hosted by the tab provider.

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
