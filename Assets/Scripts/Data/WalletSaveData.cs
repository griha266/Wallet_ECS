using System;

namespace Wallet.Data
{
    [Serializable]
    public struct WalletSaveData
    {
        public const int DataVersion = 0;
        
        public CurrencySaveData[] Currencies;
    }
}