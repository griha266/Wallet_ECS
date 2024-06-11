namespace Wallet.Data
{
    public interface IVersionResolver<T>
    {
        T ResolveVersion(DataWithVersion<T> dataWithVersion);
    }
}