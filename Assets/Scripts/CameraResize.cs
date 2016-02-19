using UnityEngine;
using System.Collections;

public class CameraResize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		this.GetComponent<Camera>().aspect = 16f/10f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
