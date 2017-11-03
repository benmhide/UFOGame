using UnityEngine;
using System.Collections;

public class AsteroidRotatorScript : MonoBehaviour
{
    public float tumbleMin, tumbleMax;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(tumbleMin, tumbleMax)));
    }
}

