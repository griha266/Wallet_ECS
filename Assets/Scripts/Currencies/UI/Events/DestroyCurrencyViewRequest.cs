namespace Wallet.Currencies.UI
{
    public readonly struct DestroyCurrencyViewRequest
    {
        public readonly int Id;

        public DestroyCurrencyViewRequest(int id)
        {
            Id = id;
        }
    }
}