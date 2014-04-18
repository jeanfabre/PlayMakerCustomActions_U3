using System.IO;
using UnityEngine;
using System;
using UnityEditor;

public class CUEditorAssetUtility
{
	
	private string productFolderName;
	private string configScriptName;
	private string basePath;
	private bool errorPrinted = false;
	
	public CUEditorAssetUtility(string basePath, string productFolderName, string configScriptName) {
		this.basePath = basePath;
		this.productFolderName = productFolderName;
		this.configScriptName = configScriptName;
	}
	
	/// <summary>
	/// Finds a texture with a unique name within the project. If a texture is not found an error message will be printed.
	/// </summary>
	/// <returns>
	/// The texture or null if no such texture could be found.
	/// </returns>
	public Texture2D FindTexture (string name)
	{
		 var result = AssetDatabase.LoadMainAssetAtPath("Assets/" + basePath+"/"+name) as Texture2D;
		if (result == null) {
			if (!errorPrinted) {
				Debug.LogWarning("It seems that you moved the "+productFolderName+" folder within the project. Please open the " + 
					configScriptName + " script (use Unity's search function to locate it) and update the new folder path in there.\nIf you accidently deleted the " + 
					productFolderName + " folder or it's contents please re-import it into the project.");
				errorPrinted = true;
			}
		}
		return result;
	} 
	 
	// ------ EVERYTHING BELOW HERE IS OBSOLETE AND WILL BE REMOVED IN A FUTURE VERSION -------

	/// <summary>
	/// Gets the project root.
	/// </summary>
	public static DirectoryInfo ProjectRoot {
		get {
			return new DirectoryInfo (Application.dataPath).Parent;
		}
	}
	
	/// <summary>
	/// Finds a texture with a unique name within the project.
	/// </summary>
	/// <returns>
	/// The texture or null if no such texture could be found.
	/// </returns>
	public static Texture2D FindTextureByName (string withinDirectory, string name)
	{
		var root = ProjectRoot;
		FileInfo foundFile = null;
		
		var files = root.GetFiles (name, SearchOption.AllDirectories);
		foreach (var file in files) {
			if (file.FullName.Replace("\\","/").Contains ("/"+withinDirectory+"/")) {
				foundFile = file;
				break;
			}
		}
		
		if (foundFile == null) {
			return null;
		}
		
		var rootName = root.FullName;
		var fullName = foundFile.FullName;
		
		rootName = rootName.Replace ("\\", "/");
		fullName = fullName.Replace ("\\", "/");
		var path = fullName.Substring (rootName.Length + 1);
		return AssetDatabase.LoadMainAssetAtPath (path) as Texture2D;
	} 
	
}