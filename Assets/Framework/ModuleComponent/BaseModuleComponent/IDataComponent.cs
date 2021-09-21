using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataComponent : IModuleComponent
{
    void Instantiate();

    IDataComponent Clone();
}

public interface ISingletonDataComponent : IDataComponent
{

}