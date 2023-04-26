using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Image healthBar; // healthbar image object
	public Image healthBar2; // healthbar image object for vr ui

	public float healthAmount = 100.0f; // healtbar amount
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void takeDamage(float damage) // fucntion to dicrease the amount of health
	{
		healthAmount -= damage;
        healthBar.fillAmount = healthAmount/ 100f;
		healthBar2.fillAmount = healthAmount / 100f;

	}

	public void heal(float healAmount)// fucntion to increase the amount of health
	{
		healthAmount += healAmount;
        healthAmount =Mathf.Clamp(healthAmount, 0, 100);
		healthBar.fillAmount = healthAmount / 100f;
		healthBar2.fillAmount = healthAmount / 100f;

	}



}
