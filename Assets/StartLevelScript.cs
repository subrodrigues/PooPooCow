using UnityEngine;
using System.Collections;

public class StartLevelScript : MonoBehaviour {
	AsyncOperation nextLevel = null;
	bool isLoadLevelInitiated = false;

	// Use this for initialization
	void Start () {
	}

	IEnumerator LaunchLevelAfterLoading(){
		isLoadLevelInitiated = true;
		yield return new WaitForSeconds (2);
	//	Application.LoadLevel ("level");

		nextLevel = Application.LoadLevelAsync("level");
		nextLevel.allowSceneActivation = false;
		while (nextLevel.progress < 0.9f)
		{ 
			yield return new WaitForEndOfFrame(); 
		} 
		nextLevel.allowSceneActivation = true;
	}
		
	// Update is called once per frame
	void Update () {
		if(!isLoadLevelInitiated)
			StartCoroutine (LaunchLevelAfterLoading ());
	}
}
