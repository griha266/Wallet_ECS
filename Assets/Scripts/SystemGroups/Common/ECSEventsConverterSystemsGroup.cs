using Unity.Entities;

namespace Wallet.SystemGroups
{
    [CreateAfter(typeof(UIEventsConverterSystemsGroup))]
    [UpdateAfter(typeof(UIEventsConverterSystemsGroup))]
    public partial class ECSEventsConverterSystemsGroup : ComponentSystemGroup
    {
    }
}
