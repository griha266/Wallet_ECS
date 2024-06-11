using System;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Serialization;
using Wallet.Utils;


namespace Wallet.Data
{
    public abstract class DataRepositoryBase<T>
    {
        private readonly int _currentDataVersion;
        private readonly ISerializer _serializer;
        private readonly IVersionResolver<T> _versionResolver;

        protected DataRepositoryBase(int currentDataVersion, ISerializer serializer,
            IVersionResolver<T> versionResolver)
        {
            _currentDataVersion = currentDataVersion;
            _serializer = serializer;
            _versionResolver = versionResolver;
        }

        public abstract void Clear();

        public async Task<T> Load(T defaultData, CancellationToken cancellationToken)
        {
            Result<string> rawData;

            try
            {
                rawData = await GetRawData(cancellationToken);
            }
            catch (Exception e)
            {
                rawData = Result.Error<string>(e);
            }

            var withVersion = rawData
                .MapMany(str => _serializer.Deserialize<DataWithVersion<T>>(str));

            var result = withVersion
                .Map(data => _versionResolver.ResolveVersion(data))
                .LogErrorAndGetDefault(defaultData);

            return result;
        }

        public async Task<bool> Save(T data, CancellationToken cancellationToken)
        {
            var withVersionData = new DataWithVersion<T>() { Data = data, Version = _currentDataVersion };

            var serialized = _serializer.Serialize(withVersionData);

            var rawDataResult = await serialized.MapManyAsync(SetRawData, cancellationToken);

            var result = rawDataResult
                .Map(_ => true)
                .LogErrorAndGetDefault(false);

            return result;
        }

        protected abstract Task<Result<string>> GetRawData(CancellationToken cancellationToken);
        protected abstract Task<Result<Unit>> SetRawData(string rawData, CancellationToken cancellationToken);
    }
}