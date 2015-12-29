using FubuMVC.Core;

namespace NewFeature
{
    public class NewFeatureFubuRegistry : FubuPackageRegistry
    {
        public NewFeatureFubuRegistry()
        {
            Actions.IncludeTypesNamed(n => n.EndsWith("Action"));
        }

    }
}