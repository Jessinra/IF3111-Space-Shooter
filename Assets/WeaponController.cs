using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ShotConfig shotConfig = null;
	[SerializeField] private float delay = 0.0F;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, shotConfig.shotDelay);
	}

	void Fire ()
	{
		Instantiate(shotConfig.bullet, shotConfig.shotSpawnPoint.position, shotConfig.shotSpawnPoint.rotation);
		GetComponent<AudioSource>().Play();
	}
}