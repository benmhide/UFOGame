using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGameScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            return;
        }
    }
}
