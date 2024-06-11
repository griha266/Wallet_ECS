using Unity.Entities;

namespace Wallet.SystemGroups
{
    [CreateBefore(typeof(WalletSimulationSystemsGroup))]
    [UpdateBefore(typeof(WalletSimulationSystemsGroup))]
    public partial class WalletInitializationSystemsGroup : ComponentSystemGroup { }
}
