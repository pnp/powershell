---
Module Name: PnP.PowerShell
title: Remove-PnPVivaConnectionsDashboardACE
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPVivaConnectionsDashboardACE.html
---
 
# Remove-PnPVivaConnectionsDashboardACE

## SYNOPSIS
Removes the Adaptive card extensions from the Viva connections dashboard page. This requires that you connect to a SharePoint Home site and have configured the Viva connections page.

## SYNTAX

```powershell
Remove-PnPVivaConnectionsDashboardACE [-Identity <VivaACEPipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove Adaptive card extensions from the Viva connections dashboard page.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPVivaConnectionsDashboardACE -Identity "58108715-185e-4214-8786-01218e7ab9ef"
```

Removes the adaptive card extensions with Instance Id `58108715-185e-4214-8786-01218e7ab9ef` from the Viva connections dashboard page


## PARAMETERS

### -Identity
The instance Id of the Adaptive Card extension present on the Viva connections dashboard page. You can retrieve the value for this parameter by executing `Get-PnPVivaConnectionsDashboardACE` cmdlet. This parameter takes either the Instance Id, the Id or the Title property. But as the latter two are not necessarily unique within the dashboard, the preferred value is to use the Instance Id of the ACE.

```yaml
Type: VivaACEPipeBind
Parameter Sets: (All)

Required: True
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

