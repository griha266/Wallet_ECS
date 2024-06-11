using Unity.Entities;

namespace Wallet.Currencies
{
    public struct AmountComponent : IComponentData
    {
        public int Value;
    }
}