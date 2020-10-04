---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpworkflowsubscription
schema: 2.0.0
title: Get-PnPWorkflowSubscription
---

# Get-PnPWorkflowSubscription

## SYNOPSIS
Return a workflow subscription

## SYNTAX

```
Get-PnPWorkflowSubscription [[-Name] <String>] [[-List] <ListPipeBind>] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Returns a workflow subscriptions from a list

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPWorkflowSubscription -Name MyWorkflow
```

Gets an Workflow subscription with the name "MyWorkflow".

### EXAMPLE 2
```powershell
Get-PnPWorkflowSubscription -Name MyWorkflow -list $list
```

Gets an Workflow subscription with the name "MyWorkflow" from the list $list.

### EXAMPLE 3
```powershell
Get-PnPList -identity "MyList" | Get-PnPWorkflowSubscription -Name MyWorkflow
```

Gets an Workflow subscription with the name "MyWorkflow" from the list "MyList".

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
A list to search the association for

```yaml
Type: ListPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name of the workflow

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### Microsoft.SharePoint.Client.WorkflowServices.WorkflowSubscription

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)