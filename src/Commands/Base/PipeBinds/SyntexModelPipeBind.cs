using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Model;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class SyntexModelPipeBind
    {
        private readonly int syntexModelId;
        private readonly string syntexModelName;
        private readonly ISyntexModel syntexModel;

        public SyntexModelPipeBind(ISyntexModel syntexModel)
        {
            this.syntexModel = syntexModel;
        }

        public SyntexModelPipeBind(string name)
        {
            this.syntexModelName = name;
        }

        public SyntexModelPipeBind(int id)
        {
            this.syntexModelId = id;
        }

        public ISyntexModel SyntexModel => syntexModel;

        public string Name => syntexModelName;

        public int Id => syntexModelId;

        public override string ToString() => Name;

        internal ISyntexModel GetSyntexModel(PnPConnection connection)
        {
            if (syntexModel != null)
            {
                return syntexModel;
            }
            else if (syntexModelId > 0)
            {
                var ctx = connection.PnPContext;
                var syntexContentCenter = ctx.Web.AsSyntexContentCenter();
                var models = syntexContentCenter.GetSyntexModels();
                return models.FirstOrDefault(p => p.Id == syntexModelId);
            }
            else if (!string.IsNullOrEmpty(syntexModelName))
            {
                var ctx = connection.PnPContext;
                var syntexContentCenter = ctx.Web.AsSyntexContentCenter();
                var models = syntexContentCenter.GetSyntexModels();
                return models.FirstOrDefault(p => p.Name.Equals(syntexModelName, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                return null;
            }
        }

        internal ISyntexModel GetSyntexModel(PnPBatch batch, PnPConnection connection)
        {
            if (syntexModel != null)
            {
                return syntexModel;
            }
            else if (syntexModelId > 0)
            {
                var batchedSyntexModel = batch.GetCachedSyntexModel(syntexModelId);
                if (batchedSyntexModel != null)
                {
                    return batchedSyntexModel;
                }

                var ctx = connection.PnPContext;
                var syntexContentCenter = ctx.Web.AsSyntexContentCenter();
                var models = syntexContentCenter.GetSyntexModels();
                var syntexModel = models.FirstOrDefault(p => p.Id == syntexModelId);
                batch.CacheSyntexModel(syntexModel);
                return syntexModel;
            }
            else if (!string.IsNullOrEmpty(syntexModelName))
            {
                var batchedSyntexModel = batch.GetCachedSyntexModel(syntexModelName);
                if (batchedSyntexModel != null)
                {
                    return batchedSyntexModel;
                }

                var ctx = connection.PnPContext;
                var syntexContentCenter = ctx.Web.AsSyntexContentCenter();
                var models = syntexContentCenter.GetSyntexModels();
                var syntexModel = models.FirstOrDefault(p => p.Name.Equals(syntexModelName, StringComparison.InvariantCultureIgnoreCase));
                batch.CacheSyntexModel(syntexModel);
                return syntexModel;
            }
            else
            {
                return null;
            }
        }
    }
}
