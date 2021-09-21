using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContorlComponent : IModuleComponent
{
    ModuleBase owner { get; set; }

}
