using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Wallet.Utils
{
    public static class Result
    {
        public static Result<Unit> Ok() => new(Unit.Default, null);
        public static Result<Unit> Error(Exception exception) => new(Unit.Default, exception);
        public static Result<T> Ok<T>(T value) => new(value, null);
        public static Result<T> Error<T>(Exception exception) => new(default, exception);

        public static Result<T> WrapWithResult<T>(Func<T> func)
        {
            try
            {
                var result = func();
                return Ok(result);
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        public static TResult Unwrap<TSource, TResult>(this Result<TSource> source, Func<TSource, TResult> onValue,
            Func<Exception, TResult> onError)
        {
            return source.IsValid ? onValue(source.GetValueOrDefault()) : onError(source.Exception);
        }

        public static T Unwrap<T>(this Result<T> source, Func<Exception, T> onError)
        {
            return source.IsValid ? source.GetValueOrDefault() : onError(source.Exception);
        }

        public static Result<T> Unfold<T>(this Result<Result<T>> source)
        {
            if (source.IsValid)
            {
                return source.GetValueOrDefault();
            }

            return Error<T>(source.Exception);
        }


        public static T LogErrorAndGetDefault<T>(this Result<T> source, T onErrorValue)
        {
            if (!source.IsValid)
            {
                Debug.LogException(source.Exception);
                return onErrorValue;
            }

            return source.GetValueOrDefault();
        }

        public static async Task<Result<TResult>> MapManyAsync<TSource, TResult>(this Result<TSource> source,
            Func<TSource, CancellationToken, Task<Result<TResult>>> mapper, CancellationToken cancellationToken)
        {
            if (!source.IsValid)
            {
                return Error<TResult>(source.Exception);
            }

            try
            {
                var result = await mapper(source.GetValueOrDefault(), cancellationToken);
                return result;
            }
            catch (Exception e)
            {
                return Error<TResult>(e);
            }
        }
    
        public static Result<TResult> MapMany<TSource, TResult>(this Result<TSource> source,
            Func<TSource, Result<TResult>> mapper)
        {
            if (!source.IsValid)
            {
                return Error<TResult>(source.Exception);
            }

            try
            {
                var result = mapper(source.GetValueOrDefault());
                return result;
            }
            catch (Exception e)
            {
                return Error<TResult>(e);
            }
        }

        public static async Task<Result<TResult>> MapAsync<TSource, TResult>(this Result<TSource> source, Func<TSource, CancellationToken, Task<TResult>> mapper, CancellationToken cancellationToken = default)
        {
            if (!source.IsValid)
            {
                return Error<TResult>(source.Exception);
            }

            try
            {
                var result = await mapper(source.GetValueOrDefault(), cancellationToken);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Error<TResult>(e);
            }
        }

        public static Result<TResult> Map<TSource, TResult>(this Result<TSource> source, Func<TSource, TResult> mapper)
        {
            if(source.IsValid)
            {
                var value = source.GetValueOrDefault();
                try
                {
                    var mapped = mapper(value);
                    return Ok(mapped);
                }
                catch (Exception e)
                {
                    return Error<TResult>(e);
                }
            }

            return Error<TResult>(source.Exception);
        }
    }

    public readonly struct Result<T>
    {
        private readonly T _value;
        public Exception Exception { get; }

        public bool IsValid => Exception == null;

        public Result(T value, Exception exception)
        {
            _value = value;
            Exception = exception;
        }

        public T GetValueOrDefault(T defaultValue = default) => IsValid ? _value : defaultValue;
    }
}