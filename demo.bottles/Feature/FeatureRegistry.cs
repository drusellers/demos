using BaseProduct.BizznessCode.Services.DogNames;
using Feature.Services.TextFormatting;
using StructureMap.Configuration.DSL;

namespace Feature
{
    public class FeatureRegistry : Registry
    {
         public FeatureRegistry()
         {
             For<IDogNameRepository>().Use<OverrideService>();
             For<IFormattingService>().Use<FormattingService>();
         }
    }
}