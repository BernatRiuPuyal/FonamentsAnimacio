using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeConstraints : MonoBehaviour
{
    public bool active;
    public bool debugLines;

    public Transform parent;
    public Transform child;

    public Transform plane;
 
    // To define how "strict" we want to be
    private float threshold = 0.00001f;
 
    void LateUpdate()
    {
           
         

    }

    
    private float ComputeAngle(Vector3 ToParent, Vector3 ToChild)
    {
        return 0.0f;
    }

    


}
