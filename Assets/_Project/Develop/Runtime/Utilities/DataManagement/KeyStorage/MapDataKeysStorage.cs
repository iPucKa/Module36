using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.KeyStorage
{
	public class MapDataKeysStorage : IDataKeysStorage
	{
		private readonly Dictionary<Type, string> Keys = new Dictionary<Type, string>()
		{
			{typeof(PlayerData), "PlayerData" },
		};

		public string GetKeyFor<TData>() where TData : ISaveData
			=> Keys[typeof(TData)];
	}
}
