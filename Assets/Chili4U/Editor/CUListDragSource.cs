//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;
using System.Collections.Generic;

/// <summary>
/// Interface used by the list control to start drag and drop operations
/// </summary>
public interface CUListDragSource {
	
	/// <summary>
	/// Determines whether a DnD operation can be started for the provided indices or not
	/// </summary>
	/// <returns>
	/// <c>true</c> if a DnD operation is possible, <c>false</c> otherwise.
	/// </returns>
	/// <param name='indices'>
	/// the indices to drag
	/// </param>
	bool CanDrag(IList<int> indices);
	
	/// <summary>
	/// Initializes the DnD operation.
	/// </summary>
	/// <param name='indices'>
	/// the indices to drag
	/// </param>
	void InitializeDrag(IList<int> indices);
	
}
