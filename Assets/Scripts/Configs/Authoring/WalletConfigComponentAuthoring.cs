using Unity.Entities;
using UnityEngine;

namespace Wallet.Configs
{
    public class WalletConfigAuthoring : MonoBehaviour
    {
        [SerializeField] private WalletConfig walletConfig;
    
        public class WalletConfigComponentBaker : Baker<WalletConfigAuthoring>
        {
            public override void Bake(WalletConfigAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponentObject(entity, new WalletConfigComponent { Currencies = authoring.walletConfig.ToLookup() });
            }
        }
    }
}