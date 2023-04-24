using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Image healthBar;
    public float healthAmount = 100.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void takeDamage(float damage)
	{
		healthAmount -= damage;
        healthBar.fillAmount = healthAmount/ 100f;
	}

	public void heal(float healAmount)
	{
		healthAmount += healAmount;
        healthAmount =Mathf.Clamp(healthAmount, 0, 100);
		healthBar.fillAmount = healthAmount / 100f;

	}



}
