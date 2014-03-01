using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("Breed")]
[Tooltip("Create a new breed using a a Prefab")]
public class BreedCreate : FsmStateAction
{
	
	[RequiredField]
	[Tooltip("The name of the breed to create")]
	public FsmString name;

	
	[RequiredField]
	[Tooltip("GameObject to create. Usually a Prefab.")]
	public FsmGameObject gameObject;
	
	// Code that runs on entering the state.
	public override void OnEnter()
	{
		//Breed.Instance().Create (name.Value, gameObject.Value);
		Finish();
	}


}
