# PnP.Powershell

## Featured tags

### Latest

* latest: The latest stable image

  * alpine-3.14
  * `docker pull pnp/powershell` or `docker pull pnp/powershell:latest`

### Nightly

* nightly: The latest night image

  * alpine-3.14
  * `docker pull pnp/powershell:nightly`

## About this image

**PnP PowerShell** is a .NET Core 3.1 / .NET Framework 4.6.1 based PowerShell Module providing over 600 cmdlets that work with Microsoft 365 environments such as SharePoint Online, Microsoft Teams, Microsoft Project, Security & Compliance, Azure Active Directory, and more.

## Usage examples

### Windows-container

Starting an isolated container with PnP.PowerShell module installed:

```
docker run --rm -it asapozhkov/powershell:1.10.0-nanoserver-1809
```

Starting a PnP.PowerShell container with the current directory mounted:

```PowerShell
docker run --rm -it -v ${PWD}:c:/app -w c:/app asapozhkov/powershell:1.10.0-nanoserver-1809
```

### Linux-container

Starting an isolated container with PnP.PowerShell module installed:

```
docker run --rm -it asapozhkov/powershell
```

Starting a PnP.PowerShell container with the current directory mounted:

```bash
docker run --rm -it -v ${PWD}:/home -w /home asapozhkov/powershell
```

## Tag explanation

Tags names mean the following:

`<version>(-nightly)-<platform>`

Currently supported platforms:

* nanoserver-ltsc2022
* nanoserver-1809
* alpine-3.14

Tag name examples:

* 1.8.0-nanoserver-ltsc2022
* 1.9.0-nanoserver-ltsc2022
* 1.10.0-nanoserver-1809
* 1.10.0-alpine-3.14
* 1.10.26-nightly-nanoserver-ltsc2022

To find the version numbers please visit https://www.powershellgallery.com/packages/PnP.PowerShell

## Feedback

* To give feedback for PnP.PowerShell or for how the images are built, file an issue at [PnP/PowerShell](https://github.com/pnp/powershell/issues/new/choose)
