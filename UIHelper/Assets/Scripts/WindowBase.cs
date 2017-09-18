using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBase : MonoBehaviour, IWindowAPI
{

    public virtual void Init() { }
    public virtual void Open() { }
    public virtual void Close() { }
    public virtual void Destroy() { }
    public virtual void Refresh() { }

    private void Start()
    {
        this.Init();
    }

    private void OnDestroy()
    {
        this.Destroy();
    }
}
