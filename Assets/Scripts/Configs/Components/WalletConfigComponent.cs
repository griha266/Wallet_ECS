using System.Collections.Generic;
using Unity.Entities;

namespace Wallet.Configs
{
    public class WalletConfigComponent : IComponentData
    {
        public IReadOnlyDictionary<int, WalletConfig.Entry> Currencies;
    }
}