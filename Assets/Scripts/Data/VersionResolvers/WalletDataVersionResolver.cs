namespace Wallet.Data
{
    public class WalletDataVersionResolver : IVersionResolver<WalletSaveData>
    {
        public WalletSaveData ResolveVersion(DataWithVersion<WalletSaveData> dataWithVersion)
        {
            return dataWithVersion.Data;
        }
    }
}