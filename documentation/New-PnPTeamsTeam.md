---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPTeamsTeam.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPTeamsTeam
---

# New-PnPTeamsTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Creates a new team in Microsoft Teams or teamifies an existing Microsoft 365 Group. If the Microsoft 365 Group does not exist yet, it will create it first and then add a Microsoft Teams team to the group. If it does already exist, it will use the provided Microsoft 365 Group and just teamify it by adding a Microsoft Teams team to it.

## SYNTAX

### For an existing group

```
New-PnPTeamsTeam -GroupId <String> [-AllowAddRemoveApps <Boolean>] [-AllowChannelMentions <Boolean>]
 [-AllowCreateUpdateChannels <Boolean>] [-AllowCreateUpdateRemoveConnectors <Boolean>]
 [-AllowCreateUpdateRemoveTabs <Boolean>] [-AllowCustomMemes <Boolean>]
 [-AllowDeleteChannels <Boolean>] [-AllowGiphy <Boolean>]
 [-AllowGuestCreateUpdateChannels <Boolean>] [-AllowGuestDeleteChannels <Boolean>]
 [-AllowOwnerDeleteMessages <Boolean>] [-AllowStickersAndMemes <Boolean>]
 [-AllowTeamMentions <Boolean>] [-AllowUserDeleteMessages <Boolean>]
 [-AllowUserEditMessages <Boolean>] [-GiphyContentRating <TeamGiphyContentRating>]
 [-ShowInTeamsSearchAndSuggestions <Boolean>] [-Classification <String>] [-Owners <String[]>]
 [-Members <String[]>]
```

### For a new group

```
New-PnPTeamsTeam -DisplayName <String> [-MailNickName <String>] [-Description <String>]
 [-AllowAddRemoveApps <Boolean>] [-AllowChannelMentions <Boolean>]
 [-AllowCreateUpdateChannels <Boolean>] [-AllowCreateUpdateRemoveConnectors <Boolean>]
 [-AllowCreateUpdateRemoveTabs <Boolean>] [-AllowCustomMemes <Boolean>]
 [-AllowDeleteChannels <Boolean>] [-AllowGiphy <Boolean>]
 [-AllowGuestCreateUpdateChannels <Boolean>] [-AllowGuestDeleteChannels <Boolean>]
 [-AllowOwnerDeleteMessages <Boolean>] [-AllowStickersAndMemes <Boolean>]
 [-AllowTeamMentions <Boolean>] [-AllowUserDeleteMessages <Boolean>]
 [-AllowUserEditMessages <Boolean>] [-GiphyContentRating <TeamGiphyContentRating>]
 [-Visibility <TeamVisibility>] [-ShowInTeamsSearchAndSuggestions <Boolean>]
 [-Classification <String>] [-Owners <String[]>] [-Members <String[]>]
 [-ResourceBehaviorOptions <TeamResourceBehaviorOptions>] [-SensitivityLabels <GUID[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to create new team in Microsoft Teams or to teamify an existing Microsoft 365 Group. If the Microsoft 365 Group does not exist yet, it will create it first and then add a Microsoft Teams team to the group.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPTeamsTeam -DisplayName "myPnPDemo1" -Visibility Private -AllowCreateUpdateRemoveTabs $false -AllowUserDeleteMessages $false
```

This will create a new Microsoft Teams team called "myPnPDemo1" and sets the privacy to Private, as well as preventing users from deleting their messages or update/remove tabs. The user creating the Microsoft Teams team will be added as Owner.

### EXAMPLE 2

```powershell
New-PnPTeamsTeam -GroupId $groupId
```

This will create a new Microsoft Teams team from an existing Microsoft 365 Group using the Group ID (teamify)

### EXAMPLE 3

```powershell
New-PnPTeamsTeam -DisplayName "myPnPDemo1" -Visibility Private -AllowCreateUpdateRemoveTabs $false -AllowUserDeleteMessages $false -ResourceBehaviorOptions WelcomeEmailDisabled
```

This will create a new Microsoft Teams team called "myPnPDemo1" and sets the privacy to Private, as well as preventing users from deleting their messages or update/remove tabs. The user creating the Microsoft Teams team will be added as Owner. Welcome Email will not be sent when the Group is created.

### EXAMPLE 4

```powershell
New-PnPTeamsTeam -DisplayName "myPnPDemo1" -Visibility Private -AllowCreateUpdateRemoveTabs $false -AllowUserDeleteMessages $false -ResourceBehaviorOptions WelcomeEmailDisabled, HideGroupInOutlook
```

This will create a new Microsoft Teams team called "myPnPDemo1" and sets the privacy to Private, as well as preventing users from deleting their messages or update/remove tabs. The user creating the Microsoft Teams team will be added as Owner. Welcome Email will not be sent when the Group is created. The M365 Group will also not be visible in Outlook.

### EXAMPLE 5

```powershell
New-PnPTeamsTeam -DisplayName "myPnPDemo1" -Visibility Private -Owners "user1@contoso.onmicrosoft.com","user2@contoso.onmicrosoft.com" -Members "user3@contoso.onmicrosoft.com"
```

This will create a new Microsoft Teams team called "myPnPDemo1" and sets the privacy to Private. User1 and user2 will be added as owners. User3 will be added as a member.

### EXAMPLE 6

```powershell
New-PnPTeamsTeam -DisplayName "myPnPDemo1" -Visibility Private -Owners "user1@contoso.onmicrosoft.com","user2@contoso.onmicrosoft.com" -Members "user3@contoso.onmicrosoft.com" -SensitivityLabels "bc98af29-59eb-4869-baaa-9a8dff631aa4"
```

