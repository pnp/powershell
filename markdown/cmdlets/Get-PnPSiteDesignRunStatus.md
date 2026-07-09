---
Module Name: PnP.PowerShell
title: Get-PnPSiteDesignRunStatus
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteDesignRunStatus.html
---
 
# Get-PnPSiteDesignRunStatus

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieves and displays a list of all site script actions executed for a specified site design applied to a site.

## SYNTAX

```powershell
Get-PnPSiteDesignRunStatus -Run <TenantSiteDesignRun> [-Connection <PnPConnection>]  
 
```

## DESCRIPTION

Allows to retrieve a list of all site script actions executed for a specified site design applied to a site.

## EXAMPLES

### EXAMPLE 1
```powershell
$myrun = Get-PnPSiteDesignRun -WebUrl "https://contoso.sharepoint.com/sites/project-playbook" -SiteDesignId cefd782e-sean-4814-a68a-b33b116c302f
Get-PnPSiteDesignRunStatus -Run $myrun
```

This example gets the run for a specific site design applied to a site and sets it to a variable. This variable is then passed into the command -Run parameter. The result is a display of all the site script actions applied for that site design run, including the script action title and outcome. 

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

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

### -Run
The site design run for the desired set of script action details.

```yaml
Type: TenantSiteDesignRun
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

