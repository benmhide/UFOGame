using UnityEngine;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour
{
    public int playerHealth;
    public float maxPlayerHealth = 100;
    private int powerUpHealthLarge;
    private int powerUpHealthSmall;

    private GameController gameController;
    private LargeHealthPowerUpScript largeHealthPowerUpScript;
    private SmallHealthPowerUpScript smallHealthPowerUpScript;

    public GameObject playerExplosion;
    public GameObject largeHealthPowerUp;
    public GameObject smallHealthPowerUp;
    public GameObject healthBar;

    public AudioClip healthIncreased;

    AudioSource HealthPowerUp;

    void Start()
    {
        HealthPowerUp = GetComponent<AudioSource>(); 

        playerHealth = (int)maxPlayerHealth;

        largeHealthPowerUpScript = largeHealthPowerUp.GetComponent<LargeHealthPowerUpScript>();
        powerUpHealthLarge = largeHealthPowerUpScript.powerUpHealthLarge;
        Debug.Log("Access to the variables from the LargeHealthPowerUpScript script!");

        smallHealthPowerUpScript = smallHealthPowerUp.GetComponent<SmallHealthPowerUpScript>();
        powerUpHealthSmall = smallHealthPowerUpScript.powerUpHealthSmall;
        Debug.Log("Access to the variables from the SmallHealthPowerUpScript script!");

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Shot") || other.CompareTag("Torpedo"))
        {
            return;
        }
        if (other.tag == "LargeHealth" && playerHealth < 100)
        {
            HealthPowerUp.PlayOneShot(healthIncreased);
            playerHealth += powerUpHealthLarge;
            Debug.Log("Large health power up collected by the player // " + powerUpHealthLarge + " health added to the players health");
        }
        if (other.tag == "SmallHealth" && playerHealth < 100)
        {
            HealthPowerUp.PlayOneShot(healthIncreased);
            playerHealth += powerUpHealthSmall;
            Debug.Log("Small health power up collected by the player // " + powerUpHealthSmall + " health added to the players health");
        }
    }

    void Update()
    {
        float calculatedHealth = playerHealth / maxPlayerHealth;
        SetHealthBar(calculatedHealth);

        gameController.healthText.text = ("Health: " + playerHealth + "/100");

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            gameController.GameOver();
            Destroy(gameObject);
            Instantiate(playerExplosion, transform.position, transform.rotation);
        }
        if (playerHealth > 100)
        {
            playerHealth = 100;
        }
    }
    public void SetHealthBar(float myHealth)
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
