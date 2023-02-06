using System;
using System.Collections.Generic;
using Source.Core.Input;
using Source.Core.Service;

namespace Source.Core
{
    public sealed class ServiceLocator
    {
        public static ServiceLocator Instance { get; } = new ServiceLocator();

        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        private ServiceLocator()
        {
            _services.Add(typeof(IInputService), new InputService());
            _services.Add(typeof(IStorageDataService), new PlayerPrefsDataStorage());
            _services.Add(typeof(IPlayerStateService), new PlayerStateService());
        }

        /// <summary>
        /// Get service by interface. Return null if service not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public T GetService<T>() where T : class, IService
        {
            var type = typeof(T);
            if (!type.IsInterface)
                throw new ArgumentException(
                    $"Type of: {type.Name} is not interface.");
            
            if (_services.TryGetValue(typeof(T), out var service))
                return service as T;
            
            return null;
        }
    }
}