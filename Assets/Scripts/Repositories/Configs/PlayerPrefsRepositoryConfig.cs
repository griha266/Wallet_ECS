using UnityEngine;
using Wallet.Serialization;

namespace Wallet.Data
{
    [CreateAssetMenu(menuName = "Wallet/Repositories configs/Player Prefs", fileName = "PlayerPrefsRepositoryConfig",
        order = 0)]
    public class PlayerPrefsRepositoryConfig : RepositoryConfig
    {
        [SerializeField] public string Key;

        public override DataRepositoryBase<T> GetRepository<T>(int currentVersion, ISerializer serializer,
            IVersionResolver<T> versionResolver)
        {
            return new PlayerPrefsRepository<T>(Key, currentVersion, serializer, versionResolver);
        }
    }
}