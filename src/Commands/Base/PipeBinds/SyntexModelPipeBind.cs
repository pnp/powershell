namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class SyntexModelPipeBind
    {
        private readonly int syntexModelId;
        private readonly string syntexModelName;

        public SyntexModelPipeBind(string name)
        {
            this.syntexModelName = name;
        }

        public SyntexModelPipeBind(int id)
        {
            this.syntexModelId = id;
        }

        public string Name => syntexModelName;

        public int Id => syntexModelId;

        public override string ToString() => Name;
    }
}
