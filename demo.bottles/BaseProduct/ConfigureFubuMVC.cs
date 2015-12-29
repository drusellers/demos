using BaseProduct.Actions;
using FubuMVC.Core;
using FubuMVC.WebForms;

namespace BaseProduct
{
    public class ConfigureFubuMVC : FubuRegistry
    {
        public ConfigureFubuMVC()
        {
            // This line turns on the basic diagnostics and request tracing
            IncludeDiagnostics(true);

            // All public methods from concrete classes ending in "Action"
            // in this assembly are assumed to be action methods
            Actions.IncludeTypesNamed(name => name.EndsWith("Action"));

            // Policies
            Routes
                .IgnoreControllerNamesEntirely()
                .IgnoreMethodSuffix("Html")
                .RootAtAssemblyNamespace()
                .HomeIs<DemoAction>(x=>x.Execute(null));

            Import<WebFormsEngine>();

            // Match views to action methods by matching
            // on model type, view name, and namespace
            Views.TryToAttachWithDefaultConventions();
        }
    }
}