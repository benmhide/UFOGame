using UnityEngine;
using System.Collections;

public class WeaponScriptAssualt: MonoBehaviour
{
    public GameObject assualtShot;
    public Transform shotSpawn;
    public GameObject target;

    public float fireRate = 3f;
    public float delay = 3f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("Fire", delay, fireRate);
        Debug.Log("Assualt fired a missile, look out!!");
    }


    void Fire()
    {
        if (target != null)
        {
            Instantiate(assualtShot, shotSpawn.position, shotSpawn.rotation);
        }
        else if (target == null)
        {
            return;
        }
    }
}