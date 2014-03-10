using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the Start Speed of a Shuriken System")]
	public class SetParticleSpeed : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		public FsmFloat startSpeed;
		private GameObject go;
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			startSpeed = 0.0f;
			go = null;
			everyFrame = false;
		}
		public override void OnEnter()
		{
			DoSetSpeed();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoSetSpeed();
		}
		
		void DoSetSpeed()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			go.particleSystem.startSpeed = startSpeed.Value;
		}
	}
}