This will create a new Microsoft Teams team called "myPnPDemo1" and sets the privacy to Private. User1 and user2 will be added as owners. User3 will be added as a member. The team will also get the sensitivity label value corresponding to the GUID specified.

## PARAMETERS

### -AllowAddRemoveApps

Boolean value that determines whether or not members (not only owners) are allowed to add apps to the team.

```yaml
Type: Boolean
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

### -AllowChannelMentions

Boolean value that determines whether or not channels in the team can be @ mentioned so that all users who follow the channel are notified.

```yaml
Type: Boolean
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

### -AllowCreateUpdateChannels

Setting that determines whether or not members (and not just owners) are allowed to create channels.

```yaml
Type: Boolean
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

### -AllowCreateUpdateRemoveConnectors

Setting that determines whether or not members (and not only owners) can manage connectors in the team.

```yaml
Type: Boolean
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

### -AllowCreateUpdateRemoveTabs

Setting that determines whether or not members (and not only owners) can manage tabs in channels.

```yaml
Type: Boolean
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

### -AllowCustomMemes

Setting that determines whether or not members can use the custom memes functionality in teams.

```yaml
Type: Boolean
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

### -AllowDeleteChannels

Setting that determines whether or not members (and not only owners) can delete channels in the team.

```yaml
Type: Boolean
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

### -AllowGiphy

Setting that determines whether or not giphy can be used in the team.

```yaml
Type: Boolean
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

### -AllowGuestCreateUpdateChannels

Setting that determines whether or not guests can create channels in the team.

```yaml
Type: Boolean
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

### -AllowGuestDeleteChannels

Setting that determines whether or not guests can delete channels in the team.

```yaml
Type: Boolean
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

### -AllowOwnerDeleteMessages

Setting that determines whether or not owners can delete messages that they or other members of the team have posted.

```yaml
Type: Boolean
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

### -AllowStickersAndMemes

Setting that determines whether stickers and memes usage is allowed in the team.

```yaml
Type: Boolean
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

### -AllowTeamMentions

Setting that determines whether the entire team can be @ mentioned (which means that all users will be notified)

```yaml
Type: Boolean
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

### -AllowUserDeleteMessages

Setting that determines whether or not members can delete messages that they have posted.

```yaml
Type: Boolean
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

### -AllowUserEditMessages

Setting that determines whether or not users can edit messages that they have posted.

```yaml
Type: Boolean
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

### -Classification



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

### -Description

Team description. Characters Limit - 1024.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: For a new group
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DisplayName

Team display name. Characters Limit - 256.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: For a new group
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -GiphyContentRating

Setting that determines the level of sensitivity of gify usage that is allowed in the team. Accepted values are "Strict" or "Moderate"

```yaml
Type: TeamGiphyContentRating
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
AcceptedValues:
- moderate
- strict
HelpMessage: ''
```

### -GroupId

Specify a GroupId to convert to a Team. If specified, you cannot provide the other values that are already specified by the existing group, namely: Visibility, Alias, Description, or DisplayName.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: For an existing group
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MailNickName

The MailNickName parameter specifies the alias for the associated Microsoft 365 Group. This value will be used for the mail enabled object and will be used as PrimarySmtpAddress for this Microsoft 365 Group.The value of the MailNickName parameter has to be unique across your tenant.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: For a new group
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Members

The User Principal Name(s) of the user(s) to be added to the Microsoft 365 Group as members. You can provide as many members as you want, as long as you stay within the [Microsoft 365 Groups limits](https://learn.microsoft.com/microsoft-365/admin/create-groups/office-365-groups?view=o365-worldwide#group-limits). Notice that e-mail addresses are not accepted, if they differ from the User Principal Name on the same account.

```yaml
Type: String[]
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

### -Owner

User to be added as both a member and an owner of the group. If not specified, the user who creates the team will be added as both a member and an owner. This parameter has been deprecated and will be removed in a future version. Use -Owners instead which allows providing one or even multiple owners at once.

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

### -Owners

The User Principal Name(s) of the user(s) to be added to the Microsoft 365 Group as owners. If omitted and the cmdlet is run using a token containing a user identity, such as when logging on with -Interactive or -DeviceLogin, the user used to authenticate with would become the owner. You can provide as many owners as you want, as long as you stay within the [Microsoft 365 Groups limits](https://learn.microsoft.com/microsoft-365/admin/create-groups/office-365-groups?view=o365-worldwide#group-limits). Notice that e-mail addresses are not accepted, if they differ from the User Principal Name on the same account.

```yaml
Type: String[]
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

### -ResourceBehaviorOptions

Allows providing ResourceBehaviorOptions which accepts multiple values that specify group behaviors for a Microsoft 365 Group. This will only work when you create a new Microsoft 365 Group, it will not work for existing groups.

```yaml
Type: TeamResourceBehaviorOptions
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: For a new group
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- AllowOnlyMembersToPost
- HideGroupInOutlook
- SubscribeNewGroupMembers
- WelcomeEmailDisabled
HelpMessage: ''
```

### -SensitivityLabels

The Sensitivity label to be set to the Microsoft 365 Group and Team. To retrieve the sensitivity label you need to use the Graph API mentioned [here](https://learn.microsoft.com/en-us/graph/api/informationprotectionlabel-get?view=graph-rest-beta&tabs=http).

```yaml
Type: GUID[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: For a new group
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ShowInTeamsSearchAndSuggestions

Setting that determines whether or not private teams should be searchable from Teams clients for users who do not belong to that team. Set to $false to make those teams not discoverable from Teams clients.

```yaml
Type: Boolean
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

### -Visibility

Set to Public to allow all users in your organization to join the group by default. Set to Private to require that an owner approve the join request.

```yaml
Type: TeamVisibility
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: For a new group
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Private
- Public
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
