# Office font catalog: maintainer notes

These notes cover `OfficeFontCatalogBuilder`, `OfficeFontFace` and `CustomFontsCatalogPublisher`,
used by `Set-PnPOfficeFontCatalog` and `Sync-PnPBrandCenterOfficeFonts`. Read them before changing
those files.

## Why this exists

Office apps offer an organization's fonts from the **Office font library** (the organization assets
library with `OrgAssetType` `OfficeFontLibrary`). Office does not read the font files directly: it
reads a **font catalog** (`fontcatalog*.fontconfig`) that describes each font.

Microsoft's `Set-SPOCustomFontCatalog` builds that catalog with a generator that ships inside the
SharePoint Online Management Shell module (`CustomFontDataGenerator.exe`, a .NET Framework app with
a native DirectWrite helper). That rules out access tokens, non-Windows machines and Azure Functions
on Linux. This code builds the same catalog in C#, with no Microsoft binaries.

## How the format was worked out

Microsoft does not document the catalog format. It was reproduced from the generator's **output**:

1. The generator was run on sample fonts. Its catalog files are plain JSON.
2. Each field was matched to a value in the font file, defined by the public
   [OpenType specification](https://learn.microsoft.com/typography/opentype/spec/).
3. Naming rules (family grouping, variable fonts) follow DirectWrite's documented behaviour.
4. The builder's output was compared byte for byte with the generator's.

The upload sequence (see below) follows what `Set-SPOCustomFontCatalog` does for large uploads, as seen
in the SharePoint Online Management Shell module. The generator itself was not decompiled.

## Files

Each catalog version has its own folder in the library: `1/` and `2/`.

| Version | Files |
|---|---|
| 1 | `fontcatalog.fontconfig`, `fontcatalog_hier.fontconfig`, `fontcatalog_light.fontconfig` |
| 2 | `fontcatalog.fontconfig`, `fontcatalog_hier.fontconfig` |

UTF-8 without BOM, no indentation, non-ASCII text not escaped. **Key order matters**: keep it as
written in the builder.

- `fontcatalog`: `ful` and `sub` are empty arrays.
- `fontcatalog_hier`: `ful` and `sub` are filled.
- `fontcatalog_light` (v1 only): no `Expiration`; families have no `fam`; faces have no `fs` or `v`.

## Fields

Root: `MajorVersion` (1 or 2), `MinorVersion` (from `Tenant.GetCustomFontsMinorVersion`, at least 1),
`Expiration` (always 14), `Fonts` (one entry per family).

Family:

| Key | Value | Source |
|---|---|---|
| `a` | `[4294966911]` | **Unknown.** Same value (0xFFFFFE7F) for every font in every sample. |
| `f` | Family name | WWS family (name ID 21), else typographic family (16), else family (1) without trailing style words |
| `fam` | `[{lcp:0, lsc:"Latn", ltx:f}]` | Same as `f` |
| `ldf` (v2) | `[{lng, lnv}]` | Localized family names, only for fonts with a localized family name |
| `sf` | Faces | One per font file |

Face:

| Key | Value | Source |
|---|---|---|
| `ads` (v2) | `[]` | Always empty in the samples, variable fonts included |
| `c` | 2 numbers | OS/2 `ulCodePageRange1/2` (0 for OS/2 version 0) |
| `dn` | Full name | Name ID 4 |
| `fs` | File size | Bytes |
| `ful` | `[{lcp, lsc, ltx}]` | v1: English full name. v2: English plus localized, with their ISO 15924 script |
| `gn` | GDI family | Name ID 1 |
| `id` | File name without extension | Case kept |
| `ldn`, `lgn` (v2) | `[{lng, lnv}]` | Localized full names and GDI families |
| `p` | 10 numbers | OS/2 PANOSE |
| `sub` | `[{lcp, lsc, ltx}]` | v2: English typographic subfamily (17), else subfamily (2). v1: the record with the **highest language id** (Arial Bold is "Gras") |
| `t` | Extension | Case kept: `TTF` stays `TTF` |
| `u` | 4 numbers | OS/2 `ulUnicodeRange1-4` |
| `v` | Font revision | `head.fontRevision`, raw 16.16 value (65536 = 1.0) |
| `w` | Number | `usWeightClass << 16 \| usWidthClass << 8 \| style` (0 normal, 1 oblique, 2 italic) |

Ordering: faces are sorted by file name, case-insensitively, and families appear in the order of
their first face (Book Antiqua, `ANTQUAB`, comes before Arial, `arial`).

Variable fonts: named after their default instance. The STAT names of the default axis values that
are not elidable are added: weight, width and slope names to the face name, others (optical size...)
to the family name. Segoe UI Variable becomes family "Segoe UI Variable Text".

Rejected, as the generator rejects them: font collections (`.ttc`). Fonts missing a printable ASCII
character get a warning (the generator's `AsciiSupportFailure`).

## What is verified

Generator: SharePoint Online Management Shell 16.0.27709.12000.

| Font set | Result |
|---|---|
| 11 fonts: Windows fonts (regular, bold, italic, bold italic), a CFF `.otf`, a variable font | All 5 files identical |
| Korean font with localized names, Segoe UI Variable, Tahoma | All 5 files identical |
| 11 SIL OFL fonts: Lato (4 styles), Inter and Inter Italic (variable), Noto Sans KR and Noto Sans Arabic (variable), Lobster, Source Sans 3 (CFF `.otf`) | All 5 files identical |
| All 325 `.ttf`/`.otf` in `C:\Windows\Fonts` (Windows 11) | 290 faces identical, 35 different |

The 35 differences:

- **Old fonts with inconsistent internal data**, where DirectWrite guesses weight or family from the
  font name: Arial Rounded MT Bold, Eras, Franklin Gothic Demi/Heavy, Gill Sans Ultra, Tw Cen MT,
  Rockwell Extra Bold, Gloucester MT Extra and similar. Office may group them differently or show
  another weight.
- **Variable fonts:** Cascadia Code and Cascadia Mono.

Variable fonts: besides elidable STAT names, "normal" names (Regular, Normal, Roman, Upright, Standard)
are dropped; a default instance with no style name takes the family as its full name ("Inter", not
"Inter Regular"); and STAT axes that fvar does not vary take the font's fixed value (an italic file
has ital = 1, "Italic").

Fonts made with current tools and complete names are expected to match. To check a new font, compare
with the generator as below.

## How to re-verify

1. Publish a folder of fonts with `Set-SPOCustomFontCatalog` and download the catalog files it creates.
2. Build the catalog of the same folder with `OfficeFontCatalogBuilder.Build` and the same minor version.
3. Compare the files byte for byte, then face by face (by `id`) to see which fields differ.

## Upload

`CustomFontsCatalogPublisher` makes the same calls as `Set-SPOCustomFontCatalog` (16.0.27709), in
the same order and with the same arguments. Every publish uploads **every** font: the previous upload
is replaced, not merged. The threshold is the total size of fonts plus catalog files.

**Up to 1 000 000 bytes:** one call, `Tenant.UploadCustomFontsAndCatalog(resources, libUrl)`: fonts
first (`MajVer` 0), then catalog files (`MajVer` 1, 2). SharePoint replaces the previous upload,
writes the files and bumps the minor version.

**Over 1 000 000 bytes:**

1. `Tenant.IsOfficeFontLibraryOrgAssetFlightEnabled(OfficeFontLibrary)`: server-side feature gate.
2. `Tenant.RemovePreviousCustomFontUpload(["1", "2"], libUrl)`: catalog versions, never font names.
3. The library's folders are read **after** step 2 (it may remove `1/` and `2/`).
4. Font files to the library root folder, catalog files to the folder of their version, overwriting.
5. `Tenant.LogCustomFontsLargeUpload(catalogs, fonts, total)` (usage log; a failure is ignored,
   where Microsoft stops).
6. `Tenant.IncrementCustomFontsMinorVersion(libUrl)`, so Office clients reload the catalog.

`libUrl` is the absolute library URL as typed (trimmed), as Microsoft passes it: the minor version is
kept per URL string.

Known differences, none expected to matter: `CustomFontsResource.fullPath` is the file name (Microsoft
sends its local temp path); a `false` result of `UploadCustomFontsAndCatalog` fails the publish
(Microsoft ignores it); the library is opened with the PnP connection instead of a second sign-in.

### Uploading the files without these calls does not work

An earlier version skipped `RemovePreviousCustomFontUpload` and `UploadCustomFontsAndCatalog` and only
wrote the files and bumped the minor version. The files were byte-identical to Microsoft's, but Office
apps never offered the fonts (tested November 2026). The server-side calls are required.

### Broken library: "urlOfFile"

`RemovePreviousCustomFontUpload` takes **catalog versions** (`"1"`, `"2"`). On a test tenant
(November 2026) it was once called with font file names. From then on, it and
`UploadCustomFontsAndCatalog` failed on every call with "Specified value is not supported for the
urlOfFile parameter", and so does `Set-SPOCustomFontCatalog` on that library. No client call repairs
it: register a new Office font library, or ask Microsoft support to reset it. The publisher turns
that error into a message saying so.

### Registration: remove, upload, add again

Microsoft's documentation says that to change a font library you remove it with
`Remove-SPOOrgAssetsLibrary`, upload again, and register it again. `Set-SPOCustomFontCatalog`
does not touch the registration: it only reads and bumps the minor version
and replaces the catalog. Whatever makes Office reload the library runs on the server, so we follow
the documented procedure instead of relying on the upload alone.

`OfficeFontLibraryChecks.PublishAndRegister` (in `OfficeFontLibraryChecks.cs`) wraps the upload of both
`Set-PnPOfficeFontCatalog` and `Sync-PnPBrandCenterOfficeFonts`:

1. `Tenant.RemoveFromOrgAssetsAndCdn(false, Public, library)`: removes the registration and keeps the CDN origin.
2. The upload above.
3. `Tenant.AddToOrgAssetsLibWithConfig(Public, library, thumbnail, OfficeFontLibrary, false, config)`, in a
   `finally`, so a failed upload never leaves the tenant without its font library. The thumbnail
   from the old registration is passed back.

A library that is not registered yet is registered after the upload; `Set-SPOCustomFontCatalog`
leaves that to `Add-SPOOrgAssetsLibrary`, which is how a tenant ends up with fonts in a library
that Office never reads (`Get-SPOOrgAssetsLibrary` returns nothing). `-KeepRegistration` skips
steps 1 and 3.
