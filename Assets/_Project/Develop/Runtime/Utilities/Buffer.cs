namespace Assets._Project.Develop.Runtime.Utilities
{
	public class Buffer<T>
	{
		public T[] Items;
		public int Count; // актуальное число касаний

		public Buffer(int initialSize)
		{
			Items = new T[initialSize];
			Count = 0;
		}
	}
}
