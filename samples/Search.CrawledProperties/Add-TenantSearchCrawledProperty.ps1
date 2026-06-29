<# 
.SYNOPSIS
Creates a tenant-level SharePoint Online search crawled property using PnP.PowerShell.

.DESCRIPTION
This script imports an additive tenant search schema configuration package to create a crawled property.
It is intended as a temporary support script until equivalent functionality is available in PnP.PowerShell.

Important:
- Run this against the tenant admin site, for example https://contoso-admin.sharepoint.com.
- This script is additive only. There is no supported PnP.PowerShell command to delete crawled properties
  created in error or to move an existing crawled property to a different property set.
- Verify the crawled property name and property set before running in a production tenant.

.EXAMPLE
Connect-PnPOnline -Url https://contoso-admin.sharepoint.com -ClientId "<client-id>" -Tenant "<tenant>.onmicrosoft.com" -CertificatePath "<path-to-pfx>" -CertificatePassword (Read-Host -AsSecureString)
.\Add-TenantSearchCrawledProperty.ps1 -Name "ows_ProjectCode" -PropertySet SharePointDefault

.EXAMPLE
.\Add-TenantSearchCrawledProperty.ps1 -Name "ows_q_TEXT_ProjectCode" -PropertySet SharePointStructured

.EXAMPLE
.\Add-TenantSearchCrawledProperty.ps1 -Name "ows_taxId_ProjectCategory" -PropertySet SharePointTaxonomy

.EXAMPLE
.\Add-TenantSearchCrawledProperty.ps1 -Name "ows_ProjectCode" -PropertySetGuid "00130329-0000-0130-C000-000000131346" -Force
#>

[CmdletBinding(DefaultParameterSetName = "KnownPropertySet", SupportsShouldProcess = $true)]
param(
    [Parameter(Mandatory = $true)]
    [ValidateNotNullOrEmpty()]
    [string] $Name,

    [Parameter(Mandatory = $true, ParameterSetName = "KnownPropertySet")]
    [ValidateSet(
        "SharePointDefault",
        "SharePointTaxonomy",
        "SharePointStructured",
        "SharePointRich",
        "OfficeSummary",
        "OfficeDocumentSummary",
        "SharePointCrawl",
        "SharePointInternal",
        "Storage",
        "Basic",
        "BasicExtended",
        "BasicContent",
        "SharePointDav",
        "SharePointList",
        "BasicLegacy",
        "PublicStrings",
        "SharePointContent",
        IgnoreCase = $true)]
    [string] $PropertySet,

    [Parameter(Mandatory = $true, ParameterSetName = "PropertySetGuid")]
    [Guid] $PropertySetGuid,

    [Parameter(Mandatory = $false)]
    [switch] $Force,

    [Parameter(Mandatory = $false)]
    [switch] $PrintConfig
)

Set-StrictMode -Version 3.0
$ErrorActionPreference = "Stop"

