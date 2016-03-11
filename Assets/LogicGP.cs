using UnityEngine;
using System.Collections;

public class LogicGP : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if(PlayerPrefs.HasKey("GPSignOut")){
		   // there is a saved option, auto-login or not
         	if(PlayerPrefs.GetInt("GPSignOut") == 0){ // Logged in
				GPLeaderBoard.gpgInstance.GPGSignIn();
         	}
		}else{
			GPLeaderBoard.gpgInstance.GPGSignIn(); // No key, auto-login
		}
			
		// If hi score submit failed last time, try to update now
		if (GPLeaderBoard.gpgInstance.checkIfIsAuthenticated ()) { 
			long notSavedScore = (long) PlayerPrefs.GetFloat ("NotSavedHiScore");
			if(notSavedScore != -1f)
				GPLeaderBoard.gpgInstance.ScoreToLeaderboard (notSavedScore, false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// If logged in app and logged out in leaderboard
		if(GPLeaderBoard.gpgInstance.signInTried && !GPLeaderBoard.gpgInstance.checkIfIsAuthenticated()){ // Logout
			GPLeaderBoard.gpgInstance.setLoggedOut();
		} 

	//	if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit ();}
	}
}
