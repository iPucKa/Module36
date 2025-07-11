using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Core
{
	public abstract class PopupViewBase : MonoBehaviour, IShowableView
	{
		public event Action CloseRequest;

		[SerializeField] private CanvasGroup _mainGroup;
		[SerializeField] private Image _anticlicker;
		[SerializeField] private CanvasGroup _body;

		[SerializeField] private PopupAnimationTypes _animationType;

		private float _anticlickerDefaultAlpha;

		private Tween _currentAnimatoin;

		private void Awake()
		{
			_anticlickerDefaultAlpha = _anticlicker.color.a;
			_mainGroup.alpha = 0f;
		}

		public void OnCloseButtonClicked() => CloseRequest?.Invoke();

		public Tween Show()
		{
			KillCurrentAnimation();

			OnPreShow();

			//тут потом появятся анимации
			_mainGroup.alpha = 1;
			//_mainGroup.DOFade(1, 1).Play();

			//Sequence animation = DOTween.Sequence(); // пустая очередность анимаций, не содержащая никаких элементов

			//animation
			//	.Append(_anticlicker
			//		.DOFade(0.75f, 0.2f)
			//		.From(0))
			//	.Join(_body
			//		.DOScale(1, 0.5f)
			//		.From(0)
			//		.SetEase(Ease.OutBack));

			Sequence animation = PopupAnimationsCreator
				.CreateShowAnimation(_body, _anticlicker, _animationType, _anticlickerDefaultAlpha);

			ModifyShowAnimation(animation);

			animation.OnComplete(OnPostShow);

			return _currentAnimatoin = animation.SetUpdate(true).Play();
		}

		public Tween Hide()
		{
			KillCurrentAnimation();

			OnPreHide();

			//тут потом появятся анимации
			//_mainGroup.alpha = 0f;
			//Sequence animation = DOTween.Sequence();
			Sequence animation = PopupAnimationsCreator
				.CreateHideAnimation(_body, _anticlicker, _animationType, _anticlickerDefaultAlpha);

			ModifyHideAnimation(animation);

			animation.OnComplete(OnPostHide);

			return _currentAnimatoin = animation.SetUpdate(true).Play();
		}

		protected virtual void ModifyShowAnimation(Sequence animation) { }

		protected virtual void ModifyHideAnimation(Sequence animation) { }

		protected virtual void OnPostShow() { }

		protected virtual void OnPreShow() { }

		protected virtual void OnPostHide() { }

		protected virtual void OnPreHide() { }

		private void OnDestroy() => KillCurrentAnimation();

		private void KillCurrentAnimation()
		{
			if (_currentAnimatoin != null)
				_currentAnimatoin.Kill();
		}
	}
}
