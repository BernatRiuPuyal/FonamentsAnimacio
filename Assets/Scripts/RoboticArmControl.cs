using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticArmControl : MonoBehaviour {



    public GameObject Joint0;
    public GameObject Joint1;
    public GameObject Joint2;
    public GameObject Joint3;
    public GameObject Joint4;


    public float angle0;
    public float angle1;
    public float angle2;
    public float angle3;
    public float angle4;

    private Quaternion Rot0;
    private Quaternion Rot1;
    private Quaternion Rot2;
    private Quaternion Rot3;
    private Quaternion Rot4;





    // Use this for initialization
    void Start () {

        //lerpC = 0;
                                                                      
        Rot0 = Joint0.transform.localRotation;                             
        Rot1 = Joint1.transform.localRotation;                             
        Rot2 = Joint2.transform.localRotation;   
        Rot3 = Joint3.transform.localRotation;
        Rot4 = Joint4.transform.localRotation;

    }

    // Update is called once per frame
    void Update () {

        //lerpC += Time.deltaTime*0.3f;



        Joint0.transform.localRotation = Rot0 * Quaternion.AngleAxis(angle0, Vector3.up);
        Joint1.transform.localRotation = Rot1 * Quaternion.AngleAxis(angle1, Vector3.right);
        Joint2.transform.localRotation = Rot2 * Quaternion.AngleAxis(angle2, Vector3.right);
        Joint3.transform.localRotation = Rot3 * Quaternion.AngleAxis(angle3, Vector3.right);
        Joint4.transform.localRotation = Rot4 * Quaternion.AngleAxis(angle4, Vector3.up);
     







    }
}
