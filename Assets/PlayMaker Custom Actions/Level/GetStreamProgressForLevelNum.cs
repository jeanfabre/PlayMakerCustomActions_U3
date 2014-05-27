// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Get the Stream progres for a level id")]
	public class GetStreamProgressForLevelNum : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The id of the level to load. NOTE: Must be in the list of levels defined in File->Build Settings... ")]
		public FsmInt level;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the loading progress: range 0-1")]
		public FsmFloat loadingProgress;
		
		[Tooltip("Event to send when the level has loaded. NOTE: This only makes sense if the FSM is still in the scene!")]
		public FsmEvent loadedEvent;
		
		public override void Reset()
		{
			level = null;
			loadingProgress = null;
			loadedEvent = null;
		}

		public override void OnEnter()
		{
			GetProgress();
		}

		public override void OnUpdate()
		{
			GetProgress();
		}

		void GetProgress()
		{
			loadingProgress.Value = Application.GetStreamProgressForLevel(level.Value);
		
			if (loadingProgress.Value == 1f)
			{
				Fsm.Event(loadedEvent);
				Finish();
			}
		}
	}
}

