using Newtonsoft.Json;

namespace BlockChainApp.Models
{
    public class BitCoin
    {
        public string Currency { get; set; }

        [JsonProperty("15m")]
        public decimal PeriodSetting { get; set; }

        public decimal Last { get; set; }

        public decimal Buy { get; set; }

        public decimal Sell { get; set; }

        public string Symbol { get; set; }
    }
}
