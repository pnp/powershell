# copy documentation to output folder
# make cmdlets folder
New-Item -Path "./dev/pages/cmdlets" -ItemType Directory
New-Item -Path "./dev/pages/cmdlets/released" -ItemType Directory
New-Item -Path "./dev/pages/cmdlets/nightly" -ItemType Directory

$nightlycmdlets = Get-ChildItem "./dev/documentation/*.md" | ForEach-Object{$_ | Select-Object -ExpandProperty Name}
$releasedcmdlets = Get-ChildItem "./master/documentation/*.md" | ForEach-Object{$_ | Select-Object -ExpandProperty Name}

foreach($nightlycmdlet in $nightlycmdlets)
{
    if(!$releasedcmdlets.Contains($nightlycmdlet))
    {
        Copy-Item "./dev/documentation/$nightlycmdlet" -Destination "./dev/pages/cmdlets/nightly"
    }
}
Copy-Item -Path "./master/documentation/*.md" -Destination "./dev/pages/cmdlets/released" -Force

# generate cmdlet toc

$items = Get-ChildItem "./dev/pages/cmdlets/released/*.md"
$toc = "- name: Released
  items:`n"
foreach($item in $items)
{
    $toc = $toc + "    - name: $($item.Name -replace '.md','')`n      href: released/$($item.Name)`n"
}
#$toc | Out-File "./dev/pages/cmdlets/released/toc.yml" -Force

$items = Get-ChildItem "./dev/pages/cmdlets/nightly/*.md"
$toc = $toc + "- name: Nightly`n  items:`n"
foreach($item in $items)
{
    $toc = $toc + "    - name: $($item.Name -replace '.md','')`n      href: nightly/$($item.Name)`n"
}
$toc | Out-File "./dev/pages/cmdlets/toc.yml" -Force

docfx build ./dev/pages/docfx.json

Copy-Item -Path "./dev/pages/_site/*" -Destination "./gh-pages" -Force -Recurse

