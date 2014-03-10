using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the color of the particles in a Shuriken System")]
	public class SetParticleColor : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		public FsmColor particleColor;
		private GameObject go;
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			particleColor = Color.white;
			go = null;
			everyFrame = false;
		}
		public override void OnEnter()
		{
			DoSetColor();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoSetColor();
		}
		
		void DoSetColor()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			go.particleSystem.startColor = particleColor.Value;
		}
	}
}