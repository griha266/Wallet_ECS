using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Wallet.Currencies;
using Wallet.SystemGroups;

namespace Wallet.WalletController
{
    [UpdateInGroup(typeof(WalletSimulationSystemsGroup))]
    [BurstCompile]
    public partial struct ChangeCurrentControlledCurrencyAmountSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CurrencyControlStateComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState systemState)
        {
            var controlEntity = SystemAPI.GetSingletonEntity<CurrencyControlStateComponent>();
            var currentId = SystemAPI.GetComponent<CurrencyControlStateComponent>(controlEntity).CurrentCurrencyId;
            var selectedCurrency = Entity.Null;
            RefRW<AmountComponent> selectedCurrencyAmount = default;
            foreach (var (id, amount, currency) in SystemAPI.Query<RefRO<CurrencyIdComponent>, RefRW<AmountComponent>>().WithEntityAccess())
            {
                if (currentId == id.ValueRO.Id)
                {
                    selectedCurrency = currency;
                    selectedCurrencyAmount = amount;
                    break;
                }
            }

            using var ecb = new EntityCommandBuffer(Allocator.Temp);
            if (SystemAPI.HasComponent<AddCurrencyAmountRequestComponent>(controlEntity))
            {
                var request = SystemAPI.GetComponentRO<AddCurrencyAmountRequestComponent>(controlEntity);
                selectedCurrencyAmount.ValueRW.Value += request.ValueRO.AddAmount;
                ecb.RemoveComponent<AddCurrencyAmountRequestComponent>(controlEntity);
            }

            if (SystemAPI.HasComponent<ClearCurrencyRequestComponent>(controlEntity))
            {
                selectedCurrencyAmount.ValueRW.Value = 0;
                ecb.RemoveComponent<ClearCurrencyRequestComponent>(controlEntity);
            }

            if (!ecb.IsEmpty)
            {
                ecb.AddComponent<AmountChangedEventComponent>(selectedCurrency);
                ecb.Playback(systemState.EntityManager);
            }
        }
    }
}