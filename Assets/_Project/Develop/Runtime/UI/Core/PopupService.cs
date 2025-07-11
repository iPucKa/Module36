using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.UI.Gameplay.EndGamePopup;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Assets._Project.Develop.Runtime.UI.MainMenu.GameModeSelectorPopup;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core
{
	public abstract class PopupService : IDisposable // Методы создания попапов для всего проекта
	{
		protected readonly ViewsFactory ViewsFactory;
		private readonly MainMenuPresentersFactory _mainMenuPresentersFactory;
		private readonly GameplayPresentersFactory _gameplayPresentersFactory;

		private readonly Dictionary<PopupPresenterBase, PopupInfo> _presenterToInfo = new();

		protected PopupService(
			ViewsFactory viewsFactory,
			GameplayPresentersFactory presentersFactory)
		{
			ViewsFactory = viewsFactory;
			_gameplayPresentersFactory = presentersFactory;
		}

		protected PopupService(
			ViewsFactory viewsFactory,
			MainMenuPresentersFactory presentersFactory)
		{
			ViewsFactory = viewsFactory;
			_mainMenuPresentersFactory = presentersFactory;
		}

		protected abstract Transform PopupLayer { get; }

		public EndGamePopupPresenter OpenEndGamePopup(string message, Action closedCallback = null)
		{
			EndGamePopupView view = ViewsFactory.Create<EndGamePopupView>(ViewIDs.EndGamePopup, PopupLayer);

			EndGamePopupPresenter popup = _gameplayPresentersFactory.CreateEndGamePopupPresenter(view, message);

			OnPopupCreated(popup, view, closedCallback);

			return popup;
		}

		public GameModeSelectorPopupPresenter OpenGameModeSelectorPopup(Action closedCallback = null)
		{
			GameModeSelectorPopupView view = ViewsFactory.Create<GameModeSelectorPopupView>(ViewIDs.GameModeSelectorPopup, PopupLayer);

			GameModeSelectorPopupPresenter popup = _mainMenuPresentersFactory.CreateGameModeSelectorPopupPresenter(view);

			OnPopupCreated(popup, view, closedCallback);

			return popup;
		}

		public void ClosePopup(PopupPresenterBase popup)
		{
			popup.CloseRequest -= ClosePopup;
			popup.Hide(() =>
			{
				_presenterToInfo[popup].ClosedCallback?.Invoke();

				DisposeFor(popup);
				_presenterToInfo.Remove(popup);
			});
		}

		public void Dispose()
		{
			foreach (PopupPresenterBase popup in _presenterToInfo.Keys)
			{
				popup.CloseRequest -= ClosePopup;
				DisposeFor(popup);
			}

			_presenterToInfo.Clear();
		}

		protected void OnPopupCreated(
			PopupPresenterBase popup,
			PopupViewBase view,
			Action closedCallback = null)
		{
			PopupInfo popupInfo = new PopupInfo(view, closedCallback);

			_presenterToInfo.Add(popup, popupInfo);
			popup.Initialize();
			popup.Show();

			popup.CloseRequest += ClosePopup;
		}

		private void DisposeFor(PopupPresenterBase popup)
		{
			popup.Dispose();
			ViewsFactory.Release(_presenterToInfo[popup].View);
		}

		private class PopupInfo
		{
			public PopupInfo(PopupViewBase view, Action closedCallback)
			{
				View = view;
				ClosedCallback = closedCallback;
			}

			public PopupViewBase View { get; }
			public Action ClosedCallback { get; }
		}
	}
}
