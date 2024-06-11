using Unity.Collections;
using Unity.Entities;
using Wallet.SystemGroups;
using Wallet.Utils;

namespace Wallet.WalletController.UI
{
    [UpdateInGroup(typeof(UIEventsConverterSystemsGroup))]
    public partial class WalletControllerUIEventsConverterSystem : SystemBase
    {
        protected override void OnCreate()
        {
            EventBus.GetEntry<SetNextCurrencyIndexRequest>().Event += OnSetNextCurrencyIndexRequest;
            EventBus.GetEntry<SetPreviousCurrencyIndexRequest>().Event += OnSetPreviousCurrencyIndexRequest;
            EventBus.GetEntry<IncreaseCurrentCurrencyRequest>().Event += OnIncreaseCurrentCurrencyRequest;
            EventBus.GetEntry<ClearCurrentCurrencyRequest>().Event += OnClearCurrentCurrencyRequest;
        }

        private void OnSetNextCurrencyIndexRequest(SetNextCurrencyIndexRequest obj)
        {
            var controlEntity = SystemAPI.GetSingletonEntity<CurrencyControlStateComponent>();
            using var ecb = new EntityCommandBuffer(Allocator.Temp);
            ecb.AddComponent(controlEntity, new ChangeCurrencyIdRequestComponent()
            {
                IsIncrease = true
            });
            ecb.Playback(EntityManager);
        }

        private void OnSetPreviousCurrencyIndexRequest(SetPreviousCurrencyIndexRequest obj)
        {
            var controlEntity = SystemAPI.GetSingletonEntity<CurrencyControlStateComponent>();
            using var ecb = new EntityCommandBuffer(Allocator.Temp);
            ecb.AddComponent(controlEntity, new ChangeCurrencyIdRequestComponent()
            {
                IsIncrease = false
            });
            ecb.Playback(EntityManager);
        }

        private void OnIncreaseCurrentCurrencyRequest(IncreaseCurrentCurrencyRequest obj)
        {
            var controlEntity = SystemAPI.GetSingletonEntity<CurrencyControlStateComponent>();
            using var ecb = new EntityCommandBuffer(Allocator.Temp);
            ecb.AddComponent(controlEntity, new AddCurrencyAmountRequestComponent()
            {
                AddAmount = 1
            });
            ecb.Playback(EntityManager);
        }

        private void OnClearCurrentCurrencyRequest(ClearCurrentCurrencyRequest obj)
        {
            var controlEntity = SystemAPI.GetSingletonEntity<CurrencyControlStateComponent>();
            using var ecb = new EntityCommandBuffer(Allocator.Temp);
            ecb.AddComponent(controlEntity, new ClearCurrencyRequestComponent());
            ecb.Playback(EntityManager);
        }

        protected override void OnDestroy()
        {
            EventBus.GetEntry<SetNextCurrencyIndexRequest>().Event -= OnSetNextCurrencyIndexRequest;
            EventBus.GetEntry<SetPreviousCurrencyIndexRequest>().Event -= OnSetPreviousCurrencyIndexRequest;
            EventBus.GetEntry<IncreaseCurrentCurrencyRequest>().Event -= OnIncreaseCurrentCurrencyRequest;
            EventBus.GetEntry<ClearCurrentCurrencyRequest>().Event -= OnClearCurrentCurrencyRequest;
        }

        protected override void OnUpdate()
        {
        }
    }
}