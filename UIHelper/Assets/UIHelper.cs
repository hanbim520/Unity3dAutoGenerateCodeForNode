﻿/*
    by zhanghaijun 710605420@qq.com
    命名规则：
       凡是将要在代码中使用的对象，命名必须以@开头或者包含，对象名必须包含而且只能包含一种unity类型（可不分大小写，但是必须正确）,挂载脚本的对象如果带@ 也会被处理，所以挂载对象不要加@,
    className 为生成的类名，默认为对象名，parentName为父类，默认为MonoBehaviour。目前只生成了C# 脚本，暂时没有生成LUA脚本。可以父节点与子节点同时挂载脚本，但是会生成两份类文件，
    父节点生成文件不包含子类的所有节点，也就是说，子类自动截断节点。
    其他命名，请跟进C#语言命名规则。
    如有bug，请提交710605420@qq.com邮箱。
 */
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System.IO;

public class UIHelper : MonoBehaviour
{
    [HideInInspector]
	public string flag = @"@";
    [HideInInspector]
    private string objectPath = @"";
    [HideInInspector]
    private Stack<Transform> preTrans = new Stack<Transform>();
	private Dictionary<string,string> generateUIComponentDic = new Dictionary<string, string> ();
	private List<string> TypeList = new List<string> ();
	string bodyStr = "";
	[SerializeField]
	public string className = ""; //default transform.name;
    [SerializeField]
    public string parentName = "MonoBehaviour";
    [SerializeField]
    bool NGUI = false;

    //     private string defineArea = "\n";
    //     private string defineFind = "\n";

    private List<string> defineAreas = new List<string>();
    private List<string> defineFinds = new List<string>();

    public void GenerateUI() 
    {
        preTrans.Clear();
		TypeList.Clear ();
		TypeList.Add (ObjectType.AudioSettings);
		TypeList.Add (ObjectType.AudioSource);
		TypeList.Add (ObjectType.Button);
		TypeList.Add (ObjectType.EventSystem);
		TypeList.Add (ObjectType.GameObject);
		TypeList.Add (ObjectType.Grid);
		TypeList.Add (ObjectType.Image);
		TypeList.Add (ObjectType.Light);
		TypeList.Add (ObjectType.Mesh);
		TypeList.Add (ObjectType.Panel);
		TypeList.Add (ObjectType.ParticleSystem);
		TypeList.Add (ObjectType.RawImage);
		TypeList.Add (ObjectType.Rigidbody);
		TypeList.Add (ObjectType.ScrollView);
		TypeList.Add (ObjectType.Sprite);
		TypeList.Add (ObjectType.TextMesh);
		TypeList.Add (ObjectType.Texture);
		TypeList.Add (ObjectType.Texture2D);
		TypeList.Add (ObjectType.Toggle);
		TypeList.Add (ObjectType.Transform);
        TypeList.Add(ObjectType.Text);

		string selfName = transform.name;
		MatchCollection m = Regex.Matches(selfName,flag,RegexOptions.IgnoreCase);
		if (m.Count > 0) {
			selfName = System.Text.RegularExpressions.Regex.Replace (selfName, flag, "");

		}
        if(string.IsNullOrEmpty(className))
		    className = selfName;
        objectPath = "";
		generateUIComponentDic.Clear ();
        GetChildren(transform);
		GenerateCSharpCode ();


    }

