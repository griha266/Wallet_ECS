using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Wallet.SystemGroups;

namespace Wallet.Currencies
{
    [UpdateInGroup(typeof(WalletEventsCleanerSystemsGroup))]
    [BurstCompile]
    public partial struct AmountChangeEventsCleanerSystem : ISystem
    {
        private EntityQuery _eventsToDelete;

        [BurstCompile]
        public void OnCreate(ref SystemState systemState)
        {
            _eventsToDelete = SystemAPI.QueryBuilder().WithAll<AmountChangedEventComponent>().Build();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState systemState)
        {
            if (!_eventsToDelete.IsEmpty)
            {
                using var ecb = new EntityCommandBuffer(Allocator.Temp);
                ecb.RemoveComponent<AmountChangedEventComponent>(_eventsToDelete, EntityQueryCaptureMode.AtPlayback);
                ecb.Playback(systemState.EntityManager);
            }
        }
    }
}