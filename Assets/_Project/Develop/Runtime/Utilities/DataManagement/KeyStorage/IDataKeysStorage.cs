namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.KeyStorage
{
	public interface IDataKeysStorage
	{
		string GetKeyFor<TData>() where TData : ISaveData;
	}
}
