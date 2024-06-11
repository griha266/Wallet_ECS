using System.Linq;
using Wallet.Currencies.UI;
using Wallet.Configs;
using UnityEngine;

namespace Wallet.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private WalletConfig walletConfig;
        [SerializeField] private CurrenciesGroupView currencies;

        private void Awake()
        {
            var icons = walletConfig
                .Currencies
                .Select(entry => entry.Currnecy)
                .ToDictionary(e => e.Id, e => e.Icon);
        
            currencies.SetIconsConfig(icons);
        }
    }
}