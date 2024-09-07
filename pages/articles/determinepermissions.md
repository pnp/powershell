# How to determine which permissions you need

As of September 9<sup>th</sup>, 2024, it is [required to use your own Entra ID Application Registration](https://pnp.github.io/blog/post/changes-pnp-management-shell-registration/) to use PnP PowerShell. This introduces the complexity of trying to determining the minimum set of permissions you will need to be able to execute your script. This article aims to help you in determining the permissions you need to set on your Entra ID Application Registration.

You will find the app under 'Application Registrations' and consent to the application there.

## Creating an Entra ID Application Registration

In case you're starting from the beginning and you do not have your own Entra ID Application Registration yet to use with PnP Powershell, which is mandatory, you can [follow these steps](registerapplication.md) to create your Entra ID Application Registration.

