// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
// PLAYMAKER ECOSYSTEM DO NOT EDIT
/*---
EcoMetaStart
{
"type":"__ECO_ACTION__",
"script dependancies":["Assets/PlayMaker Custom Actions/GUI/Internal/GUISizer.cs"]
}
EcoMetaEnd
---*/

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