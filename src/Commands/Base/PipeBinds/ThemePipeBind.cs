using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ThemePipeBind
    {
        private readonly string _name;

        public ThemePipeBind()
        {
            _name = string.Empty;
        }

        public ThemePipeBind(string name)
        {
            _name = name;
        }

        public ThemePipeBind(SPOTheme theme)
        {
            _name = theme.Name;
        }

        public string Name => _name;
    }
}