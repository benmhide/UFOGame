using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region GameController Variables
    public GameObject[] asteroids;
    public Vector3 asteroidSpawnValues;
    public int asteroidCount;
    public float asteroidSpawnWait, asteroidStartWait, asteroidWaveWait;

    public GameObject[] enemies;
    public Vector3 enemySpawnValues;
    public int enemyCount;
    public float enemySpawnWait, enemyStartWait, enemyWaveWait;
    private int enemySpawnWaveValue = 1;
    public int enemiesRemaining = 1;

    public GameObject[] powerUps;
    public Vector3 powerUpSpawnValues;
    public int powerUpCount;
    public float powerUpSpawnWait, powerUpStartWait, powerUpWaveWait;

    public GUIText scoreText;
    public GUIText healthText;
    public GUIText shotTextFireRate;
    public GUIText shotTextDamageValue;
    public GUIText torpedoTextFireRate;
    public GUIText torpedoTextDamageValue;
    public GUIText restartText;
    public GUIText quitText;
    public GUIText gameOverText;
    public GUIText spawnWaveText;
    public GUIText enemyCounter;

    public bool gameOver;
    private bool restart;
    private bool quit;
    private int score;
    public Object endGame;

    public bool startCountDownFinished = false;
    public GameObject countDownImage;

    public AudioClip gameOverSound;
    public AudioClip youWinSound;
    public AudioClip spawnWaveComplete;

    AudioSource GameSounds;
    #endregion

    #region Start and Update Functions
    void Start()
    {
        GameSounds = GetComponent<AudioSource>();

        enemiesRemaining++;
        startCountDownFinished = false;
        restart = false;
        restartText.text = "";

        gameOver = false;
        gameOverText.text = "";

        quit = false;
        quitText.text = "";

        shotTextFireRate.text = "Laser Fire Rate: x1";
        shotTextDamageValue.text = "Laser Damage: x1";
        torpedoTextFireRate.text = "Torpedo Fire Rate: x1";
        torpedoTextDamageValue.text = "Torpedo Damage: x1";

        score = 0;
        UpdateScore();
        StartCoroutine(AsteroidSpawnWaves());
        StartCoroutine(EnemySpawnWaves());
        StartCoroutine(PowerUpSpawnWaves());
    }

    void Update()
    {

        if (startCountDownFinished == true)
        {
            countDownImage.SetActive(false);
        }
        else if (startCountDownFinished == false)
        {
            countDownImage.SetActive(true);
        }

        if (enemiesRemaining < 0)
        {
            enemiesRemaining = 0;
        }
        if (enemiesRemaining > 20)
        {
            enemiesRemaining = 0;
        }

        if (enemySpawnWaveValue >= 20 && enemiesRemaining == 0)
        {
            StartCoroutine(EndGame());
            enemyCounter.text = "";
            Debug.Log("End Game coroutine started");
        }       

        enemyCounter.text = ("Enemy Remaining: " + enemiesRemaining);

        if (enemySpawnWaveValue >= 20)
        {
            enemySpawnWaveValue = 20;
        }
        spawnWaveText.text = "Enemy Spawn Wave: " + enemySpawnWaveValue + "/20";

        if (restart && quit)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene(0);
            }
        }
    } 
    #endregion


    #region Enemy/Asteroid/Power Up Coroutines
    IEnumerator EnemySpawnWaves()
    {
        yield return new WaitForSeconds(enemyStartWait);
        while (enemySpawnWaveValue <= 20)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                Vector3 enemySpawnPosition = new Vector3(enemySpawnValues.x, Random.Range(-enemySpawnValues.y, enemySpawnValues.y), 0.0f);
                Quaternion enemySpawnRotation = Quaternion.identity;
                Instantiate(enemy, enemySpawnPosition, enemySpawnRotation);
                yield return new WaitForSeconds(enemySpawnWait);

            }
            yield return new WaitForSeconds(enemyStartWait);
            enemyCount++;
            Debug.Log("The enemy spawn count is: " + enemyCount);

            enemySpawnWaveValue++;
            Debug.Log("The enemy spawn wave is: " + enemySpawnWaveValue);

            enemySpawnWait -= 0.05f;
            Debug.Log("Wait between enemy spawn is: " + enemySpawnWait);

            //GameSounds.PlayOneShot(spawnWaveComplete);

            enemiesRemaining = enemyCount;

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                quitText.text = "Press 'Q' to Quit";
                quit = true;
                break;
            }
        }
    }

    IEnumerator AsteroidSpawnWaves()
    {
        yield return new WaitForSeconds(asteroidStartWait);
        while (asteroidCount <= 20)
        {
            for (int i = 0; i < asteroidCount; i++)
            {
                GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];
                Vector3 asteroidSpawnPosition = new Vector3(asteroidSpawnValues.x, Random.Range(-asteroidSpawnValues.y, asteroidSpawnValues.y), 0.0f);
                Quaternion asteroidSpawnRotation = Quaternion.identity;
                Instantiate(asteroid, asteroidSpawnPosition, asteroidSpawnRotation);
                yield return new WaitForSeconds(asteroidSpawnWait);
            }
            yield return new WaitForSeconds(asteroidWaveWait);
            asteroidCount++;
            Debug.Log("The asteroid spawn count is: " + asteroidCount);

            asteroidSpawnWait -= 0.05f;
            Debug.Log("Wait between asteroid spawn is: " + enemySpawnWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                quitText.text = "Press 'Q' to Quit";
                quit = true;
                break;
            }
        }
    }

    IEnumerator PowerUpSpawnWaves()
    {
        yield return new WaitForSeconds(powerUpStartWait);
        while (powerUpCount <= 20)
        {
            for (int i = 0; i < powerUpCount; i++)
            {
                GameObject powerUp = powerUps[Random.Range(0, powerUps.Length)];
                Vector3 powerUpSpawnPosition = new Vector3(Random.Range(-powerUpSpawnValues.x, powerUpSpawnValues.x), Random.Range(-powerUpSpawnValues.y, powerUpSpawnValues.y), 0.0f);
                Quaternion powerUpSpawnRotation = Quaternion.identity;
                Instantiate(powerUp, powerUpSpawnPosition, powerUpSpawnRotation);
                yield return new WaitForSeconds(powerUpSpawnWait);
            }
            yield return new WaitForSeconds(powerUpWaveWait);
            powerUpCount++;
            Debug.Log("The power up spawn count is: " + powerUpCount);

            asteroidSpawnWait -= 0.1f;
            Debug.Log("Wait between power up spawn is: " + enemySpawnWait);

            if (gameOver)
            {
                break;
            }
        }
    }
    #endregion

    #region End Game Coroutine
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(10);
        gameOverText.text = ("You Win!\n" + "Your Scored " + score + " points!");
        yield return new WaitForSeconds(10);
        GameSounds.PlayOneShot(youWinSound);
        Instantiate(endGame, transform.position, transform.rotation);
        gameOverText.text = ("");
    }
    #endregion


    #region UpdateScore/AddScore and GameOver Functions
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
        Debug.Log("Score has been updated");
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        healthText.text = "You're Dead!";
        gameOver = true;
        GameSounds.PlayOneShot(gameOverSound);
    } 
    #endregion
}
