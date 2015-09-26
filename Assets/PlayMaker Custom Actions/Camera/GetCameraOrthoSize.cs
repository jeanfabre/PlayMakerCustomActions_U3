// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Gets the ortho size of the Camera.")]
	public class GetCameraOrthoSize : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The ortho size.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat orthoSize;
		
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			orthoSize = null;
			
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoGetCameraOrthoSize();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetCameraOrthoSize();
		}
		
		void DoGetCameraOrthoSize()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			Camera _camera = go.camera;
			if (_camera == null)
			{
				LogError("Missing Camera Component!");
				return;
			}

			orthoSize.Value = _camera.orthographicSize;
		}
	}
}
