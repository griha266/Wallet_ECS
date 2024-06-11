namespace Wallet.Currencies
{
    public readonly struct CurrencyAmountChangedEvent
    {
        public readonly int Id;
        public readonly int NewAmount;

        public CurrencyAmountChangedEvent(int id, int newAmount)
        {
            Id = id;
            NewAmount = newAmount;
        }
    }
}