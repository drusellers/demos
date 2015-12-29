using FubuMVC.Core;

namespace ReplaceAction
{
    public class ReplaceActionFubuRegistry : FubuPackageRegistry
    {
        public ReplaceActionFubuRegistry()
        {
            Actions.IncludeTypesNamed(n => n.EndsWith("Action"));
        }

    }
}