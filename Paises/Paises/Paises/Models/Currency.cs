namespace Paises.Models
{
    using Newtonsoft.Json;

    public class Currency
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "ymbol")]
        public string Symbol { get; set; }
    }
}
