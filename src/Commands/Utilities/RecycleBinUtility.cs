using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class RecycleBinUtility
    {
        internal static List<RecycleBinItem> GetRecycleBinItems(ClientContext ctx, int? rowLimit = null, RecycleBinItemState? recycleBinStage = null)
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

                if (recycleBinStage.HasValue)
                {
                    items = ctx.Site.GetRecycleBinItems(pagingInfo, iterationRowLimit, false, RecycleBinOrderBy.DefaultOrderBy, recycleBinStage.Value);
                    ctx.Load(items);
                    ctx.ExecuteQueryRetry();
                    recycleBinItems.AddRange(items.ToList());
                }
                else
                {
                    items = ctx.Site.GetRecycleBinItems(pagingInfo, iterationRowLimit, false, RecycleBinOrderBy.DefaultOrderBy, RecycleBinItemState.None);
                    ctx.Load(items);
                    ctx.ExecuteQueryRetry();
                    recycleBinItems.AddRange(items.ToList());
                }

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
    }
}