using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour {

    public Rigidbody rb;
    public int HP;

	// Use this for initialization
	void Start () {
        rb.GetComponent<Rigidbody>();
        HP = 2;
	}
	
	// Update is called once per frame
	void Update () {
        if (HP <= 0) {
            Destroy(rb.gameObject);
        }
	}
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name + other.gameObject.tag);
        if (other.gameObject.tag == "CharacterMelee") {
            Debug.Log("Hit!! ");
            Vector3 force = other.gameObject.transform.position;
            rb.AddExplosionForce(500, force, 1);
            HP--;
        }
    }
}
