using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumDefines;
using System;

public class WindowBase : MonoBehaviour, IWindowAPI
{
    protected Dictionary<string, GameObject> SelfObjects = new Dictionary<string, GameObject>();
    protected DeepEnum WindowType = DeepEnum.eOneLevel;
    public virtual void Init() { }
    public virtual void Open() { }
    public virtual void Close() { }
    public virtual void Destroy() { }
    public virtual void Refresh() { }

    private void Start()
    {
        this.Init();
        SelfObjects.Clear();
 //       Recursive(gameObject);
    }

//     private void Recursive(GameObject parentGameObject)
//     {
//         System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
//         sw.Start();
//         foreach (Transform child in parentGameObject.transform)
//         {
//             SelfObjects.Add(child.gameObject.name, child.gameObject);
//             Recursive(child.gameObject);
//         }
//         sw.Stop();
// 
//         TimeSpan ts2 = sw.Elapsed;
//         Debug.Log("root time " + ts2.TotalMilliseconds);
        private void OnDestroy()
    {
        this.Destroy();
    }
}
