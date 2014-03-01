// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("GUI End Size.")]
	public class GUIEndSize : FsmStateAction
	{
		public override void OnGUI()
		{
			base.OnGUI();

            GUISizer.EndGUI();
		}
	}
}