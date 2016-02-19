using UnityEngine;
using System.Collections;

public class ChickenSpawner : MonoBehaviour {
	float lastEnemy;
	float spawnRate; // In seconds
	public ChickenEnemy enemy;
	public ChickenEnemyReverse enemyRev;
	public Transform target;
	public bool reverse = false;
	
	
	// Use this for initialization
	void Start ()
	{
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		target = go.transform;
		
		lastEnemy = 0.0f;
		spawnRate = 2.0f + (Random.value * 5.0f); // In seconds
	}
	
	// Update is called once per frame
	void Update ()
	{
		//	Debug.DrawLine (target.position, transform.position, Color.red);
		
		if (!ControlScript.GAME_OVER && ControlScript.START) {
			if (Time.time > lastEnemy + spawnRate) {
				lastEnemy = Time.time;
				spawnRate = 4.0f + (Random.value * 4.0f); 

				float threshold = Random.value * 1.2f;
				float topSpawn = Random.value;
				if(topSpawn >= 0.5f){
					if (reverse){
						Instantiate (enemyRev, new Vector3(transform.position.x, transform.position.y - threshold, transform.position.z), transform.rotation);
					}
					else{
						Instantiate (enemy, new Vector3(transform.position.x, transform.position.y - threshold, transform.position.z), transform.rotation);
					}
				}
				else {
					if(reverse){
						Instantiate (enemyRev, new Vector3(transform.position.x, transform.position.y + threshold, transform.position.z), transform.rotation);
					}
					else{
						Instantiate (enemy, new Vector3(transform.position.x, transform.position.y + threshold, transform.position.z), transform.rotation);
					}
				}
			}
		}
		
	}

}
