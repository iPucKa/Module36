using Assets._Project.Develop.Runtime.Gameplay.Progress;
using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.Progress
{
	public class ProgressBarPresenter : IPresenter
	{
		private readonly ProgressService _progressService;
		private readonly ProjectPresentersFactory _presentersFactory;
		private readonly ViewsFactory _viewsFactory;

		private readonly IconTextListView _view;
		private readonly List<ProgressPresenter> _progressPresenters = new();

		public ProgressBarPresenter(
			ProgressService progressService,
			ProjectPresentersFactory presentersFactory,
			ViewsFactory viewsFactory,
			IconTextListView view)
		{
			_progressService = progressService;
			_presentersFactory = presentersFactory;
			_viewsFactory = viewsFactory;
			_view = view;
		}

		public void Initialize()
		{
			foreach (GameProgressTypes pregressType in _progressService.AllKindOfProgress)
			{
				IconTextView progressView = _viewsFactory.Create<IconTextView>(ViewIDs.ProgressView);

				_view.Add(progressView);

				ProgressPresenter progressPresenter = _presentersFactory.CreateProgressPresenter(
					progressView,
					_progressService.GetProgress(pregressType),
					pregressType);

				progressPresenter.Initialize();
				_progressPresenters.Add(progressPresenter);
			}
		}

		public void Dispose()
		{
			foreach (ProgressPresenter progressPresenter in _progressPresenters)
			{
				_view.Remove(progressPresenter.View);
				_viewsFactory.Release(progressPresenter.View);
				progressPresenter.Dispose();
			}

			_progressPresenters.Clear();
		}
	}
}
