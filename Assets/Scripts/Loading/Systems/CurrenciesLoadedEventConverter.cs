using Unity.Entities;
using Wallet.SystemGroups;
using Wallet.Utils;

namespace Wallet.Loading
{
    [UpdateInGroup(typeof(ECSEventsConverterSystemsGroup))]
    public partial class CurrenciesLoadedEventConverter : SystemBase
    {
        protected override void OnUpdate()
        {
            if (SystemAPI.TryGetSingletonEntity<CurrenciesLoadedEventComponent>(out _))
            {
                EventBus.Push<CurrenciesLoadedEvent>();
            }
        }
    }
}