	void ColloctObjec(Transform uiObj)
	{
		string objectName = uiObj.name;
		MatchCollection m = Regex.Matches(objectName,flag,RegexOptions.IgnoreCase);
		if (m.Count > 0) {
			string objName = System.Text.RegularExpressions.Regex.Replace(objectName,flag,"");
//			string objPath = System.Text.RegularExpressions.Regex.Replace(objectPath,flag,"");
			generateUIComponentDic.Add (objName, objectPath);
		}
	}
    void GetChildren(Transform trans)
    {
        if(preTrans.Count == 0)
            preTrans.Push(trans);
		else if (trans.parent == preTrans.Peek())
			preTrans.Push(trans);
        if (trans.childCount ==0 )
        {
            objectPath = "";
            foreach (Transform preT in preTrans)
            {
                objectPath = preT.name + "/" + objectPath;
            }
            Debug.Log(objectPath );
            Transform lastPre =  preTrans.Pop();
			ColloctObjec (lastPre);

        }
        else
        {
            objectPath = "";
            foreach (Transform preT in preTrans)
            {
                objectPath = preT.name + "/" + objectPath;
            }
            Debug.Log(objectPath );
			Transform lastPre =  preTrans.Peek();
			ColloctObjec (lastPre);
        }
        for (int i = 0; i < trans.childCount; ++i)
        {
            Transform tranChild = trans.GetChild(i);
			if(tranChild.GetComponent<UIHelper>() != null)
				continue;
            GetChildren(tranChild);
			if (i == (trans.childCount - 1))
				preTrans.Pop ();
				
        }
    }

   
   string DefineArea (string type, string param)
   {
       return string.Format("\t[HideInInspector]private {0} @{1} = null;\n", type, param);
   }

