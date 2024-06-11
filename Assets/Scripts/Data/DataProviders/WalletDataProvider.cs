using System.Threading;
using System.Threading.Tasks;

namespace Wallet.Data
{
    public class WalletDataProvider : IDataProvider<WalletSaveData>
    {
        private readonly DataRepositoryBase<WalletSaveData> _dataRepository;
        private readonly WalletSaveData _defaultData;

        public WalletDataProvider(DataRepositoryBase<WalletSaveData> dataRepository, WalletSaveData defaultData)
        {
            _defaultData = defaultData;
            _dataRepository = dataRepository;
        }

        public Task<WalletSaveData> Get(CancellationToken cancellationToken)
        {
            return _dataRepository.Load(_defaultData, cancellationToken);
        }

        public Task<bool> Set(WalletSaveData value, CancellationToken cancellationToken)
        {
            return _dataRepository.Save(value, cancellationToken);
        }
    }
}