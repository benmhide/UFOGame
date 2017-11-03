using UnityEngine;
using System.Collections;

public class DroneShotScript: MonoBehaviour
{
    public float speed;
    public int damage = 5;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
    void FixedUpdate()
    {
        Destroy(gameObject, 3f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth -= damage;
            Debug.Log("Player Helath: " + GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth + " // Player lost " + damage + " health points");
        }
    }
}
