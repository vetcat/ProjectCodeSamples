using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTarget : MonoBehaviour
{
    private Camera mainCamera;    

	void Awake()
	{
		mainCamera = Camera.main; 
		if (!mainCamera)
			Debug.LogError("[CanvasTarget] Camera.main not found");
	}
	
	void Update ()
	{    	    
        if (!mainCamera) return;
		
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);        
	}
}
