using System;

namespace Wallet.Data
{
    [Serializable]
    public struct DataWithVersion<T>
    {
        public T Data;
        public int Version;
    }
}