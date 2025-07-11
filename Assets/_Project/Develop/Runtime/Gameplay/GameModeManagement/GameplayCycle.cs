using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.GameRules;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Gameplay.Progress;
using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.GameModeManagement
{
	public class GameplayCycle : IDisposable
	{
		private readonly SymbolInputMode _mode;

		private readonly WalletService _walletService;
		private readonly ProgressService _progressService;

		private readonly ConfigsProviderService _configProviderService;
		private readonly PlayerDataProvider _playerDataProvider;
		private readonly ICoroutinesPerformer _coroutinesPerformer;
		private readonly SceneSwitcherService _sceneSwitcherService;
		private readonly IRule _gameRule;

		private bool _isPopupGlosed;

		public GameplayCycle(
			WalletService walletService,
			ProgressService progressService,
			PlayerDataProvider playerDataProvider,
			ConfigsProviderService configProviderService,
			SceneSwitcherService sceneSwitcherService,
			ICoroutinesPerformer coroutinesPerformer,
			IRule gameRule,
			IInputSceneArgs sceneArgs = null)
		{

			_walletService = walletService;
			_progressService = progressService;
			_playerDataProvider = playerDataProvider;
			_configProviderService = configProviderService;
			_coroutinesPerformer = coroutinesPerformer;
			_sceneSwitcherService = sceneSwitcherService;
			_gameRule = gameRule;

			if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
				throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)}");

			_mode = gameplayInputArgs.Mode;

			_isPopupGlosed = false;
		}

		public IEnumerator Launch()
		{		
			_gameRule.IsMatch += OnGameModeWin;
			_gameRule.IsNotMatch += OnGameModeDefeat;

			yield return null;
		}

		public void Start() => _gameRule.Start();		

		public void Dispose() => OnGameModeEnded();		

		private void OnGameModeDefeat()
		{
			int goldValueToSpend = _configProviderService.GetConfig<GameplayConfig>().GetDefeatValue;

			if (_walletService.Enough(CurrencyTypes.Gold, goldValueToSpend))
			{
				_walletService.Spend(CurrencyTypes.Gold, goldValueToSpend);
				_progressService.Increase(GameProgressTypes.Defeat);

				_coroutinesPerformer.StartPerform(_playerDataProvider.Save());
				//Debug.Log("Золота осталось: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
			}

			OnGameModeEnded();
			Debug.Log("ПОРАЖЕНИЕ");
			_coroutinesPerformer.StartPerform(ResetProcess(Scenes.Gameplay));
		}

		private void OnGameModeWin()
		{
			int goldValueToAdd = _configProviderService.GetConfig<GameplayConfig>().GetWinValue;

			_walletService.Add(CurrencyTypes.Gold, goldValueToAdd);
			_progressService.Increase(GameProgressTypes.Win);

			_coroutinesPerformer.StartPerform(_playerDataProvider.Save());
			//Debug.Log("Золота стало: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);

			OnGameModeEnded();
			Debug.Log("ПОБЕДА");
			_coroutinesPerformer.StartPerform(ResetProcess(Scenes.MainMenu));
		}

		private void OnGameModeEnded()
		{
			if (_gameRule != null)
				_gameRule.Dispose();
		}

		private IEnumerator ResetProcess(string sceneName)
		{
			yield return new WaitUntil(() => _isPopupGlosed == true);

			_coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(sceneName, new GameplayInputArgs(_mode)));
		}

		public void CanResetGame(bool isPopupGlosed) => _isPopupGlosed = isPopupGlosed;
	}
}
