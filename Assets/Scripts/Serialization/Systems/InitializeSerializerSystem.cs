using Unity.Collections;
using Unity.Entities;
using Wallet.SystemGroups;

namespace Wallet.Serialization
{
    [UpdateInGroup(typeof(WalletInitializationSystemsGroup))]
    public partial class InitializeSerializerSystem : SystemBase
    {
        protected override void OnStartRunning()
        {
            var serializer = new JsonSerializer();
            using var ecb = new EntityCommandBuffer(Allocator.Temp);
            var serializerEntity = ecb.CreateEntity();
            ecb.AddComponent(serializerEntity, new SerializerReference() { Serializer = serializer });
            ecb.Playback(EntityManager);
        }

        protected override void OnUpdate()
        {
        }
    }
}