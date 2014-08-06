// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Specifies logical orientation of the screen. Default value is taken from the 'Default Orientation' in Player Settings. Currently screen orientation is only relevant on mobile platforms")]
	public class SetDeviceScreenOrientation : FsmStateAction
	{
		[Tooltip("Amount of acceleration under which to trigger the event. Use low numbers.")]
		public ScreenOrientation orientation;
		
		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			orientation = ScreenOrientation.Landscape;
			everyFrame = false;
		}
		
		public override void OnUpdate()
		{
			Screen.orientation = orientation;
			
			if (!everyFrame)
			{
				Finish();
			}
		}

	}
}