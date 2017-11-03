using UnityEngine;
using System.Collections;

public class WeaponScriptBomber: MonoBehaviour
{ 
    public GameObject bomb;
    public Transform bombSpawn;
    public GameObject target;

    public float bombRate;
    public float delay;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("Fire", delay, bombRate);
        Debug.Log("Bomber is dropping a bombs, look out!!");
    }


    void Fire()
    {
        if (target != null)
        {
            Instantiate(bomb, bombSpawn.position, bombSpawn.rotation);
        }
        else if (target == null)
        {
            return;
        }
    }
}