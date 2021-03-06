using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iksolver : MonoBehaviour {

	// Array to hold all the joints
	// index 0 - root
	// index END - End Effector
	[SerializeField]
    GameObject[] joints;

    // The target for the IK system
    [SerializeField]
    GameObject targ;


    // Array of angles to rotate by (for each joint), as well as sin and cos values
    [SerializeField]
    float[] _theta, _sin, _cos;

	// To check if the target is reached at any point
	bool _done = false;
	
    // To store the position of the target
	private Vector3 tpos;

	// Max number of tries before the system gives up (Maybe 10 is too high?)
	[SerializeField]
	private int _mtries = 10;
	// The number of tries the system is at now
	[SerializeField]
	private int _tries = 0;
	
	// the range within which the target will be assumed to be reached
	readonly float _epsilon = 0.1f;


	// Initializing the variables
	void Start () {
		_theta = new float[joints.Length];
		_sin = new float[joints.Length];
		_cos = new float[joints.Length];
		tpos = targ.transform.position;
	}
	
	// Running the solver - all the joints are iterated through once every frame
	void Update () {
		// if the target hasn't been reached
		if (!_done)
		{	
			// if the Max number of tries hasn't been reached
			if (_tries <= _mtries)
			{
				// starting from the second last joint (the last being the end effector)
				// going back up to the root
				for (int i = joints.Length - 2; i >= 0; i--)
				{
                    // The vector from the ith joint to the end effector
                    Vector3 r1 = joints[joints.Length - 1].transform.position - joints[i].transform.position;
                    //r1.Normalize();

                    // The vector from the ith joint to the target
                    Vector3 r2 = targ.transform.position - joints[i].transform.position;

                    //r2.Normalize();

                    // to avoid dividing by tiny numbers
                     if (r1.magnitude * r2.magnitude <= 0.001f)
					{
                        // cos ? sin? 
                        _sin[i] = 0; //Mathf.Sin(Vector3.Angle(r1, r2));
                        _cos[i] = 1; // Mathf.Sin(Vector3.Angle(r1, r2));

                    }
                    else
					{
                        // find the components using dot and cross product
                        //TODO4
                        _cos[i] = Vector3.Dot(r1.normalized, r2.normalized);
                        _sin[i] = Vector3.Cross(r1.normalized, r2.normalized).magnitude;

					}

                    // The axis of rotation 
                    Vector3 axis = Vector3.Cross(r1.normalized,r2.normalized);

                    // find the angle between r1 and r2 (and clamp values if needed avoid errors)
                    _theta[i] = Mathf.Acos(Mathf.Max(-1, Mathf.Min(1, _cos[i])));

                    //Optional. correct angles if needed, depending on angles invert angle if sin component is negative
                    //if (TODO)
                    //	theta[i] = TODO7



                    // obtain an angle value between -pi and pi, and then convert to degrees (B: already in degrees!)

                    _theta[i] = (float)SimpleAngle(_theta[i]) * Mathf.Rad2Deg;

                    //Debug.Log(_theta[i]);
                    //_theta[i] *= Mathf.Rad2Deg;

                    // rotate the ith joint along the axis by theta degrees in the world space.
                    joints[i].transform.Rotate(axis, _theta[i]);

                }
				
				// increment tries
				_tries++;
			}
		}

        // find the difference in the positions of the end effector and the target
        float difference = Vector3.Distance(targ.transform.position, joints[joints.Length - 1].transform.position);




		
		// if target is within reach (within epsilon) then the process is done
		if (difference < _epsilon)
		{
			_done = true;
		}
		// if it isn't, then the process should be repeated
		else
		{
			_done = false;
		}
		
		// the target has moved, reset tries to 0 and change tpos
		if(targ.transform.position!=tpos)
		{
			_tries = 0;
			tpos = targ.transform.position;
		}




	}

    
	// function to convert an angle to its simplest form (between -pi to pi radians)
	double SimpleAngle(double theta)
	{

        theta = theta % (2.0 * Mathf.PI);
        if(theta < -Mathf.PI)
        {
            theta += 2 * Mathf.PI;
        }
        else if(theta > Mathf.PI){

            theta -= 2.0 * Mathf.PI;
        }
		return theta;
	}
}
