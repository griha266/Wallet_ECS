using UnityEngine;
using UnityEngine.UI;
using Wallet.Utils;

namespace Wallet.Currencies.UI
{
    public class CurrencyView : MonoBehaviour, IPoolable
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text amountText;

        public void SetIcon(Sprite iconSprite)
        {
            icon.sprite = iconSprite;
        }

        public void SetAmount(int amount)
        {
            amountText.text = amount.ToString();
        }

        public void OnGet()
        {
            gameObject.SetActive(true);
        }

        public void OnRelease()
        {
            gameObject.SetActive(false);
        }
    }
}