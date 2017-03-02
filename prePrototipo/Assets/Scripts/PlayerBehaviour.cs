using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour {

    public int life;
    public float f_secondsCount;
    public bool b_damageRecieved;

	// Use this for initialization
	void Start ()
    {
        f_secondsCount = 20f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (b_damageRecieved)
            f_secondsCount = 20f;       //initial value

        //if (life == 0)
        //    PlayerDead();

        LifeSecondsCount();
    }

    /// <summary>
    /// Function which detects if the player hasnt been hitten in 20''
    /// It restores 1 point of life per 20 seconds
    /// maximum restore = 4;
    /// </summary>
    void LifeSecondsCount()
    {
        f_secondsCount -= Time.deltaTime;

        if (f_secondsCount < 0 && life < 4)
        {
            life++;
            f_secondsCount = 20f;
        }
        
    }

    /// <summary>
    /// Functions which calls the gameover
    /// </summary>
    void PlayerDead()
    {

    }

}
