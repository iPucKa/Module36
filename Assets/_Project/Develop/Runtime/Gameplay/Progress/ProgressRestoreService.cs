using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Progress
{
	public class ProgressRestoreService
	{
		private readonly ICoroutinesPerformer _coroutinesPerformer;
		private readonly PlayerDataProvider _playerDataProvider;
		private readonly WalletService _walletService;
		private readonly ProgressService _progressService;
		
		private readonly int _valueToReset;

		public ProgressRestoreService(
			ICoroutinesPerformer coroutinesPerformer, 
			PlayerDataProvider playerDataProvider, 
			ConfigsProviderService configsProviderService, 
			WalletService walletService, 
			ProgressService progressService)
		{
			_coroutinesPerformer = coroutinesPerformer;
			_playerDataProvider = playerDataProvider;
			_progressService = progressService;
			_walletService = walletService;
			
			_valueToReset = configsProviderService.GetConfig<GameplayConfig>().ValueToResetProgress;
		}

		public void SetInitialValues()
		{

			if (_walletService.Enough(CurrencyTypes.Gold, _valueToReset))
			{
				_walletService.Spend(CurrencyTypes.Gold, _valueToReset);

				foreach (GameProgressTypes pregressType in _progressService.AllKindOfProgress)				
					_progressService.ResetValue(pregressType);

				_coroutinesPerformer.StartPerform(_playerDataProvider.Save());
				//Debug.Log("Золота осталось: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
			}
			else
			{
				Debug.Log("Недостаточно золота");
			}
		}
	}
}
