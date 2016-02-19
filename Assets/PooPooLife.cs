using UnityEngine;
using System.Collections;

public class PooPooLife : MonoBehaviour {
	public float barDisplay; //current progress
	public Camera camera;
	private bool drawBar = false;
	private float x, y;
	private Vector2 resolution;
	private float initialXScale;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		resolution = new Vector2(Screen.width, Screen.height);

		x=Screen.width/1280.0f; 
		y=Screen.height/720.0f; 

		initialXScale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update() {

	}

	public void SetBarDisplay(float life){
		if (life <= 0.0f) {
			Destroy (gameObject);
		}
		else {
			barDisplay = life;

			float newXScale = barDisplay * initialXScale;
			transform.localScale = new Vector3 (newXScale, transform.localScale.y, transform.localScale.z);
		}
	}

	public void AutoDestroy(){
		Destroy (gameObject);
	}
}
