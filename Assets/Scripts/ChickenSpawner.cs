using UnityEngine;
using System.Collections;

public class ChickenSpawner : MonoBehaviour {
	float lastEnemy;
	float spawnRate; // In seconds
	public ChickenEnemy enemy, superEnemy;
	public ChickenEnemyReverse enemyRev, superEnemyRev;
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

				float threshold = Random.value * 0.6f;
				float topSpawn = Random.value;
				if(topSpawn >= 0.5f){
					if (reverse){
						if(Random.value <= 0.2f)
							Instantiate (superEnemyRev, new Vector3(transform.position.x, transform.position.y - threshold, transform.position.z), transform.rotation);
						else
							Instantiate (enemyRev, new Vector3(transform.position.x, transform.position.y - threshold, transform.position.z), transform.rotation);
					}
					else{
						if(Random.value <= 0.2f)
							Instantiate (superEnemy, new Vector3(transform.position.x, transform.position.y - threshold, transform.position.z), transform.rotation);
						else
							Instantiate (enemy, new Vector3(transform.position.x, transform.position.y - threshold, transform.position.z), transform.rotation);
					}
				}
				else {
					if(reverse){
						if(Random.value <= 0.2f)
							Instantiate (superEnemyRev, new Vector3(transform.position.x, transform.position.y + threshold, transform.position.z), transform.rotation);
						else
							Instantiate (enemyRev, new Vector3(transform.position.x, transform.position.y + threshold, transform.position.z), transform.rotation);					}
					else{
						if(Random.value <= 0.2f)
							Instantiate (superEnemy, new Vector3(transform.position.x, transform.position.y + threshold, transform.position.z), transform.rotation);
						else
							Instantiate (enemy, new Vector3(transform.position.x, transform.position.y + threshold, transform.position.z), transform.rotation);					}
				}
			}
		}
		
	}

}
