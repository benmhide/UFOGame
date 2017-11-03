using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[System.Serializable]

#region Boundary Class
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
} 
#endregion

public class PlayerController : MonoBehaviour
{
    #region PlayerController Variables
    public float speed;
    public float playerShipRoll;

    public Boundary boundary;

    public GameObject shot;
    public GameObject shotIncreasedDamage;
    public GameObject torpedo;
    public GameObject torpedoIncreasedDamage;
    public GameObject rapidFirePowerUpShot;
    public GameObject rapidFirePowerUpTorpedo;
    public GameObject increaseDamagePowerUpShot;
    public GameObject increaseDamagePowerUpTorpedo;
    public Transform shotSpawn;
    public Transform torpedoSpawn;


    private Rigidbody2D rb;


    public int shotDamage = 5;
    public bool isShotDamageIncreased = false;
    public int torpedoDamage = 15;
    public bool isTorpedoDamageIncreased = false;


    public float shotFireRate;
    public bool isRapidFireShot = false;
    public float torpedoFireRate;
    public bool isRapidFireTorpedo = false;


    private float shotNextFire;
    private float torpedoNextFire;

    public GameObject mainCamera;


    private RapidFireShotPowerUpScript rapidFireShotPowerUpScript;
    private RapidFireTorpedoPowerUpScript rapidFireTorpedoPowerUpScript;
    private IncreaseDamageShotPowerUpScript increaseDamageShotPowerUpScript;
    private IncreaseDamageTorpedoPowerUpScript increaseDamageTorpedoPowerUpScript;
    private GameController gameController;
    public CameraShakeScript cameraShakeScript;


    private string rapidFireShotText;
    private string increasedDamageShotText;
    private string rapidFireTorpedoText;
    private string increasedDamageTorpedoText;
    private string shotTextFireRate = "Laser Fire Rate: x1";
    private string shotTextDamageValue = "Laser Damage: x1";
    private string torpedoTextFireRate = "Torpedo Fire Rate: x1";
    private string torpedoTextDamageValue = "Torpedo Damage: x1";

    public AudioClip shotFireRateIncreased;
    public AudioClip shotFireRateDecreased;
    public AudioClip torpedoFireRateIncreased;
    public AudioClip torpedoFireRateDecreased;
    public AudioClip shotDamageIncreased;
    public AudioClip shotDamageDecreased;
    public AudioClip torpedoDamageIncreased;
    public AudioClip torpedoDamageDecreased;

    AudioSource PowerUpSounds;

    #endregion

    #region Start Function
    void Start()
    {
        PowerUpSounds = GetComponent<AudioSource>();

        Debug.Log("Rapid fire shot is off // is: " + isRapidFireShot);
        Debug.Log("Rapid fire torpedo is off // is: " + isRapidFireTorpedo);
        Debug.Log("Increased shot damage is off // is: " + isRapidFireShot);
        Debug.Log("Increased torpedo damage is off // is: " + isRapidFireTorpedo);

        rb = GetComponent<Rigidbody2D>();

        Debug.Log("Enemy has access to the cameraShake script!");

        rapidFireShotPowerUpScript = rapidFirePowerUpShot.GetComponent<RapidFireShotPowerUpScript>();
        Debug.Log("Access to the variables from the RapidFireShotPowerUpScript script!");

        rapidFireTorpedoPowerUpScript = rapidFirePowerUpTorpedo.GetComponent<RapidFireTorpedoPowerUpScript>();
        Debug.Log("Access to the variables from the RapidFireTorpedoPowerUpScript script!");

        increaseDamageShotPowerUpScript = increaseDamagePowerUpShot.GetComponent<IncreaseDamageShotPowerUpScript>();
        Debug.Log("Access to the variables from the IncreaseDamageShotPowerUpScript script!");

        increaseDamageTorpedoPowerUpScript = increaseDamagePowerUpTorpedo.GetComponent<IncreaseDamageTorpedoPowerUpScript>();
        Debug.Log("Access to the variables from the IncreaseDamageTorpedoPowerUpScript script!");

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    } 
    #endregion

