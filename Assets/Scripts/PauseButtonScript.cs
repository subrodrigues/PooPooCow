using UnityEngine;
using System.Collections;

public class PauseButtonScript : MonoBehaviour {
	public Sprite button, buttonPressed;
	public bool gamePaused = false;
	public bool isButtonFocused = false;

	public void UnpauseGame(){
		isButtonFocused = false;

		gameObject.GetComponent<SpriteRenderer>().sprite = button;

		if(gamePaused){
			Time.timeScale = 1.0f;
		}
		else{
			Time.timeScale = 0.0f;
		}
		gamePaused = !gamePaused;
	}

	void OnMouseEnter ()
	{
		isButtonFocused = true;
	}
	void OnMouseDown ()
	{
		isButtonFocused = true;

		gameObject.GetComponent<SpriteRenderer>().sprite = buttonPressed;
	}
	void OnMouseExit ()
	{
		isButtonFocused = false;
	}	
	void OnMouseUp ()
	{
		UnpauseGame ();
	}
}
