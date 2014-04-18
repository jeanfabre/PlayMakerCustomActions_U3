//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;

/// <summary>
/// All possible drop types during a drag and drop operation
/// </summary>
public enum CUDropType {
	/// <summary>
	/// Drop at a specific position. E.g. between items
	/// </summary>
	AtPosition,
	/// <summary>
	/// Drop into a specific item. E.g. move from one folder to another
	/// </summary>
	IntoItem,
	/// <summary>
	/// Drop into the whole container. Special case to change the state of an item for example.
	/// </summary>
	IntoContainer
}

