using UnityEngine;

namespace Wallet.Configs
{
    [CreateAssetMenu(menuName = "Wallet/Currency config", fileName = "CurrencyEntryConfig", order = 0)]
    public class CurrencyEntryConfig : ScriptableObject
    {
        [SerializeField] public int Id;
        [SerializeField] public string DebugName;
        [SerializeField] public Sprite Icon;
    }
}