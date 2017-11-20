﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  


public class NavyUIHelper : EditorWindow {

// 	[MenuItem("Window/NavyUIHelper  %r")]  
// 	private static void GenerateUI()
// 	{
// 		foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) {
// 			Debug.Log (obj.name);
// 			UIHelper helper = obj.GetComponent<UIHelper> ();
// 			if (null != helper)
// 				helper.GenerateUI ();
// 		}
// 	}

    //添加菜单
    [MenuItem(@"Tool/NavyUIHelper Command #&r")]

    public static void GetTransforms()
    {
        Dictionary<string, Vector3> dic = new Dictionary<string, Vector3>();
        //transforms是Selection类的静态字段，其返回的是选中的对象的Transform
        Transform[] transforms = Selection.transforms;

        //将选中的对象的postion保存在字典中
        for (int i = 0; i < transforms.Length; i++)
        {
            dic.Add(transforms[i].name, transforms[i].position);
        }

        //将字典中的信息打印出来
        foreach (Transform item in transforms)
        {
            Debug.Log(item.name + ":" + item.position);
            UIHelper helper = item.gameObject.AddComponent<UIHelper>();
            if (null != helper)
            {
                helper.NGUI = true;
                helper.className = item.name;
                helper.parentName = "WindowBase";
                helper.GenerateUI();
            }
            DestroyImmediate(item.gameObject.GetComponent<UIHelper>());
        }
    }
}
