namespace MetalApi
{
    public class QuandlApiOptions
    {
        public const string QuandlApi = nameof(QuandlApi);
        public string Url { get; set; }
        public string GoldPricesUrl { get; set; }
        public string SilverPricesUrl { get; set; }
    }
}
