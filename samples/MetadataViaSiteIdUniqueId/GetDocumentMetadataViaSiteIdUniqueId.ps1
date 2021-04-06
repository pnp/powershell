<############################################################################################
    This script running requires an interactive session to retrieve the data properly,
    you should use the interactive paramenter with the Connect-PnPOnline, i.e.:
        Connect-PnPOnline -Url https://contoso.sharepoint.com -Interactive
    
    To learn more about usage of this script see:
    https://github.com/pnp/powershell/blob/f3a46f8b8b3c326b12da56b04ec931fdd65bf30c/samples/MetadataViaSiteIdUniqueId/readme.md
#############################################################################################>

Param(
    [String] $SiteId,
    [String] $DocId
    )
 
$query="SiteId:" + $SiteId + " UniqueId={" + $DocId + "}"
$query
$result= Submit-PnPSearchQuery -Query $query
$results = @()
foreach ($row in $result.ResultRows)
{
    $res = New-Object psobject
    $res | Add-Member Noteproperty "Author" $row["Author"]
    $res | Add-Member Noteproperty "Path" $row["Path"]
    $results+=$res
}
$results
