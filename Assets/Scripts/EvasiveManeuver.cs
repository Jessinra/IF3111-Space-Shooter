using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour
{
	[SerializeField] private MovementBoundary boundary = null;
	[SerializeField] private ManeuverConfig maneuverConfig = null;
	
	private float currentSpeed;
	private float targetManeuver;

	void Start ()
	{
		currentSpeed = GetComponent<Rigidbody>().velocity.z;
		StartCoroutine(Evade());
	}
	
	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (maneuverConfig.startWait.x, maneuverConfig.startWait.y));
		while (true)
		{
			targetManeuver = Random.Range (1, maneuverConfig.dodge) * -Mathf.Sign (this.transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverConfig.maneuverTime.x, maneuverConfig.maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverConfig.maneuverWait.x, maneuverConfig.maneuverWait.y));
		}
	}
	
	void FixedUpdate ()
	{
		float newManeuver = Mathf.MoveTowards (GetComponent<Rigidbody>().velocity.x, targetManeuver, maneuverConfig.smoothing * Time.deltaTime);
		GetComponent<Rigidbody>().velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0, 0, GetComponent<Rigidbody>().velocity.x * -maneuverConfig.tilt);
	}
}

[System.Serializable]
public class ManeuverConfig{
	public float tilt;
	public float dodge;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
}