using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotConstraints : MonoBehaviour {


    public Transform[] Joints;

    public float[] maxAngles;

    RoboticArmControl controlScript;

	// Use this for initialization
	void Start () {

        controlScript = GetComponent<RoboticArmControl>();

	}
	
	// Update is called once per frame
	void lateUpdate () {
		

        //if(Vector3.Angle)


	}
}
