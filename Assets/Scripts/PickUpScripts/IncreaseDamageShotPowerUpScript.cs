using UnityEngine;
using System.Collections;

public class IncreaseDamageShotPowerUpScript : MonoBehaviour
{
    public int shotDamage = 10;
    public GameObject powerUpCollectedEffect;

    private GameController gameController;

    public string increasedDamageShotText = "Laser Damage x2";

    void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary")
            || other.CompareTag("Enemies")
            || other.CompareTag("Asteroid")
            || other.CompareTag("EnemyShot")
            || other.CompareTag("Shot")
            || other.CompareTag("Torpedo"))
        {
            return;
        }
        if (other.tag == "Player")
        {
            gameController.shotTextDamageValue.text = increasedDamageShotText;
            Destroy(gameObject);
            Instantiate(powerUpCollectedEffect, other.transform.position, other.transform.rotation);
            Debug.Log("Pickup collected by player // Lets see what you can get from this?!");
        }
    }
}