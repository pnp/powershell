using System;
using PnP.Core.Services;
using PnP.Core.Model.SharePoint;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Model
{

    public class PnPBatch
    {
        internal bool RetainAfterExecute { get; set; }
        internal PnPContext Context { get; set; }
        internal Batch Batch { get; set; }

        internal List<IList> Lists { get; set; } = new List<IList>();
        internal List<IContentType> ContentTypes { get; set; } = new List<IContentType>();

        public bool Executed => Batch.Executed;

        public int RequestCount => Requests.Count;
        
        internal SortedList<int, PnP.Core.Services.BatchRequest> Requests => Batch.Requests;

        public PnPBatch(PnPContext context, bool retainRequestsAfterExecute)
        {
            this.RetainAfterExecute = retainRequestsAfterExecute;
            Batch = context.NewBatch();
            this.Context = context;
        }

        public List<BatchResult> Execute(bool throwOnError)
        {
            if (Batch != null)
            {
                var results = Context.ExecuteAsync(Batch, false).GetAwaiter().GetResult();

                ClearCache();
                if (!RetainAfterExecute)
                {
                    Batch = Context.NewBatch();
                }
                return results;
            }
            return null;
        }

        internal void ClearCache()
        {
            Lists.Clear();
            ContentTypes.Clear();
        }

        internal IList GetCachedList(Guid id)
        {
            return Lists.FirstOrDefault(l => l.Id == id);
        }

        internal IList GetCachedList(string titleOrUrl)
        {
            var list = Lists.FirstOrDefault(l => l.Title == titleOrUrl);
            if (list == null)
            {
                list = Lists.FirstOrDefault(l => l.RootFolder.ServerRelativeUrl == titleOrUrl);
            }
            return list;
        }

        internal IContentType GetCachedContentType(string idOrName)
        {
            var ct = ContentTypes.FirstOrDefault(c => c.StringId == idOrName);
            if (ct == null)
            {
                ct = ContentTypes.FirstOrDefault(c => c.Name == idOrName);
            }
            return ct;
        }

        internal void CacheList(IList list)
        {
            var existingList = Lists.FirstOrDefault(l => l.Id == list.Id);
            if (existingList != null)
            {
                Lists.Remove(existingList);
            }
            list.EnsureProperties(l => l.RootFolder);
            Lists.Add(list);
        }

        internal void CacheContentType(IContentType contentType)
        {
            var existingCT = ContentTypes.FirstOrDefault(l => l.StringId == contentType.StringId);
            if (existingCT != null)
            {
                ContentTypes.Remove(existingCT);
            }
            ContentTypes.Add(contentType);
        }
    }
}