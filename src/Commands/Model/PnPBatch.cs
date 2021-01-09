using System;
using PnP.Core.Services;
using PnP.Core.Model.SharePoint;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Model
{

    public class PnPBatch
    {
        internal PnPContext Context { get; set; }
        internal Batch Batch { get; set; }

        internal List<IList> Lists { get; set; } = new List<IList>();
        internal List<IContentType> ContentTypes { get; set; } = new List<IContentType>();

        public bool Executed => Batch.Executed;

        internal SortedList<int, PnP.Core.Services.BatchRequest> Requests => Batch.Requests;

        public PnPBatch(PnPContext context)
        {
            Batch = context.NewBatch();
            this.Context = context;
        }

        public void Execute()
        {
            if (Batch != null)
            {
                Context.Execute(Batch);
                ClearCache();
            }
        }

        internal void ClearCache()
        {
            Lists.Clear();
            ContentTypes.Clear();
        }

        public IList GetCachedList(Guid id)
        {
            return Lists.FirstOrDefault(l => l.Id == id);
        }

        public IList GetCachedList(string titleOrUrl)
        {
            var list = Lists.FirstOrDefault(l => l.Title == titleOrUrl);
            if (list == null)
            {
                list = Lists.FirstOrDefault(l => l.RootFolder.ServerRelativeUrl == titleOrUrl);
            }
            return list;
        }

        public IContentType GetCachedContentType(string idOrName)
        {
            var ct = ContentTypes.FirstOrDefault(c => c.StringId == idOrName);
            if (ct == null)
            {
                ct = ContentTypes.FirstOrDefault(c => c.Name == idOrName);
            }
            return ct;
        }

        public void CacheList(IList list)
        {
            var existingList = Lists.FirstOrDefault(l => l.Id == list.Id);
            if (existingList != null)
            {
                Lists.Remove(existingList);
            }
            list.EnsureProperties(l => l.RootFolder);
            Lists.Add(list);
        }

        public void CacheContentType(IContentType contentType)
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