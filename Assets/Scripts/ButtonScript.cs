using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	public bool isExit, isPlayButton, isScoreButton, isAchievementsButton;
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

				// If hi score submit failed last time, try to update now
				long notSavedScore = (long) PlayerPrefs.GetFloat ("NotSavedHiScore");
				if (notSavedScore != -1f){
					GPLeaderBoard.gpgInstance.ScoreToLeaderboard (notSavedScore, true); // Update and show
				}
				else {
					GPLeaderBoard.gpgInstance.ShowScoreLeaderboard();
				}
			}
			else{
				GPLeaderBoard.gpgInstance.GPGSignIn();
			}
		}
		else if (isAchievementsButton) {
			if(GPLeaderBoard.gpgInstance.checkIfIsAuthenticated()){
				GPLeaderBoard.gpgInstance.CheckIfServerIsSyncedAndShowAchievements();
			}
			else{
				GPLeaderBoard.gpgInstance.GPGSignIn();
			}
		}
	}
}
