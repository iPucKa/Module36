using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
	public class MainMenuScreenView : MonoBehaviour, IView
	{
		public event Action ResetProgressButtonClicked;
		public event Action ModeSelectorButtonClicked;

		[field: SerializeField] public IconTextListView WalletView { get; private set; }
		[field: SerializeField] public IconTextListView ProgressView { get; private set; }

		[SerializeField] private Button _resetProgressButton;
		[SerializeField] private Button _modeSelectorButton;

		private void OnEnable()
		{
			_resetProgressButton.onClick.AddListener(OnResetProgressButtonClicked);
			_modeSelectorButton.onClick.AddListener(OnModeSelectorButtonClicked);
		}
		private void OnDisable()
		{
			_resetProgressButton.onClick.RemoveListener(OnResetProgressButtonClicked);
			_modeSelectorButton.onClick.RemoveListener(OnModeSelectorButtonClicked);
		}

		private void OnModeSelectorButtonClicked() => ModeSelectorButtonClicked?.Invoke();

		private void OnResetProgressButtonClicked() => ResetProgressButtonClicked?.Invoke();
	}
}
