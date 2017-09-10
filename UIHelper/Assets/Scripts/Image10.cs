using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Image10 : MonoBehaviour
{	
	[HideInInspector]private Button @Button100 = null;
	private void InitUI()
	{		
		 @Button100 = transform.Find("@Button100/").GetComponent<Button>();

	}
}