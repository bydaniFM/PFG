using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePhysics : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //if (transform.childCount != 0)
        //    GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();

        //GameObject RopeSegment = Resources.Load("RopeSegment");

        //GameObject FirstSegment = (GameObject)Instantiate(Resources.Load("RopeSegment"));
        //FirstSegment.transform.position = this.transform.position;

        if (Resources.Load("RopeSegment") != null) {
            Debug.Log("Prefab encontrado");
            for (int i = 0; i < 20; i++) {
                GameObject RopeSegment = (GameObject)Instantiate(Resources.Load("RopeSegment"));
                RopeSegment.transform.parent = this.gameObject.transform;
                //RopeSegment.transform.position = RopeSegment.transform.parent.transform.position; //+ new Vector3(0, 0, 15f * (i+1));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
