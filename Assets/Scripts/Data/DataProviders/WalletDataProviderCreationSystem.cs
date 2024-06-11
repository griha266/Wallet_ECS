using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Wallet.Configs;
using Wallet.Serialization;
using Wallet.SystemGroups;

namespace Wallet.Data
{
    [UpdateInGroup(typeof(WalletInitializationSystemsGroup))]
    [CreateAfter(typeof(InitializeSerializerSystem))]
    [UpdateAfter(typeof(InitializeSerializerSystem))]
    public partial class WalletDataProviderCreationSystem : SystemBase
    {
        protected override void OnStartRunning()
        {
            var serializer = SystemAPI.ManagedAPI.GetSingleton<SerializerReference>().Serializer;
            var walletVersionResolver = new WalletDataVersionResolver();
            var repositoryConfig = SystemAPI.ManagedAPI.GetSingleton<RepositoryConfigReference>().Value;
            var dataRepository = repositoryConfig.GetRepository(WalletSaveData.DataVersion, serializer, walletVersionResolver);
            var walletConfig = SystemAPI.ManagedAPI.GetSingleton<WalletConfigComponent>();
            var walletDataProvider = new WalletDataProvider(dataRepository, GetDefaultData(walletConfig.Currencies));
            
            using var ecb = new EntityCommandBuffer(Allocator.Temp);
            var walletEntity = ecb.CreateEntity();
            ecb.AddComponent(walletEntity, new WalletDataProviderReferenceComponent(){Value = walletDataProvider});
            ecb.Playback(EntityManager);
        }

        private static WalletSaveData GetDefaultData(IReadOnlyDictionary<int, WalletConfig.Entry> walletConfig)
        {
            var currenciesCount = walletConfig.Count;
            var currenciesSaveData = new CurrencySaveData[currenciesCount];
            var index = 0;
            foreach (var currencyConfig in walletConfig.Values)
            {
                currenciesSaveData[index] = new CurrencySaveData()
                {
                    Amount = currencyConfig.InitialAmount,
                    Id = currencyConfig.Currnecy.Id
                };
                index++;
            }

            return new WalletSaveData() { Currencies = currenciesSaveData };
        }
        protected override void OnUpdate()
        {
        }
    }
}