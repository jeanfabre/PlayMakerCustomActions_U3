// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Gets various useful Time measurements. This advanced version allows you to select when to perform the action.")]
	public class GetTimeInfoAdvanced : FsmStateActionAdvanced
	{
		public enum TimeInfo
		{
			DeltaTime,
			TimeScale,
			SmoothDeltaTime,
			TimeInCurrentState,
			TimeSinceStartup,
			TimeSinceLevelLoad,
			RealTimeSinceStartup,
			RealTimeInCurrentState
		}
		
		public TimeInfo getInfo;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeValue;


		public override void Reset()
		{
			base.Reset();

			getInfo = TimeInfo.TimeSinceLevelLoad;
			storeValue = null;
		}

		public override void OnActionUpdate()
		{
			DoGetTimeInfo();
		}
		
		void DoGetTimeInfo()
		{
			switch (getInfo) 
			{
			
			case TimeInfo.DeltaTime:
				storeValue.Value = Time.deltaTime;
				break;
				
			case TimeInfo.TimeScale:
				storeValue.Value = Time.timeScale;
				break;
				
			case TimeInfo.SmoothDeltaTime:
				storeValue.Value = Time.smoothDeltaTime;
				break;
				
			case TimeInfo.TimeInCurrentState:
				storeValue.Value = State.StateTime;
				break;
			
			case TimeInfo.TimeSinceStartup:
				storeValue.Value = Time.time;
				break;
				
			case TimeInfo.TimeSinceLevelLoad:
				storeValue.Value = Time.timeSinceLevelLoad;
				break;
				
			case TimeInfo.RealTimeSinceStartup:
				storeValue.Value = FsmTime.RealtimeSinceStartup;
				break;
			
			case TimeInfo.RealTimeInCurrentState:
				storeValue.Value = FsmTime.RealtimeSinceStartup - State.RealStartTime;
				break;
				
			default:
				storeValue.Value = 0f;
				break;
			}
		}
	}
}