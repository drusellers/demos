using Feature.Services.TextFormatting;
using HtmlTags;

namespace FeatureTweak.Services.TextFormatting
{
    public class CustomerOverriddenFormattingService : IFormattingService
    {
        public void Format(HtmlTag htmlTag)
        {
            htmlTag.Style("color", "#0f0");
        }
    }
}
