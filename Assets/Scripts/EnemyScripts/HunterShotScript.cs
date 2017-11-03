using UnityEngine;
using System.Collections;

public class HunterShotScript: MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;
    public int damage = 15;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
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
            Debug.Log("Player Health: " + GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth + " // Player lost " + damage + " health points");
        }
    }
}
