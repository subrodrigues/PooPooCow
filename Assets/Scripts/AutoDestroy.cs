﻿using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ControlScript.START)
			Destroy(gameObject);
	}
}
