using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using PnP.PowerShell.Commands.Model.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Xml.Linq;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class RecycleBinUtility
    {
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
                    restoreRecycleBinItem.WriteVerbose($"Switching to individul restore of items ...");

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
    }
}