# About

This script allows admins to collect information (metadata) about a SPO site given its SiteID GUID.

## Usage

```powershell
.\SiteIdToUrl.ps1
```

The script asks for the SPO Admin URL (eg. `https://contoso-admin.sharepoint.com`). 
The script then opens a file-select dialog for the input file of SiteID GUIDs.
The output is a CSV file of metadata for each site, including its URL.

Here is an abbreviated sample of the output. The script will return more metadata than below.

```
Title,Author,Size,Path
CommSiteShowcase,Sam,13,https://contoso.sharepoint.com/sites/CommSiteShowcase
contoso,System Account,207979,https://contoso.sharepoint.com/
```

## **Disclaimer** 

THIS CODE IS PROVIDED AS IS WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

> IMPORTANT - PnP PowerShell and all other PnP components are open-source tools backed by an active community providing support for them. There is no SLA for open-source tool support from official Microsoft support channels.