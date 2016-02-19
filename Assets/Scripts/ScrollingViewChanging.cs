using UnityEngine;
using System.Collections;

public class ScrollingViewChanging : MonoBehaviour {

	public float speed = 0.0f;
	int currentBack = 0;
	int currOffSet = 0;
	public Texture original, smb, sw;
	
	// Update is called once per frame
	void Update () {
		if(!ControlScript.GAME_OVER)
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.deltaTime * speed + GetComponent<Renderer>().material.mainTextureOffset.x, 0f);

		if(currOffSet < (int)GetComponent<Renderer>().material.mainTextureOffset.x){ // Change back
			currOffSet = 0;
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, GetComponent<Renderer>().material.mainTextureOffset.y);

			Debug.Log("object message");
			if(currentBack > 0){
				currentBack = 0;
				GetComponent<Renderer>().material.mainTexture = original;
			}

			else if(Random.value >= 0.6){
				currentBack = 1;
				GetComponent<Renderer>().material.mainTexture = smb;
			}

			else if(Random.value >= 0.8){
				currentBack = 2;
				GetComponent<Renderer>().material.mainTexture = sw;
			}
		}
	}
}
