using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour {

    public int totalLife;
    public int totalStamina;
    public float f_secondsCountLife;
    public float f_secondsCountStamina;
    public bool b_damageRecieved;

    public Image[] lifePoints;
    public Image[] staminaPoints;

	// Use this for initialization
	void Start ()
    {
        f_secondsCountLife = 20f;
        f_secondsCountStamina = 6f;
}
	
	// Update is called once per frame
	void Update ()
    {
        if (b_damageRecieved)
            f_secondsCountLife = 20f;       //initial value

        //if (life == 0)
        //    PlayerDead();

        LifeSecondsCount();
        StaminaSecondsCount();

        UpdateStamina(totalStamina);
    }

    /// <summary>
    /// Function which detects if the player hasnt been hitten in 20''
    /// It restores 1 point of life per 20 seconds
    /// maximum restore = 4;
    /// </summary>
    void LifeSecondsCount()
    {
        f_secondsCountLife -= Time.deltaTime;

        if (f_secondsCountLife < 0 && totalLife < 4)
        {
            totalLife++;
            f_secondsCountLife = 20f;
        }
        
    }

    void StaminaSecondsCount()
    {
        f_secondsCountStamina -= Time.deltaTime;

        if (f_secondsCountStamina < 0 && totalStamina < 6)
        {
            totalLife++;
            f_secondsCountStamina = 6f;
        }

    }

    /// <summary>
    /// Updates the stamina on the UI depending
    /// on the total stamina the character has.
    /// </summary>
    /// <param name="totalStamina"></param>
    void UpdateLifePoints(int totalLife)
    {
        for (int i = totalLife; i <= 0; i--)
        {
            if(i!= totalLife)
                lifePoints[i].enabled = false;
        }
    }


    /// <summary>
    /// Updates the stamina on the UI depending
    /// on the total stamina the character has.
    /// </summary>
    /// <param name="totalStamina"></param>
    void UpdateStamina(int totalStamina)
    {
        for (int i = totalStamina; i <= 0; i--)
        {
            if (i != totalStamina)
                staminaPoints[i].enabled = false;
        }
    }

    /// <summary>
    /// Functions which calls the gameover
    /// </summary>
    void PlayerDead()
    {

    }

}
