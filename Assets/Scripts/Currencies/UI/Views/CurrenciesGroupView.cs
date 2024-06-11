using System.Collections.Generic;
using UnityEngine;
using Wallet.Utils;

namespace Wallet.Currencies.UI
{
    public class CurrenciesGroupView : MonoBehaviour
    {
        [SerializeField] private CurrencyViewPool viewPool;
        [SerializeField] private RectTransform viewContainer;

        private IReadOnlyDictionary<int, Sprite> _icons;
        private readonly Dictionary<int, CurrencyView> _currentViews = new();
    
        private void Awake()
        {
            EventBus.GetEntry<CreateCurrencyViewRequest>().Event += OnCreateViewRequest;
            EventBus.GetEntry<DestroyCurrencyViewRequest>().Event += OnDestroyViewRequest;
            EventBus.GetEntry<CurrencyAmountChangedEvent>().Event += OnCurrencyAmountChangedEvent;
        }

        private void OnCurrencyAmountChangedEvent(CurrencyAmountChangedEvent eventData)
        {
            var id = eventData.Id;
            if (!_currentViews.ContainsKey(id))
            {
                Debug.LogError($"Cannot find view for currency {id}");
                return;
            }
            var newAmount = eventData.NewAmount;
            _currentViews[id].SetAmount(newAmount);
        }

        public void SetIconsConfig(IReadOnlyDictionary<int, Sprite> icons)
        {
            _icons = icons;
        }

        private void OnCreateViewRequest(CreateCurrencyViewRequest request)
        {
            var id = request.Id;
            if (_currentViews.ContainsKey(id))
            {
                Debug.LogError($"Currency View {id} is already created");
                return;
            }

            if (!_icons.ContainsKey(id))
            {
                Debug.LogError($"Cannot find icon for currency {id}");
                return;
            }

            var newView = viewPool.Get(viewContainer);
            newView.SetIcon(_icons[id]);
            _currentViews.Add(id, newView);
        }
    
        private void OnDestroyViewRequest(DestroyCurrencyViewRequest request)
        {
            var id = request.Id;
            if (!_currentViews.ContainsKey(id))
            {
                Debug.LogError($"Cannot find currency view {id}");
                return;
            }

            var existingView = _currentViews[id];
            _currentViews.Remove(id);
            viewPool.Release(existingView);
        }

        private void OnDestroy()
        {
            EventBus.GetEntry<CreateCurrencyViewRequest>().Event -= OnCreateViewRequest;
            EventBus.GetEntry<DestroyCurrencyViewRequest>().Event -= OnDestroyViewRequest;
            EventBus.GetEntry<CurrencyAmountChangedEvent>().Event -= OnCurrencyAmountChangedEvent;
        }
    }
}