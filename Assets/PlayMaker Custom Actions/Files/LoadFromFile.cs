// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Files")]
	[Tooltip("Load a File into a string")]
	public class LoadFromFile : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The file name")]
		public FsmString filePath;
		
		[RequiredField]
		[Tooltip("The text")]
		[UIHint(UIHint.Variable)]
		public FsmString text;
		
		public FsmEvent successEvent;
		public FsmEvent failureEvent;
		
		
		public override void Reset()
		{
			filePath = null;
			text = null;
			
		}
		
		
		public override void OnEnter()
		{
			if (Load())
			{
				Fsm.Event(successEvent);
			}else{
				Fsm.Event(failureEvent);
			}
			
			Finish ();
		}

		
		private bool Load()
		{
			if ( string.IsNullOrEmpty(filePath.Value) )
			{
				return false;
			}
			
			//text.Value = System.IO.File.ReadAllText(filePath.Value);// System.IO.File.ReadAllText(filePath.Value);
			
			return true;
		}

	}
}

