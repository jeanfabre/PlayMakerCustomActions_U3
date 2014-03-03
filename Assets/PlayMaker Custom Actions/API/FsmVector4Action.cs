// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
// PLAYMAKER ECOSYSTEM DO NOT EDIT
/*--- 
<<<<<<< HEAD
EcoMetaStart
=======
>>>>>>> FETCH_HEAD
{
"type":"__ECO_ACTION__",
"script dependancies":["FsmVector4ActionEditor.cs"]
}
<<<<<<< HEAD
EcoMetaEnd
=======
>>>>>>> FETCH_HEAD
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vector4")]
	public class FsmVector4Action : FsmStateAction
	{

		public FsmQuaternion vector4;
		
		[UIHint(UIHint.Variable)]
		public FsmQuaternion storeVector4;
		
		public bool everyframe;
		
		public override void Reset()
		{
			vector4=null;
			storeVector4 = null;
			everyframe = true;
		}

		public override void OnEnter()
		{

			storeVector4.Value = vector4.Value;
			
			Debug.Log(storeVector4.Value.x+","+storeVector4.Value.y+","+storeVector4.Value.z+","+storeVector4.Value.w);
			if (!everyframe)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			storeVector4.Value = vector4.Value;
		}
	}
}

