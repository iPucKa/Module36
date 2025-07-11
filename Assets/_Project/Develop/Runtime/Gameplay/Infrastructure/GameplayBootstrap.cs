using Assets._Project.Develop.Runtime.Gameplay.GameModeManagement;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayBootstrap : SceneBootstrap
	{
		private DIContainer _container;
		private GameplayCycle _gameplayCycle;
		private GameplayInputArgs _inputArgs;

		public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container = container;

			if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
				throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)}");

			_inputArgs = gameplayInputArgs;

			GameplayContextRegistrations.Process(_container, _inputArgs);
		}

		public override IEnumerator Initialize()
		{
			_gameplayCycle = _container.Resolve<GameplayCycle>();

			yield return _gameplayCycle.Launch();

			Debug.Log("Инициализация геймплейной сцены");
		}

		public override void Run()
		{
			Debug.Log("Старт геймплейной сцены");

			_gameplayCycle.Start();
		}

		private void OnDestroy()
		{
			_gameplayCycle?.Dispose();
		}
	}
}
