using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalDataComponent : IDataComponent
{
    public string Name { get; set; }

    public IDataComponent Clone()
    {
        PlayerGlobalDataComponent newData = new PlayerGlobalDataComponent();
        newData.Name = Name;
        return newData;
    }

    public void Instantiate()
    {
        Name = "Lonely";
    }

}
