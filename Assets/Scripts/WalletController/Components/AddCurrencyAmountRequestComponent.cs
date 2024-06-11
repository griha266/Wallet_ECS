using Unity.Entities;

namespace Wallet.WalletController
{
    public struct AddCurrencyAmountRequestComponent : IComponentData
    {
        public int AddAmount;
    }
}