//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;
using UnityEngine;
 
public class CUDefaultItemRenderer<T> : CUItemRenderer<T> {
	
	public float defaultHeight = 20f;
	
	public override float MeasureHeight (T item) {
		return defaultHeight;
	}
	
	public override void Arrange (T item, int itemIndex, bool selected, bool focused, Rect itemRect) {
		GUIStyle backgroundStyle = itemIndex % 2 == 1 ? ListStyle.oddBackground : ListStyle.evenBackground;
		backgroundStyle.Draw(itemRect, false, false, selected, focused);					
        ListStyle.item.Draw(itemRect, item.ToString(), true, false, selected, false);
	}
	
}
