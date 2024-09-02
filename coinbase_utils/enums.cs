namespace coinbase_enum
{
    public enum cbChannels
    {
        NONE = 0,
        heartbeats,
        candles,
        status,
        ticker,
        ticker_batch,
        level2,
        user,
        market_trades
    }
    public enum logType
    {
        NONE = 0,
        INFO,
        WARNING,
        ERROR,
        CRITICAL
    }
}
