﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENTICourse.IK
{

    // A typical error function to minimise
    public delegate float ErrorFunction(Vector3 target, float[] solution);

    public struct PositionRotation
    {
        Vector3 position;
        Quaternion rotation;

        public PositionRotation(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        // PositionRotation to Vector3
        public static implicit operator Vector3(PositionRotation pr)
        {
            return pr.position;
        }
        // PositionRotation to Quaternion
        public static implicit operator Quaternion(PositionRotation pr)
        {
            return pr.rotation;
        }
    }

    //[ExecuteInEditMode]
    public class InverseKinematics : MonoBehaviour
    {
        [Header("Joints")]
        public Transform BaseJoint;
       

        [ReadOnly]
        public RobotJoint[] Joints = null;

        // The current angles
        [ReadOnly]
        public float[] Solution = null;

        [Header("Destination")]
        public Transform Effector;
        [Space]
        public Transform Destination;
        public float DistanceFromDestination;
        private Vector3 target;

        [Header("Inverse Kinematics")]
        [Range(0, 1f)]
        public float DeltaGradient = 0.1f; // Used to simulate gradient (degrees)
        [Range(0, 100f)]
        public float LearningRate = 0.1f; // How much we move depending on the gradient

        [Space()]
        [Range(0, 0.25f)]
        public float StopThreshold = 0.1f; // If closer than this, it stops
        [Range(0, 10f)]
        public float SlowdownThreshold = 0.25f; // If closer than this, it linearly slows down


        public ErrorFunction ErrorFunction;


        //private Vector3[] AxisJoints; 


        [Header("Debug")]
        public bool DebugDraw = true;



        // Use this for initialization
        void Start()
        {
            if (Joints == null)
                GetJoints();

            ErrorFunction = DistanceFromTarget;


            for(int i = 1; i < Solution.Length; i++)
            {
                Solution[i] = Joints[i].GetAngle();

                i++;
            }
            

        }

        [ExposeInEditor(RuntimeOnly = false)]
        public void GetJoints()
        {
            Joints = BaseJoint.GetComponentsInChildren<RobotJoint>();
            Solution = new float[Joints.Length];
        }



        // Update is called once per frame
        void Update()
        {
            // Do we have to approach the target?
            //TODO
            target = Destination.position;
           
            if (ErrorFunction(target, Solution) > StopThreshold)
                ApproachTarget(target);

            if (DebugDraw)
            {
                Debug.DrawLine(Effector.transform.position, target, Color.green);
                Debug.DrawLine(Destination.transform.position, target, new Color(0, 0.5f, 0));
            }

            //Debug.Log(target);
        }

        public void ApproachTarget(Vector3 target)
        {
            //TODO


            for(int i = 0; i < Solution.Length - 1; i++)
            {
                Solution[i] -= CalculateGradient(target, Solution, i, DeltaGradient) * LearningRate;

            }

            for (int i = 0; i < Solution.Length - 1; i++)
            {
                Joints[i].MoveArm(Solution[i]);

            }
            

           
        }

        
        public float CalculateGradient(Vector3 target, float[] Solution, int i, float delta)
        {

            // my  code

            float gradient = 0;

                                 
            gradient = -DistanceFromTarget(target, Solution);

            float[] nextSol = Solution;
            nextSol[i] += delta;


            gradient += DistanceFromTarget(target, nextSol);
                       

            return gradient;
        }

        // Returns the distance from the target, given a solution
        public float DistanceFromTarget(Vector3 target, float[] Solution)
        {
            Vector3 point = ForwardKinematics(Solution);
            return Vector3.Distance(point, target);
        }


        /* Simulates the forward kinematics,
         * given a solution. */

    
        public PositionRotation ForwardKinematics(float[] Solution) // solution = angulos
        {
            Vector3 prevPoint = Joints[0].transform.position;
    
            // Takes object initial rotation into account
            Quaternion rotation = transform.rotation;
           
            //TODO

            for(int i = 1; i < Solution.Length; i++)
            {

                rotation = Quaternion.AngleAxis(Solution[i - 1], Joints[i - 1].Axis) * rotation;

                // operator* on vector3 * quaternion overrided to respond the rotation of a vector3

                Vector3 posFinal = prevPoint + rotation * Joints[i].StartOffset;

                prevPoint = posFinal;
                
            }

          

                                 


            // The end of the effector
            return new PositionRotation(prevPoint, rotation);
        }
    }
}