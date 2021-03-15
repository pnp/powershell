using PnP.Core.Model.SharePoint;
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

        internal ISyntexModel GetSyntexModel()
        {
            if (syntexModel != null)
            {
                return syntexModel;
            }
            else if (syntexModelId > 0)
            {
                var ctx = PnPConnection.Current.PnPContext;
                var syntexContentCenter = ctx.Web.AsSyntexContentCenter();
                var models = syntexContentCenter.GetSyntexModels();
                return models.FirstOrDefault(p => p.Id == syntexModelId);
            }
            else if (!string.IsNullOrEmpty(syntexModelName))
            {
                var ctx = PnPConnection.Current.PnPContext;
                var syntexContentCenter = ctx.Web.AsSyntexContentCenter();
                var models = syntexContentCenter.GetSyntexModels();
                return models.FirstOrDefault(p => p.Name.Equals(syntexModelName, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                return null;
            }
        }
    }
}
