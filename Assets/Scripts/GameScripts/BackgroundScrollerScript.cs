using UnityEngine;
using System.Collections;

public class BackgroundScrollerScript : MonoBehaviour
{
    public float speed = 0.5f;
    private Renderer rd;

	void Start ()
    {
        rd = GetComponent<Renderer>();
        Debug.Log("The background will scroll!!");
    }
	
	void FixedUpdate ()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        rd.material.mainTextureOffset = offset;
	}
}
