using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Wallet.Serialization;
using Wallet.Utils;

namespace Wallet.Data
{
    public class PlayerPrefsRepository<T> : DataRepositoryBase<T>
    {
        private readonly string _key;

        public PlayerPrefsRepository(string key, int currentDataVersion, ISerializer serializer,
            IVersionResolver<T> versionResolver) : base(currentDataVersion, serializer, versionResolver)
        {
            _key = key;
        }

        public override void Clear()
        {
            if (PlayerPrefs.HasKey(_key))
            {
                PlayerPrefs.DeleteKey(_key);
            }
        }

        protected override Task<Result<string>> GetRawData(CancellationToken cancellationToken)
        {
            if (PlayerPrefs.HasKey(_key))
            {
                var rawData = PlayerPrefs.GetString(_key);
                return Task.FromResult(Result.Ok(rawData));
            }

            return Task.FromResult(
                Result.Error<string>(new KeyNotFoundException($"Cannot find key {_key} in Player prefs")));
        }

        protected override Task<Result<Unit>> SetRawData(string rawData, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult(Result.Error<Unit>(new TaskCanceledException("Save data was cancelled")));
            }

            PlayerPrefs.SetString(_key, rawData);
            return Task.FromResult(Result.Ok());
        }
    }
}