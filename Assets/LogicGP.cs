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
