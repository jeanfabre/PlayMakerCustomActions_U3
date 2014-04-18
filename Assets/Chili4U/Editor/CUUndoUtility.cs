using UnityEngine;
using UnityEditor;
using System.Collections;
using UObject = UnityEngine.Object;

/// <summary>
/// Utility abstracting away the Undo system changes introduced in Unity 4.3.
/// </summary>
public class CUUndoUtility {

	public static void RegisterUndo(UObject objectToUndo, string message) {
#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
		Undo.RegisterUndo(objectToUndo, message);
#else
		// Unity 4.3+
		Undo.RecordObject(objectToUndo, message);
#endif
	}

	public static void RegisterUndo(UObject[] objectsToUndo, string message) {
#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
		Undo.RegisterUndo(objectsToUndo, message);
#else
		// Unity 4.3+
		Undo.RecordObjects(objectsToUndo, message);
#endif
	}

}
