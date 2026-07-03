---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Export-PnPPage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Export-PnPPage
---
  
# Export-PnPPage

## SYNOPSIS
Exports a Client Side Page to a PnP Provisioning Template

## SYNTAX

```powershell
Export-PnPPage [-Identity] <PagePipeBind> [-PersistBrandingFiles] [-Out <String>] [-Force]
 [-Configuration <ExtractConfigurationPipeBind>] [-OutputInstance]  [-Connection <PnPConnection>] 
  
```

## DESCRIPTION

Allows to export a Client Side Page to a PnP Provisioning Template.

## EXAMPLES

### EXAMPLE 1
```powershell
Export-PnPPage -Identity Home.aspx
```

Exports the page 'Home.aspx' to a new PnP Provisioning Template

### EXAMPLE 2
```powershell
Export-PnPPage -Identity HR/Home.aspx -Out template.pnp
```

Exports the page 'Home.aspx' to a new PnP Provisioning Template

## PARAMETERS

### -Configuration
Specify a JSON configuration file to configure the extraction progress.

```yaml
Type: ExtractConfigurationPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Force
Specify to override the question to overwrite a file if it already exists.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The name/identity of the page. This can be a page instance or the filename of the page. I.e. if the page is called MyPage.aspx and is located in the root of the Site Pages library, provide "MyPage" or "MyPage.aspx". If the page is called MyOtherPage.aspx and is located inside a subfolder called HR located in the root of the Site Pages library, provide "HR/MyOtherPage" or "HR/MyOtherPage.aspx

```yaml
Type: ClientSidePagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Out
If specified the template will be saved to the file specified with this parameter.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PersistBrandingFiles
If specified referenced files will be exported to the current folder.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputInstance
Returns the template as an in-memory object, which is an instance of the SiteTemplate type of the PnP Core Component. It cannot be used together with the -Out parameter.

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


