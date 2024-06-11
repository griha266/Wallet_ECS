using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Wallet.SystemGroups;

namespace Wallet.Loading
{
    [UpdateInGroup(typeof(WalletEventsCleanerSystemsGroup))]
    [BurstCompile]
    public partial struct CurrenciesLoadedEventCleanerSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (SystemAPI.TryGetSingletonEntity<CurrenciesLoadedEventComponent>(out var entity))
            {
                using var ecb = new EntityCommandBuffer(Allocator.Temp);
                ecb.DestroyEntity(entity);
                ecb.Playback(state.EntityManager);
            }
        }
    }
}