using Unity.Entities;

namespace Wallet.Serialization
{
    public class SerializerReference : IComponentData
    {
        public ISerializer Serializer;
    }
}