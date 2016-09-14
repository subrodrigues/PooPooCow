using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GPLeaderBoard : MonoBehaviour
{
	static int RANKED_MANURE = 10, JUNIOR_POO = 25, ADDICTED_TO_FECES = 50, THE_POOMINATOR = 100, HOLY_CRAP = 200;
//	static int RANKED_MANURE = 1, JUNIOR_POO = 2, ADDICTED_TO_FECES = 3, THE_POOMINATOR = 4, HOLY_CRAP = 5;
	const string RANKED_MANURE_ID = "CgkI-47-7rAZEAIQAw";
	const string JUNIOR_POO_ID = "CgkI-47-7rAZEAIQAg";
	const string ADDICTED_TO_FECES_ID = "CgkI-47-7rAZEAIQBA";
	const string THE_POOMINATOR_ID = "CgkI-47-7rAZEAIQBQ";
	const string HOLY_CRAP_ID = "CgkI-47-7rAZEAIQBg";


	static public GPLeaderBoard gpgInstance;
	public bool signInTried;
	// Call this using if(GPG.gpgInstance.singInTried) ... to test whether or not user is signed in to GPG
	 
	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);
	 
		if (gpgInstance != null) {
			Debug.LogError ("Multiple instances of GPG!");
		}
		gpgInstance = this;
	}

	void Start ()
	{
		PlayGamesPlatform.Activate ();    
	}

	public void GPGSignIn ()
	{
		Social.localUser.Authenticate ((bool success) => {
			if (success) {
				PlayerPrefs.SetInt ("GPSignOut", 0);
				signInTried = true;
			} else {
				signInTried = false;
				PlayerPrefs.SetInt ("GPSignOut", 1);
			}
		});
	}

	public void ScoreToLeaderboard (long lscore, bool showLeaderBoard)
	{
		Social.ReportScore (lscore, "CgkI-47-7rAZEAIQBw", (bool success) => {
			if (success) {
				if (showLeaderBoard) {
					ShowScoreLeaderboard ();
					PlayerPrefs.SetFloat ("HiScore", lscore);
					PlayerPrefs.SetFloat ("NotSavedHiScore", -1f);
				}

				UnlockAchievements (lscore);
			}
			else{
				PlayerPrefs.SetFloat ("NotSavedHiScore", lscore);
			}
		});
	}

	public void UnlockAchievements (long lscore)
	{
		if (lscore >= RANKED_MANURE) {
			Social.ReportProgress(RANKED_MANURE_ID, 100.0f, (bool success) => {
				// handle success or failure
				if(success){
					PlayerPrefs.SetInt (RANKED_MANURE_ID, 0);
				}
				else{
					PlayerPrefs.SetInt (RANKED_MANURE_ID, 1); // Need to be updated later
				}
			});
		}
		if (lscore >= JUNIOR_POO) {
			Social.ReportProgress(JUNIOR_POO_ID, 100.0f, (bool success) => {
				// handle success or failure
				if(success){
					PlayerPrefs.SetInt (JUNIOR_POO_ID, 0);
				}
				else{
					PlayerPrefs.SetInt (JUNIOR_POO_ID, 1); // Need to be updated later
				}
			});
		}
		if (lscore >= ADDICTED_TO_FECES) {
			Social.ReportProgress(ADDICTED_TO_FECES_ID, 100.0f, (bool success) => {
				// handle success or failure
				if(success){
					PlayerPrefs.SetInt (ADDICTED_TO_FECES_ID, 0);
				}
				else{
					PlayerPrefs.SetInt (ADDICTED_TO_FECES_ID, 1); // Need to be updated later
				}
			});
		}
		if (lscore >= THE_POOMINATOR) {
			Social.ReportProgress(THE_POOMINATOR_ID, 100.0f, (bool success) => {
				// handle success or failure
				if(success){
					PlayerPrefs.SetInt (THE_POOMINATOR_ID, 0);
				}
				else{
					PlayerPrefs.SetInt (THE_POOMINATOR_ID, 1); // Need to be updated later
				}
			});
		}
		if (lscore >= HOLY_CRAP) {
			Social.ReportProgress(HOLY_CRAP_ID, 100.0f, (bool success) => {
				// handle success or failure
				if(success){
					PlayerPrefs.SetInt (HOLY_CRAP_ID, 0);
				}
				else{
					PlayerPrefs.SetInt (HOLY_CRAP_ID, 1); // Need to be updated later
				}
			});
		}
	}

	public void ShowScoreLeaderboard ()
	{
		PlayGamesPlatform.Instance.ShowLeaderboardUI ("CgkI-47-7rAZEAIQBw");
	}

	public void GPGSignOut ()
	{
		if (signInTried) {
			((PlayGamesPlatform)Social.Active).SignOut ();
			signInTried = false;

		}
	}

	public bool checkIfIsAuthenticated ()
	{
		return PlayGamesPlatform.Instance.IsAuthenticated ();
	}

	public void setLoggedOut ()
	{
		signInTried = false;
		PlayerPrefs.SetInt ("GPSignOut", 1);

	}

	public void CheckIfServerIsSyncedAndShowAchievements ()
	{
		CheckIfServerIsSynced ();

		Social.ShowAchievementsUI ();
	}

	static void CheckIfServerIsSynced ()
	{
		// RANKED MANURE
		if (PlayerPrefs.GetInt (RANKED_MANURE_ID) == 1) {
			// Server not synced
			Social.ReportProgress (RANKED_MANURE_ID, 100.0f, (bool success) =>  {
				// handle success or failure
				if (success) {
					PlayerPrefs.SetInt (RANKED_MANURE_ID, 0);
				}
			});
		}
		// JUNIOR POO
		if (PlayerPrefs.GetInt (JUNIOR_POO_ID) == 1) {
			// Server not synced
			Social.ReportProgress (JUNIOR_POO_ID, 100.0f, (bool success) =>  {
				// handle success or failure
				if (success) {
					PlayerPrefs.SetInt (JUNIOR_POO_ID, 0);
				}
			});
		}
		// ADDICTED TO FECES
		if (PlayerPrefs.GetInt (ADDICTED_TO_FECES_ID) == 1) {
			// Server not synced
			Social.ReportProgress (ADDICTED_TO_FECES_ID, 100.0f, (bool success) =>  {
				// handle success or failure
				if (success) {
					PlayerPrefs.SetInt (ADDICTED_TO_FECES_ID, 0);
				}
			});
		}
		// THE POOMINATOR
		if (PlayerPrefs.GetInt (THE_POOMINATOR_ID) == 1) {
			// Server not synced
			Social.ReportProgress (THE_POOMINATOR_ID, 100.0f, (bool success) =>  {
				// handle success or failure
				if (success) {
					PlayerPrefs.SetInt (THE_POOMINATOR_ID, 0);
				}
			});
		}
		// HOLY CRAP
		if (PlayerPrefs.GetInt (HOLY_CRAP_ID) == 1) {
			// Server not synced
			Social.ReportProgress (HOLY_CRAP_ID, 100.0f, (bool success) =>  {
				// handle success or failure
				if (success) {
					PlayerPrefs.SetInt (HOLY_CRAP_ID, 0);
				}
			});
		}
	}
}
