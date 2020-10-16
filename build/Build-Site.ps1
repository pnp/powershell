param (
    [switch]
    $CleanUp,
    [switch]
    $Push
)
# copy documentation to output folder
copy-item -Path "$PSScriptRoot/../documentation/*.md" -Destination "$PSScriptRoot/../pages/cmdlets"

# generate cmdlet toc

$items = Get-ChildItem "$PSScriptRoot/../pages/cmdlets/*.md"
$toc = ""
foreach($item in $items)
{
    $toc = $toc + "- name: $($item.Name -replace '.md','')`n  href: $($item.Name)`n"
}
$toc | Out-File "$PSScriptRoot/../pages/cmdlets/toc.yml" -Force