using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    internal class MultilingualHelper
    {
        internal static void EnsureMultilingual(PnPConnection connection, List<int> translationLanguageCodes)
        {
            try
            {
                var web = connection.Context.Web;
                FeatureCollection featureCollection = web.Features;
                IEnumerable<Feature> query = connection.Context.LoadQuery(featureCollection.IncludeWithDefaultProperties(new Expression<Func<Feature, object>>[] { f => f.DisplayName }));
                connection.Context.ExecuteQueryRetry();

                var f = query.Where(q => q.DefinitionId == new Guid("24611c05-ee19-45da-955f-6602264abaf8")).FirstOrDefault();
                if (f == null)
                {
                    // activate feature
                    web.ActivateFeature(new Guid("24611c05-ee19-45da-955f-6602264abaf8"));
                }

                web.EnsureProperties(w => w.SupportedUILanguageIds);
                bool updated = false;
                foreach (var languageCode in translationLanguageCodes)
                {
                    if (!web.SupportedUILanguageIds.Contains(languageCode))
                    {
                        web.SupportedUILanguageIds.ToList().Add(languageCode);
                        updated = true;
                    }
                }
                if (updated)
                {
                    connection.Context.ExecuteQueryRetry();
                }
            }
            catch { }
        }
    }
}
