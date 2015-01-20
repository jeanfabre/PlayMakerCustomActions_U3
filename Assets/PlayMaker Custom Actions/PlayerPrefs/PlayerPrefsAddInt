// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// Action made by DjayDino
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Adds a value to a playerprefs int identified by key. WARNING!! use PlayerPrefs only at key moments")]
	public class PlayerPrefsAddInt : FsmStateAction
	{
		[Tooltip("Case sensitive key.")]
		public FsmString key;
		public FsmInt add;
		private FsmInt variables;

		public override void Reset()
		{
			key = "";
			variables = new FsmInt();
			add = null;
		}

		public override void OnEnter()
		{
				if(!key.IsNone || !key.Value.Equals(""))  
				variables.Value = PlayerPrefs.GetInt(key.Value, variables.IsNone ? 0 : variables.Value);
				
				variables.Value += add.Value;
				
				PlayerPrefs.SetInt(key.Value, variables.IsNone ? 0 : variables.Value);

			Finish();
		}

	}
}
