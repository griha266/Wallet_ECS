using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Wallet.Serialization;
using Wallet.Utils;

namespace Wallet.Data
{
    public class FileRepository<T> : DataRepositoryBase<T>
    {
        private readonly string _filePath;
        private readonly Encoding _encoding;

        public FileRepository(string filePath, Encoding encoding, int currentDataVersion, ISerializer serializer,
            IVersionResolver<T> versionResolver) : base(currentDataVersion, serializer, versionResolver)
        {
            _filePath = filePath;
            _encoding = encoding;
        }

        public override void Clear()
        {
            var fullPath = GetFullPath();
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        protected override async Task<Result<string>> GetRawData(CancellationToken cancellationToken)
        {
            var fullPath = GetFullPath();
            if (!File.Exists(fullPath))
            {
                return Result.Error<string>(
                    new FileNotFoundException($"Cannot find file {_filePath} at path {fullPath}"));
            }

            var allContent = await File.ReadAllTextAsync(fullPath, _encoding, cancellationToken);
            return Result.Ok(allContent);
        }

        protected override async Task<Result<Unit>> SetRawData(string rawData, CancellationToken cancellationToken)
        {
            try
            {
                var fullPath = GetFullPath();
                await File.WriteAllTextAsync(fullPath, rawData, cancellationToken);
                return Result.Ok(Unit.Default);
            }
            catch (Exception e)
            {
                return Result.Error<Unit>(e);
            }
        }

        private string GetFullPath() => Path.Combine(Application.persistentDataPath, _filePath);
    }
}