using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumDefines;
public class UIManager : SingletonMono<UIManager>
{
    private Stack<WindowBase> windows = new Stack<WindowBase>();
    private void OpenWindow(WindowBase window)
    {
        windows.Push(window);
        window.Open();
    }	

}
