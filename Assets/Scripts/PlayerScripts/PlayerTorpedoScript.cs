using UnityEngine;
using System.Collections;

public class PlayerTorpedoScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject enemyDamageEffects;

    public float speed;

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

        if (other.CompareTag("Boundary")
            || other.CompareTag("Player")
            || other.CompareTag("Shot")
            || other.CompareTag("Torpedo")
            || other.CompareTag("LargeHealth")
            || other.CompareTag("SmallHealth"))
        {
            return;
        }

        if (other.tag == "Asteroid" || other.tag == "Enemies")
        {
            Destroy(gameObject);
            Instantiate(enemyDamageEffects, other.transform.position, other.transform.rotation);
            Debug.Log("Direct hit with a torpedo // Player hit an enemy or asteroid // Great kid, dont get cocky!");
        }

        if (other.tag == "EnemyShot")
        {
            Instantiate(enemyDamageEffects, other.transform.position, other.transform.rotation);
            Debug.Log("Direct hit with a torpedo // Player hit an enemy shot // Have you been practising?");
        }
    }
}

