using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class UIRootTestBase : WindowBase
{	
	#region Define 
	protected Dictionary<string, string> Variables = new Dictionary<string, string>(){
		{"CameraMain","UICamera"},
		{"LabelName","UILabel"},
		{"SpriteHeader","UISprite"},
		{"WidgetContainer","UIWidget"},
		{"AnchorTest","UIAnchor"},
		{"Paneltest","UIPanel"},
		{"uitableTest","UITable"},
		{"GameObjectTest","GameObject"},
		{"scrollViewList","UIScrollView"},
		{"TransformTest","Transform"},
	};

	[HideInInspector]protected UICamera m_CameraMain = null;
	[HideInInspector]protected UILabel m_LabelName = null;
	[HideInInspector]protected UISprite m_SpriteHeader = null;
	[HideInInspector]protected UIWidget m_WidgetContainer = null;
	[HideInInspector]protected UIAnchor m_AnchorTest = null;
	[HideInInspector]protected UIPanel m_Paneltest = null;
	[HideInInspector]protected UITable m_uitableTest = null;
	[HideInInspector]protected GameObject m_GameObjectTest = null;
	[HideInInspector]protected UIScrollView m_scrollViewList = null;
	[HideInInspector]protected Transform m_TransformTest = null;

	#endregion

	#region Init 
	public override void Init()
	{		
		base.Init();
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();
       
        m_CameraMain = transform.Find("@CameraMain").GetComponent<UICamera>(); 
		m_LabelName = transform.Find("@LabelName").GetComponent<UILabel>(); 
		m_SpriteHeader = transform.Find("ui2DSpriteHeader/@SpriteHeader").GetComponent<UISprite>(); 
		m_WidgetContainer = transform.Find("ui2DSpriteHeader/@SpriteHeader/@WidgetContainer").GetComponent<UIWidget>(); 
		m_AnchorTest = transform.Find("Container/@AnchorTest").GetComponent<UIAnchor>(); 
		m_Paneltest = transform.Find("@Paneltest").GetComponent<UIPanel>(); 
		m_uitableTest = transform.Find("@uitableTest").GetComponent<UITable>(); 
		m_GameObjectTest = transform.Find("@GameObjectTest").gameObject; 
		m_scrollViewList = transform.Find("@scrollViewList").GetComponent<UIScrollView>(); 
		m_TransformTest = transform.Find("@TransformTest");

        sw.Stop();

        TimeSpan ts = sw.Elapsed;
        Debug.Log("test time "+ ts.TotalMilliseconds);

        System.Diagnostics.Stopwatch sw1 = new System.Diagnostics.Stopwatch();
        sw1.Start();
        Recursive(gameObject);
        sw1.Stop();

        TimeSpan ts2= sw1.Elapsed;
        Debug.Log("root time " + ts2.TotalMilliseconds);
    }
    private void Recursive(GameObject parentGameObject)
    {
       
        foreach (Transform child in parentGameObject.transform)
        {
            SelfObjects.Add(child.gameObject.name, child.gameObject);
            Recursive(child.gameObject);
        }
       
    }
    #endregion

    #region Open 
    public override void Open()
	{		
		base.Open();

	}
	#endregion

	#region Close 
	public override void Close()
	{		
		base.Close();

	}
	#endregion

	#region Destroy 
	public override void Destroy()
	{		
		base.Destroy();

	}
	#endregion

	#region Refresh 
	public override void Refresh()
	{		
		base.Refresh();

	}
	#endregion

}