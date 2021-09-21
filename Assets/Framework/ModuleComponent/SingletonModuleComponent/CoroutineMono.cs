using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineMonoComponent : ISingletonModuleComponent
{
    CoroutineMono _mono;

    public void Initialize()
    {
        if (_mono == null) 
        {
            _mono = GameObject.FindObjectOfType<CoroutineMono>();
            if (_mono == null)
            {
                GameObject go = new GameObject("MonoManger");
                go.transform.position = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.transform.rotation = Quaternion.identity;
                _mono = go.AddComponent<CoroutineMono>();

                GameObject.DontDestroyOnLoad(_mono);
            }
        }
    }

    public void DelayAction(float time, System.Action action)
    {
        _mono.DelayAction(time, action);
    }
    public void DelayAction<T>(float time, System.Action<T> action, T param)
    {
        _mono.DelayAction<T>(time, action, param);
    }

    public Coroutine StartCoroutine(string methodName)
    {
        return _mono.StartCoroutine(methodName);
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return _mono.StartCoroutine(routine);
    }


    public void StopCoroutine(IEnumerator routine)
    {
         _mono.StopCoroutine(routine);
    }

    public void StopCoroutine(Coroutine routine)
    {
         _mono.StopCoroutine(routine);
    }

    public void StopCoroutine(string methodName)
    {
        _mono.StopCoroutine(methodName);
    }

}

public class CoroutineMono : MonoBehaviour
{
    public void Stop()
    {
        StopAllCoroutines();
        GameObject.Destroy(this);
    }
    public void DelayAction(float time, System.Action action)
    {
        StartCoroutine(delay(time, action));
    }
    public void DelayAction<T>(float time, System.Action<T> action, T param)
    {
        StartCoroutine(delay(time, action, param));
    }
    IEnumerator delay<T>(float time, System.Action<T> a, T param)
    {
        yield return new WaitForSeconds(time);
        if (a != null)
            a.Invoke(param);
    }
    IEnumerator delay(float time, System.Action a)
    {
        yield return new WaitForSeconds(time);
        if (a != null)
            a.Invoke();
    }
}
