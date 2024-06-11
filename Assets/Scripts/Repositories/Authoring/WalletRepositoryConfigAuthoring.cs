using Unity.Entities;
using UnityEngine;

namespace Wallet.Data
{
    public class WalletRepositoryConfigAuthoring : MonoBehaviour
    {
        [SerializeField] private RepositoryConfig repositoryConfig;

        public class RepositoryConfigReferenceBaker : Baker<WalletRepositoryConfigAuthoring>
        {
            public override void Bake(WalletRepositoryConfigAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponentObject(entity, new RepositoryConfigReference { Value = authoring.repositoryConfig });
            }
        }
    }
}