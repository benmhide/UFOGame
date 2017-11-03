using UnityEngine;
using System.Collections;

public class BomberShotScript: MonoBehaviour
{
    public int damage;
    public float tumbleMin, tumbleMax;

    public GameObject bombExpolsion;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(tumbleMin, tumbleMax)));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(bombExpolsion, other.transform.position, other.transform.rotation);
            GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth -= damage;

            Debug.Log("Player Health: " + GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth +
                " // Player lost " + damage + " health points // Bomb expolsion effects BOOM!");
        }
        else if (other.tag == "Shot" || other.tag == "Torpedo")
        {
            Destroy(gameObject);
            Instantiate(bombExpolsion, other.transform.position, other.transform.rotation);
        }
    }
}
