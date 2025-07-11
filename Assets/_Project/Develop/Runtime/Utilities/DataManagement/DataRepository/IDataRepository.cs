using System;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.DataRepository
{
	public interface IDataRepository
	{
		IEnumerator Read(string key, Action<string> onRead); //Action вызывается как реакция на то, что чтение прошло
		IEnumerator Write(string key, string serializedData);
		IEnumerator Remove(string key);
		IEnumerator Exists(string key, Action<bool> onExistsResult); // Action вызывается когда у нас закончится операция проверки и внутрь будет прилетать результат операции. Извне передается делегат, который обрабатывает результат
	}
}
