using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{
    [SerializeField] 
    private GameObject thingToFollow;

    void Update()
    {
        transform.position = thingToFollow.transform.position + new Vector3(0,0,-10);
    }
}
