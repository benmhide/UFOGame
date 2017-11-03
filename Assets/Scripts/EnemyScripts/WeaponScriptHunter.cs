using UnityEngine;
using System.Collections;

public class WeaponScriptHunter: MonoBehaviour
{
    public GameObject hunterShot;
    public Transform shotSpawn;
    public GameObject target;

    private float fireRate;
    private float delay;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        fireRate = Random.Range(0.5f, 1.5f);
        delay = Random.Range(0.5f, 1.5f);

        InvokeRepeating("Fire", delay, fireRate);
        Debug.Log("Hunter fired a shot, look out!!");
    }


    void Fire()
    {
        if (target != null)
        {
            Instantiate(hunterShot, shotSpawn.position, shotSpawn.rotation);
        }
        else if (target == null)
        {
            return;
        }
    }
}