//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;
using System.Collections.Generic;

/// <summary>
/// Interface used by the list control to support execution of stuff via double click or enter key
/// </summary>
public interface CUListExecutionListener {
	
	/// <summary>
	/// Invoked to execute an action
	/// </summary>
	/// <returns>
	/// whether or not the event was handled
	/// </returns>
	/// <param name='indices'>
	/// The indices of the selected items
	/// </param>
	bool HandleExecution(List<int> indices);
	
}

