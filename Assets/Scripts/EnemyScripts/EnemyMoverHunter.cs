using UnityEngine;
using System.Collections;

public class EnemyMoverHunter : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D rb;
    private GameController gameController;

    public float speed = 5;
    public float rotatingSpeed = 200;

    public int impactDamage = 5;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;
            point2Target.Normalize();
            float value = Vector3.Cross(point2Target, -transform.right).z;


            if (value > 0)
            {
                rb.angularVelocity = rotatingSpeed;
            }
            else if (value < 0)
            {
                rb.angularVelocity = -rotatingSpeed;
            }
            else
            {
                rotatingSpeed = 0;
            }

            rb.angularVelocity = rotatingSpeed * value;
            rb.velocity = -transform.right * speed;
        }

        else if (target == null)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.rotation = 0f;
            return;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameController.enemiesRemaining--;
            Destroy(gameObject);
            GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth -= impactDamage;

            Debug.Log("Enemy collided with the player // Player health: " +
                GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth +
                " // Damage value: " + impactDamage);
        }
    }
}



