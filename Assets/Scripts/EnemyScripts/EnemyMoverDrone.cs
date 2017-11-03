using UnityEngine;
using System.Collections;

public class EnemyMoverDrone : MonoBehaviour
{
    private Rigidbody2D rb;
    public Boundary boundary;
    private GameController gameController;

    private Vector3 axis;
    private Vector3 pos;

    private float speed, frequency, magnitude;
    public int impactDamage;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        speed = (Random.Range(5f, 8f));
        frequency = (Random.Range(1f, 3f));
        magnitude = (Random.Range(5f, 8f));
        Debug.Log("Drone will move in a sine wave with values of: " + speed + " speed // " + frequency +
            " frequency // " + magnitude + " magnitude");

        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
        axis = transform.up;
    }

    void FixedUpdate()
    {
        pos += transform.right * Time.deltaTime * -speed;
        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;

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


