# copy documentation to output folder
copy-item -Path "$PSScriptRoot/../documentation/*.md" -Destination "$PSScriptRoot/../pages/content/cmdlets"

$items = Get-ChildItem -Path "$PSScriptRoot/../pages/content/cmdlets"
foreach($item in $items)
{
    $metadata = Get-MarkdownMetadata -Path $item
    $title = $metadata.title
    $content = Get-Content -Path $item.FullName -Raw
    $splitted = $content -split '---'
    $header = "---
title: $title
---"
    $pageContent = $splitted[2] -replace "# $title",""  # Remove Title markdown
    $newContent = $header + $pageContent
    $newContent | Out-File $item.FullName -Force
}
hugo.exe -s "$PSScriptRoot\..\pages" -d "$PSScriptRoot\..\public"

# Remove cmdlets folder in pages folder. No need for duplicates here.
Remove-Item "$PSScriptRoot/../pages/content/cmdlets/*" -Recurse -Force -ErrorAction Stop
Set-Location $PSScriptRoot/../public 
git add --all 
git commit -m "Publishing to gh-pages"
git push origin
