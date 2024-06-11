using UnityEngine;
using UnityEngine.UI;
using Wallet.Loading;
using Wallet.Utils;

namespace Wallet.WalletController.UI
{
    public class WalletController : MonoBehaviour
    {
        [SerializeField] private Button increaseCurrency;
        [SerializeField] private Button clearCurrency;
        [SerializeField] private Button nextCurrencyId;
        [SerializeField] private Button previousCurrencyId;

        private void Awake()
        {
            increaseCurrency.onClick.AddListener(EventBus.Push<IncreaseCurrentCurrencyRequest>);
            clearCurrency.onClick.AddListener(EventBus.Push<ClearCurrentCurrencyRequest>);
            nextCurrencyId.onClick.AddListener(EventBus.Push<SetNextCurrencyIndexRequest>);
            previousCurrencyId.onClick.AddListener(EventBus.Push<SetPreviousCurrencyIndexRequest>);
            EventBus.GetEntry<CurrenciesLoadedEvent>().Event += OnCurrenciesLoaded;
        }

        private void OnCurrenciesLoaded(CurrenciesLoadedEvent _)
        {
            increaseCurrency.interactable = true;
            clearCurrency.interactable = true;
            nextCurrencyId.interactable = true;
            previousCurrencyId.interactable = true;
        }

        private void OnDestroy()
        {
            EventBus.GetEntry<CurrenciesLoadedEvent>().Event -= OnCurrenciesLoaded;
            if (increaseCurrency) { 
                increaseCurrency.onClick.RemoveListener(EventBus.Push<IncreaseCurrentCurrencyRequest>);
            }
            if (clearCurrency) { 
                clearCurrency.onClick.RemoveListener(EventBus.Push<ClearCurrentCurrencyRequest>);
            }
            if (nextCurrencyId) { 
                nextCurrencyId.onClick.RemoveListener(EventBus.Push<SetNextCurrencyIndexRequest>);
            }
            if (previousCurrencyId) { 
                previousCurrencyId.onClick.RemoveListener(EventBus.Push<SetPreviousCurrencyIndexRequest>);
            }
        }
    }
}