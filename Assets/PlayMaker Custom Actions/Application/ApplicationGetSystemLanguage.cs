// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Get System Language")]
	public class ApplicationGetSystemLanguage : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The application language")]
		public FsmString language;

		public override void Reset()
		{
			language = null;
		}

		public override void OnEnter()
		{
			language.Value = Application.systemLanguage.ToString();
			
			Finish();
		}

	}
}