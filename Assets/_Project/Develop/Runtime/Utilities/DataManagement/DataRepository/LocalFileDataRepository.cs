using System;
using System.Collections;
using System.IO;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.DataRepository
{
	public class LocalFileDataRepository : IDataRepository
	{
		private readonly string _folderPath;
		private readonly string _saveFileExtention;

		public LocalFileDataRepository(string folderPath, string saveFileExtention)
		{
			_folderPath = folderPath;
			_saveFileExtention = saveFileExtention;
		}

		public IEnumerator Exists(string key, Action<bool> onExistsResult)
		{
			bool exists = File.Exists(FullPathFor(key));
			onExistsResult?.Invoke(exists);
			yield break;
		}

		public IEnumerator Read(string key, Action<string> onRead)
		{
			string text = File.ReadAllText(FullPathFor(key));
			onRead?.Invoke(text);
			yield break;
		}

		public IEnumerator Remove(string key)
		{
			File.Delete(FullPathFor(key));
			yield break;
		}

		public IEnumerator Write(string key, string serializedData)
		{
			File.WriteAllText(FullPathFor(key), serializedData);
			yield break;
		}

		private string FullPathFor(string key)
			=> Path.Combine(_folderPath, key) + "." + _saveFileExtention;
	}
}
