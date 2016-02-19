using UnityEngine;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour {
	float lastEnemy;
	float spawnRate; // In seconds
	public Enemy enemy;
	public ScoreItem scoreItem;
	public Transform target;
	
	// Use this for initialization
	void Start ()
	{
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		target = go.transform;
		
		lastEnemy = 0.0f;
		spawnRate = (Random.value * 2.0f); // In seconds
	}
	
	// Update is called once per frame
	void Update ()
	{
		//	Debug.DrawLine (target.position, transform.position, Color.red);

		if (!ControlScript.GAME_OVER && ControlScript.START) {
			if (Time.time > lastEnemy + spawnRate) {
					lastEnemy = Time.time;
					spawnRate = 1f + (Random.value * 3.0f); 

					if(Random.value < 0.5){
					Instantiate (enemy, transform.position, transform.rotation);
					 }
					 else{
					 	Instantiate (scoreItem, transform.position, transform.rotation);
					 }
			}
		}
		
	}

}
