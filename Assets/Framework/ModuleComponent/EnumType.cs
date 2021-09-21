using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CanvasType
{
    None,
    UIRoot,   //  ScreenSpaceCamera
    Overlay,  // 覆盖模式
    World,    // 世界模式
}

/// <summary>
/// UI的层级
/// </summary>
public enum LayerType
{
    Bottom = 0,
    Content,
    TOP,
    TIP,
}
