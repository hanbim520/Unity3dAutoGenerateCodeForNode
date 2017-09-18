using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageRoot : WindowBase
{	
	#region Define 

	[HideInInspector]private Image m_Image01 = null;
	[HideInInspector]private Image m_IMAGEYYIGI = null;
	[HideInInspector]private Image m_image = null;
	[HideInInspector]private Text m_TextName = null;
	[HideInInspector]private InputField m_InputFieldTest = null;
	[HideInInspector]private Scrollbar m_ScrollbarTest = null;

	#endregion

	#region Init 
	public override void Init()
	{		
		base.Init();

		m_Image01 = transform.Find("Image0/@Image01").GetComponent<Image>(); 
		m_IMAGEYYIGI = transform.Find("Image0/@Image01/@IMAGEYYIGI").GetComponent<Image>(); 
		m_image = transform.Find("Image0/@Image01/@image").GetComponent<Image>(); 
		m_TextName = transform.Find("Image0/@TextName").GetComponent<Text>(); 
		m_InputFieldTest = transform.Find("@InputFieldTest").GetComponent<InputField>(); 
		m_ScrollbarTest = transform.Find("@ScrollbarTest").GetComponent<Scrollbar>(); 

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