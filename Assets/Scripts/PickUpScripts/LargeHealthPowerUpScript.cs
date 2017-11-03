﻿using UnityEngine;
using System.Collections;

public class LargeHealthPowerUpScript : MonoBehaviour
{
    public int powerUpHealthLarge = 30;
    //private int playerHealth;

    //private PlayerHealthScript playerHealthScript;

    //public GameObject player;
    public GameObject powerUpCollectedEffect;

    //void Start()
    //{
    //    playerHealthScript = player.GetComponent<PlayerHealthScript>();
    //    playerHealth = playerHealthScript.playerHealth;
    //    Debug.Log("Access to the large health power up script // player health is: " + playerHealth);
    //}

    //void FixedUpdate()
    //{
    //    playerHealth = playerHealthScript.playerHealth;
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary")
            || other.CompareTag("Enemies")
            || other.CompareTag("Asteroid")
            || other.CompareTag("EnemyShot")
            || other.CompareTag("Shot")
            || other.CompareTag("Torpedo"))
        {
            return;
        }
        if (other.tag == "Player" /*&& playerHealth < 100*/)
        {
            Destroy(this.gameObject);
            Instantiate(powerUpCollectedEffect, other.transform.position, other.transform.rotation);
            Debug.Log("Pickup collected by player // Large health power up");
        }
    }
}