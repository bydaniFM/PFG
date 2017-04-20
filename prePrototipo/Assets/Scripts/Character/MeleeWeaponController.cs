using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour {

    public bool cooldown;
    public bool attack;

    public GameObject Weapon;

	// Use this for initialization
	void Start () {
        cooldown = false;
        attack = false;
        Weapon.GetComponent<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!cooldown) {
            if (Input.GetMouseButtonDown(1)) {  // Right mouse button
                Weapon.GetComponent<Animator>().Play("Attack");
                StartCoroutine(Cooldown());
            }
        }
	}

    IEnumerator Cooldown() {
        cooldown = true;
        yield return new WaitForSeconds(1.5f);
        cooldown = false;
    }
}
