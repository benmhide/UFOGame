using UnityEngine;
using System.Collections;

public class RapidFireShotPowerUpScript : MonoBehaviour
{
    public float shotFireRate = 0.25f;

    public GameObject powerUpCollectedEffect;

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
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            Instantiate(powerUpCollectedEffect, other.transform.position, other.transform.rotation);
            Debug.Log("Pickup collected by player // Lets see what you can get from this?!");
        }
    }
}