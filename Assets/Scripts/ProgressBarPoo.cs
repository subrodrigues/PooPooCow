using UnityEngine;
using System.Collections;

public class ProgressBarPoo : MonoBehaviour
{
	public float barDisplay;
	//current progress
	private Camera mainCamera;
	private bool drawBar = false;
	public PooPooLife pooPooLife;
	private PooPooLife instantiatedObject;
	private float x, y;
	private Vector2 resolution;

	void Start ()
	{
		//	pooPooLife.GetComponent<Renderer>().enabled = false;
		instantiatedObject = (PooPooLife)Instantiate (pooPooLife, transform.position, transform.rotation);
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		resolution = new Vector2 (Screen.width, Screen.height);

		x = Screen.width / 1280.0f; 

		y = Screen.height / 720.0f; 
	}
		
	void Update ()
	{
        
		if (Screen.width != resolution.x || Screen.height != resolution.y) {
			resolution = new Vector2 (Screen.width, Screen.height);
			x = resolution.x / 1280.0f;
			y = resolution.y / 720.0f;
		}
	     
		//for this example, the bar display is linked to the current time,
		//however you would set this value based on your desired display
		//eg, the loading progress, the player's health, or whatever.
		StreamCell tempVal = transform.GetComponentInChildren<StreamCell> ();
		barDisplay = tempVal.life;

		if (drawBar && barDisplay <= 0.0f) {
			instantiatedObject.AutoDestroy ();
			drawBar = false;
		}
		else if (drawBar) {
			instantiatedObject.transform.position = new Vector3 (transform.position.x, transform.position.y - 0.16f, transform.position.z);
			instantiatedObject.SetBarDisplay (tempVal.life);
		}

	}

	public void startBar ()
	{
		this.drawBar = true;
	}
 
}
