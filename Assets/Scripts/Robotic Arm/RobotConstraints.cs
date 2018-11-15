using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotConstraints : MonoBehaviour
{


    //public Transform[] Joints;

    public Vector2[] Min_MaxAngles;

    RoboticArmControl controlScript;

    // Use this for initialization
    void Start()
    {

        controlScript = GetComponent<RoboticArmControl>();






    }

    // Update is called once per frame
    void Update()
    {


        for (int i = 0; i < controlScript.Joints.Length; i++)
        {
            controlScript.angles[i] = Mathf.Clamp(controlScript.angles[i], Min_MaxAngles[i].x, Min_MaxAngles[i].y);

        }


    }
}
