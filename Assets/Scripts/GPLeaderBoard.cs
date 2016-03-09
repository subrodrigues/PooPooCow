using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GPLeaderBoard : MonoBehaviour {

 static public GPLeaderBoard gpgInstance;
 public bool signInTried;  // Call this using if(GPG.gpgInstance.singInTried) ... to test whether or not user is signed in to GPG
 
 void Awake(){
     DontDestroyOnLoad (this.gameObject);
 
     if (gpgInstance != null){
         Debug.LogError("Multiple instances of GPG!");
     }
     gpgInstance = this;
 }
 
 void Start ()
 {
     PlayGamesPlatform.Activate ();    
 }
 
 public void GPGSignIn()
 {
     Social.localUser.Authenticate ((bool success) => {
         if(success){
         	PlayerPrefs.SetInt("GPSignOut", 0);
             signInTried = true;
         }else{
             signInTried = false;
             PlayerPrefs.SetInt("GPSignOut", 1);
         }
     });
 }
 
 public void ScoreToLeaderboard(long lscore, bool showLeaderBoard)
 {
		Social.ReportScore (lscore, "CgkI-47-7rAZEAIQAQ", (bool success)=>{
         if(success){
            if(showLeaderBoard){
                PlayerPrefs.SetFloat("HiScore", lscore);
                ShowScoreLeaderboard();
            }
         }
     });
 }
 
 public void ShowScoreLeaderboard()
 {
		PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI-47-7rAZEAIQAQ");
 }
 
 public void GPGSignOut()
 {
     if (signInTried) {
         ((PlayGamesPlatform)Social.Active).SignOut ();
         signInTried = false;

     }
 }

 public bool checkIfIsAuthenticated(){
 	return PlayGamesPlatform.Instance.IsAuthenticated();
 }

 public void setLoggedOut(){
 	signInTried = false;
 	PlayerPrefs.SetInt("GPSignOut", 1);

 }

 public void showAchievements () {
	Social.ShowAchievementsUI ();
 }

}
