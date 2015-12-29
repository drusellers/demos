using FubuMVC.Core;

namespace ReplaceView
{
    public class ReplaceViewFubuRegistry : FubuPackageRegistry
    {
        public ReplaceViewFubuRegistry()
        {
            Actions.IncludeTypesNamed(n => n.EndsWith("Action"));
        }

    }
}