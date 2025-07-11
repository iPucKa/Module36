using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Mono
{
	public abstract class MonoEntityRegistrator : MonoBehaviour
	{
		public abstract void Register(Entity entity);
	}
}
