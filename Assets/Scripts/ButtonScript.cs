using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	public bool isExit, isPlayButton, isScoreButton, isAchievementsButton;
	public Sprite button, buttonPressed;
	private PauseButtonScript pauseButton;

	void Start(){
		pauseButton = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<PauseButtonScript>();
	}

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
			if (pauseButton != null && pauseButton.gamePaused) {
				pauseButton.UnpauseGame ();
			}

			Application.LoadLevel ("level");
		} else if (isScoreButton) {
			if (pauseButton != null && pauseButton.gamePaused) {
				pauseButton.UnpauseGame ();
			}

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
			if (pauseButton != null && pauseButton.gamePaused) {
				pauseButton.UnpauseGame ();
			}

			if(GPLeaderBoard.gpgInstance.checkIfIsAuthenticated()){
				GPLeaderBoard.gpgInstance.CheckIfServerIsSyncedAndShowAchievements();
			}
			else{
				GPLeaderBoard.gpgInstance.GPGSignIn();
			}
		}
	}
}
