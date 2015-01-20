// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets information from the Host when a template is running as a Sub FSM.")]
	public class GetHostInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmString hostFsmName;

		[UIHint(UIHint.Variable)]
		public FsmString gameObjectName;

		[UIHint(UIHint.Variable)]
		public FsmGameObject gameObject;

		[UIHint(UIHint.Variable)]
		public FsmString currentStateName;

		public bool everyFrame;

		public override void Reset()
		{
			hostFsmName = null;
			gameObjectName = null;
			gameObject = null;
			currentStateName = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GetInfo();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			GetInfo();
		}

		public void GetInfo()
		{
			if (hostFsmName.Value != null){
				hostFsmName.Value = Fsm.Host.Name;
			}

			if (gameObject.Value != null){
				gameObject.Value = Fsm.Host.GameObject;
			}

			if (gameObjectName.Value != null){
				gameObjectName.Value = Fsm.Host.GameObjectName;
			}

			if (currentStateName != null){
				currentStateName.Value = Fsm.Host.ActiveStateName;
			}
		}
	}
}