//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;
using UnityEngine;

/// <summary>
/// Interface used by the list control to send key events
/// </summary>
public interface CUListKeyListener {
	
	/// <summary>
	/// Called by the list control for every unhandled key event. Depending on the event only the keyCode or the character is set.
	/// </summary>
	/// <returns>
	/// whether or not the event was handled
	/// </returns>
	/// <param name='keyCode'>
	/// the key code of the event or KeyEvent.None
	/// </param>
	/// <param name='character'>
	/// the character of the event or '\0' if there is no character
	/// </param>
	/// <param name='modifier'>
	/// all modifiers keys
	/// </param>
	bool HandleKeyEvent(KeyCode keyCode, char character, EventModifiers modifiers);
		
}

