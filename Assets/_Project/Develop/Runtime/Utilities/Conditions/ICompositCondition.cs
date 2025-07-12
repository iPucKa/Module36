namespace Assets._Project.Develop.Runtime.Utilities.Conditions
{
	public interface ICompositCondition : ICondition
	{
		ICompositCondition Add(ICondition condition);

		ICompositCondition Remove(ICondition condition);
	}
}
