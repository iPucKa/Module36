using Assets._Project.Develop.Runtime.UI.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu.GameModeSelectorPopup
{
	public class GameModeSelectorPopupView : PopupViewBase
	{
		public event Action CharModeSelected;
		public event Action NumberModeSelected;

		[SerializeField] private Button _charModeSelectButton;
		[SerializeField] private Button _numberModeSelectButton;

		[SerializeField] private TMP_Text _title;

		public void SetText(string message) => _title.text = message;

		private void OnEnable()
		{
			_charModeSelectButton.onClick.AddListener(OnCharModeSelected);
			_numberModeSelectButton.onClick.AddListener(OnNumberModeSelected);
		}

		private void OnDisable()
		{
			_charModeSelectButton.onClick.RemoveListener(OnCharModeSelected);
			_numberModeSelectButton.onClick.RemoveListener(OnNumberModeSelected);
		}

		private void OnNumberModeSelected() => NumberModeSelected?.Invoke();

		private void OnCharModeSelected() => CharModeSelected?.Invoke();
	}
}
