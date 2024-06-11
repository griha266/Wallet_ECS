using System.Threading;
using Wallet.Currencies;
using Unity.Collections;
using Unity.Entities;
using Wallet.Data;
using Wallet.SystemGroups;

namespace Wallet.Loading
{
    [UpdateInGroup(typeof(WalletInitializationSystemsGroup))]
    [UpdateAfter(typeof(WalletDataProviderCreationSystem))]
    [CreateAfter(typeof(WalletDataProviderCreationSystem))]
    public partial class LoadWalletDataSystem : SystemBase
    {
        private WalletDataProvider _walletDataProvider;
        private WalletSaveData _currentSaveData;
        private CancellationTokenSource _cancellationTokenSource;

        protected override void OnCreate()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected override void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        protected override void OnStartRunning()
        {
            _walletDataProvider = SystemAPI
                .ManagedAPI
                .GetSingleton<WalletDataProviderReferenceComponent>()
                .Value;
            LoadWalletData();
        }

        private async void LoadWalletData()
        {
            _currentSaveData = await _walletDataProvider.Get(_cancellationTokenSource.Token);
            using var ecb = new EntityCommandBuffer(Allocator.Temp);
            for (int i = 0; i < _currentSaveData.Currencies.Length; i++)
            {
                var currencyData = _currentSaveData.Currencies[i];
                var currencyEntity = ecb.CreateEntity();
                ecb.AddComponent(currencyEntity, new CurrencyIdComponent(){Id = currencyData.Id});
                ecb.AddComponent(currencyEntity, new AmountComponent(){Value = currencyData.Amount});
                ecb.AddComponent<AmountChangedEventComponent>(currencyEntity);
                ecb.AddComponent<CreatedEventComponent>(currencyEntity);
            }

            var currenciesLoadedEventEntity = ecb.CreateEntity();
            ecb.AddComponent<CurrenciesLoadedEventComponent>(currenciesLoadedEventEntity);
            ecb.Playback(EntityManager);
        }

        protected override void OnUpdate()
        {
        }
    }
}
