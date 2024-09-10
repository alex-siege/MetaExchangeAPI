namespace MetaExchangeAPI.Models
{
    public class ExecutionRequest
    {
        public string OrderType { get; set; } // "Buy" or "Sell"
        public decimal Amount { get; set; }   // Amount of BTC to Buy/Sell
    }
}
