//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;
using UnityEngine;
using UnityEditor;

public class CUResizableContainerStyle {

	private static CUResizableContainerStyle defaultStyle;
	public GUIStyle resizerVertical;
	public GUIStyle resizerHorizontal;
	
	private static CUEditorAssetUtility assetUtility;

	public static CUResizableContainerStyle DefaultStyle {
		get {
			if (defaultStyle == null) {
			 	defaultStyle = new CUResizableContainerStyle();
			}
			return defaultStyle;
		}
		set {
			defaultStyle = value;
		}
	}
	
	public CUResizableContainerStyle() { 
		resizerVertical = new GUIStyle();
		resizerVertical.fixedHeight = 6;
		resizerVertical.fixedWidth = 42;
		resizerVertical.margin = new RectOffset(0, 0, 1, 0);
		resizerVertical.imagePosition = ImagePosition.ImageOnly;
		if (EditorGUIUtility.isProSkin) {
			resizerVertical.normal.background = LoadTexture("CUResizeDarkVertical.png");	
		} else {
			resizerVertical.normal.background = LoadTexture("CUResizeLightVertical.png");	
		}

		resizerHorizontal = new GUIStyle();
		resizerHorizontal.fixedHeight = 42;
		resizerHorizontal.fixedWidth = 6;
		resizerHorizontal.margin = new RectOffset(1, 0, 0, 0);
		resizerHorizontal.imagePosition = ImagePosition.ImageOnly;
		if (EditorGUIUtility.isProSkin) {
			resizerHorizontal.normal.background = LoadTexture("CUResizeDarkHorizontal.png");	
		} else {
			resizerHorizontal.normal.background = LoadTexture("CUResizeLightHorizontal.png");	
		}
	}
	
	public static Texture2D LoadTexture(string name) { 
		if (assetUtility == null) {
			assetUtility = new CUEditorAssetUtility(CUEditorResourcesLocator.ResourcePath, "Chili4U", "CUEditorResourcesLocator");
		}
		return assetUtility.FindTexture(name);
    }
}

