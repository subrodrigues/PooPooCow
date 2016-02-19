using UnityEngine;
using System.Collections;

public class PooPooRateGauge : MonoBehaviour {
	public float barDisplay; //current progress
	private float initialXScale;

	// Use this for initialization
	void Start () {
		initialXScale = transform.localScale.x;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetBarDisplay(float scale){
		barDisplay = scale;

		float newXScale = barDisplay * initialXScale;
	//	transform.localScale = new Vector3 (newXScale, newXScale, newXScale);
		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (newXScale, newXScale, newXScale), Time.deltaTime);
	}

	public void AutoDestroy(){
		Destroy (gameObject);
	}
}
