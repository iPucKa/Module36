using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayInputArgs : IInputSceneArgs
	{
		private readonly SymbolInputMode _mode;

		public GameplayInputArgs(SymbolInputMode mode)
		{
			_mode = mode;
		}

		public SymbolInputMode Mode => _mode;
	}
}
