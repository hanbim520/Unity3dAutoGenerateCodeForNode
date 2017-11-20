using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIRootTestBase : WindowBase
{	
	#region Define 

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