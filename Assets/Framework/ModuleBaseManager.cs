using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public enum ModuleIDMap
{
    None = 0,
    Common = 1,  // 通用模块
    Main,
    Test2
}

public static class ModuleBaseManager
{
    /// <summary>
    /// Key:moduleID
    /// Value:具体的模块类型
    /// </summary>
    static Dictionary<ModuleIDMap, Type> moduleTypeMap = new Dictionary<ModuleIDMap, Type>();

    static Dictionary<ModuleIDMap, ModuleBase> moduleMaps = new Dictionary<ModuleIDMap, ModuleBase>();

    static ModuleBaseManager()
    {
        // 注册ModuleBase
        RegisterModuleBase<CommonModule>(ModuleIDMap.Common);
        RegisterModuleBase<MainModule>(ModuleIDMap.Main);
        RegisterModuleBase<TestModule>(ModuleIDMap.Test2);
    }

    public static ModuleBase GetModuleBase(ModuleIDMap moduleID)
    {
        if (moduleMaps.ContainsKey(moduleID))
        {
            return moduleMaps[moduleID];
        }
        return null;
    }

    static void RegisterModuleBase<T>(ModuleIDMap moduleID) where T : ModuleBase, new()
    {
        if (!moduleTypeMap.ContainsKey(moduleID))
        {
            moduleTypeMap.Add(moduleID,typeof(T));
        }
    }

    public static ModuleBase CallModule(ModuleIDMap moduleID)
    {
        if (!moduleTypeMap.ContainsKey(moduleID))
        {
            Debug.LogError("模块没有注册");
            return null;
        }

        if (moduleMaps.ContainsKey(moduleID))
        {
            //moduleMaps[moduleID].InstantiateModule();
            moduleMaps[moduleID].EnterModule();
            return moduleMaps[moduleID];
        }

        Type type = moduleTypeMap[moduleID];

        ModuleBase moduleBase = (ModuleBase)Activator.CreateInstance(type);
        moduleMaps.Add(moduleID, moduleBase);

        moduleBase.InstantiateModule();
        moduleMaps[moduleID].EnterModule();
        return moduleBase;
    }

    public static void UninstallModule(ModuleIDMap moduleID)
    {
        if (moduleMaps.ContainsKey(moduleID))
        {
            moduleMaps[moduleID].UninstallModule();
            moduleMaps.Remove(moduleID);
        }
    }

    public static void Update()
    {
        var itr = moduleMaps.Values.GetEnumerator();
        while (itr.MoveNext())
        {
            itr.Current.Update();
        }
        itr.Dispose();
    }

}
