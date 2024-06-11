using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Wallet.SystemGroups;

namespace Wallet.WalletController
{
    [UpdateInGroup(typeof(WalletEventsCleanerSystemsGroup))]
    [BurstCompile]
    public partial struct WalletControlStateEventsCleanerSystem : ISystem
    {
        
        [BurstCompile]
        public void OnUpdate(ref SystemState systemState)
        {
            using var ecb = new EntityCommandBuffer(Allocator.Temp);
        
            foreach (var (_, entity) in SystemAPI.Query<RefRO<ChangeCurrencyIdRequestComponent>>().WithEntityAccess())
            {
                ecb.RemoveComponent<ChangeCurrencyIdRequestComponent>(entity);
            }
        
            foreach (var (_, entity) in SystemAPI.Query<RefRO<AddCurrencyAmountRequestComponent>>().WithEntityAccess())
            {
                ecb.RemoveComponent<AddCurrencyAmountRequestComponent>(entity);
            }
        
            foreach (var (_, entity) in SystemAPI.Query<RefRO<ChangeCurrencyIdRequestComponent>>().WithEntityAccess())
            {
                ecb.RemoveComponent<ChangeCurrencyIdRequestComponent>(entity);
            }

            if (!ecb.IsEmpty)
            {
                ecb.Playback(systemState.EntityManager);
            }
        }
    }
}