using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.UI.Progress
{
	public class ProgressPresenter : IPresenter
	{
		//Бизнес-логика
		private readonly IReadOnlyVariable<int> _progressValue;
		private readonly GameProgressTypes _progressType;
		private readonly ProgressIconsConfig _progressIconsConfig;

		//Визуал
		private readonly IconTextView _view;

		private IDisposable _disposable;

		public ProgressPresenter(
			IReadOnlyVariable<int> progressValue,
			GameProgressTypes progressType,
			ProgressIconsConfig progressIconsConfig,
			IconTextView view)
		{
			_progressValue = progressValue;
			_progressType = progressType;
			_progressIconsConfig = progressIconsConfig;
			_view = view;
		}

		public IconTextView View => _view;

		public void Initialize()
		{
			UpdateValue(_progressValue.Value);
			_view.SetIcon(_progressIconsConfig.GetSpriteFor(_progressType));

			_disposable = _progressValue.Subscribe(OnCurrencyChanged);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}

		private void OnCurrencyChanged(int arg1, int newValue) => UpdateValue(newValue);

		private void UpdateValue(int value) => _view.SetText(value.ToString());
	}
}
