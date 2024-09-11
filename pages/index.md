# PnP PowerShell
PnP PowerShell is a cross-platform PowerShell Module providing over 700 cmdlets that work with Microsoft 365 environments and products such as SharePoint Online, Microsoft Teams, Microsoft Planner, Microsoft Power Platform, Microsoft Entra, Microsoft Purview, Microsoft Search, and more. It runs on Windows, Linux and MacOS.

> [!NOTE]
> As part of a focus on improving the security posture, the multi-tenant PnP Management Shell Entra ID app (with Client/ApplicationID: '31359c7f-bd7e-475c-86db-fdb8c937548e') has been deleted. It will impact credentials(username + password) & Interactive auth flow. It has always been a recommended practice to register your own Entra ID Application to use with PnP PowerShell. As of September 9<sup>th</sup>, 2024, [this has become mandatory step](https://github.com/pnp/powershell/discussions/4249). This post has more details and it guide you through how to do so and get your scripts back up & running with minimal code changes.

## Getting up and running

Starting to use PnP PowerShell consists out of 3 steps:

1. [Installing the PnP.PowerShell module](./articles/installation.md)
   
2. [Registering your own Entra ID Application](./articles/registerapplication.md)
   
3. [Connecting and authenticating](./articles/authentication.md)

Once you're set up, check out the [cmdlets](/powershell/cmdlets) section to discover what you can do and how to use the cmdlets.

Have a look at the [articles](/powershell/articles) section covering various topics how to get the most out of using PnP PowerShell. 

## I've found a bug, where do I need to log an issue or create a PR

You can create an issue at https://github.com/pnp/powershell/issues, but please consider first if asking a question at https://github.com/pnp/powershell/discussions is more appropriate. We would like to focus the issues on actual bugs whereas PnP PowerShell related questions can be asked in the discussions section.

As some of the code being PnP PowerShell is actually coming from other PnP repositories, we can move your issue over to that specific repository. You will be notified by email about that.

Before you start to work on code change consider starting a discussion in the repository first. It could potentially save you a lot of time if someone is about to submit a change with exactly the same functionality. It will also allow you to discuss a possible change with the maintainers of the repository before you start to work on it.

## Contributing to PnP PowerShell

Follow the [getting started contributing](/powershell/articles/gettingstartedcontributing.html) guidelines to help out. Sharing is caring!

## Supportability and SLA

This library is open-source and community provided library with active community providing support for it. This is not Microsoft provided module so there's no SLA or direct support for this open-source component from Microsoft. Please report any issues using the [issues list](https://github.com/pnp/powershell/issues).

-------
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

<img src="https://m365-visitor-stats.azurewebsites.net/pnp-powershell/readme" /> 