    string DefineFind(string param,string path, string type)
    {
        return string.Format("\t\t @{0} = transform.Find({1}).GetComponent<{2}>(); \n", param, "\"" + path + "\"", type);
    }
    private void Serizerize(string uiName, string path, string ty)
	{
        int idx = path.IndexOf("/");
        string uiPath = path.Substring(idx + 1, path.Length - idx -1);

        MatchCollection m = Regex.Matches(uiName, ty, RegexOptions.IgnoreCase);
		if (m.Count <= 0)
			return;
		switch (m[0].Value.ToLower()) {
		case ObjectType.AudioSettings:
			{
                    if(NGUI)
                    {
                        defineAreas.Add(DefineArea("AudioSettings", uiName));
                        defineFinds.Add(DefineFind(uiName,uiPath, "AudioSettings"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("AudioSettings", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "AudioSettings"));
                    }
                    

                break;
			}
        case ObjectType.Text:
            {
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UIText", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UIText"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Text", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Text"));
                    }
                    break;
            }
            case ObjectType.AudioSource:
			{
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("AudioSource", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "AudioSource"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("AudioSource", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "AudioSource"));
                    }
                    break;
			}
		case ObjectType.Button:
			{
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UIButton", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UIButton"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Button", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Button"));
                    }
                    break;
			}
		case ObjectType.EventSystem:
			{
                    defineAreas.Add(DefineArea("EventSystem", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "EventSystem"));
                    break;
			}
		case ObjectType.GameObject:
			{
                    defineAreas.Add(DefineArea("GameObject", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "GameObject"));
                    break;
			}
		case ObjectType.Grid:
			{
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UIGrid", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UIGrid"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Grid", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Grid"));
                    }
                    break;
			}
		case ObjectType.Image:
			{
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UIImage", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UIImage"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Image", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Image"));
                    }
                    break;
			}
		case ObjectType.Light:
			{
                    defineAreas.Add(DefineArea("Light", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "Light"));
                    break;
			}
		case ObjectType.Mesh:
			{
                    defineAreas.Add(DefineArea("Mesh", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "Mesh"));
                    break;
			}
		case ObjectType.Panel:
			{
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UIPanel", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UIPanel"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Panel", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Panel"));
                    }
                    break;
			}
		case ObjectType.ParticleSystem:
			{
                    defineAreas.Add(DefineArea("ParticleSystem", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "ParticleSystem"));
                    break;
			}
		case ObjectType.RawImage:
			{
                    defineAreas.Add(DefineArea("RawImage", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "RawImage"));
                    break;
			}
		case ObjectType.Rigidbody:
			{
                    defineAreas.Add(DefineArea("Rigidbody", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "Rigidbody"));
                    break;
			}
		case ObjectType.ScrollView:
			{
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UIScrollView", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UIScrollView"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("ScrollView", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "ScrollView"));
                    }
                    break;
			}
		case ObjectType.Sprite:
			{
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UISprite", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UISprite"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Sprite", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Sprite"));
                    }
                    break;
                }
		case ObjectType.TextMesh:
			{
                    defineAreas.Add(DefineArea("TextMesh", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "TextMesh"));
                    break;
			}
		case ObjectType.Texture:
			{
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UITexture", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UITexture"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Texture", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Texture"));
                    }
                    break;
			}
		case ObjectType.Texture2D:
			{
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UITexture2D", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UITexture2D"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Texture2D", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Texture2D"));
                    }
                    break;
			}
		case ObjectType.Toggle:
			{
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UIToggle", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UIToggle"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Toggle", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Toggle"));
                    }
                    break;
			}
		case ObjectType.Transform:
			{
                    defineAreas.Add(DefineArea("Transform", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "Transform"));
                    break;
			}
		default:
			break;
		}
       


    }
	private void GenerateCSharpCode()
	{
		bodyStr = "";
        defineFinds.Clear();
        defineAreas.Clear();
        defineFinds.Add("\n");
        defineAreas.Add("\n");
        foreach (KeyValuePair<string, string> pair in generateUIComponentDic) {
			string key = pair.Key;

			foreach(string ty in TypeList)
			{
				Serizerize (key, pair.Value, ty);
			}

		}
        bodyStr += "\n\t#region Init";
        
        string func = "\tprivate void InitUI()\n\t{\t\t";
        foreach (var v in defineAreas)
        {
            bodyStr += v;
        }
        bodyStr += "\n";
        bodyStr += func;
        foreach (var v in defineFinds)
            bodyStr += string.Format("{0}", v);
        bodyStr +=  "\n\t}";
        bodyStr += "\n\t#endregion";
        //引用
        string usingStr = "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\nusing UnityEngine.UI;\n\n\n";

        //开始整合类体
        string classView = string.Format ("{0}public class {1} : {2}\n{{\t{3}\n}}", usingStr, className, parentName, bodyStr);

		Write (Application.dataPath + "/Scripts/" + className + ".cs", classView);
	}

	private void GenerateLuaCode()
	{
	}

	public void Write(string path,string param)
	{
		FileStream fs = new FileStream(path, FileMode.Create);
		StreamWriter sw = new StreamWriter(fs);
		//开始写入
		sw.Write(param);
		//清空缓冲区
		sw.Flush();
		//关闭流
		sw.Close();
		fs.Close();
	}
}



//[CustomEditor(typeof(UIHelper))]
//public class EditorOnlyEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        UIHelper gizmos = target as UIHelper;
//        EditorGUI.BeginChangeCheck();
//        gizmos.tag = EditorGUILayout.TagField("Tag for Objects:", gizmos.tag);
//        if (EditorGUI.EndChangeCheck())
//        {
//            EditorUtility.SetDirty(gizmos);
//        }
//    }
//}

public class ObjectType
{
	public const string Button = "button";
	public const string Sprite = "sprite";
	public const string Texture = "texture";
	public const string Image ="image";
	public const string Toggle = "toggle";
	public const string ScrollView = "scrollview";
	public const string Grid = "grid";
	public const string Panel = "panel";
	public const string EventSystem = "eventsystem";
	public const string ParticleSystem = "particlesystem";
	public const string RawImage = "rawimage";
	public const string Light = "light";
	public const string AudioSource = "audiosource";
	public const string AudioSettings = "audiosettings";
	public const string Texture2D = "texture2d";
	public const string TextMesh = "textmesh";
	public const string Transform = "transform";
	public const string GameObject = "gameobject";
	public const string Mesh = "mesh";
	public const string Rigidbody = "rigidbody";
    public const string Text = "text";

}
#endif