using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
	public class SceneSwitcherService
	{
		private readonly SceneLoaderService _sceneLoaderService;
		private readonly ILoadingScreen _loadingScreen;
		private readonly DIContainer _projectContainer;

		private DIContainer _currentSceneContainer;

		public SceneSwitcherService(
			SceneLoaderService sceneLoaderService, 
			ILoadingScreen loadingScreen, 
			DIContainer projectContainer)
		{
			_sceneLoaderService = sceneLoaderService;
			_loadingScreen = loadingScreen;
			_projectContainer = projectContainer;
		}

		public IEnumerator ProcessSwitchTo(string sceneName, IInputSceneArgs sceneArgs = null)
		{
			_loadingScreen.Show();
			_currentSceneContainer?.Dispose();

			yield return _sceneLoaderService.LoadAsync(Scenes.Empty);
			yield return _sceneLoaderService.LoadAsync(sceneName);

			SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

			if (sceneBootstrap == null)
				throw new NullReferenceException(nameof(sceneBootstrap) + " not found");

			_currentSceneContainer = new DIContainer(_projectContainer);			
			sceneBootstrap.ProcessRegistrations(_currentSceneContainer, sceneArgs);

			_currentSceneContainer.Initialize();
			yield return sceneBootstrap.Initialize();

			_loadingScreen.Hide();

			sceneBootstrap.Run();
		}
	}
}
