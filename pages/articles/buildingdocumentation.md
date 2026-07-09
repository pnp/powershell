---
uid: buildingdocumentation
---

# Building the documentation site locally

The documentation site is built with [DocFX](https://dotnet.github.io/docfx/). If you're changing articles, cmdlet documentation, the site template, or the documentation build scripts, build the site locally before you submit your pull request.

Do not run `docfx build ./pages/docfx.json` directly when you want to check the site as users will see it. That only builds the files that already exist under `pages` and skips the generated cmdlet pages. The result is a partial site where the home page can load, but the cmdlets section and parts of the navigation will be missing.

Use `pages/Build-Site.ps1` instead. It copies the cmdlet markdown files from `documentation` into `pages/cmdlets`, generates the cmdlets table of contents and index page, runs DocFX, and then cleans the generated source files again. The generated site remains available under `pages/_site` for local preview.

The GitHub Actions workflows use DocFX 2.78.5 and the .NET SDK 10. The commands below use the same DocFX version.

## Prerequisites

Install the following tools first:

1. [Git](https://git-scm.com/downloads)
1. [PowerShell 7](https://learn.microsoft.com/powershell/scripting/install/installing-powershell)
1. [.NET SDK 10](https://dotnet.microsoft.com/download)

You can validate your installation by running:

```powershell
git --version
pwsh --version
dotnet --version
```

The build also needs access to the PowerShell Gallery. During the build, `Build-Site.ps1` installs the latest prerelease version of PnP PowerShell in order to discover aliases and generate documentation pages for them.

## Build on Windows

Open PowerShell 7 and navigate to your local clone of the repository.

```powershell
cd C:\repos\powershell
```

Install DocFX into a local `.tools` folder. This keeps the version used for this repository separate from any globally installed DocFX version.

```powershell
New-Item -ItemType Directory -Force .\.tools | Out-Null
dotnet tool install docfx --tool-path .\.tools --version 2.78.5
```

If you already have DocFX installed in that folder and want to refresh it to the expected version, run:

```powershell
dotnet tool update docfx --tool-path .\.tools --version 2.78.5
```

Add the local tools folder to the current PowerShell session path so the build script can call `docfx`:

```powershell
$env:PATH = "$(Resolve-Path .\.tools);$env:PATH"
```

Build the documentation site:

```powershell
.\pages\Build-Site.ps1 -SkipPublish
```

Serve the generated site locally:

```powershell
.\.tools\docfx.exe serve .\pages\_site --port 8080
```

Open http://localhost:8080 in your browser. If port 8080 is already in use, pick another port, for example `--port 8091`.

When you're done, remove the generated output:

```powershell
Remove-Item .\pages\_site, .\pages\obj -Recurse -Force -ErrorAction SilentlyContinue
```

## Build on macOS or Linux

Open a terminal and navigate to your local clone of the repository.

```bash
cd ~/repos/powershell
```

Install DocFX into a local `.tools` folder.

```bash
mkdir -p .tools
dotnet tool install docfx --tool-path ./.tools --version 2.78.5
```

If you already have DocFX installed in that folder and want to refresh it to the expected version, run:

```bash
dotnet tool update docfx --tool-path ./.tools --version 2.78.5
```

Add the local tools folder to the current shell path so the build script can call `docfx`:

```bash
export PATH="$(pwd)/.tools:$PATH"
```

Build the documentation site:

```bash
pwsh ./pages/Build-Site.ps1 -SkipPublish
```

Serve the generated site locally:

```bash
./.tools/docfx serve ./pages/_site --port 8080
```

Open http://localhost:8080 in your browser. If port 8080 is already in use, pick another port, for example `--port 8091`.

When you're done, remove the generated output:

```bash
rm -rf ./pages/_site ./pages/obj
```

## Testing the workflow layout

The normal local build above is enough for most documentation changes. If you want to test the same folder layout used by the GitHub Actions workflow, create worktrees for `master`, `dev`, and `gh-pages` next to each other.

### Windows

```powershell
$repo = "C:\repos\powershell"
$root = "C:\repos\powershell-docs-local"

git -C $repo fetch origin
New-Item -ItemType Directory -Force $root | Out-Null

git -C $repo worktree add "$root\dev" HEAD
git -C $repo worktree add "$root\master" origin/master
git -C $repo worktree add "$root\gh-pages" origin/gh-pages
```

Install DocFX in the temporary build folder and run the workflow build:

```powershell
Push-Location $root

New-Item -ItemType Directory -Force .\.tools | Out-Null
dotnet tool install docfx --tool-path .\.tools --version 2.78.5
$env:PATH = "$(Resolve-Path .\.tools);$env:PATH"

.\dev\pages\Build-Site.ps1
```

Serve the generated site:

```powershell
.\.tools\docfx.exe serve .\dev\pages\_site --port 8080
```

Open http://localhost:8080 in your browser and check the pages you changed. Press `CTRL+C` in the terminal when you're done serving the site.

Clean up the worktrees from your original checkout:

```powershell
Pop-Location

git -C $repo worktree remove "$root\dev" --force
git -C $repo worktree remove "$root\master" --force
git -C $repo worktree remove "$root\gh-pages" --force
Remove-Item $root -Recurse -Force -ErrorAction SilentlyContinue
```

### macOS or Linux

```bash
repo=~/repos/powershell
root=~/repos/powershell-docs-local

git -C "$repo" fetch origin
mkdir -p "$root"

git -C "$repo" worktree add "$root/dev" HEAD
git -C "$repo" worktree add "$root/master" origin/master
git -C "$repo" worktree add "$root/gh-pages" origin/gh-pages
```

Install DocFX in the temporary build folder and run the workflow build:

```bash
cd "$root"

mkdir -p .tools
dotnet tool install docfx --tool-path ./.tools --version 2.78.5
export PATH="$(pwd)/.tools:$PATH"

pwsh ./dev/pages/Build-Site.ps1
```

Serve the generated site:

```bash
./.tools/docfx serve ./dev/pages/_site --port 8080
```

Open http://localhost:8080 in your browser and check the pages you changed. Press `CTRL+C` in the terminal when you're done serving the site.

Clean up the worktrees from your original checkout:

```bash
git -C "$repo" worktree remove "$root/dev" --force
git -C "$repo" worktree remove "$root/master" --force
git -C "$repo" worktree remove "$root/gh-pages" --force
rm -rf "$root"
```

## What to check in the browser

At minimum, verify the following:

1. The home page loads and is not blank.
1. The top navigation contains Articles and Cmdlets.
1. The Articles link opens the articles section and shows the article table of contents.
1. The Cmdlets link opens the cmdlets index and shows cmdlet pages in the table of contents.
1. Search opens without JavaScript errors.
1. The theme selector in the top navigation can switch between Light, Dark and Auto.
1. The favicon loads from `images/favicon-pnp.svg` on the home page, article pages, and cmdlet pages.
1. The Copy markdown and View as Markdown actions work for the home page, an article page, and a cmdlet page.
1. The browser developer tools do not show 404 responses for `public/docfx.min.css`, `public/docfx.min.js`, `public/main.css` or `public/main.js`.

DocFX 2.77 and newer emit the vendor assets as minified files. With the modern template, the site should load its built-in assets from the `public` folder and the PnP branding overrides from `public/main.css` and `public/main.js`. If these files return 404 responses, the template stack is not being applied correctly and the site can appear broken or unstyled.

## Build warnings

DocFX can finish successfully while still reporting warnings. Treat warnings in files you touched as something to fix before submitting your pull request. Existing warnings elsewhere in the site should not block you from validating your local change, but do not introduce new ones.

Common warnings are broken file links or broken bookmarks. If you see a warning for a file you changed, fix it before you submit the pull request.