using System.Text.Json.Serialization;

public class ContractStatModel
    {
        [JsonPropertyName("time")]
        public Int64 Time { get; set; }

        [JsonPropertyName("contract")]
        public string? Contract { get; set; }

        [JsonPropertyName("mark_price")]
        public decimal? MarkPrice { get; set; }

        [JsonPropertyName("lsr_taker")]
        public decimal? LsrTaker { get; set; }

        [JsonPropertyName("lsr_account")]
        public decimal? LsrAccount { get; set; }

        [JsonPropertyName("long_liq_size")]
        public decimal? LongLiqSize { get; set; }

        [JsonPropertyName("long_liq_amount")]
        public decimal? LongLiqAmount { get; set; }

        [JsonPropertyName("long_liq_usd")]
        public decimal? LongLiqUsd { get; set; }

        [JsonPropertyName("short_liq_size")]
        public decimal? ShortLiqSize { get; set; }

        [JsonPropertyName("short_liq_amount")]
        public decimal? ShortLiqAmount { get; set; }

        [JsonPropertyName("short_liq_usd")]
        public decimal? ShortLiqUsd { get; set; }

        [JsonPropertyName("open_interest")]
        public decimal? OpenInterest { get; set; }

        [JsonPropertyName("open_interest_usd")]
        public decimal? OpenInterestUsd { get; set; }

        [JsonPropertyName("top_lsr_account")]
        public decimal? TopLsrAccount { get; set; }

        [JsonPropertyName("top_lsr_size")]
        public decimal? TopLsrSize { get; set; }
    }