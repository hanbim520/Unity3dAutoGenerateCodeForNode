using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageRoot : MonoBehaviour
{	
	[HideInInspector]private Image @Image01 = null;
	[HideInInspector]private Image @IMAGEYYIGI = null;
	[HideInInspector]private Image @image = null;
	[HideInInspector]private Text @TextName = null;
	private void InitUI()
	{		
		 @Image01 = transform.Find("Image0/@Image01/").GetComponent<Image>();
		 @IMAGEYYIGI = transform.Find("Image0/@Image01/@IMAGEYYIGI/").GetComponent<Image>();
		 @image = transform.Find("Image0/@Image01/@image/").GetComponent<Image>();
		 @TextName = transform.Find("Image0/@TextName/").GetComponent<Text>(); 

	}
}