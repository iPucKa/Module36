using System;

namespace Assets._Project.Develop.Runtime.Gameplay.GameRules
{
	public interface IRule : IDisposable
	{
		event Action IsMatch;
		event Action IsNotMatch;
		event Action<string> IsGenerated;

		void Start();

		void Check(string text);
	}
}
