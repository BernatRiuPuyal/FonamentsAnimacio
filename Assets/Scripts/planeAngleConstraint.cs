using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeAngleConstraint : MonoBehaviour {

    public Transform parent;
    public Transform child;
    public Transform plane;

    public float maxAngle;


    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void lateUpdate () {
		
        if(Vector3.Angle(child.up,parent.up) > maxAngle)
        {
            Vector3 axis;
            float angle;

            Quaternion diference = Quaternion.Inverse(parent.rotation) * child.rotation;

            diference.ToAngleAxis(out angle, out axis);

            child.rotation = parent.rotation * Quaternion.AngleAxis(maxAngle, axis);
        }


	}
}
