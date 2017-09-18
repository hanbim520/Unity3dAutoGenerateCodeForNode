using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Image10 : WindowBase
{	
	#region Define 

	[HideInInspector]private Button m_Button100 = null;

	#endregion

	#region Init 
	public override void Init()
	{		
		base.Init();

		m_Button100 = transform.Find("@Button100").GetComponent<Button>(); 

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