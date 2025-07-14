using Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore;
using Assets._Project.Develop.Runtime.GameplayMechanics.Features.ApplyDamage;

namespace Assets._Project.Develop.Runtime.GameplayMechanics.Features.Attack
{
	public static class DamageHandler
	{
		public static void ApplyDamage(Entity contactEntity, float damage)
		{
			if (contactEntity.HasComponent<TakeDamageRequest>())
				contactEntity.TakeDamageRequest.Invoke(damage);
		}
	}
}
