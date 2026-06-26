using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Http;

namespace PnP.PowerShell.Commands.Utilities;

/// <summary>
/// Class containing utility methods for handling SharePoint Recycle Bin operations.
/// </summary>
internal static class RecycleBinUtility
{
    /// <summary>
    /// Retrieves recycle bin items from the site collection with support for paging to handle large result sets.
    /// Implements batching to avoid exceeding the List View Threshold (5000 items).
    /// </summary>
    /// <param name="ctx">The client context for the SharePoint site</param>
    /// <param name="rowLimit">Optional maximum number of items to retrieve</param>
    /// <param name="recycleBinStage">The recycle bin stage to query (first stage, second stage, or both)</param>
    /// <returns>A list of recycle bin items</returns>
    internal static List<RecycleBinItem> GetRecycleBinItems(ClientContext ctx, int? rowLimit = null, RecycleBinItemState recycleBinStage = RecycleBinItemState.None)
    {
        var recycleBinItems = new List<RecycleBinItem>();
        string pagingInfo = null;
        RecycleBinItemCollection items;

        // This part is only here to make debugging easier if you ever run into issues with this code :)
        //ctx.Load(ctx.Site.RecycleBin);
        //ctx.ExecuteQueryRetry();
        //var totalRecyclebinContentsCount = ctx.Site.RecycleBin.Count;

        do
        {
            // We don't actually know what the List View Threshold for the Recycle Bin is, so we'll use the safe number (5000) and implement paging.
            int iterationRowLimit;
            if (rowLimit.HasValue && rowLimit.Value >= 5000)
            {
                // Subtract this page's count from the rowLimit (we don't want duplicates or go out of bounds)
                if (rowLimit.HasValue) rowLimit -= 5000;

                iterationRowLimit = 5000;
            }
            else if (rowLimit.HasValue && rowLimit.Value > 0 && rowLimit.Value < 5000)
            {
                iterationRowLimit = rowLimit.Value;
            }
            else // rowLimit was not set, just fetch a "whole page"
            {
                iterationRowLimit = 5000;
            }

            items = ctx.Site.GetRecycleBinItems(pagingInfo, iterationRowLimit, false, RecycleBinOrderBy.DefaultOrderBy, recycleBinStage);
            ctx.Load(items);
            ctx.ExecuteQueryRetry();
            recycleBinItems.AddRange(items.ToList());

            // Paging magic (if needed)
            // Based on this work our good friends at Portiva did ❤
            // https://www.portiva.nl/portiblog/blogs-cat/paging-through-sharepoint-recycle-bin
            if (items.Count > 0)
            {
                var nextId = items.Last().Id;
                //var nextTitle = items.Last().Title;
                var nextTitle = WebUtility.UrlEncode(items.Last().Title);
                //var deletionTime = items.Last().DeletedDate;
                pagingInfo = $"id={nextId}&title={nextTitle}"; // &searchValue=${deletionTime}
            }
        }
        while (items?.Count == 5000); // if items had 5000 items, there might be more since that's the page size we're using

        return recycleBinItems;
    }

    /// <summary>
    /// Retrieves recycle bin item collections from the site collection with support for paging.
    /// Similar to GetRecycleBinItems but returns collections instead of individual items.
    /// </summary>
    /// <param name="ctx">The client context for the SharePoint site</param>
    /// <param name="rowLimit">Optional maximum number of items to retrieve per collection</param>
    /// <param name="recycleBinItemState">The recycle bin stage to query</param>
    /// <returns>A list of recycle bin item collections</returns>
    internal static List<RecycleBinItemCollection> GetRecycleBinItemCollection(ClientContext ctx, int? rowLimit = null, RecycleBinItemState recycleBinItemState = RecycleBinItemState.None)
    {
        string pagingInfo = null;
        RecycleBinItemCollection items;
        var recycleBinItems = new List<RecycleBinItemCollection>();

        do
        {
            // We don't actually know what the List View Threshold for the Recycle Bin is, so we'll use the safe number (5000) and implement paging.
            int iterationRowLimit;
            if (rowLimit.HasValue && rowLimit.Value >= 5000)
            {
                // Subtract this page's count from the rowLimit (we don't want duplicates or go out of bounds)
                if (rowLimit.HasValue) rowLimit -= 5000;

                iterationRowLimit = 5000;
            }
            else if (rowLimit.HasValue && rowLimit.Value > 0 && rowLimit.Value < 5000)
            {
                iterationRowLimit = rowLimit.Value;
            }
            else
            {
                iterationRowLimit = 5000;
            }

            items = ctx.Site.GetRecycleBinItems(pagingInfo, iterationRowLimit, false, RecycleBinOrderBy.DefaultOrderBy, recycleBinItemState);
            ctx.Load(items);
            ctx.ExecuteQueryRetry();
            recycleBinItems.Add(items);

            if (items.Count > 0)
            {
                var nextId = items.Last().Id;
                var nextTitle = WebUtility.UrlEncode(items.Last().Title);
                pagingInfo = $"id={nextId}&title={nextTitle}";
            }
        }
        while (items?.Count == 5000);

        return recycleBinItems;
    }

