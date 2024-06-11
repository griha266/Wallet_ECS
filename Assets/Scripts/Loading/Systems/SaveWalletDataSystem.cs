using System.Threading;
using Wallet.Currencies;
using Unity.Collections;
using Unity.Entities;
using Wallet.Data;
using Wallet.SystemGroups;

namespace Wallet.Loading
{
    [UpdateInGroup(typeof(WalletEventsHandlerSystemsGroup))]
    public partial class SaveWalletDataSystem : SystemBase
    {
        private WalletDataProvider _walletDataProvider;
        private EntityQuery _currencyChangesQuery;
        private EntityQuery _allCurrenciesQuery;
        private CancellationTokenSource _cancellationTokenSource;

        protected override void OnCreate()
        {
            _currencyChangesQuery = SystemAPI.QueryBuilder()
                .WithAll<AmountChangedEventComponent, CurrencyIdComponent, AmountComponent>().Build();
            _allCurrenciesQuery = SystemAPI.QueryBuilder().WithAll<AmountComponent, CurrencyIdComponent>().Build();
        }

        protected override void OnStartRunning()
        {
            _walletDataProvider = SystemAPI
                .ManagedAPI
                .GetSingleton<WalletDataProviderReferenceComponent>()
                .Value;
        }

        protected override void OnDestroy()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
        }

        protected override void OnUpdate()
        {
            if (_currencyChangesQuery.IsEmpty)
            {
                return;
            }

            using var currencies = _allCurrenciesQuery.ToEntityArray(Allocator.Temp);
            var saveData = new CurrencySaveData[currencies.Length];
            for (int i = 0; i < currencies.Length; i++)
            {
                var currency = currencies[i];
                var id = SystemAPI.GetComponent<CurrencyIdComponent>(currency).Id;
                var amount = SystemAPI.GetComponent<AmountComponent>(currency);
                saveData[i] = new CurrencySaveData() { Id = id, Amount = amount.Value };
            }

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            _walletDataProvider
                .Set(new WalletSaveData() { Currencies = saveData }, _cancellationTokenSource.Token)
                .ContinueWith(_ =>
                {
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                });
        }
    }
}