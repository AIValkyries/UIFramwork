using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件组件
/// </summary>
public interface IEventComponent : IModuleComponent
{
    ModuleBase owner { get; set; }
    List<int> GetListener();
    void Notify(int op, System.Object param = null);
}
