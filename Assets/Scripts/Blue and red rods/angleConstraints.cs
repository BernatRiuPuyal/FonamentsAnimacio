using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angleConstraints : MonoBehaviour
{
    public bool active;

    [Range(0.0f, 180.0f)]
    public float maxAngle;

    [Range(0.0f, 180.0f)]
    public float minAngle;

    public Transform parent;
    public Transform child;



    void Start()
    {
    }

    void LateUpdate()
    {


        if (active)
        {
            //solve your exercise here
            //Debug.Log(Vector3.Angle(parent.up, child.up));
            if (Vector3.Angle(parent.up, child.up) > maxAngle)
            {
                Vector3 axis;
                float angle;

                Quaternion diference = Quaternion.Inverse(parent.rotation) * child.rotation;

                diference.ToAngleAxis(out angle, out axis);

                child.rotation = parent.rotation;

                child.Rotate(axis, maxAngle);


            }



        }
    }

    //add auxiliary functions, if needed, below




}

