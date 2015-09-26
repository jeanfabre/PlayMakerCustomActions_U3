// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vector2")]
	[Tooltip("Clamps the Magnitude of Vector2 Variable.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1012")]
	public class Vector2ClampMagnitude : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2")]
		public FsmVector2 vector2Variable;
		
		[RequiredField]
		[Tooltip("The maximum Magnitude")]
		public FsmFloat maxLength;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			vector2Variable = null;
			maxLength = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoVector2ClampMagnitude();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoVector2ClampMagnitude();
		}
		
		void DoVector2ClampMagnitude()
		{
			vector2Variable.Value = Vector2.ClampMagnitude(vector2Variable.Value, maxLength.Value);
		}
	}
}

