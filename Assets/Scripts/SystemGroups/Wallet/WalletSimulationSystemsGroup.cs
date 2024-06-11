using Unity.Entities;

namespace Wallet.SystemGroups
{
    [CreateAfter(typeof(UIEventsConverterSystemsGroup))]
    [UpdateAfter(typeof(UIEventsConverterSystemsGroup))]
    [CreateBefore(typeof(ECSEventsConverterSystemsGroup))]
    [UpdateBefore(typeof(ECSEventsConverterSystemsGroup))]
    public partial class WalletSimulationSystemsGroup : ComponentSystemGroup
    {
    }
}
