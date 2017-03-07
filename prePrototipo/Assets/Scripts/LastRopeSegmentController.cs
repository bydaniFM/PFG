using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastRopeSegmentController : MonoBehaviour {

    GameObject LastSegment;
    public Vector3 destiny;

    bool launching;
    int speed;

    LastRopeSegmentController(GameObject LastSegment, Vector3 destiny) {
        this.destiny = destiny;
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Launch script added");
        launching = true;
        speed = 10;
	}
	
	// Update is called once per frame
	void Update () {
        if (launching && destiny != Vector3.zero)
            //this.transform.position = this.transform.position + new Vector3(0, 0, speed) * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, destiny, speed*Time.deltaTime);
    }
    void OnTriggerEnter(Collider other) {
        launching = false;
        this.transform.parent = null;
    }
}
