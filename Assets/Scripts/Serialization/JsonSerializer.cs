using System;
using UnityEngine;
using Wallet.Utils;

namespace Wallet.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public Result<T> Deserialize<T>(string data)
        {
            try
            {
                return Result.Ok(JsonUtility.FromJson<T>(data));
            }
            catch (Exception e)
            {
                return Result.Error<T>(e);
            }
        }

        public Result<string> Serialize<T>(T data)
        {
            try
            {
                return Result.Ok(JsonUtility.ToJson(data));
            }
            catch (Exception e)
            {
                return Result.Error<string>(e);
            }
        }
    }
}