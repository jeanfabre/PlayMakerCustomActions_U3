//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;
using UnityEngine;

public abstract class CUItemRenderer<T>  {
	
	private CUListStyle listStyle;
	
	public CUListStyle ListStyle {
		get {
			if (listStyle == null) {
				listStyle = CUListStyle.DefaultStyle;
			}
			return listStyle;
		}
		set {
			listStyle = value;
		}
	}
	
	public abstract float MeasureHeight(T item);
	
	public abstract void Arrange(T item, int itemIndex, bool selected, bool focused, Rect itemRect);
	
}

