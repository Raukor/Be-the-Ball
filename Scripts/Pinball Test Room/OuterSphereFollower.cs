using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterSphereFollower : MonoBehaviour
{
    public Transform outerSphere;

 
    void Update()
    {
        gameObject.transform.position = outerSphere.position;
    }
}
