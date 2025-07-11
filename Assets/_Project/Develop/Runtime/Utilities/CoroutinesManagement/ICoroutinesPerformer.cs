using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement
{
	public interface ICoroutinesPerformer
	{
		Coroutine StartPerform(IEnumerator corouineFunction);

		void StopPerform(Coroutine corouine);
	}
}
