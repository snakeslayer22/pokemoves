using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour{

    private void Start()
    {
        SetHealth(0);
    }

    public static void SetHealth(float damage)
    {
        PlayerMovement.PlayerHealth -= damage;

        Transform player = GameObject.FindWithTag("HealthBar").transform;
        Slider slider = player.GetComponent<Slider>();
        slider.value = PlayerMovement.PlayerHealth / PlayerMovement.maxPlayerHealth;

        if (PlayerMovement.PlayerHealth > PlayerMovement.maxPlayerHealth)
        {
            PlayerMovement.PlayerHealth = PlayerMovement.maxPlayerHealth;
        }

        if (PlayerMovement.PlayerHealth <= 0)
        {
            Debug.Log("player died");
        }
    }
}
