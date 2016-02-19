using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	public bool isExit, isPlayButton, isScoreButton;
	public Sprite button, buttonPressed;

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

		if (isExit) {
			Application.Quit ();
		} else if (isPlayButton) {
			Application.LoadLevel ("level");
		} else if (isScoreButton) {
			if(GPLeaderBoard.gpgInstance.checkIfIsAuthenticated()){
				GPLeaderBoard.gpgInstance.ShowScoreLeaderboard();
			}
			else{
				GPLeaderBoard.gpgInstance.GPGSignIn();
			}
		}
	}
}
