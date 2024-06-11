using System.Threading;
using System.Threading.Tasks;

namespace Wallet.Data
{
    public interface IDataProvider<T>
    {
        Task<T> Get(CancellationToken cancellationToken);
        Task<bool> Set(T value, CancellationToken cancellationToken);
    }
}