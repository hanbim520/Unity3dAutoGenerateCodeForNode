/*
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
    public string parentName = "WindowBase";
    [SerializeField]
    public bool NGUI = false;

    //     private string defineArea = "\n";
    //     private string defineFind = "\n";

    private List<string> defineAreas = new List<string>();
    private List<string> defineFinds = new List<string>();
    private string Variables = "";

    public void GenerateUI() 
    {
        preTrans.Clear();
		TypeList.Clear ();
		TypeList.Add (UIType.ObjectType.AudioSettings);
		TypeList.Add (UIType.ObjectType.AudioSource);
		TypeList.Add (UIType.ObjectType.Button);
		TypeList.Add (UIType.ObjectType.EventSystem);
		TypeList.Add (UIType.ObjectType.GameObject);
		TypeList.Add (UIType.ObjectType.Grid);
		TypeList.Add (UIType.ObjectType.Image);
		TypeList.Add (UIType.ObjectType.Light);
		TypeList.Add (UIType.ObjectType.Mesh);
		TypeList.Add (UIType.ObjectType.Panel);
		TypeList.Add (UIType.ObjectType.ParticleSystem);
		TypeList.Add (UIType.ObjectType.RawImage);
		TypeList.Add (UIType.ObjectType.Rigidbody);
		TypeList.Add (UIType.ObjectType.ScrollView);
		TypeList.Add (UIType.ObjectType.Sprite);
		TypeList.Add (UIType.ObjectType.TextMesh);
		TypeList.Add (UIType.ObjectType.Texture);
		TypeList.Add (UIType.ObjectType.Texture2D);
		TypeList.Add (UIType.ObjectType.Toggle);
		TypeList.Add (UIType.ObjectType.Transform);
        TypeList.Add(UIType.ObjectType.Text);
        TypeList.Add(UIType.ObjectType.UI2DSprite);
        TypeList.Add(UIType.ObjectType.Font);
        TypeList.Add(UIType.ObjectType.Camera);
        TypeList.Add(UIType.ObjectType.Label);
        TypeList.Add(UIType.ObjectType.Anchor);
        TypeList.Add(UIType.ObjectType.InputField);
        TypeList.Add(UIType.ObjectType.Slider);
        TypeList.Add(UIType.ObjectType.Widget);
        TypeList.Add(UIType.ObjectType.ScrollBar);
        TypeList.Add(UIType.ObjectType.UITable);

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
            if(generateUIComponentDic.ContainsKey(objName))
            {
                Debug.LogError(string.Format("{objName} is repeated!!! generate code failed."));
                return;
            }
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
            //Debug.Log(objectPath );
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
           // Debug.Log(objectPath );
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
        Variables += "\t\t{" + "\""+ param + "\"" + ","+"\""+ type + "\"" + "},\n";
       return string.Format("\t[HideInInspector]protected {0} {1}{2} = null;\n", type,"m_", param);
   }

    string DefineFind(string param,string path, string type)
    {
        if (type == "GameObject")
            return string.Format("\t\t{0}{1} = transform.Find({2}).gameObject; \n", "m_", param, "\"" + path + "\"");
        else if (type == "Transform")
        {
            return string.Format("\t\t{0}{1} = transform.Find({2}); \n", "m_", param, "\"" + path + "\"");
        }
        else
            return string.Format("\t\t{0}{1} = transform.Find({2}).GetComponent<{3}>(); \n", "m_", param, "\"" + path + "\"", type);
    }
    private void Serialized(string uiName, string path, string ty)
	{
        int idx = path.IndexOf("/");
        string uiPath = path.Substring(idx + 1, path.Length - idx -2);

        MatchCollection m = Regex.Matches(uiName, ty, RegexOptions.IgnoreCase);
		if (m.Count <= 0)
			return;
		switch (m[0].Value.ToLower()) {
		case UIType.ObjectType.AudioSettings:
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
        case UIType.ObjectType.Text:
            {
                    if (NGUI)
                    {
                        //                         defineAreas.Add(DefineArea("UIText", uiName));
                        //                         defineFinds.Add(DefineFind(uiName, uiPath, "UIText"));
                        Debug.LogError("ngui has no type " + uiName);
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Text", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Text"));
                    }
                    break;
            }
            case UIType.ObjectType.AudioSource:
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
		case UIType.ObjectType.Button:
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
		case UIType.ObjectType.EventSystem:
			{
                    defineAreas.Add(DefineArea("EventSystem", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "EventSystem"));
                    break;
			}
		case UIType.ObjectType.GameObject:
			{
                    defineAreas.Add(DefineArea("GameObject", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "GameObject"));
                    break;
			}
		case UIType.ObjectType.Grid:
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
		case UIType.ObjectType.Image:
			{
                    if (NGUI)
                    {
                        Debug.LogError("ngui has no type " + uiName);
//                         defineAreas.Add(DefineArea("UIImage", uiName));
//                         defineFinds.Add(DefineFind(uiName, uiPath, "UIImage"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Image", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Image"));
                    }
                    break;
			}
		case UIType.ObjectType.Light:
			{
                    defineAreas.Add(DefineArea("Light", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "Light"));
                    break;
			}
		case UIType.ObjectType.Mesh:
			{
                    defineAreas.Add(DefineArea("Mesh", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "Mesh"));
                    break;
			}
		case UIType.ObjectType.Panel:
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
		case UIType.ObjectType.ParticleSystem:
			{
                    defineAreas.Add(DefineArea("ParticleSystem", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "ParticleSystem"));
                    break;
			}
		case UIType.ObjectType.RawImage:
			{
                    defineAreas.Add(DefineArea("RawImage", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "RawImage"));
                    break;
			}
		case UIType.ObjectType.Rigidbody:
			{
                    defineAreas.Add(DefineArea("Rigidbody", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "Rigidbody"));
                    break;
			}
		case UIType.ObjectType.ScrollView:
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
		case UIType.ObjectType.Sprite:
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
		case UIType.ObjectType.TextMesh:
			{
                    defineAreas.Add(DefineArea("TextMesh", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "TextMesh"));
                    break;
			}
		case UIType.ObjectType.Texture:
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
		case UIType.ObjectType.Texture2D:
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
		case UIType.ObjectType.Toggle:
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
		case UIType.ObjectType.Transform:
			{
                    defineAreas.Add(DefineArea("Transform", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "Transform"));
                    
                    break;
			}
        case UIType.ObjectType.Camera:
            {
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UICamera", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UICamera"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Camera", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Camera"));
                    }
                    break;
            }
        case UIType.ObjectType.Label:
            {
                if (NGUI)
                {
                    defineAreas.Add(DefineArea("UILabel", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "UILabel"));
                }
                else
                {
                        //                     defineAreas.Add(DefineArea("Label", uiName));
                        //                     defineFinds.Add(DefineFind(uiName, uiPath, "Label"));
                        Debug.LogError("ugui has no type " + uiName);
                    }
                break;
            }
        case UIType.ObjectType.Font:
            {
                if (NGUI)
                {
                    defineAreas.Add(DefineArea("UIFont", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "UIFont"));
                }
                else
                {
                    defineAreas.Add(DefineArea("Font", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "Font"));
                        
                    }
                break;
            }
        case UIType.ObjectType.Anchor:
            {
                if (NGUI)
                {
                    defineAreas.Add(DefineArea("UIAnchor", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "UIAnchor"));
                }
                else
                {
                        //                     defineAreas.Add(DefineArea("Label", uiName));
                        //                     defineFinds.Add(DefineFind(uiName, uiPath, "Label"));
                        Debug.LogError("ugui has no type " + uiName);
                    }
                break;
            }
        case UIType.ObjectType.UI2DSprite:
            {
                if (NGUI)
                {
                    defineAreas.Add(DefineArea("UI2DSprite", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "UI2DSprite"));
                }
                else
                {
                        //                     defineAreas.Add(DefineArea("Label", uiName));
                        //                     defineFinds.Add(DefineFind(uiName, uiPath, "Label"));
                        Debug.LogError("ugui has no type " + uiName);
                    }
                break;
            }
        case UIType.ObjectType.InputField:
            {
                if (NGUI)
                {
                    defineAreas.Add(DefineArea("UIInput", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "UIInput"));
                }
                else
                {
                    defineAreas.Add(DefineArea("InputField", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "InputField"));
                }
                break;
            }
        case UIType.ObjectType.Slider:
            {
                if (NGUI)
                {
                    defineAreas.Add(DefineArea("UISlider", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "UISlider"));
                }
                else
                {
                    defineAreas.Add(DefineArea("Slider", uiName));
                    defineFinds.Add(DefineFind(uiName, uiPath, "Slider"));
                }
                break;
            }
            case UIType.ObjectType.Widget:
                {
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UIWidget", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UIWidget"));
                    }
                    else
                    {
                        //                         defineAreas.Add(DefineArea("Slider", uiName));
                        //                         defineFinds.Add(DefineFind(uiName, uiPath, "Slider"));
                        Debug.LogError("ugui has no type " + uiName);
                    }
                    break;
                }
            case UIType.ObjectType.ScrollBar:
                {
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UIScrollBar", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UIScrollBar"));
                    }
                    else
                    {
                        defineAreas.Add(DefineArea("Scrollbar", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "Scrollbar"));
                    }
                    break;
                }
            case UIType.ObjectType.UITable:
                {
                    if (NGUI)
                    {
                        defineAreas.Add(DefineArea("UITable", uiName));
                        defineFinds.Add(DefineFind(uiName, uiPath, "UITable"));
                    }
                    else
                    {
                        Debug.LogError("ugui has no type " + uiName);
                    }
                    break;
                }
            default:
                Debug.LogError("ui has no type " + uiName);
                break;
		}
       


    }
	private void GenerateCSharpCode()
	{
		bodyStr = "";
        Variables = "\tprotected Dictionary<string, string> Variables = new Dictionary<string, string>(){\n";
        defineFinds.Clear();
        defineAreas.Clear();
        defineFinds.Add("\n");
        defineAreas.Add("\n");
        
        foreach (KeyValuePair<string, string> pair in generateUIComponentDic) {
			string key = pair.Key;

			foreach(string ty in TypeList)
			{
                Serialized(key, pair.Value, ty);
			}

		}


        /*----------------override define----------------*/


        bodyStr += "\n\t#region Define \n";
        bodyStr += Variables + "\t};\n";
        foreach (var v in defineAreas)
        {
            bodyStr += v;
        }
        bodyStr += "\n\t#endregion\n";
        /*----------------override define end----------------*/



        /*----------------override init----------------*/
        bodyStr += "\n\t#region Init \n";
        string func = "\tpublic override void Init()\n\t{\t\t\n\t\tbase.Init();\n";
        bodyStr += func;
        foreach (var v in defineFinds)
            bodyStr += string.Format("{0}", v);
        bodyStr +=  "\n\t}";
        bodyStr += "\n\t#endregion\n";
        /*----------------override init end----------------*/

        // the following code can be deleted
        /*----------------override open----------------*/
        bodyStr += "\n\t#region Open \n";
        func = "\tpublic override void Open()\n\t{\t\t\n\t\tbase.Open();\n";
        bodyStr += func;
        
        bodyStr += "\n\t}";
        bodyStr += "\n\t#endregion\n";
        /*----------------override Open end----------------*/

        /*----------------override Close----------------*/
        bodyStr += "\n\t#region Close \n";
        func = "\tpublic override void Close()\n\t{\t\t\n\t\tbase.Close();\n";
        bodyStr += func;

        bodyStr += "\n\t}";
        bodyStr += "\n\t#endregion\n";
        /*----------------override Close end----------------*/

        /*----------------override Destroy----------------*/
        bodyStr += "\n\t#region Destroy \n";
        func = "\tpublic override void Destroy()\n\t{\t\t\n\t\tbase.Destroy();\n";
        bodyStr += func;

        bodyStr += "\n\t}";
        bodyStr += "\n\t#endregion\n";
        /*----------------override Destroy end----------------*/

        /*----------------override Refresh----------------*/
        bodyStr += "\n\t#region Refresh \n";
        func = "\tpublic override void Refresh()\n\t{\t\t\n\t\tbase.Refresh();\n";
        bodyStr += func;

        bodyStr += "\n\t}";
        bodyStr += "\n\t#endregion\n";
        /*----------------override Refresh end----------------*/


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

        Debug.Log("Code generation successful");
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

namespace UIType
{
    public class ObjectType
    {
        public const string Button = "button";
        public const string Sprite = "sprite";
        public const string Texture = "texture";
        public const string Image = "image";
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
        public const string Camera = "camera";
        public const string Label = "label";
        public const string Font = "font";
        public const string Anchor = "anchor";
        public const string UI2DSprite = "ui2dsprite";
        public const string InputField = "inputfield";
        public const string Slider = "slider";
        public const string Widget = "widget";
        public const string ScrollBar = "scrollbar";
        public const string UITable = "uitable";
    }
}

#endif