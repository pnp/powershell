---
uid: pnp.powershell.articles.extract-configuration
title: The extract configuration (using PnP Provisioning Engine)
description: Reference and recipes for the JSON configuration accepted by Get-PnPSiteTemplate, Get-PnPTenantTemplate and Export-PnPPage.
---

# The extract configuration (using PnP Provisioning Engine)

`Get-PnPSiteTemplate`, `Get-PnPTenantTemplate` and `Export-PnPPage` accept a `-Configuration` parameter which describes what to take out of a site. It gives you control the individual switches on those cmdlets do not, such as extracting one named list together with its items.

The value is either the path to a file holding the JSON, or the JSON itself:

```powershell
Get-PnPSiteTemplate -Out template.xml -Configuration .\extract.json
Get-PnPSiteTemplate -Out template.xml -Configuration '{ "handlers": [ "Lists" ] }'
```

A configuration which cannot be read is an error, not something that is passed over: a path which does not exist, JSON which cannot be parsed, a null anywhere in it, or a `handlers` array in which no name is recognized will all stop the cmdlet. A property or a handler name which is not recognized is reported as a warning and then ignored, so a configuration written for a newer version still applies the parts this version understands.

For applying a template, see [The apply configuration](apply-configuration.md).

## Things which are easy to get wrong

**Leaving `handlers` out is not the same as an empty list.** Omit the property and every handler runs. An empty array does the same. Only a non-empty array narrows the work, and because a misspelled name is dropped, a `handlers` array whose names are all wrong would otherwise quietly widen the extract to everything. That case is rejected.

**Handler names are case sensitive.** `"Lists"` is a handler, `"lists"` is not.

**`includeItems` only produces data rows for lists.** On a document library the items are the files, so they come out as `<pnp:Files>` in the template rather than `<pnp:DataRows>`. Setting `includeItems` on a library is not an error and not a no-op, it simply does not give you data rows.

**Naming a list under `lists.lists` narrows the whole Lists handler.** Once you name one, only the ones you name are extracted.

**`title` takes the list title or the list ID, not a URL.** A value which parses as a GUID is matched against the list ID; anything else has to equal the list title exactly. A server relative path such as `Lists/Events` matches nothing and leaves you with an empty template.

**Addressing a list by ID and asking for its items at the same time currently fails** with an `Object reference not set to an instance of an object` from PnP Framework. Use the list title when you need `includeItems`.

**A property name which does not exist is ignored.** `includeItem` instead of `includeItems` used to leave you with a template that silently held no items. It is now reported as a warning, but the setting still has no effect, so read the warnings.

## Recipes

### Extract one list together with its items

```json
{
  "handlers": [ "Lists" ],
  "lists": {
    "lists": [
      { "title": "Events", "includeItems": true }
    ]
  }
}
```

The `<pnp:ListInstance>` for Events carries a `<pnp:DataRows>` element, one `<pnp:DataRow>` per item.

### Extract only some columns of the items, newest first

```json
{
  "handlers": [ "Lists" ],
  "lists": {
    "lists": [
      {
        "title": "Events",
        "includeItems": true,
        "skipEmptyFields": true,
        "query": {
          "camlQuery": "<OrderBy><FieldRef Name='Created' Ascending='FALSE' /></OrderBy>",
          "viewFields": [ "Title", "EventDate", "Location" ],
          "rowLimit": 50
        }
      }
    ]
  }
}
```

`viewFields` limits which columns end up in each data row and `skipEmptyFields` drops the empty ones, which keeps a template readable. Without a `query` every field of every item is written out, including the system ones.

### Extract the pages of a site along with their assets

```json
{
  "handlers": [ "Pages", "Files" ],
  "pages": {
    "includeAllClientSidePages": true,
    "excludeAuthorInformation": true
  },
  "persistAssetFiles": true
}
```

`persistAssetFiles` is what puts the images and other files into the package, so use `-Out template.pnp` rather than `.xml` to have somewhere to put them. `excludeAuthorInformation` keeps the authors of the source site out of the template.

### Extract a list by ID rather than by title

```json
{
  "handlers": [ "Lists" ],
  "lists": {
    "lists": [
      { "title": "9f8b6c1e-3d2a-4b7c-8e5f-1a2b3c4d5e6f" }
    ]
  }
}
```

Useful when the title is localized or may change: put the list ID in `title` and a value which parses as a GUID is matched against the list ID instead. Note that this cannot be combined with `includeItems`, see above.

## Properties

<!-- BEGIN GENERATED PROPERTIES -->

### `$`

