﻿using UnityEngine;
using System.Collections;

public class EggBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			ControlScript.GAME_OVER = true;
		} else if (other.tag == "EndPoint") {
			Destroy (gameObject);
		} 
	}
}