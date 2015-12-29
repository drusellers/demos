namespace BaseProduct.BizznessCode.Services.DogNames
{
    public class DogNameRepository : IDogNameRepository
    {
        public string[] Names()
        {
            return new[]{"roxy"};
        }
    }
}