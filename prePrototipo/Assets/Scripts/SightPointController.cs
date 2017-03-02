using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightPointController : MonoBehaviour {

    public Texture2D t_sightPoint;

    void OnGUI()
    {
        GUI.Label( new Rect(Screen.width / 2, Screen.height / 2, Screen.height * 0.1f, Screen.width * 0.1f), t_sightPoint);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