$propertySets = @{
    SharePointDefault         = @{ Id = [Guid]"00130329-0000-0130-C000-000000131346"; CategoryName = "SharePoint"; MapToContents = $true; Recommended = $true }
    SharePointTaxonomy        = @{ Id = [Guid]"158D7563-AEFF-4DBF-BF16-4A1445F0366C"; CategoryName = "SharePoint"; MapToContents = $false; Recommended = $true }
    SharePointStructured      = @{ Id = [Guid]"ED280121-B677-4E2A-8FBC-0D9E2325B0A2"; CategoryName = "SharePoint"; MapToContents = $false; Recommended = $true }
    SharePointRich            = @{ Id = [Guid]"FEA84DF6-A0FC-492C-9AA7-D28B8DCB08B3"; CategoryName = "SharePoint"; MapToContents = $false; Recommended = $true }
    OfficeSummary             = @{ Id = [Guid]"F29F85E0-4FF9-1068-AB91-08002B27B3D9"; CategoryName = "Office"; MapToContents = $false; Recommended = $false }
    OfficeDocumentSummary     = @{ Id = [Guid]"D5CDD502-2E9C-101B-9397-08002B2CF9AE"; CategoryName = "Office"; MapToContents = $false; Recommended = $false }
    SharePointCrawl           = @{ Id = [Guid]"D1B5D3F0-C0B3-11CF-9A92-00A0C908DBF1"; CategoryName = "SharePoint"; MapToContents = $false; Recommended = $false }
    SharePointInternal        = @{ Id = [Guid]"012357BD-1113-171D-1F25-292BB0B0B0B0"; CategoryName = "Internal"; MapToContents = $false; Recommended = $false }
    Storage                   = @{ Id = [Guid]"B725F130-47EF-101A-A5F1-02608C9EEBAC"; CategoryName = "Basic"; MapToContents = $false; Recommended = $false }
    Basic                     = @{ Id = [Guid]"49691C90-7E17-101A-A91C-08002B2ECDA9"; CategoryName = "Basic"; MapToContents = $false; Recommended = $false }
    BasicExtended             = @{ Id = [Guid]"C82BF597-B831-11D0-B733-00AA00A1EBD2"; CategoryName = "Basic"; MapToContents = $false; Recommended = $false }
    BasicContent              = @{ Id = [Guid]"70EB7A10-55D9-11CF-B75B-00AA0051FE20"; CategoryName = "Basic"; MapToContents = $false; Recommended = $false }
    SharePointDav             = @{ Id = [Guid]"00140329-0000-0140-C000-000000141446"; CategoryName = "SharePoint"; MapToContents = $false; Recommended = $false }
    SharePointList            = @{ Id = [Guid]"00110329-0000-0110-C000-000000111146"; CategoryName = "SharePoint"; MapToContents = $false; Recommended = $false }
    BasicLegacy               = @{ Id = [Guid]"0B63E343-9CCC-11D0-BCDB-00805FCCCE04"; CategoryName = "Basic"; MapToContents = $false; Recommended = $false }
    PublicStrings             = @{ Id = [Guid]"00020329-0000-0000-C000-000000000046"; CategoryName = "SharePoint"; MapToContents = $false; Recommended = $false }
    SharePointContent         = @{ Id = [Guid]"0C4B2ABA-0518-4EC2-807E-25DD264B660F"; CategoryName = "SharePoint"; MapToContents = $false; Recommended = $false }
}

function Import-RequiredModule {
    param([Parameter(Mandatory = $true)][string] $Name)

    if (-not (Get-Module -ListAvailable -Name $Name)) {
        throw "Required PowerShell module '$Name' was not found. Install it with: Install-Module $Name -Scope CurrentUser"
    }

    Import-Module $Name -ErrorAction Stop
}

function Get-ExpectedPropertySet {
    param([Parameter(Mandatory = $true)][string] $CrawledPropertyName)

    if ($CrawledPropertyName -match "^ows_taxId") { return "SharePointTaxonomy" }
    if ($CrawledPropertyName -match "^ows_q_") { return "SharePointStructured" }
    if ($CrawledPropertyName -match "^ows_r_") { return "SharePointRich" }
    if ($CrawledPropertyName -match "^ows_") { return "SharePointDefault" }

    return $null
}

function Confirm-SafetyChecks {
    param(
        [Parameter(Mandatory = $true)][string] $CrawledPropertyName,
        [Parameter(Mandatory = $true)][string] $ResolvedPropertySetName,
        [Parameter(Mandatory = $true)][hashtable] $ResolvedPropertySet,
        [Parameter(Mandatory = $true)][bool] $UsedGuidParameter
    )

    $warnings = New-Object System.Collections.Generic.List[string]

    if ($UsedGuidParameter) {
        $warnings.Add("You are using a property set GUID directly. Prefer -PropertySet unless you are reproducing an existing crawled property pattern.")
    }

    if (-not $ResolvedPropertySet.Recommended) {
        $warnings.Add("'$ResolvedPropertySetName' is a less common property set. Most SharePoint crawled properties should use SharePointDefault, SharePointTaxonomy, SharePointStructured, or SharePointRich.")
    }

    $expected = Get-ExpectedPropertySet -CrawledPropertyName $CrawledPropertyName
    if ($null -ne $expected -and $ResolvedPropertySetName -ne $expected) {
        $warnings.Add("The crawled property name '$CrawledPropertyName' usually belongs in '$expected', but '$ResolvedPropertySetName' was selected.")
    }

    if ($warnings.Count -eq 0 -or $Force) {
        foreach ($warning in $warnings) {
            Write-Warning $warning
        }
        return
    }

    foreach ($warning in $warnings) {
        Write-Warning $warning
    }

    $caption = "Confirm crawled property creation"
    $message = "Crawled properties cannot be deleted or moved to a different property set through supported PnP.PowerShell commands. Continue?"
    if (-not $PSCmdlet.ShouldContinue($message, $caption)) {
        throw "Crawled property creation cancelled."
    }
}

