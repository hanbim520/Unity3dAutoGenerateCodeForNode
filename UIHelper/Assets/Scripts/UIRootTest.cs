using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIRootTest : WindowBase
{	
	#region Define 

	[HideInInspector]private UICamera m_CameraMain = null;
	[HideInInspector]private UILabel m_LabelName = null;
	[HideInInspector]private UIAnchor m_AnchorTest = null;
	[HideInInspector]private UIPanel m_Paneltest = null;
	[HideInInspector]private UITable m_uitableTest = null;
	[HideInInspector]private GameObject m_GameObjectTest = null;
	[HideInInspector]private UIScrollView m_scrollViewList = null;
	[HideInInspector]private Transform m_TransformTest = null;

	#endregion

	#region Init 
	public  override void Init()
	{		
		base.Init();

		m_CameraMain = transform.Find("@CameraMain").GetComponent<UICamera>(); 
		m_LabelName = transform.Find("@LabelName").GetComponent<UILabel>(); 
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