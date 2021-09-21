using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ClientKernel
public class ClientKernel : MonoBehaviour
{
    void Start()
    {
        ModuleBaseManager.CallModule(ModuleIDMap.Common);
        ModuleBaseManager.CallModule(ModuleIDMap.Main);
    }

    // Update is called once per frame
    void Update()
    {
        ModuleBaseManager.Update();
    }
}
