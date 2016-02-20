using UnityEngine;
using System.Collections;

public class EggShooter : MonoBehaviour {

	float lastFired;
	float shootRate; // In seconds
	StreamCell currentTarget;

	public bool pooTime = false;
	public GameObject eggBullet;
	public bool reverse = false;
	public bool superChicken;
	public float currentTime;

	void Awake ()
	{

	}

	// Use this for initialization
	void Start () {
		lastFired = Time.time;

		if(superChicken) // Bigger probability of eggs barrage at super chicken
			shootRate = 1.0f + (Random.value * 2.0f); // In seconds
		else
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

				if (!superChicken) {
					lastFired = currentTime;
					shootRate = 2.5f + (Random.value * 3.0f); 
				}

				Instantiate (eggBullet, transform.position, transform.rotation);
			}
		}
	}
}