function Resolve-PropertySet {
    if ($PSCmdlet.ParameterSetName -eq "PropertySetGuid") {
        foreach ($entry in $propertySets.GetEnumerator()) {
            if ($entry.Value.Id -eq $PropertySetGuid) {
                return [PSCustomObject]@{
                    Name = $entry.Key
                    Info = $entry.Value
                    UsedGuidParameter = $true
                }
            }
        }

        throw "Property set '$PropertySetGuid' is not supported by this script."
    }

    return [PSCustomObject]@{
        Name = $PropertySet
        Info = $propertySets[$PropertySet]
        UsedGuidParameter = $false
    }
}

function Get-TenantSchemaId {
    $searchConfig = Get-PnPSearchConfiguration -Scope Subscription
    $xml = [xml]$searchConfig
    $schemaIdNode = $xml.SelectSingleNode("//*[local-name()='SchemaId']")

    if ($null -ne $schemaIdNode -and -not [string]::IsNullOrWhiteSpace($schemaIdNode.InnerText)) {
        return [int]$schemaIdNode.InnerText
    }

    throw "Could not resolve the tenant search schema ID from Get-PnPSearchConfiguration -Scope Subscription. No changes were made."
}

function New-EmptyInfoCollectionXml {
    param(
        [Parameter(Mandatory = $true)][string] $ElementName,
        [Parameter(Mandatory = $false)][bool] $IncludeTotalCount = $false
    )

    $xml = @"
<$ElementName xmlns:d3p1="http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration">
  <d3p1:LastItemName i:nil="true"/>
  <d3p1:dictionary xmlns:d4p1="http://schemas.microsoft.com/2003/10/Serialization/Arrays"/>
"@

    if ($IncludeTotalCount) {
        $xml += "`n  <d3p1:TotalCount>0</d3p1:TotalCount>"
    }

    $xml += "`n</$ElementName>"
    return $xml
}

function New-EmptyDictionaryCollectionXml {
    param([Parameter(Mandatory = $true)][string] $ElementName)

    return @"
<$ElementName xmlns:d3p1="http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration">
  <d3p1:dictionary xmlns:d4p1="http://schemas.microsoft.com/2003/10/Serialization/Arrays"/>
</$ElementName>
"@
}

