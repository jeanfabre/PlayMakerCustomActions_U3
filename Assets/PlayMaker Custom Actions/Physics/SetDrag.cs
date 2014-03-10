// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the Drag of a Game Object's Rigid Body.")]
	public class SetDrag : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[HasFloatSlider(0.0f,10f)]
		public FsmFloat drag;
		
		[Tooltip("Repeat every frame. Typically this would be set to True.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			drag = 1;
		}

		public override void OnEnter()
		{
			DoSetDrag();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetDrag();
		}

		void DoSetDrag()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			if (go.rigidbody == null) return;
			
			go.rigidbody.drag = drag.Value;
		}
	}
}