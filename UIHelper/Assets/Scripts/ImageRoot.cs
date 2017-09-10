using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageRoot : MonoBehaviour
{	
	[SerializeField]private Image Image01 = null;
	[SerializeField]private Image IMAGEYYIGI = null;
	[SerializeField]private Image imageText = null;
	private void InitUI()
	{		
		 Image01 = transform.Find("ImageRoot/Image0/Image01/").GetComponent<Image>();
		 IMAGEYYIGI = transform.Find("ImageRoot/Image0/Image01/IMAGEYYIGI/").GetComponent<Image>();
		 imageText = transform.Find("ImageRoot/Image0/Image01/imageText/").GetComponent<Image>();

	}
}