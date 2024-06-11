using Unity.Entities;

namespace Wallet.SystemGroups
{
    [CreateAfter(typeof(WalletSimulationSystemsGroup))]
    [UpdateAfter(typeof(WalletSimulationSystemsGroup))]
    [CreateBefore(typeof(ECSEventsConverterSystemsGroup))]
    [UpdateBefore(typeof(ECSEventsConverterSystemsGroup))]
    public partial class WalletEventsHandlerSystemsGroup : ComponentSystemGroup
    {
    }
}