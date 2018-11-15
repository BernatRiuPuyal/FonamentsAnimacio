using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticArmControl : MonoBehaviour
{



    public GameObject[] Joints;



    public float[] angles;


    private Quaternion[] initRots;



    private Vector3[] axis;


    // Use this for initialization
    void Start()
    {

        initRots = new Quaternion[5];

        axis = new Vector3[5] { Vector3.up, Vector3.right, Vector3.right, Vector3.right, Vector3.up };

        for (int i = 0; i < Joints.Length; i++)
        {
            initRots[i] = Joints[i].transform.localRotation;

        }




    }

    // Update is called once per frame

    void Update()
    {

        for (int i = 0; i < Joints.Length; i++)
        {
            Joints[i].transform.localRotation = initRots[i] * Quaternion.AngleAxis(angles[i], axis[i]);

        }








    }
}
