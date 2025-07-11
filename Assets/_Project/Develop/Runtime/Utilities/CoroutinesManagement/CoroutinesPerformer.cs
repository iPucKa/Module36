using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement
{
	public class CoroutinesPerformer : MonoBehaviour, ICoroutinesPerformer
	{
		private void Awake()
		{
			DontDestroyOnLoad(this);
		}
		public Coroutine StartPerform(IEnumerator corouineFunction)
			=> StartCoroutine(corouineFunction);

		public void StopPerform(Coroutine corouine)
			=> StopCoroutine(corouine);
	}
}
