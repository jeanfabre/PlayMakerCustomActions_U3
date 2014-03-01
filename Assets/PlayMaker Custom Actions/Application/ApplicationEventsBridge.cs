using UnityEngine;
using System.Collections;
using HutongGames;

public class ApplicationEventsBridge : MonoBehaviour {
	
	
	void OnApplicationQuit()
	{
		PlayMakerFSM.BroadcastEvent("APPLICATION QUIT");
	}
	void OnApplicationPause(bool pause)
	{
		
		PlayMakerFSM.BroadcastEvent("APPLICATION PAUSE");
	}
	void OnApplicationFocus(bool focus)
	{
		PlayMakerFSM.BroadcastEvent("APPLICATION FOCUS");
	}
	
}
