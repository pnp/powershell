# About

This script allows admins to collect information (metadata) about a given document based upon it's site id and unique id.

## Usage

```powershell
# Connect to a site in your tenant, see https://pnp.github.io/powershell/index.html to learn how to get started with PnP PowerShell
Connect-PnPOnline -Url https://contoso.sharepoint.com -Interactive

# List information about the document based upon the site id and document unique id:
.\GetDocumentMetadataViaSiteIdUniqueId.ps1 "b56adf79-ff6a-4964-a63a-ff1fa23be9f8" "e2a52975-ceae-41b3-9271-315a5cfba6b2"
```

The output shows the Author and Path toward the provided document:

```
Author   Path
------   ----
Joe Doe  https://contoso.sharepoint.com/sites/demo/pnp/Rocks!.docx
```
