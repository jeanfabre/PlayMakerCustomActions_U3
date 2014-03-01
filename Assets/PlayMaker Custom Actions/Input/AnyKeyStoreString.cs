// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when the user hits any Key or Mouse Button.")]
	public class AnyKeyStoreString : FsmStateAction
	{
		[RequiredField]
		public FsmEvent sendEvent;
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

		public override void Reset()
		{
			sendEvent = null;
			storeResult = null;
		}

		public override void OnUpdate()
		{
			storeResult.Value = Input.inputString;
			if (Input.anyKey)
				Fsm.Event(sendEvent);		
		}
	}
}