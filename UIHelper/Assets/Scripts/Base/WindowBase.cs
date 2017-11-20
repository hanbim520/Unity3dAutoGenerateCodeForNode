using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumDefines;

public class WindowBase : MonoBehaviour, IWindowAPI
{
    protected DeepEnum WindowType = DeepEnum.eOneLevel;
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
