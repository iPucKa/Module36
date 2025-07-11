using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Common
{
	public class RigidbodyComponent : IEntityComponent
	{
		public Rigidbody Value;
	}

	public class CharacterControllerComponent : IEntityComponent
	{
		public CharacterController Value;
	}
}
