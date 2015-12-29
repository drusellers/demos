using BaseProduct.BizznessCode.Services.DogNames;

namespace BaseProduct.Actions
{
    public class DemoAction
    {
        private readonly IDogNameRepository _service;

        public DemoAction(IDogNameRepository service)
        {
            _service = service;
        }

        public DemoView Execute(ViewTheDemo input)
        {
            var names = _service.Names();

            return new DemoView
                       {
                           Names = names
                       };
        }
    }

    public class ViewTheDemo
    {
    }

    public class DemoView
    {
        public string[] Names { get; set; }
    }
}