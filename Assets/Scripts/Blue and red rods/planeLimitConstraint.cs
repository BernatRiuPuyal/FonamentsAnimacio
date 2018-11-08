using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeLimitConstraint : MonoBehaviour {

    public Transform parent;
    public Transform child;
    public Transform plane;



	// Use this for initialization
	void Start () {
		
	}
	



	// Update is called once per frame
	void LateUpdate () {


        if (Vector3.Dot(parent.up,plane.up) < 0)
        {
            child.up = parent.up - plane.up * Vector3.Dot(parent.up, plane.up);

        }





    }
}
