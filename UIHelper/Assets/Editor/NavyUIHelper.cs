using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using test;

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

	[MenuItem(@"Tool/MD4  ")]  
	private static void GenerateUI()
	{
        int filedID = UnityEditor.Build.Utilities.FileIDUtil.Compute(typeof(test11));
        Debug.Log("filedId =>" + filedID.ToString());
    }
    //添加菜单
    [MenuItem(@"Tool/NavyUIHelper #&r")]

    public static void GetTransforms()
    {
        Dictionary<string, Vector3> dic = new Dictionary<string, Vector3>();
        //transforms是Selection类的静态字段，其返回的是选中的对象的Transform
        Transform[] transforms = Selection.transforms;

        if(transforms.Length == 0)
        {
            Debug.Log("请选择需要生成UI类的预设或者UI根节点(在Hierachy窗口上选取)");
        }
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
                helper.className = item.name + "Base";
                helper.parentName = "WindowBase";
                helper.GenerateUI();
            }
            DestroyImmediate(item.gameObject.GetComponent<UIHelper>());
        }
    }
}
