using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
	[CreateAssetMenu(menuName = "Configs/Gameplay/RulesConfig", fileName = "RulesConfig")]
	public class RulesConfig : ScriptableObject
	{		
		[SerializeField] private List<SymbolConfig> _symbols;

		public string GetSymbolsFor(SymbolInputMode mode)
			=> _symbols.First(config => config.Mode == mode).Symbols;
	}	
}
