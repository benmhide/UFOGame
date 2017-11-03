using UnityEngine;
using System.Collections;

public class CameraShakeScript: MonoBehaviour
{
    #region Camera Shake Variables
    public Transform camTransform;

    public float shakeDuration;

    public float shakeAmount;
    public float decreaseFactor;

    Vector3 originalPos; 
    #endregion

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        Debug.Log("The cameras original position is: " + originalPos);
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}