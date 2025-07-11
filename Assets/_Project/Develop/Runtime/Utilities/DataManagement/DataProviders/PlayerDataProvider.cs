using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders
{
	public class PlayerDataProvider : DataProvider<PlayerData>
	{
		private readonly ConfigsProviderService _configProviderService;
		public PlayerDataProvider(
			ISaveLoadService saveLoadService,
			ConfigsProviderService configProviderService) : base(saveLoadService)
		{
			_configProviderService = configProviderService;
		}

		protected override PlayerData GetOriginData()
		{
			return new PlayerData()
			{
				WalletData = InitWalletData(),
				ProgressData = InitProgressData(),
			};
		}

		private Dictionary<GameProgressTypes, int> InitProgressData()
		{
			Dictionary<GameProgressTypes, int> progressData = new();

			StartProgressConfig progressConfig = _configProviderService.GetConfig<StartProgressConfig>();

			foreach (GameProgressTypes progressType in Enum.GetValues(typeof(GameProgressTypes)))
				progressData[progressType] = progressConfig.GetValueFor(progressType);

			return progressData;
		}

		private Dictionary<CurrencyTypes, int> InitWalletData()
		{
			Dictionary<CurrencyTypes, int> walletData = new();

			StartWalletConfig walletConfig = _configProviderService.GetConfig<StartWalletConfig>();

			foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
				walletData[currencyType] = walletConfig.GetValueFor(currencyType);

			return walletData;
		}
	}
}
