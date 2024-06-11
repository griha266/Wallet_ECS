using Wallet.Utils;

namespace Wallet.Serialization
{
    public interface ISerializer
    {
        Result<T> Deserialize<T>(string data);
        Result<string> Serialize<T>(T data);
    }
}