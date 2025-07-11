namespace Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Systems
{
	public interface IInitializableSystem : IEntitySystem
	{
		void OnInit(Entity entity);
	}
}
