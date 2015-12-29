using HtmlTags;

namespace Feature.Services.TextFormatting
{
    public class FormattingService : IFormattingService
    {
        public void Format(HtmlTag htmlTag)
        {
            htmlTag.Style("color", "#f00");
        }
    }
}