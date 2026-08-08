---
uid: pnp.powershell.articles.apply-configuration
title: The apply configuration (using PnP Provisioning Engine)
description: Reference and recipes for the JSON configuration accepted by Invoke-PnPTenantTemplate.
---

# The apply configuration (using PnP Provisioning Engine)

`Invoke-PnPTenantTemplate` accepts a `-Configuration` parameter which describes how a template is applied. It covers the same ground as the individual switches on the cmdlet, and adds the template parameters.

The value is either the path to a file holding the JSON, or the JSON itself:

```powershell
Invoke-PnPTenantTemplate -Path template.pnp -Configuration .\apply.json
Invoke-PnPTenantTemplate -Path template.pnp -Configuration '{ "handlers": [ "Lists" ] }'
```

A configuration which cannot be read is an error, not something that is passed over: a path which does not exist, JSON which cannot be parsed, a null anywhere in it, or a `handlers` array in which no name is recognized will all stop the cmdlet before anything is applied. A property or a handler name which is not recognized is reported as a warning and then ignored.

For extracting a template, see [The extract configuration](extract-configuration.md).

## Things which are easy to get wrong

**Leaving `handlers` out applies the whole template.** Omit the property, or pass an empty array, and every handler runs. This matters more here than when extracting: a misspelling in `handlers` would mean applying everything to a tenant rather than the subset you intended, so a `handlers` array whose names are all unrecognized is rejected.

**Handler names are case sensitive.** `"Lists"` is a handler, `"lists"` is not.

**`parameters` feeds the `{parameter:key}` tokens in the template.** Keys and values are both strings and the keys are free-form, so nothing here is validated against the template. A key the template never refers to is silently unused, and a token the template refers to but you do not supply stays unresolved.

**`lists.ignoreDuplicateDataRowErrors` is how you make a re-run survive existing rows.** Applying a template holding data rows a second time will otherwise fail on the rows already there, unless the template's `DataRows` element sets a `KeyColumn`.

## Recipes

### Apply only the lists and fields of a template

```json
{
  "handlers": [ "Lists", "Fields" ]
}
```

### Supply the template's parameters

```json
{
  "handlers": [ "Lists", "Fields" ],
  "parameters": {
    "ListTitle": "Projects",
    "Owner": "megan@contoso.onmicrosoft.com"
  }
}
```

Wherever the template holds `{parameter:ListTitle}` it resolves to `Projects`.

### Re-apply a template which carries data rows

```json
{
  "handlers": [ "Lists" ],
  "lists": {
    "ignoreDuplicateDataRowErrors": true
  }
}
```

### Apply content types and fields down to the subsites

```json
{
  "handlers": [ "ContentTypes", "Fields" ],
  "contentTypes": {
    "provisionContentTypesToSubWebs": true
  },
  "fields": {
    "provisionFieldsToSubWebs": true
  }
}
```

## Properties

<!-- BEGIN GENERATED PROPERTIES -->

### `$`

| Property | Type | Default |
| -------- | ---- | ------- |
| `contentTypes` | object |  |
| `extensibility` | object |  |
| `fields` | object |  |
| `handlers` | array of strings, see [ConfigurationHandler values](#configurationhandler-values) |  |
| `lists` | object |  |
| `navigation` | object |  |
| `parameters` | object with free-form string keys and string values |  |
| `propertyBag` | object |  |
| `tenant` | object |  |

### `$.contentTypes`

| Property | Type | Default |
| -------- | ---- | ------- |
| `provisionContentTypesToSubWebs` | boolean | `false` |

### `$.extensibility`

| Property | Type | Default |
| -------- | ---- | ------- |
| `handlers` | array of `ExtensibilityHandler` objects |  |

### `$.fields`

| Property | Type | Default |
| -------- | ---- | ------- |
| `provisionFieldsToSubWebs` | boolean | `false` |

### `$.lists`

| Property | Type | Default |
| -------- | ---- | ------- |
| `ignoreDuplicateDataRowErrors` | boolean | `false` |

### `$.navigation`

| Property | Type | Default |
| -------- | ---- | ------- |
| `clearNavigation` | boolean | `false` |

### `$.propertyBag`

| Property | Type | Default |
| -------- | ---- | ------- |
| `overwriteSystemValues` | boolean | `false` |

### `$.tenant`

| Property | Type | Default |
| -------- | ---- | ------- |
| `doNotWaitForSitesToBeFullyCreated` | boolean | `false` |

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
