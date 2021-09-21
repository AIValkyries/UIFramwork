using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPanelComponent : IModuleComponent
{
    GameObject gameObject { get; }
    Transform transform { get; }
    CanvasType canvasType { get; }
    LayerType layerType { get; }

    string prefabPath { get; }

    void Show();
    void Hide();
    void Destroy();
    void Update();

    bool IsOpen();
}
