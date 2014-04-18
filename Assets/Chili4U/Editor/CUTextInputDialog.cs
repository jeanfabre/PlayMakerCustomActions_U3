//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Simple text input dialog
/// </summary>
[Serializable]
public class CUTextInputDialog : EditorWindow {
	
	private static int Width = 300;
	private static int Height = 70;
	
	private static CUTextInputDialog instance;
	private static bool visible = false;
	private static bool forceDelayedClose = false;
	private static bool wasGuiPaintedAtLeastOnce = false;
	
	private string label = "";
	private string value = "";
	private bool valid = true;
	private ValueAvailable okCallback;
	private VerifyValue verifyCallback;
	
	public static CUTextInputDialog Instance {
		get {
			if (instance == null) {
				instance = ScriptableObject.CreateInstance<CUTextInputDialog>();
			}
			return instance;
		}
	}
	
	/// <summary>
	/// Opens the input dialog
	/// </summary>
	/// <param name='label'>
	/// Label for the text field
	/// </param>
	/// <param name='value'>
	/// Initial value of the text field
	/// </param>
	/// <param name='okCallback'>
	/// Callback that will be invoked if the dialog is closed via ok
	/// </param>
	public static CUTextInputDialog ShowDialog(string label, string value, ValueAvailable okCallback) {
		return ShowDialog(label, value, okCallback, null);
	}
	
	/// <summary>
	/// Opens the input dialog
	/// </summary>
	/// <param name='label'>
	/// Label for the text field
	/// </param>
	/// <param name='value'>
	/// Initial value of the text field
	/// </param>
	/// <param name='okCallback'>
	/// Callback that will be invoked if the dialog is closed via ok
	/// </param>
	/// <param name='verifyCallback'>
	/// Callback that will be invoked after the value was changed to check if the ok button is available
	/// </param>
	public static CUTextInputDialog ShowDialog(string label, string value, ValueAvailable okCallback, VerifyValue verifyCallback) {
		if (!visible) {
			Instance.Display(label, value, okCallback, verifyCallback);
		}
		return Instance;
	}

	public void CenterAt(Vector2 center) {
		position = CalculatePosition(center);
	}
	
	private void Display(string label, string value, ValueAvailable okCallback, VerifyValue verifyCallback) {
		this.label = label;
		this.value = value;
		this.okCallback = okCallback; 
		this.verifyCallback = verifyCallback;
		visible = true;
		wasGuiPaintedAtLeastOnce = false;
		forceDelayedClose = false;
		Validate();
		CenterAt(new Vector2(400, 400));
		ShowPopup();
		Focus();
	}
	
	private Rect CalculatePosition(Vector2 center) {
		return new Rect(Mathf.Max(0, center.x - Width / 2), Mathf.Max(0, center.y - Height / 2), Width, Height);
	}
	
	/// <summary>
	/// Clsoes the input dialog.
	/// </summary>
	public static void HideDialog() {
		if (visible) {
			Instance.Hide();
		}
	}
	
	private void Hide() {
		// release delegate
		verifyCallback = null;
		okCallback = null;
		visible = false;
		forceDelayedClose = false;
		Close();
	}
	
	public void OnInspectorUpdate() {
		// closing the dialog within OnGUI or OnLostFocus leads to several exceptions but this position seems perfect
		if (forceDelayedClose) {
			Hide();
		}
	}
	
	public void OnGUI() {		
		GUI.SetNextControlName("InputTextField");
		value = EditorGUILayout.TextField(label, value);
		if (GUI.GetNameOfFocusedControl() == string.Empty) {
			// move focus to the textfield after the dialog is visible
		    GUI.FocusControl("InputTextField");
		}		
		
		if (GUI.changed) {
			Validate();
		}
		
		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUI.enabled = valid;
		if (GUILayout.Button("Ok")) {
			Apply();
		}
		GUI.enabled = true;
		if (GUILayout.Button("Cancel")) {
			CloseDialog();
		}
		EditorGUILayout.EndHorizontal();		
		
		if (Event.current.type == EventType.KeyUp) {
			if (Event.current.keyCode == KeyCode.Escape) {
				CloseDialog();
			} else if (valid && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter)) {
				Apply();
			}
		}	
		wasGuiPaintedAtLeastOnce = true;
	}
	
	private void Validate() {
		if (verifyCallback != null) {
			valid = verifyCallback(value);
		} else {
			valid = true;
		}
	}
	
	private void Apply() {
		if (okCallback != null) {
			okCallback(value);
		}
		CloseDialog();
	}
	
	private void CloseDialog() {
		Hide();
		Event.current.Use();	
	}
	
	public void OnLostFocus() {
		forceDelayedClose = wasGuiPaintedAtLeastOnce;
	}
	
	
	public delegate bool VerifyValue(string value);
	
	public delegate void ValueAvailable(string value);
}
