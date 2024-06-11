namespace Wallet.Currencies.UI
{
    public readonly struct CreateCurrencyViewRequest
    {
        public readonly int Id;

        public CreateCurrencyViewRequest(int id)
        {
            Id = id;
        }
    }
}