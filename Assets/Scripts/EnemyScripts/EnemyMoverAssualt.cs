using UnityEngine;
using System.Collections;

public class EnemyMoverAssualt : MonoBehaviour
{
    private Rigidbody2D rb;
    public Boundary boundary;
    private GameController gameController;

    public float speed;
    public int impactDamage;

    private float originalY;
    public float floatStrength;

    void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        originalY = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * -(speed/4f);

        floatStrength = (Random.Range(5f, 10.0f));
        boundary.xMin = (Random.Range(5f, 25f));
    }
    void FixedUpdate()
    {
        if (speed <= 1)
        {
            speed = 0f;
        }

        transform.position = new Vector3(transform.position.x,
        originalY + (Mathf.Sin(Time.time) * floatStrength),transform.position.z);
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax), 0.0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameController.enemiesRemaining--;
            Destroy(gameObject);
            GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth -= impactDamage;

            Debug.Log("Enemy collided with the player // Player health: "
                + GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth +
                " // Damage value: " + impactDamage);
        }
    }
}




