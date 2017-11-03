using UnityEngine;
using System.Collections;

public class AssualtShotScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject target;
    public GameObject missileDamageEffects;

    public int damage;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos - transform.position);
            rb.AddForce(gameObject.transform.up * speed);
        }
        else if (target == null)
        {
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(missileDamageEffects, other.transform.position, other.transform.rotation);
            GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth -= damage;

            Debug.Log("Player Health: " + GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth + 
                " // Player lost " + damage + " health points");
        }
    }
}