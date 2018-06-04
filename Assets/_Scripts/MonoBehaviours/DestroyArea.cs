using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour 
{
    void OnTriggerEnter(Collider other) 
    {        
        if (other.transform.parent)
        {
            var actor = other.transform.parent.GetComponent<Actor>();
            if (actor)
                actor.Despawn();
        }
    }
}
