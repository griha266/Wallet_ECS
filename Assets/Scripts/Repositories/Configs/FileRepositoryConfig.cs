using System.Text;
using UnityEngine;
using Wallet.Serialization;

namespace Wallet.Data
{
    [CreateAssetMenu(menuName = "Wallet/Repositories configs/File", fileName = "FileRepositoryConfig", order = 0)]
    public class FileRepositoryConfig : RepositoryConfig
    {
        [SerializeField] public string FilePath;

        public override DataRepositoryBase<T> GetRepository<T>(int currentVersion, ISerializer serializer, IVersionResolver<T> versionResolver)
        {
            return new FileRepository<T>(FilePath, Encoding.Default, currentVersion, serializer,
                versionResolver);
        }
    }
}