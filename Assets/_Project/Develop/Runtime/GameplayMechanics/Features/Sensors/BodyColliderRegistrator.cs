using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Mono;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.Sensors
{
	public class BodyColliderRegistrator : MonoEntityRegistrator
	{
		[SerializeField] private CapsuleCollider _body;

		public override void Register(Entity entity)
		{
			entity.AddBodyCollider(_body);
		}
	}
}
