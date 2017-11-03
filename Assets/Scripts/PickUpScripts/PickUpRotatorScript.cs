using UnityEngine;
using System.Collections;

public class PickUpRotatorScript : MonoBehaviour
{
	void Update () 
	{
		transform.Rotate (new Vector3 (45, 45, 0) * Time.deltaTime);
	}
}
