using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Mono;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Common
{
	public class RigidbodyEntityRegistrator : MonoEntityRegistrator
	{
		public override void Register(Entity entity)
		{
			entity.AddRigidbody(GetComponent<Rigidbody>());
		}
	}
}
