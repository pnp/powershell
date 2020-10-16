# copy documentation to output folder
# make cmdlets folder
New-Item -Path "./dev/pages/cmdlets" -ItemType Directory
Copy-Item -Path "./dev/documentation/*.md" -Destination "./dev/pages/cmdlets" -Force

# generate cmdlet toc

$items = Get-ChildItem "./dev/pages/cmdlets/*.md"
$toc = ""
foreach($item in $items)
{
    $toc = $toc + "- name: $($item.Name -replace '.md','')`n  href: $($item.Name)`n"
}
$toc | Out-File "./dev/pages/cmdlets/toc.yml" -Force

docfx build ./dev/pages/docfx.json

Copy-Item -Path "./dev/pages/_site/*" -Destination "./gh-pages"

