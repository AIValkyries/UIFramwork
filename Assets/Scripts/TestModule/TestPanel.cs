using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : BasePanelComponet
{
    public override CanvasType canvasType { get { return CanvasType.Overlay; } }

    public override LayerType layerType { get { return LayerType.TOP; } }

    public override string prefabPath { get { return "TestMain"; } }

    public override bool showInInitialization { get { return false; } }

    Text _playerName;
    Button _close;

    public override void Initialize()
    {
        _playerName = transform.Find("Text/Text (1)").GetComponent<Text>();
        _close = transform.Find("Button").GetComponent<Button>();

        _close.onClick.AddListener(CloseButton);
    }

    public void ShowPlayerInfo(string playerName)
    {
        _playerName.text = playerName;
    }


    void CloseButton()
    {
        ModuleBaseManager.UninstallModule(ModuleIDMap.Test2);
    }


}
