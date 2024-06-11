using Unity.Entities;
using Wallet.SystemGroups;
using Wallet.Utils;

namespace Wallet.Currencies.UI
{
    [UpdateInGroup(typeof(WalletEventsHandlerSystemsGroup))]
    public partial class CreateCurrencyViewSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            foreach (var (currencyId, _) in SystemAPI.Query<RefRO<CurrencyIdComponent>, RefRO<CreatedEventComponent>>())
            {
                EventBus.Push(new CreateCurrencyViewRequest(currencyId.ValueRO.Id));
            }
        }
    
    
    }
}