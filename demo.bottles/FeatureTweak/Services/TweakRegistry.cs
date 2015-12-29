using Feature.Services.TextFormatting;
using FeatureTweak.Services.TextFormatting;
using StructureMap.Configuration.DSL;

namespace FeatureTweak.Services
{
    public class TweakRegistry :Registry
    {
         public TweakRegistry()
         {
             For<IFormattingService>().Use<CustomerOverriddenFormattingService>();
         }
    }
}