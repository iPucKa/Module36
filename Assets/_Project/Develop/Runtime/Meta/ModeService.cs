using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Meta
{
	public class ModeService
	{
		private readonly SceneSwitcherService _sceneSwitcherService;
		private readonly ICoroutinesPerformer _coroutinesPerformer;

		public ModeService(ICoroutinesPerformer coroutinesPerformer, SceneSwitcherService sceneSwitcherService)
		{
			_sceneSwitcherService = sceneSwitcherService;
			_coroutinesPerformer = coroutinesPerformer;
		}		

		public void MoveToGameplayScene(GameplayInputArgs args) => _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, args));		
	}
}
