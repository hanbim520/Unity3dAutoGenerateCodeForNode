using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//	SingletonMono.cs
//	Author: Lu Zexi
//	2014-10-19

	//singleton mono
public abstract class SingletonMono<T> : MonoBehaviour
	where T : MonoBehaviour
{
	private static T s_cInstance;
	public static T Instance
	{
		get
		{
			if(s_cInstance==null)
			{
				GameObject go = new GameObject(typeof(T).Name);
				s_cInstance = go.AddComponent<T>();
                DontDestroyOnLoad(go);
			}
			return s_cInstance;
		}
	}
	
	void Awake()
	{
		init();
	}
	
	void OnDestroy()
	{
		destory();
	}
	
	//destory
	protected virtual void destory()
	{
		s_cInstance = null;
	}
	
	//init
	public virtual void init(){}
}