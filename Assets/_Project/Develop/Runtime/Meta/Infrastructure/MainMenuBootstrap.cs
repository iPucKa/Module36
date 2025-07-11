using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
	public class MainMenuBootstrap : SceneBootstrap
	{
		private DIContainer _container;

		public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container = container;

			MainMenuContextRegistrations.Process(_container);
		}

		public override IEnumerator Initialize()
		{
			Debug.Log("Инициализация сцены меню");

			yield break;
		}

		public override void Run()
		{			
			Debug.Log("Старт сцены меню");
			
		}
	}
}
