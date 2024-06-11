using Unity.Entities;

namespace Wallet.WalletController
{
    public struct ChangeCurrencyIdRequestComponent : IComponentData
    {
        public bool IsIncrease;
    }
}