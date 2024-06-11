using Unity.Entities;

namespace Wallet.WalletController
{
    public struct CurrencyControlStateComponent : IComponentData
    {
        public int CurrentCurrencyId;
        public int MaxId;
        public int MinId;
    }
}