using Unity.Entities;

namespace Wallet.SystemGroups
{
    [CreateAfter(typeof(WalletEventsHandlerSystemsGroup))]
    [CreateAfter(typeof(ECSEventsConverterSystemsGroup))]
    [UpdateAfter(typeof(WalletEventsHandlerSystemsGroup))]
    [UpdateAfter(typeof(ECSEventsConverterSystemsGroup))]
    public partial class WalletEventsCleanerSystemsGroup : ComponentSystemGroup
    {
    }
}