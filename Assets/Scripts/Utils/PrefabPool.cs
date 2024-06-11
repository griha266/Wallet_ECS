using System.Collections.Generic;
using UnityEngine;

namespace Wallet.Utils
{
    public interface IPoolable
    {
        void OnGet();
        void OnRelease();
    }

    public abstract class PrefabPool<T> : MonoBehaviour
        where T : MonoBehaviour, IPoolable
    {
        [SerializeField] private Transform poolContainer;
        [SerializeField] private T prefab;
        [SerializeField] private int warmupInstancesAmount;
    
        private readonly List<T> _instances = new();

        private void Awake()
        {
            for (int i = 0; i < warmupInstancesAmount; i++)
            {
                var instance = CreateInstance(poolContainer);
                _instances.Add(instance);
            }
        }

        private T CreateInstance(Transform parent)
        {
            return Instantiate(prefab, parent);
        }

        public T Get(Transform parent)
        {
            if (_instances.Count > 0)
            {
                var lastIndex = _instances.Count - 1;
                var instance = _instances[lastIndex];
                _instances.RemoveAt(lastIndex);
                instance.transform.SetParent(parent);
                instance.OnGet();
                return instance;
            }

            var newInstance = CreateInstance(parent);
            newInstance.OnGet();
            return newInstance;
        }

        public void Release(T instance)
        {
            instance.OnRelease();
            instance.transform.SetParent(poolContainer);
            _instances.Add(instance);
        }
    }
}