    /// <summary>
    /// Restores multiple recycle bin items by their IDs using the SharePoint REST API.
    /// Attempts batch restore first, then falls back to individual restore if batch fails.
    /// </summary>
    /// <param name="httpClient">HTTP client for making REST API calls</param>
    /// <param name="ctx">The client context for the SharePoint site</param>
    /// <param name="idsList">Array of recycle bin item IDs to restore</param>
    /// <param name="restoreRecycleBinItem">The cmdlet instance for logging verbose output</param>
    internal static void RestoreRecycleBinItemInBulk(HttpClient httpClient, ClientContext ctx, string[] idsList, RecycleBin.RestoreRecycleBinItem restoreRecycleBinItem)
    {
        //restoreRecycleBinItem provides us the reference to the instance of RestoreRecycleBinItem object. We use this object to log key information as verbose
        Uri currentContextUri = new Uri(ctx.Url);
        string apiCall = $"{currentContextUri}/_api/site/RecycleBin/RestoreByIds";

        string idsString = string.Join("','", idsList); // Convert array to a comma-separated string  

        try
        {
            string requestBody = $"{{'ids':['{idsString}']}}";
            REST.RestHelper.Post(httpClient, apiCall, ctx, requestBody, "application/json", "application/json");
            restoreRecycleBinItem.WriteVerbose("Whole batch restored successfuly.");
        }
        catch (Exception ex)
        {
            {
                //fall back logic
                //Unable to process as batch because of an error in restoring one of the ids in batch, processing individually
                restoreRecycleBinItem.WriteVerbose($"Unable to process as batch because of an error in restoring one of the ids in batch. Error:{ex.Message}");
                restoreRecycleBinItem.WriteVerbose($"Switching to individual restore of items ...");

                foreach (string id in idsList)
                {
                    try
                    {
                        string requestBody = $"{{'ids':['{id}']}}";
                        REST.RestHelper.Post(httpClient, apiCall, ctx, requestBody, "application/json", "application/json");
                        restoreRecycleBinItem.WriteVerbose($"Item - {id} restored successfuly.");

                    }
                    catch (Exception e)
                    {
                        var odataError = e.Message;
                        if (odataError != null)
                        {
                            if (odataError.Contains("Value does not fall within the expected range."))
                            {
                                restoreRecycleBinItem.WriteVerbose($"Item - {id} already restored.");
                            }
                            else
                            {
                                //Most common reason is that an item with the same name already exists. To restore the item, rename the existing item and try again
                                restoreRecycleBinItem.WriteVerbose($"Item - {id} restore failed. Error:{odataError}");
                            }
                        }
                        //Digest errors because we cannot do anything
                    }
                }
            }
        }
    }

    /// <summary>
    /// Restores a single recycle bin item, multiple items by row limit, or all items.
    /// Handles different scenarios: specific item by identity, limited batch by row count, or all items.
    /// </summary>
    /// <param name="ctx">The client context for the SharePoint site</param>
    /// <param name="restoreRecycleBinItem">The cmdlet instance containing parameters and for logging</param>
    internal static void RestoreRecycleBinItemSingle(ClientContext ctx, RecycleBin.RestoreRecycleBinItem restoreRecycleBinItem)
    {
        if (restoreRecycleBinItem.ParameterSpecified(nameof(restoreRecycleBinItem.Identity)))
        {
            // if Identity has item, use it
            if (restoreRecycleBinItem.Identity.Item != null)
            {
                if (restoreRecycleBinItem.Force || restoreRecycleBinItem.ShouldContinue(string.Format(Properties.Resources.RestoreRecycleBinItem, restoreRecycleBinItem.Identity.Item.LeafName), Properties.Resources.Confirm))
                {
                    restoreRecycleBinItem.Identity.Item.Restore();
                    ctx.ExecuteQueryRetry();
                }
            }
            else
            {
                var recycleBinItem = restoreRecycleBinItem.Identity.GetRecycleBinItem(restoreRecycleBinItem.Connection.PnPContext);

                if (recycleBinItem == null)
                {
                    throw new PSArgumentException("Recycle bin item not found with the ID specified", nameof(restoreRecycleBinItem.Identity));
                }

                if (restoreRecycleBinItem.Force || restoreRecycleBinItem.ShouldContinue(string.Format(Properties.Resources.RestoreRecycleBinItem, recycleBinItem.LeafName), Properties.Resources.Confirm))
                {
                    recycleBinItem.Restore();
                }
            }
        }
        else
        {
            if (restoreRecycleBinItem.ParameterSpecified(nameof(restoreRecycleBinItem.RowLimit)))
            {
                if (restoreRecycleBinItem.Force || restoreRecycleBinItem.ShouldContinue(string.Format(Properties.Resources.Restore0RecycleBinItems, restoreRecycleBinItem.RowLimit), Properties.Resources.Confirm))
                {
                    var recycleBinItemCollection = GetRecycleBinItemCollection(ctx, restoreRecycleBinItem.RowLimit, RecycleBinItemState.None);
                    for (var i = 0; i < recycleBinItemCollection.Count; i++)
                    {
                        var recycleBinItems = recycleBinItemCollection[i];
                        recycleBinItems.RestoreAll();
                        ctx.ExecuteQueryRetry();
                    }
                }
            }
            else
            {
                if (restoreRecycleBinItem.Force || restoreRecycleBinItem.ShouldContinue(Properties.Resources.RestoreRecycleBinItems, Properties.Resources.Confirm))
                {
                    restoreRecycleBinItem.Connection.PnPContext.Site.RecycleBin.RestoreAll();
                }
            }
        }
    }
}