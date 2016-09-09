using UnityEngine;
using System.Collections;

public class PauseButtonScript : MonoBehaviour {
	public Sprite button, buttonPressed;
	public Sprite playButton, playButtonPressed;
	public bool gamePaused = false;
	public bool isButtonFocused = false;

	public void UnpauseGame(){
		isButtonFocused = false;

		if(gamePaused){
			
			gameObject.GetComponent<SpriteRenderer>().sprite = button;
			Time.timeScale = 1.0f;
		}
		else{
			gameObject.GetComponent<SpriteRenderer>().sprite = playButton;
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

		if(gamePaused)
			gameObject.GetComponent<SpriteRenderer>().sprite = playButtonPressed;
		else
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
