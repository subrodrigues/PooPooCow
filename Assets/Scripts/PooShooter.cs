using UnityEngine;
using System.Collections;

public class PooShooter : MonoBehaviour {

	float lastFired;
	float shootRate; // In seconds
	StreamCell currentTarget;

	public bool pooTime = false;
	public PooBullet pooBullet;

	void Awake ()
	{

	}

	// Use this for initialization
	void Start () {
		lastFired = 0.0f;
		shootRate = 0.3f; // In seconds
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastFired + shootRate) {
				
		}
		if (pooTime) {
				pooTime = false;
				lastFired = Time.time;
				//shootRate = 0.5f;
				PooBullet poo = (PooBullet) Instantiate (pooBullet, transform.position, transform.rotation);
				poo.setTarget(currentTarget);
		} 
	}

	public void shoot(StreamCell target){

			this.pooTime = true;
			this.currentTarget = target;
		
	}
}
