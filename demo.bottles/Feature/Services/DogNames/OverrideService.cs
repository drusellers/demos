using BaseProduct.BizznessCode.Services.DogNames;
using Feature.Services.TextFormatting;
using HtmlTags;

namespace Feature
{
    public class OverrideService : IDogNameRepository
    {
        private readonly IFormattingService _service;

        public OverrideService(IFormattingService service)
        {
            _service = service;
        }

        public string[] Names()
        {
            var x = new HtmlTag("h2");
            x.Text("Roxy");
            _service.Format(x);
            return new[]{x.ToString()};
        }
    }
}
