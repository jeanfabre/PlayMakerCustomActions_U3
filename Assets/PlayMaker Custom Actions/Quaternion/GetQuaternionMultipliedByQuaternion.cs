// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Quaternion")]
	[Tooltip("Get the quaternion from a quaternion multiplied by a quaternion.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W969")]
	public class GetQuaternionMultipliedByQuaternion : QuaternionBaseAction
	{

		[RequiredField]
		[Tooltip("The first quaternion to multiply")]
		public FsmQuaternion quaternionA;
		
		[RequiredField]
		[Tooltip("The second quaternion to multiply")]
		public FsmQuaternion quaternionB;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting quaternion")]
		public FsmQuaternion result;

		public override void Reset()
		{
			quaternionA = null;
			quaternionB = null;
	
			result = null;
			everyFrame = false;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			DoQuatMult();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatMult();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatMult();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatMult();
			}
		}
		
		void DoQuatMult()
		{
			result.Value = quaternionA.Value * quaternionB.Value;		
		}
	}
}