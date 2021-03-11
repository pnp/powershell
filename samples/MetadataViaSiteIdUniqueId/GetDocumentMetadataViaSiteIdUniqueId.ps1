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