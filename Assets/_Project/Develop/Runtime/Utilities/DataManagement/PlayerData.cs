using Assets._Project.Develop.Runtime.Meta.Features.GameProgress;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement
{
	public class PlayerData : ISaveData
	{
		public Dictionary<CurrencyTypes, int> WalletData;

		public Dictionary<GameProgressTypes, int> ProgressData;

		//public int WinProgress;

		//public int DefeatProgress;
	}
}
