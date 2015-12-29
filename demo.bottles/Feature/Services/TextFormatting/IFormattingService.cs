using HtmlTags;

namespace Feature.Services.TextFormatting
{
    public interface IFormattingService
    {
        void Format(HtmlTag htmlTag);
    }
}