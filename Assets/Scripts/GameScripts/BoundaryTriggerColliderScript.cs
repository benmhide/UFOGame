using UnityEngine;
using System.Collections;

public class BoundaryTriggerColliderScript : MonoBehaviour
{
    public GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.tag == "Enemies")
        {
            gameController.enemiesRemaining--;
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
