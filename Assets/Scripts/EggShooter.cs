using UnityEngine;
using System.Collections;

public class EggShooter : MonoBehaviour {

	float lastFired;
	float shootRate; // In seconds
	StreamCell currentTarget;

	public bool pooTime = false;
	public GameObject eggBullet;
	public bool reverse = false;
	public float currentTime;

	void Awake ()
	{

	}

	// Use this for initialization
	void Start () {
		lastFired = Time.time;
		shootRate = 2.0f + (Random.value * 3.0f); // In seconds
		currentTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (!ControlScript.GAME_OVER && ControlScript.START && 
			((!reverse && (transform.parent.gameObject).GetComponentInChildren<ChickenEnemy>().startPlayDeath == -1.0f) 
				|| (reverse && (transform.parent.gameObject).GetComponentInChildren<ChickenEnemyReverse>().startPlayDeath == -1.0f) )) {
			if (currentTime > (lastFired + shootRate)) {
				lastFired = currentTime;
				shootRate = 2.5f + (Random.value * 3.0f); 
				Instantiate (eggBullet, transform.position, transform.rotation);
			}
		}
	}
}
