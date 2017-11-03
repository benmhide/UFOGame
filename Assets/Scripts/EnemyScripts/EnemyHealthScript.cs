using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour
{
    public GameObject explosion;
    public GameObject damageEffects;
    public GameObject player;
    public GameObject healthBar;

    public int scoreValue;
    public int enemyHealth;
    public float maxEnemyHealth;
    private int shotDamage;
    private int torpedoDamage;

    private GameController gameController;
    private PlayerController playerController;


    void Start()
    {
        enemyHealth = (int)maxEnemyHealth;

        playerController = player.GetComponent<PlayerController>();
        Debug.Log("Enemy has access to the PlayerController script!");

        shotDamage = playerController.shotDamage;
        Debug.Log("Enemy has access to the shotDamage variable from the PlayerController script!");

        torpedoDamage = playerController.torpedoDamage;
        Debug.Log("Enemy has access to the torpedoDamage variable from the PlayerController script!");

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
            Debug.Log("Enemy Damaged by shot // enemy health: " + enemyHealth + " // damage: "
                + shotDamage + "  // Enemy damage effects");
        }

        if (other.tag == "Torpedo")
        {
            enemyHealth -= torpedoDamage;
            Instantiate(damageEffects, transform.position, transform.rotation);
            Debug.Log("Enemy Damaged by shot // enemy health: " + enemyHealth + " // damage: "
                + torpedoDamage + "  // Enemy damage effects");
        }

        if (other.tag == "Player")
        {
            Instantiate(damageEffects, other.transform.position, other.transform.rotation);
            Instantiate(explosion, transform.position, transform.rotation);
            Debug.Log("You got hit by an enemy BE MORE CAREFUL!! // Player damaged by impact // Enemy explosion effects // No points for that hahahahaha");
        }
    }

    void Update()
    {
        float calculatedHealth = enemyHealth / maxEnemyHealth;
        SetHealthBar(calculatedHealth);

        if (enemyHealth <= 0)
        {
            gameController.enemiesRemaining--;
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            Debug.Log("Enemy destoryed" + " // Score added + " + scoreValue +
                " // You got some points whoop!!");

            gameController.AddScore(scoreValue);
            Debug.Log(scoreValue + " points have been added to your score using the GameController AddScore Function");
        }
    }
    public void SetHealthBar(float enemiesHealth)
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(enemiesHealth, 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}