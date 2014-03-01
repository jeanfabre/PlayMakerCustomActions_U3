// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("GUI Begin Size.")]
	public class GUIBeginSize : FsmStateAction
	{
		public override void OnGUI()
		{
			base.OnGUI();

            GUISizer.BeginGUI();
		}
	}
}