using UnityEngine;
using System.Collections;

public class WeaponScriptDrone : MonoBehaviour
{
    public GameObject droneShot;
    public Transform shotSpawn;
    public GameObject target;

    private float fireRate, delay;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        fireRate = Random.Range(0.5f, 1f);
        delay = Random.Range(0.5f, 1f);

        InvokeRepeating("Fire", delay, fireRate);
        Debug.Log("Drone is firing, look out!!");
    }


    void Fire()
    {
        if (target != null)
        {
            Instantiate(droneShot, shotSpawn.position, shotSpawn.rotation);
        }
        else if (target == null)
        {
            return;
        }
    }
}