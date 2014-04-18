//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Control to vertically or horizontally resize other controls
/// </summary>
public class CUResizableContainer {

	private static int ControlHint = "resizableContainer".GetHashCode();
	private static Stack resizableContainerStates = new Stack();

	public static float BeginVertical(float height, params GUILayoutOption[] options) {
		return BeginVertical(height, 0f, float.MaxValue, HandlePosition.After, options);
	}
	
	public static float BeginVertical(float height, HandlePosition handlePosition, params GUILayoutOption[] options) {
		return BeginVertical(height, 0f, float.MaxValue, handlePosition, options);
	}

	public static float BeginVertical(float height, float minHeight, float maxHeight, params GUILayoutOption[] options) {
		return BeginVertical(height, minHeight, maxHeight, HandlePosition.After, options);	
	}
	
	public static float BeginVertical(float height, float minHeight, float maxHeight, HandlePosition handlePosition, params GUILayoutOption[] options) {
		return Begin(height, minHeight, maxHeight, true, handlePosition, options);	
	}
	
	public static float BeginHorizontal(float width, params GUILayoutOption[] options) {
		return BeginHorizontal(width, 0f, float.MaxValue, HandlePosition.After, options);
	}

	public static float BeginHorizontal(float width, HandlePosition handlePosition, params GUILayoutOption[] options) {
		return BeginHorizontal(width, 0f, float.MaxValue, handlePosition, options);
	}

	public static float BeginHorizontal(float width, float minWidth, float maxWidth, params GUILayoutOption[] options) {
		return BeginHorizontal(width, minWidth, maxWidth, HandlePosition.After, options);
	}

	public static float BeginHorizontal(float width, float minWidth, float maxWidth, HandlePosition handlePosition, params GUILayoutOption[] options) {
		return Begin(width, minWidth, maxWidth, false, handlePosition, options);
	}

	private static float Begin(float size, float minSize, float maxSize, bool vertical, HandlePosition handlePosition, params GUILayoutOption[] options) {
		int controlId = GUIUtility.GetControlID(ControlHint, FocusType.Passive);
		ResizableContainerState state = (ResizableContainerState) GUIUtility.GetStateObject(typeof(ResizableContainerState), controlId);
		
		state.vertical = vertical;
		state.controlId = controlId;
		state.handlePosition = handlePosition;
		
		if (state.applySize) {
			size = Mathf.Max(minSize, Mathf.Min(state.size, maxSize));
			state.applySize = false;
		} else {
			state.size = size;
		}
		
		resizableContainerStates.Push(state);
		
		GUILayoutOption[] newOptions;
		if (options == null || options.Length == 0) {
			newOptions = new GUILayoutOption[1];
		} else {
			newOptions = new GUILayoutOption[options.Length + 1];
			for(int i = 0; i < options.Length; i++) {
				newOptions[i] = options[i];
			}
		}
		
		if (vertical) {
			newOptions[newOptions.Length - 1] = GUILayout.Height(size);
			EditorGUILayout.BeginVertical(newOptions);	
		} else {
			newOptions[newOptions.Length - 1] = GUILayout.Width(size);
			EditorGUILayout.BeginHorizontal(newOptions);	
		}
		
		if (handlePosition == HandlePosition.Before) {
			DrawHandle(state);
		}
		return size;
	}

	public static void EndVertical() {
		End();
	}
	
	public static void EndHorizontal() {
		End();
	}
	
	private static void End() {
		ResizableContainerState state = (ResizableContainerState) resizableContainerStates.Pop();
		if (state.handlePosition == HandlePosition.After) {
			DrawHandle(state);
		}
		if (state.vertical) {
			EditorGUILayout.EndVertical();
		} else {
			EditorGUILayout.EndHorizontal();
		}
	}
	
	private static void DrawHandle(ResizableContainerState state) {
		Rect rect = new Rect();
		if (state.vertical) {
			EditorGUILayout.BeginHorizontal(GUILayout.Height(6));
			GUILayout.FlexibleSpace();
			rect = GUILayoutUtility.GetRect(42, 6, CUResizableContainerStyle.DefaultStyle.resizerVertical);
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
			EditorGUIUtility.AddCursorRect(rect, MouseCursor.ResizeVertical);
			GUI.Box(rect, "", CUResizableContainerStyle.DefaultStyle.resizerVertical);
		} else {
			EditorGUILayout.BeginVertical(GUILayout.Width(6));
			GUILayout.FlexibleSpace();
			rect = GUILayoutUtility.GetRect(6, 42, CUResizableContainerStyle.DefaultStyle.resizerHorizontal);
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndVertical();
			EditorGUIUtility.AddCursorRect(rect, MouseCursor.ResizeHorizontal);
			GUI.Box(rect, "", CUResizableContainerStyle.DefaultStyle.resizerHorizontal);
		}
		
		switch (Event.current.GetTypeForControl(state.controlId)) {
			case EventType.MouseDown:
				if (!rect.Contains(Event.current.mousePosition) || GUIUtility.hotControl != 0) {
					break;
				}
				GUIUtility.hotControl = state.controlId;
				Event.current.Use();
				break;
			
			case EventType.MouseUp:
				if (GUIUtility.hotControl != state.controlId) {
					break;
				}
				GUIUtility.hotControl = 0;
				Event.current.Use();
				break;
			
			case EventType.MouseDrag:
				if (GUIUtility.hotControl != state.controlId) {
					break;
				}
				float delta = state.vertical ? Event.current.delta.y : Event.current.delta.x;
				if (state.handlePosition == HandlePosition.Before) {
					delta *= -1;
				}
				state.size += delta;
				state.applySize = true;			
				Event.current.Use();
				break;
		}	
	}
	
	public enum HandlePosition {
		Before,
		After
	}
	
	internal sealed class ResizableContainerState {
		public float size = 0;
		public bool applySize = false;
		public int controlId;	
		public bool vertical;
		public HandlePosition handlePosition;
	}
}

