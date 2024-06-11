using System;

namespace Wallet.Utils
{
    public readonly struct Option<T>
    {
        private readonly T _value;
        public readonly bool HasValue;

        public T GetValueOrDefault(T defaultValue = default) => HasValue ? _value : defaultValue;
    
        public Option(T value, bool hasValue)
        {
            _value = value;
            HasValue = hasValue;
        }
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value) => new(value, true);
        public static Option<T> None<T>() => new(default, false);

        public static Option<TResult> Map<TSource, TResult>(this Option<TSource> source, Func<TSource, TResult> mapper)
        {
            if (source.HasValue)
            {
                var value = source.GetValueOrDefault();
                var mapped = mapper(value);
                return Some(mapped);
            }

            return None<TResult>();
        }
    }
}