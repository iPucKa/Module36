using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Mono;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.LifeCycle
{
	public class DisableCollidersOnDeathRegistrator : MonoEntityRegistrator
	{
		[SerializeField] private List<Collider> _colliders;

		public override void Register(Entity entity)
		{
			entity.AddDisableCollidersOnDeath(_colliders);
		}
	}
}
