using System;
using PnP.Core.Services;
using PnP.Core.Model.SharePoint;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint.Client.Taxonomy;

namespace PnP.PowerShell.Commands.Model
{

    public class PnPBatch
    {
        internal bool RetainAfterExecute { get; set; }
        internal PnPContext Context { get; set; }
        internal Batch Batch { get; set; }

        internal List<IList> Lists { get; set; } = new List<IList>();
        internal List<IContentType> ContentTypes { get; set; } = new List<IContentType>();
        internal List<(string key, Guid id, string label)> Terms { get; set; } = new List<(string key, Guid id, string label)>();

        internal List<ISyntexModel> SyntexModels { get; set; } = new List<ISyntexModel>();

        internal int? DefaultTermStoreLanguage { get; set; }
        internal TaxonomySession TaxonomySession { get; set; }
        internal Microsoft.SharePoint.Client.Taxonomy.TermStore TermStore { get; set; }
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
                var results = Context.Execute(Batch, throwOnError);

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
            SyntexModels.Clear();
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

        internal ISyntexModel GetCachedSyntexModel(int id)
        {
            return SyntexModels.FirstOrDefault(c => c.Id == id);
        }

        internal ISyntexModel GetCachedSyntexModel(string modelName)
        {
            return SyntexModels.FirstOrDefault(c => c.Name == modelName);
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

        internal void CacheSyntexModel(ISyntexModel model)
        {
            var existingModel = SyntexModels.FirstOrDefault(c => c.UniqueId == model.UniqueId);
            if (existingModel != null)
            {
                SyntexModels.Remove(existingModel);
            }

            SyntexModels.Add(model);
        }

        internal void CacheTerm(string key, Guid id, string label)
        {
            this.Terms.Add((key, id, label));
        }

        internal (string key, Guid id, string label) GetCachedTerm(string key)
        {
            return this.Terms.FirstOrDefault(t => t.key == key);
        }
    }
}