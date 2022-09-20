# PnP.Powershell

![PnP PowerShell](https://repository-images.githubusercontent.com/296298081/933a6d00-072b-11eb-839d-56df16c29588)

**PnP PowerShell** is a .NET Core 3.1 / .NET Framework 4.6.2 based PowerShell Module providing over 650 cmdlets that work with Microsoft 365 environments such as SharePoint Online, Microsoft Teams, Microsoft Project, Security & Compliance, Azure Active Directory, and more.

Last version | Last nightly version
-------------|---------------------
[![PnP.PowerShell](https://img.shields.io/powershellgallery/v/pnp.powershell)](https://www.powershellgallery.com/packages/PnP.PowerShell/) | [![PnP.PowerShell](https://img.shields.io/powershellgallery/v/pnp.powershell?include_prereleases)](https://www.powershellgallery.com/packages/PnP.PowerShell/)

## Latest

* latest: The latest stable image

  * alpine-3.14
  * `docker pull m365pnp/powershell` or `docker pull m365pnp/powershell:latest`

### Nightly

* nightly: The latest night image

  * alpine-3.14
  * `docker pull m365pnp/powershell:nightly`

## About this image

## Features

* Runs on any OS
  * Linux
  * MacOS
  * Windows
* Run on any shell
  * Azure Cloud Shell
  * PowerShell
* Unified login
  * Access all your Microsoft 365 workloads
* Supported workloads
  * Azure Active Directory
  * Microsoft Teams
  * OneDrive
  * Planner
  * Power Automate
  * Power Apps
  * SharePoint Online
  * Yammer
  * Microsoft Project
  * Security & Compliance
* Supported authentication methods
  * Azure Managed Identity
  * Certificate
  * Client Secret
  * Device Code
  * Username and Password
  * WebLogin
  * SPO Management Shell
  * Access Token

> Follow our [Twitter](https://twitter.com/pnppowershell) account to keep yourself updated about new features, improvements, and bug fixes.

## Usage examples

### Windows-container

Starting an isolated container with PnP.PowerShell module installed:

```
docker run --rm -it m365pnp/powershell:1.10.0-nanoserver-1809
```

Starting a PnP.PowerShell container with the current directory mounted:

```PowerShell
docker run --rm -it -v ${PWD}:c:/app -w c:/app m365pnp/powershell:1.10.0-nanoserver-1809
```

### Linux-container

Starting an isolated container with PnP.PowerShell module installed:

```
docker run --rm -it m365pnp/powershell
```

Starting a PnP.PowerShell container with the current directory mounted:

```bash
docker run --rm -it -v ${PWD}:/home -w /home m365pnp/powershell
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
* 1.11.0-alpine-3.14
* 1.11.76-nightly-nanoserver-ltsc2022

To find the version numbers please visit <https://www.powershellgallery.com/packages/PnP.PowerShell>

## Contribute

We love to accept contributions.

If you want to get involved with helping us grow PnP PowerShell, whether that is suggesting or adding a new cmdlet, extending an existing cmdlet or updating our documentation, we would love to hear from you.

Checkout our [Wiki](https://pnp.github.io/powershell/articles/buildingsource.html) for guides on how to contribute to this project.

## Microsoft 365 Platform Community

PnP PowerShell is a [Microsoft 365 Platform Community](https://pnp.github.io) (PnP) project. Microsoft 365 Platform Community is a virtual team consisting of Microsoft employees and community members focused on helping the community make the best use of Microsoft products. PnP PowerShell is an open-source project not affiliated with Microsoft and not covered by Microsoft support. If you experience any issues using PnP PowerShell, please submit an issue in the [issues list](https://github.com/pnp/powershell/issues/new/choose).

## Disclaimer

**THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.**
