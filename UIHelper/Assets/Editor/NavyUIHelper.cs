using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  


public class NavyUIHelper : EditorWindow {

	[MenuItem("Window/NavyUIHelper")]  
	private static void GenerateUI()
	{
		foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) {
			Debug.Log (obj.name);
			UIHelper helper = obj.GetComponent<UIHelper> ();
			if (null != helper)
				helper.GenerateUI ();
		}
	}
}
