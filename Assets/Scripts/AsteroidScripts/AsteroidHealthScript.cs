using UnityEngine;
using System.Collections;

public class AsteroidHealthScript : MonoBehaviour
{
    public GameObject damageEffects;
    public GameObject asteroidExplosion;
    public GameObject player;

    public int scoreValue;
    public int enemyHealth;
    private int shotDamage;
    private int torpedoDamage;

    private GameController gameController;
    private PlayerController playerController;


    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        Debug.Log("Enemy has access to the PlayerController script!");

        shotDamage = playerController.shotDamage;
        Debug.Log("Asteroid has access to the shotDamage variable from the PlayerController script!");

        torpedoDamage = playerController.torpedoDamage;
        Debug.Log("Asteroid has access to the torpedoDamage variable from the PlayerController script!");

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary")
            || other.CompareTag("Enemies")
            || other.CompareTag("Asteroid")
            || other.CompareTag("LargeHealth")
            || other.CompareTag("SmallHealth")
            || other.CompareTag("EnemyShot"))
        {
            return;
        }

        if (other.tag == "Shot")
        {
            enemyHealth -= shotDamage;
            Instantiate(damageEffects, transform.position, transform.rotation);
            Debug.Log("Asteroid damaged by shot // Asteroid health: " + enemyHealth + 
                " // Damage: " + shotDamage + "  // Asteroid damage effects");
        }

        if (other.tag == "Torpedo")
        {
            enemyHealth -= torpedoDamage;
            Instantiate(damageEffects, transform.position, transform.rotation);
            Debug.Log("Asteroid damaged by shot // Asteroid health: " + enemyHealth +
                " // Damage: " + shotDamage + "  // Asteroid damage effects");
        }

        if (other.tag == "Player")
        {
            Instantiate(damageEffects, other.transform.position, other.transform.rotation);
            Instantiate(asteroidExplosion, transform.position, transform.rotation);
            Debug.Log("You got hit by an astroid BE MORE CAREFUL!! // Player damaged" + 
                " // Asteroid Explosion effects // No points for that hahahahaha");
        }
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(asteroidExplosion, transform.position, transform.rotation);
            Debug.Log("Asteroid destoryed" + " // Score added + " + scoreValue +
                " // You got some points whoop!! // Asteroid explosion effects");

            gameController.AddScore(scoreValue);
            Debug.Log(scoreValue + " points have been added to the GameController AddScore Function");
        }
    }
}