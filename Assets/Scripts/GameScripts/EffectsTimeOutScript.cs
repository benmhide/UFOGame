using UnityEngine;
using System.Collections;

public class EffectsTimeOutScript : MonoBehaviour
{
    void Update ()
    {
            Destroy(gameObject, 5f);
    }
}
