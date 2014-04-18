//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;

/// <summary>
/// Interface used by the list control to support context menu events
/// </summary>
public interface CUListContextMenuListener {
	
	/// <summary>
	/// Invoked to display a context menu
	/// </summary>
	/// <param name='index'>
	/// The index of the clicked item or -1 if the click was in the empty part of the list
	/// </param>
	void HandleContextMenu(int index);
	
}

