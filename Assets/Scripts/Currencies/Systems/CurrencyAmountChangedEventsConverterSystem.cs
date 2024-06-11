using Unity.Entities;
using Wallet.SystemGroups;
using Wallet.Utils;

namespace Wallet.Currencies
{
    [UpdateInGroup(typeof(ECSEventsConverterSystemsGroup))]
    public partial class CurrencyAmountChangedEventsConverterSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            foreach (var (id, amount, _) in SystemAPI
                         .Query<RefRO<CurrencyIdComponent>, RefRO<AmountComponent>, RefRO<AmountChangedEventComponent>>())
            {
                EventBus.Push(new CurrencyAmountChangedEvent(id.ValueRO.Id, amount.ValueRO.Value));
            }
        }
    }
}