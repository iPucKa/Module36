using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
	public class GameplayScreenView : MonoBehaviour, IView
	{		
		public event Action<string> IsTyped;

		[field: SerializeField] public IconTextListView WalletView { get; private set; }
		[field: SerializeField] public IconTextListView ProgressView { get; private set; }

		[SerializeField] private TMP_InputField _inputField;
		[SerializeField] private TMP_Text _generatedText;

		private void OnEnable() => _inputField.onEndEdit.AddListener(OnEndEdit);		

		private void OnEndEdit(string text) => IsTyped?.Invoke(text);		

		private void OnDisable() => _inputField.onEndEdit.RemoveListener(OnEndEdit);		

		public void SetText(string text) => _generatedText.text = text;
	}
}
