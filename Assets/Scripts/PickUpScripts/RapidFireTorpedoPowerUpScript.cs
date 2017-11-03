using UnityEngine;
using System.Collections;

public class RapidFireTorpedoPowerUpScript : MonoBehaviour {

    #region Rapid Fire Torpedo Power Up Variables
    public float torpedoFireRate = 0.5f;
    public GameObject powerUpCollectedEffect;

    private GameController gameController;

    public string rapidFireTorpedoText = "Rapid Fire Torpedo x2"; 
    #endregion


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
            gameController.torpedoTextFireRate.text = rapidFireTorpedoText;
            Destroy(this.gameObject);
            Instantiate(powerUpCollectedEffect, other.transform.position, other.transform.rotation);
            Debug.Log("Pickup collected by player // Lets see what you can get from this?!");
        }
    }
}