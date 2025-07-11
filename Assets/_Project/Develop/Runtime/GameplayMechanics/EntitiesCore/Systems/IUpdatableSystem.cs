namespace Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems
{
	public interface IUpdatableSystem : IEntitySystem
	{
		void OnUpdate(float deltaTime);
	}
}
