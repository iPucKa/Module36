using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Configs.GameplayMechanics;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagement
{
	public class ResourcesConfigsLoader : IConfigsLoader
	{
		private readonly ResourcesAssetsLoader _resources;

		private readonly Dictionary<Type, string> _configsResourcesPath = new()
		{
			{typeof(GameplayConfig), "Configs/Gameplay/GameplayConfig"},
			{typeof(RulesConfig), "Configs/Gameplay/RulesConfig"},
			{typeof(StartWalletConfig), "Configs/Wallet/StartWalletConfig"},
			{typeof(StartProgressConfig), "Configs/Gameplay/StartProgressConfig"},
			{typeof(CurrencyIconsConfig), "Configs/Wallet/CurrencyIconsConfig"},
			{typeof(ProgressIconsConfig), "Configs/Gameplay/ProgressIconsConfig"},
			{typeof(GameplayMechanicsConfig), "Configs/GameplayMechanics/GameplayMechanicsConfig"},
		};

		public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
		{
			_resources = resources;
		}

		public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
		{
			Dictionary<Type, object> loadedConfigs = new();

			foreach (KeyValuePair<Type, string> configResourcesPath in _configsResourcesPath)
			{
				ScriptableObject config = _resources.Load<ScriptableObject>(configResourcesPath.Value);
				loadedConfigs.Add(configResourcesPath.Key, config);
				yield return null;
			}

			onConfigsLoaded?.Invoke(loadedConfigs);
		}
	}
}
