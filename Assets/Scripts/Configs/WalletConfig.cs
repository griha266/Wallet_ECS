using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wallet.Configs
{
    [CreateAssetMenu(menuName = "Wallet/Wallet Config", fileName = "WalletConfig", order = 0)]
    public class WalletConfig : ScriptableObject
    {
        [SerializeField] public Entry[] Currencies;
    
        [Serializable]
        public struct Entry
        {
            [SerializeField] public CurrencyEntryConfig Currnecy;
            [SerializeField] public int InitialAmount;
        }

        public IReadOnlyDictionary<int, Entry> ToLookup()
        {
            var result = new Dictionary<int, Entry>();
            for (int i = 0; i < Currencies.Length; i++)
            {
                var entry = Currencies[i];
                result.Add(entry.Currnecy.Id, entry);
            }

            return result;
        }
    }
}