function New-SearchConfigurationXml {
    param(
        [Parameter(Mandatory = $true)][string] $CrawledPropertyName,
        [Parameter(Mandatory = $true)][Guid] $PropertySetId,
        [Parameter(Mandatory = $true)][string] $CategoryName,
        [Parameter(Mandatory = $true)][bool] $MapToContents,
        [Parameter(Mandatory = $true)][int] $SchemaId
    )

    $escapedName = [System.Security.SecurityElement]::Escape($CrawledPropertyName)
    $escapedCategory = [System.Security.SecurityElement]::Escape($CategoryName)
    $propertySetText = $PropertySetId.ToString("D")
    $mapToContentsText = $MapToContents.ToString().ToLowerInvariant()

    $aliases = New-EmptyInfoCollectionXml -ElementName "Aliases"
    $crawledProperties = New-EmptyInfoCollectionXml -ElementName "CrawledProperties"
    $managedProperties = New-EmptyInfoCollectionXml -ElementName "ManagedProperties" -IncludeTotalCount $true
    $mappings = New-EmptyDictionaryCollectionXml -ElementName "Mappings"
    $overrides = New-EmptyDictionaryCollectionXml -ElementName "Overrides"

    return @"
<SearchConfigurationSettings xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Portability">
  <SearchQueryConfigurationSettings>
    <SearchQueryConfigurationSettings>
      <BestBets xmlns:d4p1="http://www.microsoft.com/sharepoint/search/KnownTypes/2008/08"/>
      <DefaultSourceId>00000000-0000-0000-0000-000000000000</DefaultSourceId>
      <DefaultSourceIdSet>true</DefaultSourceIdSet>
      <DeployToParent>false</DeployToParent>
      <DisableInheritanceOnImport>false</DisableInheritanceOnImport>
      <QueryRuleGroups xmlns:d4p1="http://www.microsoft.com/sharepoint/search/KnownTypes/2008/08"/>
      <QueryRules xmlns:d4p1="http://www.microsoft.com/sharepoint/search/KnownTypes/2008/08"/>
      <ResultTypes xmlns:d4p1="http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration"/>
      <Sources xmlns:d4p1="http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration.Query"/>
      <UserSegments xmlns:d4p1="http://www.microsoft.com/sharepoint/search/KnownTypes/2008/08"/>
    </SearchQueryConfigurationSettings>
  </SearchQueryConfigurationSettings>
  <SearchRankingModelConfigurationSettings>
    <RankingModels xmlns:d3p1="http://schemas.microsoft.com/2003/10/Serialization/Arrays"/>
  </SearchRankingModelConfigurationSettings>
  <SearchSchemaConfigurationSettings>
    $aliases
    <CategoriesAndCrawledProperties xmlns:d3p1="http://schemas.microsoft.com/2003/10/Serialization/Arrays">
      <d3p1:KeyValueOfguidCrawledPropertyInfoCollectionaSYUqUE_P>
        <d3p1:Key>$propertySetText</d3p1:Key>
        <d3p1:Value xmlns:d5p1="http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration">
          <d5p1:LastItemName>$escapedName</d5p1:LastItemName>
          <d5p1:dictionary>
            <d3p1:KeyValueOfstringCrawledPropertyInfoy6h3NzC8>
              <d3p1:Key>$escapedName</d3p1:Key>
              <d3p1:Value>
                <d5p1:Name>$escapedName</d5p1:Name>
                <d5p1:CategoryName>$escapedCategory</d5p1:CategoryName>
                <d5p1:IsImplicit>false</d5p1:IsImplicit>
                <d5p1:IsMappedToContents>$mapToContentsText</d5p1:IsMappedToContents>
                <d5p1:IsNameEnum>false</d5p1:IsNameEnum>
                <d5p1:MappedManagedProperties/>
                <d5p1:Propset>$propertySetText</d5p1:Propset>
                <d5p1:Samples/>
                <d5p1:SchemaId>$SchemaId</d5p1:SchemaId>
              </d3p1:Value>
            </d3p1:KeyValueOfstringCrawledPropertyInfoy6h3NzC8>
          </d5p1:dictionary>
        </d3p1:Value>
      </d3p1:KeyValueOfguidCrawledPropertyInfoCollectionaSYUqUE_P>
    </CategoriesAndCrawledProperties>
    $crawledProperties
    $managedProperties
    $mappings
    $overrides
    <SchemaId>$SchemaId</SchemaId>
  </SearchSchemaConfigurationSettings>
  <SearchSubscriptionSettingsConfigurationSettings i:nil="true"/>
  <SearchTaxonomyConfigurationSettings i:nil="true"/>
</SearchConfigurationSettings>
"@
}

Import-RequiredModule -Name "PnP.PowerShell"

$connection = Get-PnPConnection -ErrorAction SilentlyContinue
if ($null -eq $connection) {
    throw "No active PnP connection was found. Connect to the tenant admin site first, for example: Connect-PnPOnline -Url https://contoso-admin.sharepoint.com -ClientId <client-id> -Tenant <tenant>.onmicrosoft.com -CertificatePath <path-to-pfx>"
}

if ($connection.Url -notmatch "-admin\.sharepoint\.") {
    throw "The active PnP connection is '$($connection.Url)'. Connect to the SharePoint Online tenant admin site before running this script."
}

$resolved = Resolve-PropertySet
Confirm-SafetyChecks -CrawledPropertyName $Name -ResolvedPropertySetName $resolved.Name -ResolvedPropertySet $resolved.Info -UsedGuidParameter $resolved.UsedGuidParameter

$schemaId = Get-TenantSchemaId
$configuration = New-SearchConfigurationXml -CrawledPropertyName $Name -PropertySetId $resolved.Info.Id -CategoryName $resolved.Info.CategoryName -MapToContents $resolved.Info.MapToContents -SchemaId $schemaId

Write-Verbose "Creating tenant crawled property '$Name' using property set '$($resolved.Name)' ($($resolved.Info.Id)), category '$($resolved.Info.CategoryName)', and schema ID '$schemaId'."
Write-Debug $configuration

if ($PrintConfig) {
    $configuration
    return
}

if ($PSCmdlet.ShouldProcess($Name, "Create tenant search crawled property in property set '$($resolved.Name)'")) {
    Set-PnPSearchConfiguration -Scope Subscription -Configuration $configuration

    [PSCustomObject]@{
        Name            = $Name
        PropertySet     = $resolved.Name
        PropertySetGuid = $resolved.Info.Id
        CategoryName    = $resolved.Info.CategoryName
        MapToContents   = $resolved.Info.MapToContents
        SchemaId        = $schemaId
        Imported        = $true
    }
}
