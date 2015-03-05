// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Builds a String from other Strings. This is the same as BuildString but this one properly insert elements between items, not after.")]
	public class BuildString2 : FsmStateAction
	{
		[RequiredField]
        [Tooltip("Array of Strings to combine.")]
		public FsmString[] stringParts;

        [Tooltip("Separator to insert between each String. E.g. space character.")]
        public FsmString separator;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the final String in a variable.")]
        public FsmString storeResult;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;
	    
        private string result;

		public override void Reset()
		{
			stringParts = new FsmString[3];
			separator = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoBuildString();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoBuildString();
		}
		
		void DoBuildString()
		{
			if (storeResult == null) return;

			result = "";
			int i = 1;
			foreach (var stringPart in stringParts)
			{
				result += stringPart;

				if (i < stringParts.Length)
				{
					result += separator.Value;
				}

				i++;
			}

		    storeResult.Value = result;
		}
		
	}
}