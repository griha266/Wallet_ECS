using Unity.Entities;

namespace Wallet.Data
{
    public class WalletDataProviderReferenceComponent : IComponentData
    {
        public WalletDataProvider Value;
    }
}