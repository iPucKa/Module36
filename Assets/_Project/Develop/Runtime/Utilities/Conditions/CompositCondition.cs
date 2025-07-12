using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.Conditions
{
	public class CompositCondition : ICompositCondition
	{
		private List<ICondition> _conditions = new();
		private Func<bool, bool, bool> _standardLogicOperation;

		public CompositCondition(Func<bool, bool, bool> standardLogicOperation)
		{
			_standardLogicOperation = standardLogicOperation;
		}

		public CompositCondition() : this(LogicOperations.And)
		{

		}

		public ICompositCondition Add(ICondition condition)
		{
			_conditions.Add(condition);
			return this;
		}

		public bool Evaluate()
		{
			if (_conditions.Count == 0)
				return false;

			bool result = _conditions[0].Evaluate();

			for (int i = 1; i < _conditions.Count; i++)
			{
				ICondition condition = _conditions[i];
				result = _standardLogicOperation.Invoke(result, condition.Evaluate());
			}

			return result;
		}

		public ICompositCondition Remove(ICondition condition)
		{
			_conditions.Remove(condition);
			return this;
		}
	}
}
