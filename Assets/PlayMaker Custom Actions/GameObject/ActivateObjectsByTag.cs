// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
// original action by Graham: http://hutonggames.com/playmakerforum/index.php?topic=9547.msg45318#msg45318
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Activates or deactivates all Game Objects with a given tag.")]
	public class ActivateObjectsByTag : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The tag to filter objects to activate/deactivate")]
		[UIHint(UIHint.Tag)]
		public FsmString tag;
		
		[RequiredField]
        [Tooltip("Check to activate, uncheck to deactivate Game Object.")]
        public FsmBool activate;
		
		[Tooltip("Resets the affected game objects when exiting this state to their original activate state. Useful if you want an object to be controlled only while this state is active.")]
		public FsmBool resetOnExit;
		
		bool[] _activeStates;
		GameObject[] _gos;
		
		public override void Reset()
		{
			activate = false;
			tag = null;
			resetOnExit = false;
		}

		public override void OnEnter()
		{
			_gos = GameObject.FindGameObjectsWithTag(tag.Value);
			_activeStates = new bool[_gos.Length];
			
			int i= 0;
	        foreach (GameObject go in _gos) 
			{
				
				#if UNITY_3_5 || UNITY_3_4
					_activeStates[i] = go.active;
                	go.active = activate.Value;
				#else			
					_activeStates[i] = go.activeSelf;
				   	go.SetActive(activate.Value);
				#endif
				
				i++;
	        }

			Finish();
		}
		
		public override void OnExit()
		{
			if( resetOnExit.Value && _gos!=null)
			{
				int i= 0;
		        foreach (GameObject go in _gos) 
				{
					
					#if UNITY_3_5 || UNITY_3_4
	                	go.active = _activeStates[i];
					#else			
					   	go.SetActive(_activeStates[i]);
					#endif
					
					i++;
		        }
			}
		}
	}
}