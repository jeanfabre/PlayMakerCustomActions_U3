//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;

/// <summary>
/// Interface used by the list control to complete drag and drop operations
/// </summary>
public interface CUListDropTarget {
	
	/// <summary>
	/// Determines whether a DnD operation with the provided type can be completed at the specified index
	/// </summary>
	/// <returns>
	/// <c>true</c> if a DnD operation is possible, <c>false</c> otherwise.
	/// </returns>
	/// <param name='index'>
	/// the index to drop at. 
	/// 0 <= index <= list.length for dropType AtPosition to support drop before, between and after items.
	/// 0 <= index < list.length for dropType IntoItem.
	/// -1 for dropType IntoContainer.
	/// </param>
	/// <param name='dropType'>
	/// the type of the current drop operation
	/// </param>
	bool CanDrop(int index, CUDropType dropType);
	
	/// <summary>
	/// Completes the DnD operation.
	/// </summary>
	/// <param name='index'>
	/// the index to drop at. 
	/// 0 <= index <= list.length for dropType AtPosition to support drop before, between and after items.
	/// 0 <= index < list.length for dropType IntoItem.
	/// -1 for dropType IntoContainer.
	/// </param>
	/// <param name='dropType'>
	/// the type of the current drop operation
	/// </param>
	void AcceptDrop(int index, CUDropType dropType);
	
}
