using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ui2DSpriteHeader : WindowBase
{	
	#region Define 

	[HideInInspector]private UISprite m_SpriteHeader = null;
	[HideInInspector]private UIWidget m_WidgetContainer = null;

	#endregion

	#region Init 
	public override void Init()
	{		
		base.Init();

		m_SpriteHeader = transform.Find("@SpriteHeader").GetComponent<UISprite>(); 
		m_WidgetContainer = transform.Find("@SpriteHeader/@WidgetContainer").GetComponent<UIWidget>(); 

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