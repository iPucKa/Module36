using Assets._Project.Develop.Runtime.UI.Core;
using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.EndGamePopup
{
	public class EndGamePopupView : PopupViewBase
	{
		[SerializeField] private TMP_Text _text;

		public void SetText(string text) => _text.text = text;

		protected override void ModifyShowAnimation(Sequence animation)
		{
			base.ModifyShowAnimation(animation);

			animation
				.Append(_text
					.DOFade(1, 0.2f)
					.From(0));
		}
	}
}
