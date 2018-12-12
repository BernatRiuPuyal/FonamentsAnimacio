using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IK_FABRIK2 : MonoBehaviour
{
    public Transform[] joints;
    public Transform target;

    private Vector3[] copy;
    private float[] distances;
    private bool done;

    public float tolerance = 1f;

    void Start()
    {
        distances = new float[joints.Length - 1];
        copy = new Vector3[joints.Length];


        // set distances
        for(int i = 0; i < joints.Length - 1; i++)
        {
            distances[i] = Vector3.Distance(joints[i].position, joints[i + 1].position);
        }



    }

    void Update()
    {
        // Copy the joints positions to work with
        //TODO

        for(int i = 0; i < copy.Length; i++)
        {
            copy[i] = joints[i].position;
        }


        //done = TODO
        if (!done)
        {
            float targetRootDist = Vector3.Distance(copy[0], target.position);

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                for (int i = 0; i < copy.Length - 1; i++)
                {
                    float r = Vector3.Distance(target.position, copy[i]);
                    float lamb = distances[i] / r;

                    copy[i] = (1 - lamb) * copy[i] + lamb * target.position;
                }



            }
            else
            {
                int securite = 0;
                // The target is reachable
                while (Vector3.Distance(copy[copy.Length - 1],target.position) > tolerance && securite < 1000)
                {
                    Vector3 originalRootPos = copy[0];

                    // STAGE 1: FORWARD REACHING
                    //TODO
                    copy[copy.Length - 1] = target.position;

                    for(int i = copy.Length - 2; i > 0; i--)
                    {
                        float r = Vector3.Distance(copy[i + 1], copy[i]);
                        float lambd = distances[i] / r;

                        copy[i] = (1 - lambd) * copy[i + 1] + lambd * copy[i];
                    }




                    // STAGE 2: BACKWARD REACHING
                    //TODO

                    copy[0] = originalRootPos;

                    for(int i = 1; i < copy.Length - 1; i++)
                    {
                        float r = Vector3.Distance(copy[i + 1], copy[i]);
                        float lambd = distances[i] / r;

                        copy[i + 1] = (1 - lambd) * copy[i] + lambd * copy[i];

                    }
                    securite++;
                    Debug.Log(securite);
                }
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
                joints[i].position = copy[i];
            }          
        }
    }

}
