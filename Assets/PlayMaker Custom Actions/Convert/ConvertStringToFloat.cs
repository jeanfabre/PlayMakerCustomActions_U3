// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// Added by DjaDino
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts an String value to an Float value.")]
	public class ConvertStringToFloat : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The String variable to convert to a float value.")]
		public FsmString stringVariable;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the result in an Int variable.")]
		public FsmFloat floatVariable;


        [Tooltip("Repeat every frame. Useful if the String variable is changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			floatVariable = null;

			stringVariable = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoConvertStringToInt();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoConvertStringToInt();
		}
		
		void DoConvertStringToInt()
		{
			floatVariable.Value = float.Parse(stringVariable.Value);
		}
	}
}
