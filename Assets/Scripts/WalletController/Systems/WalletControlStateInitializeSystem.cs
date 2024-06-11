using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Wallet.Currencies;
using Wallet.Loading;
using Wallet.SystemGroups;

namespace Wallet.WalletController
{
    [UpdateInGroup(typeof(WalletSimulationSystemsGroup))]
    [BurstCompile]
    public partial struct WalletControlStateInitializeSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState systemState)
        {
            if (SystemAPI.TryGetSingletonEntity<CurrenciesLoadedEventComponent>(out _))
            {
                using var ecb = new EntityCommandBuffer(Allocator.Temp);
                var currentState = ecb.CreateEntity();
                var maxId = int.MinValue;
                var minId = int.MaxValue;
                foreach (var id in SystemAPI.Query<RefRO<CurrencyIdComponent>>())
                {
                    var currentId = id.ValueRO.Id;
                    if (currentId > maxId)
                    {
                        maxId = currentId;
                    }

                    if (currentId < minId)
                    {
                        minId = currentId;
                    }
                    
                }
                ecb.AddComponent(currentState, new CurrencyControlStateComponent(){ CurrentCurrencyId = minId, MinId = minId, MaxId = maxId});
                ecb.Playback(systemState.EntityManager);
            }
        }

    }
}