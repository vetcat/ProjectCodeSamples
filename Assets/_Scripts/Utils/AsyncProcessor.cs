using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Коротины для зеинджекта - здесь место исполнения - сами коротины и вызов из классов
/// </summary>
public class AsyncProcessor : MonoBehaviour
{

    public void Delay(float time, Action success)
    {        
        StartCoroutine(DelayCoroutine(time, success));
    }
    
    private IEnumerator DelayCoroutine(float time, Action success)
    {                
        yield return new WaitForSeconds(time);        
        if (success != null)
            success();
    }

    public void DestroyGameObject(GameObject go)
    {
        Destroy(go);
    }
    
    public void DestroyGameObject(GameObject go, float time)
    {
        Destroy(go, time);
    }

    public GameObject InstaniateGameObject(GameObject go)
    {
        return Instantiate(go);
    }
}