    #region Trigger Collider 2D
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary")
            || other.CompareTag("Shot")
            || other.CompareTag("Torpedo"))
        {
            return;
        }

        if (other.CompareTag("Enemies")
            || other.CompareTag("Asteroid")
            || other.CompareTag("EnemyShot"))
        {
            cameraShakeScript.shakeAmount = 1f;
            cameraShakeScript.shakeDuration = 1f;
            Debug.Log("Camera will shake");
        }

        #region Shot Fire Rate Power Up
        if (other.tag == "RapidFireShot")
        {
            PowerUpSounds.PlayOneShot(shotFireRateIncreased);
            rapidFireShotText = "Laser Fire Rate: x2";
            isRapidFireShot = true;

            Debug.Log("Rapid fire shot is on // is: " + isRapidFireShot);

            if (shotFireRate == 0.25f)
            {
                PowerUpSounds.PlayOneShot(shotFireRateIncreased);
                shotFireRate = 0.125f;
                rapidFireShotText = "Laser Fire Rate: x4";
                gameController.shotTextFireRate.text = rapidFireShotText;
                Debug.Log("Shot fire rate is: " + shotFireRate);
            }
            else
            {
                shotFireRate = rapidFireShotPowerUpScript.shotFireRate;
            }

            Debug.Log("Shot fire rate is: " + shotFireRate);

            if (isRapidFireShot == true)
            {
                StartCoroutine(PowerUpTimerShotFireRate());
                Debug.Log("PowerUpTimerShotFireRate coroutine started for shot fire rate");
            }
        }
        #endregion

        #region Increased Shot Damage Power Up
        if (other.tag == "IncreasedDamageShot")
        {
            PowerUpSounds.PlayOneShot(shotDamageIncreased);
            increasedDamageShotText = "Laser Damage: x2";
            isShotDamageIncreased = true;

            Debug.Log("Increased shot damage is on // is: " + isShotDamageIncreased);

            if (shotDamage == 10)
            {
                PowerUpSounds.PlayOneShot(shotDamageIncreased);
                shotDamage = 20;
                increasedDamageShotText = "Laser Damage: x4";
                gameController.shotTextDamageValue.text = increasedDamageShotText;
                Debug.Log("Shot damage is: " + shotDamage);
            }
            else
            {
                shotDamage = increaseDamageShotPowerUpScript.shotDamage;
            }
            Debug.Log("Shot damage is: " + shotDamage);

            if (isShotDamageIncreased == true)
            {
                StartCoroutine(PowerUpTimerShotDamage());
                Debug.Log("PowerUpTimerShotDamage coroutine started for shot damage increased");
            }
        }
        #endregion

        #region Torpedo Fire Rate Power Up
        if (other.tag == "RapidFireTorpedo")
        {
            PowerUpSounds.PlayOneShot(torpedoFireRateIncreased);
            rapidFireTorpedoText = "Torpedo Fire Rate: x2";
            isRapidFireTorpedo = true;

            Debug.Log("Rapid fire torpedo is on // is " + isRapidFireTorpedo);

            if (torpedoFireRate == 0.5f)
            {
                PowerUpSounds.PlayOneShot(torpedoFireRateIncreased);
                torpedoFireRate = 0.25f;
                rapidFireTorpedoText = "Torpedo Fire Rate: x4";
                gameController.torpedoTextFireRate.text = rapidFireTorpedoText;
                Debug.Log("Torpedo fire rate is " + torpedoFireRate);
            }
            else
            {
                torpedoFireRate = rapidFireTorpedoPowerUpScript.torpedoFireRate;
            }
            Debug.Log("Torpedo fire rate is " + torpedoFireRate);

            if (isRapidFireTorpedo == true)
            {
                StartCoroutine(PowerUpTimerTorpedoFireRate());
                Debug.Log("PowerUpTimerTorpedoFireRate coroutine started for torpedo fire rate");
            }
        }
        #endregion

        #region Increased Torpedo Damage Power Up
        if (other.tag == "IncreasedDamageTorpedo")
        {
            PowerUpSounds.PlayOneShot(torpedoDamageIncreased);
            increasedDamageTorpedoText = "Torpedo Damage: x2";
            isTorpedoDamageIncreased = true;

            Debug.Log("Increased torpedo damage is on // is " + isRapidFireTorpedo);

            if (torpedoDamage == 30)
            {
                PowerUpSounds.PlayOneShot(torpedoDamageIncreased);
                torpedoDamage = 45;  // Not times four damage but player won't know hahahaha
                increasedDamageTorpedoText = "Torpedo Damage: x4";
                gameController.torpedoTextDamageValue.text = increasedDamageTorpedoText;
                Debug.Log("Torpedo damage is: " + torpedoDamage);
            }
            else
            {
                torpedoDamage = increaseDamageTorpedoPowerUpScript.torpedoDamage;
            }
            Debug.Log("Torpedo damage is " + torpedoDamage);

            if (isTorpedoDamageIncreased == true)
            {
                StartCoroutine(PowerUpTimerTorpedoDamage());
                Debug.Log("PowerUpTimerTorpedoDamage coroutine started for torpedo damage increased");
            }
        }
        #endregion
    } 
    #endregion

    #region Update Function - Player Shooting Controls
    void Update()
    {
        if (gameController.startCountDownFinished == true)
        {
            if (Input.GetButton("Fire1") && Time.time > shotNextFire)
            {
                shotNextFire = Time.time + shotFireRate;

                if (isShotDamageIncreased == false)
                {
                    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                }
                else if (isShotDamageIncreased == true)
                {
                    Instantiate(shotIncreasedDamage, shotSpawn.position, shotSpawn.rotation);
                }
            }
            if (Input.GetButton("Fire2") && Time.time > torpedoNextFire)
            {
                cameraShakeScript.shakeAmount = 0.2f;
                cameraShakeScript.shakeDuration = 0.1f;

                torpedoNextFire = Time.time + torpedoFireRate;

                if (isTorpedoDamageIncreased == false)
                {
                    Instantiate(torpedo, torpedoSpawn.position, torpedoSpawn.rotation);
                }
                else if (isTorpedoDamageIncreased == true)
                {
                    Instantiate(torpedoIncreasedDamage, torpedoSpawn.position, torpedoSpawn.rotation);
                }
            }
            else if (gameController.startCountDownFinished == true)
            {
                return;
            }
        }       
    }
    #endregion

    #region FixedUpdate Function - Player Movement Controls
    void FixedUpdate()
    {
        if (gameController.startCountDownFinished == true)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
            rb.velocity = movement * speed;

            rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax), 0.0f);

            rb.rotation = rb.velocity.y * playerShipRoll;
        }
        else if (gameController.startCountDownFinished == true)
        {
            return;
        }
    }
    #endregion

    #region Coroutines For Power UPs
    IEnumerator PowerUpTimerShotFireRate()
    {
        gameController.shotTextFireRate.text = rapidFireShotText;

        yield return new WaitForSeconds(15);
        Debug.Log("PowerUpTimerShotFireRate called waiting for 15 seconds // PowerUp will be lost after WaitForSeconds");

        if (isRapidFireShot == true)
        {
            PowerUpSounds.PlayOneShot(shotFireRateDecreased);
            isRapidFireShot = false;
            shotFireRate = 0.5f;
            rapidFireShotText = "";
            gameController.shotTextFireRate.text = shotTextFireRate;
            Debug.Log("Rapid fire shot is off // is: " + isRapidFireShot);
            Debug.Log("Shot fire rate is: " + shotFireRate);
        }
    }
    IEnumerator PowerUpTimerShotDamage()
    {
        gameController.shotTextDamageValue.text = increasedDamageShotText;

        yield return new WaitForSeconds(15);
        Debug.Log("PowerUpTimerShotDamage called waiting for 15 seconds // PowerUp will be lost after WaitForSeconds");

        if (isShotDamageIncreased == true)
        {
            PowerUpSounds.PlayOneShot(shotDamageDecreased);
            isShotDamageIncreased = false;
            shotDamage = 5;
            increasedDamageShotText = "";
            gameController.shotTextDamageValue.text = shotTextDamageValue;
            Debug.Log("Shot damage is off // is: " + isShotDamageIncreased);
            Debug.Log("Shot Damage is: " + shotDamage);
        }
    }
    IEnumerator PowerUpTimerTorpedoFireRate()
    {
        gameController.torpedoTextFireRate.text = rapidFireTorpedoText;

        yield return new WaitForSeconds(15);
        Debug.Log("PowerUpTimerTorpedoFireRate called waiting for 15 seconds // PowerUp will be lost after WaitForSeconds");

        if (isRapidFireTorpedo == true)
        {
            PowerUpSounds.PlayOneShot(torpedoFireRateDecreased);
            isRapidFireTorpedo = false;
            torpedoFireRate = 1f;
            rapidFireTorpedoText = "";
            gameController.torpedoTextFireRate.text = torpedoTextFireRate;
            Debug.Log("Rapid fire torpedo is off // is: " + isRapidFireTorpedo);
            Debug.Log("Torpedo fire rate is: " + torpedoFireRate);
        }
    }
    IEnumerator PowerUpTimerTorpedoDamage()
    {
        gameController.torpedoTextDamageValue.text = increasedDamageTorpedoText;

        yield return new WaitForSeconds(15);
        Debug.Log("PowerUpTimerTorpedoDamage called waiting for 15 seconds // PowerUp will be lost after WaitForSeconds");

        if (isTorpedoDamageIncreased == true)
        {
            PowerUpSounds.PlayOneShot(torpedoDamageDecreased);
            isTorpedoDamageIncreased = false;
            torpedoDamage = 15;
            increasedDamageTorpedoText = "";
            gameController.torpedoTextDamageValue.text = torpedoTextDamageValue;
            Debug.Log("Torpedo damage is off // is: " + isTorpedoDamageIncreased);
            Debug.Log("Torpedo Damage is: " + torpedoDamage);
        }
    } 
    #endregion
}