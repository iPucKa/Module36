using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
	[CreateAssetMenu(menuName = "Configs/Gameplay/SymbolConfig", fileName = "SymbolConfig")]
	public class SymbolConfig : ScriptableObject
	{
		[field: SerializeField] public SymbolInputMode Mode {  get; private set; }
		[field: SerializeField] public string Symbols { get; private set; }
	}
}
