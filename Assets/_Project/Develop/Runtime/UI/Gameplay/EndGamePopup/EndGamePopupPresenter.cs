using Assets._Project.Develop.Runtime.Gameplay.GameModeManagement;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.EndGamePopup
{
	public class EndGamePopupPresenter : PopupPresenterBase
	{
		private readonly EndGamePopupView _view;
		private readonly GameplayCycle _gameLogic;
		private readonly string _message;

		public EndGamePopupPresenter(
			EndGamePopupView view, 
			ICoroutinesPerformer coroutinesPerformer, 
			GameplayCycle gameLogic,
			string message) : base(coroutinesPerformer)
		{
			_view = view;
			_gameLogic = gameLogic;
			_message = message;
		}

		protected override PopupViewBase PopupView => _view;

		public override void Initialize()
		{
			base.Initialize();
			_view.SetText(_message);
		}

		protected override void OnPreHide()
		{
			_gameLogic.CanResetGame(true);

			base.OnPreHide();			
		}
	}
}
