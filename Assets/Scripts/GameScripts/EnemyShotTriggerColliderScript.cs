using UnityEngine;
using System.Collections;

public class EnemyShotTriggerColliderScript : MonoBehaviour
{
    public GameObject damageEffects;

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

        if (other.tag == "Shot" || other.tag == "Torpedo")
        {
            Instantiate(damageEffects, other.transform.position, other.transform.rotation);
            Debug.Log("Enemy shot destoryed // Shot destoryed by player shot // Damage effects");
            Destroy(this.gameObject);
        }

        if (other.tag == "Player")
        {
            Instantiate(damageEffects, other.transform.position, other.transform.rotation);
            Debug.Log("Enemy shot hit the player!! // Must try Harder!! // Damage effects");
            Destroy(this.gameObject);
        }
    }
}