using UnityEngine;
using System.Collections;

public class EnemyMoverBomber : MonoBehaviour
{
    private Rigidbody2D rb;
    public Boundary boundary;
    private GameController gameController;

    public float speed;
    public int impactDamage;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * -speed;
        speed = (Random.Range(20f, 25.0f));
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
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




