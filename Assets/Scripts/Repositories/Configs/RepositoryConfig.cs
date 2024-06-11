using UnityEngine;
using Wallet.Serialization;

namespace Wallet.Data
{
    public abstract class RepositoryConfig : ScriptableObject
    {
        public abstract DataRepositoryBase<T> GetRepository<T>(int currentVersion, ISerializer serializer,
            IVersionResolver<T> versionResolver);
    }
}