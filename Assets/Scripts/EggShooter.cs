using UnityEngine;
using System.Collections;

public class EggShooter : MonoBehaviour {

	float lastFired;
	float shootRate; // In seconds
	StreamCell currentTarget;

	public bool pooTime = false;
	public GameObject eggBullet;
	public bool reverse = false;

	void Awake ()
	{

	}

	// Use this for initialization
	void Start () {
		lastFired = 0.0f;
		shootRate = 5.0f + (Random.value * 8.0f); // In seconds
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastFired + shootRate) {
				
		}

		if (!ControlScript.GAME_OVER && ControlScript.START && 
			((!reverse && (transform.parent.gameObject).GetComponentInChildren<ChickenEnemy>().startPlayDeath == -1.0f) 
				|| (reverse && (transform.parent.gameObject).GetComponentInChildren<ChickenEnemyReverse>().startPlayDeath == -1.0f) )) {
			if (Time.time > lastFired + shootRate) {
				lastFired = Time.time;
				shootRate = 4.0f + (Random.value * 4.0f); 
					Instantiate (eggBullet, transform.position, transform.rotation);
			}
		}
	}
}
