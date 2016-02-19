using UnityEngine;
using System.Collections;

public class PauseButtonScript : MonoBehaviour {
	public Sprite button, buttonPressed;
	public bool gamePaused = false;

	void OnMouseEnter ()
	{
	//	gameObject.GetComponent<SpriteRenderer>().sprite = buttonPressed;
	}
	void OnMouseDown ()
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = buttonPressed;
	}
	void OnMouseExit ()
	{
	//	gameObject.GetComponent<SpriteRenderer>().sprite = button;
	}	
	void OnMouseUp ()
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = button;

		if(gamePaused){
			Time.timeScale = 1.0f;
		}
		else{
			Time.timeScale = 0.0f;
		}
		gamePaused = !gamePaused;
	}
}
