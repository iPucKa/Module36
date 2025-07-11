using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Infrastructure.DI
{
	public class DIContainer
	{
		private readonly Dictionary<Type, Registration> _container = new();

		private readonly List<Type> _requests = new();

		private readonly DIContainer _parent;

		public DIContainer() : this(null)
		{
		}

		public DIContainer(DIContainer parent) => _parent = parent;

		//Регисрация типа и способа создания сервиса в контейнере, экземпляра еще не создано
		public IRegistrationOptions RegisterAsSingle<T>(Func<DIContainer, T> creator)
		{
			if (IsAlreadyRegister<T>())
				throw new InvalidOperationException($"{typeof(T)} already register");

			Registration registration = new Registration(container => creator.Invoke(container));
			_container.Add(typeof(T), registration);

			return registration;
		}

		private bool IsAlreadyRegister<T>()
		{
			if (_container.ContainsKey(typeof(T)))
				return true;

			if (_parent != null)
				return _parent.IsAlreadyRegister<T>();

			return false;
		}

		//Метод получения зависимостей из контейнера
		public T Resolve<T>()
		{
			if (_requests.Contains(typeof(T)))
				throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

			_requests.Add(typeof(T));

			try
			{
				if (_container.TryGetValue(typeof(T), out Registration registration))
					return (T)registration.CreateInstanceFrom(this);

				if (_parent != null)
					return _parent.Resolve<T>();
			}
			finally
			{
				_requests.Remove(typeof(T));
			}

			throw new InvalidOperationException($"Registration for {typeof(T)} not found");
		}

		public void Initialize()
		{
			foreach (Registration registration in _container.Values)
			{
				if (registration.IsNonLazy)
					registration.CreateInstanceFrom(this);

				registration.OnInitialize();
			}
		}

		public void Dispose()
		{
			foreach (Registration registration in _container.Values)
				registration.OnDispose();
		}
	}
}
