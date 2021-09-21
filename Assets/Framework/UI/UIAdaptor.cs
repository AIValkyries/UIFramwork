using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.CanvasScaler))]
public class UIAdaptor : MonoBehaviour
{
    void Awake()
    {
        Adapt();
    }

    void Adapt()
    {
        CanvasScaler canvasScalerTemp = transform.GetComponent<CanvasScaler>();

        float standard_width = canvasScalerTemp.referenceResolution.x;          //标准宽度  
        float standard_height = canvasScalerTemp.referenceResolution.y;         //标准高度  
        float device_width = Screen.width;                                      //当前设备宽度 
        float device_height = Screen.height;                                    //当前设备高度  
        float adjustor = 0f;                                                    //屏幕矫正比例  

        //计算宽高比例  
        float standard_aspect = standard_width / standard_height;   // 9/16 = 0.5625
        float device_aspect = device_width / device_height;         // 9/18 = 0.5
        //计算矫正比例  
        if (device_aspect < standard_aspect)
        {
            adjustor = standard_aspect / device_aspect;
        }

        if (adjustor == 0)
        {
            canvasScalerTemp.matchWidthOrHeight = 1;
        }
        else
        {
            canvasScalerTemp.matchWidthOrHeight = 0;
        }
    }
}