| Property | Type | Default |
| -------- | ---- | ------- |
| `contentTypes` | object |  |
| `extensibility` | object |  |
| `handlers` | array of strings, see [ConfigurationHandler values](#configurationhandler-values) |  |
| `lists` | object |  |
| `multiLanguage` | object |  |
| `navigation` | object |  |
| `pages` | object |  |
| `persistAssetFiles` | boolean | `false` |
| `propertyBag` | object |  |
| `publishing` | object |  |
| `searchSettings` | object |  |
| `siteFooter` | object |  |
| `siteSecurity` | object |  |
| `syntexModels` | object |  |
| `taxonomy` | object |  |
| `tenant` | object |  |

### `$.contentTypes`

| Property | Type | Default |
| -------- | ---- | ------- |
| `excludeFromSyndication` | boolean | `false` |
| `groups` | array of strings |  |

### `$.extensibility`

| Property | Type | Default |
| -------- | ---- | ------- |
| `handlers` | array of `ExtensibilityHandler` objects |  |

### `$.lists`

| Property | Type | Default |
| -------- | ---- | ------- |
| `includeHiddenLists` | boolean | `false` |
| `lists` | array of objects |  |

### `$.multiLanguage`

| Property | Type | Default |
| -------- | ---- | ------- |
| `persistMultiLanguageResources` | boolean | `false` |
| `resourceFilePrefix` | string |  |

### `$.navigation`

| Property | Type | Default |
| -------- | ---- | ------- |
| `removeExistingNodes` | boolean | `false` |

### `$.pages`

| Property | Type | Default |
| -------- | ---- | ------- |
| `excludeAuthorInformation` | boolean | `false` |
| `includeAllClientSidePages` | boolean | `false` |

### `$.publishing`

| Property | Type | Default |
| -------- | ---- | ------- |
| `includeNativePublishingFiles` | boolean | `false` |
| `persist` | boolean | `false` |

### `$.searchSettings`

| Property | Type | Default |
| -------- | ---- | ------- |
| `include` | boolean | `false` |

### `$.siteFooter`

| Property | Type | Default |
| -------- | ---- | ------- |
| `removeExistingNodes` | boolean | `false` |

### `$.siteSecurity`

| Property | Type | Default |
| -------- | ---- | ------- |
| `includeSiteGroups` | boolean | `false` |

### `$.syntexModels`

| Property | Type | Default |
| -------- | ---- | ------- |
| `models` | array of objects |  |

### `$.taxonomy`

| Property | Type | Default |
| -------- | ---- | ------- |
| `includeAllTermGroups` | boolean | `false` |
| `includeSecurity` | boolean | `false` |
| `includeSiteCollectionTermGroup` | boolean | `false` |

### `$.tenant`

| Property | Type | Default |
| -------- | ---- | ------- |
| `sequence` | object |  |
| `teams` | object |  |

### `$.lists.lists`

| Property | Type | Default |
| -------- | ---- | ------- |
| `includeItems` | boolean | `false` |
| `keyColumn` | string |  |
| `query` | object |  |
| `removeExistingContentTypes` | boolean | `false` |
| `skipEmptyFields` | boolean | `false` |
| `title` | string |  |
| `updateBehavior` | `Overwrite` \| `Skip` | `Overwrite` |

### `$.syntexModels.models`

| Property | Type | Default |
| -------- | ---- | ------- |
| `excludeTrainingData` | boolean | `false` |
| `id` | integer | `0` |
| `name` | string |  |

### `$.tenant.sequence`

| Property | Type | Default |
| -------- | ---- | ------- |
| `includeJoinedSites` | boolean | `false` |
| `includeSubsites` | boolean | `false` |
| `maxSubsiteDepth` | integer | `0` |
| `siteUrls` | array of strings |  |

### `$.tenant.teams`

| Property | Type | Default |
| -------- | ---- | ------- |
| `includeAllTeams` | boolean | `false` |
| `includeGroupId` | boolean | `false` |
| `includeMessages` | boolean | `false` |
| `teamSiteUrls` | array of strings |  |

### `$.lists.lists.query`

| Property | Type | Default |
| -------- | ---- | ------- |
| `camlQuery` | string |  |
| `includeAttachments` | boolean | `false` |
| `pageSize` | integer | `0` |
| `rowLimit` | integer | `0` |
| `viewFields` | array of strings |  |

### ConfigurationHandler values

These names are case sensitive.

- `AuditSettings`
- `ComposedLook`
- `CustomActions`
- `ExtensibilityProviders`
- `Features`
- `Fields`
- `Files`
- `Lists`
- `Pages`
- `Publishing`
- `RegionalSettings`
- `SearchSettings`
- `SitePolicy`
- `SupportedUILanguages`
- `Taxonomy`
- `Workflows`
- `SiteSecurity`
- `ContentTypes`
- `PropertyBagEntries`
- `WebSettings`
- `Navigation`
- `ImageRenditions`
- `ApplicationLifecycleManagement`
- `Tenant`
- `WebApiPermissions`
- `SiteHeader`
- `SiteFooter`
- `Theme`
- `SiteSettings`
- `SyntexModels`

<!-- END GENERATED PROPERTIES -->
