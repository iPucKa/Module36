using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Meta;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.MainMenu.GameModeSelectorPopup
{
	public class GameModeSelectorPopupPresenter : PopupPresenterBase
	{
		private const string Message = "SELECT MODE";

		private readonly GameModeSelectorPopupView _view;
		private readonly ModeService _modeService;

		public GameModeSelectorPopupPresenter(
			GameModeSelectorPopupView view,
			ModeService modeService,
			ICoroutinesPerformer coroutinesPerformer) : base(coroutinesPerformer)
		{
			_view = view;
			_modeService = modeService;
		}

		protected override PopupViewBase PopupView => _view;

		public override void Initialize()
		{
			base.Initialize();
			_view.CharModeSelected += OnCharModeSelected;
			_view.NumberModeSelected += OnNumberModeSelected;

			_view.SetText(Message);
		}

		public override void Dispose()
		{
			base.Dispose();

			_view.CharModeSelected -= OnCharModeSelected;
			_view.NumberModeSelected -= OnNumberModeSelected;
		}

		private void OnNumberModeSelected()
		{

			GameplayInputArgs args = new GameplayInputArgs(SymbolInputMode.Numbers);
			_modeService.MoveToGameplayScene(args);

			Debug.Log("Выбран режим генерации цифр");
		}

		private void OnCharModeSelected()
		{
			GameplayInputArgs args = new GameplayInputArgs(SymbolInputMode.Chars);
			_modeService.MoveToGameplayScene(args);

			Debug.Log("Выбран режим генерации букв");
		}

		//protected override void OnPreHide()
		//{
		//	_gameLogic.CanResetGame(true);

		//	base.OnPreHide();
		//}
	}
}
