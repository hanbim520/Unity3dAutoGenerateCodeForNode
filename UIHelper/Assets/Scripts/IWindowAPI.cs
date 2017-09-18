using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWindowAPI {
    void Init();
    void Open();
    void Close();
    void Hide();
    void Destroy();
    void Refresh();
}
