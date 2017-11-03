using UnityEngine;
using System.Collections;

public class AsteroidMoverMedium : MonoBehaviour
{
    private float speed;
    public int impactDamage;

    void Start()
    {
        speed = (Random.Range(10f, 20f));
        GetComponent<Rigidbody2D>().velocity = transform.right * -speed;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth -= impactDamage;

            Debug.Log("Asteroid hit the player // Impact damage - " + impactDamage + " // Player health: " 
                + GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth);
        }
    }
}
