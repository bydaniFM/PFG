using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePhysics : MonoBehaviour {

    public GameObject prevSegment;
    public GameObject hook;

    CapsuleCollider trigger;
    public int numSegments;
    int speed;

	// Use this for initialization
	void Start () {
        //if (transform.childCount != 0)
        //    GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();

        //GameObject RopeSegment = Resources.Load("RopeSegment");

        //GameObject FirstSegment = (GameObject)Instantiate(Resources.Load("RopeSegment"));
        //FirstSegment.transform.position = this.transform.position;

        hook = GameObject.Find("Hook");

        prevSegment = null;
        trigger = null;
        numSegments = (int)hook.GetComponent<Hook>().distance * 4;
        speed = 5;

        Vector3 initialPos = this.transform.position;
        if (Resources.Load("RopeSegment") != null) {
            Debug.Log("Creating rope with " + numSegments + " segments");
            for (int i = 0; i < numSegments; i++) {
                GameObject RopeSegment = (GameObject)Instantiate(Resources.Load("RopeSegment"));
                RopeSegment.transform.parent = this.transform;
                RopeSegment.transform.position = RopeSegment.transform.parent.transform.position + new Vector3(0, 0, 0.03f * (i+1));
                RopeSegment.transform.rotation = new Quaternion(-180, 0, 0, 180);
                //Rigidbody rb = RopeSegment.AddComponent<Rigidbody>();
                if (i == 0) {
                    //RopeSegment.GetComponent<Rigidbody>()
                    RopeSegment.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                } else {
                    CharacterJoint joint = RopeSegment.AddComponent<CharacterJoint>();
                    joint.connectedBody = prevSegment.GetComponent<Rigidbody>();
                    joint.anchor = new Vector3(0, 1, 0);
                    joint.axis = new Vector3(1, 0, 0);
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = new Vector3(0, 0, 0);//(0, -3, 0)
                    joint.swingAxis = new Vector3(0, 1, 0);
                }
                if(i == numSegments - 1) {
                    RopeSegment.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    trigger = RopeSegment.AddComponent<CapsuleCollider>();
                    trigger.isTrigger = true;
                    RopeSegment.AddComponent<LastRopeSegmentController>();
                    Debug.Log("Last segment was created");
                }
                prevSegment = RopeSegment;
            }
        }
        //this.transform.position = initialPos;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
