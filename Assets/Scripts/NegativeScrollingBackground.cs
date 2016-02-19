using UnityEngine;
using System.Collections;

public class NegativeScrollingBackground : MonoBehaviour {

	public float speed = 0.0f;
	
	// Update is called once per frame
	void Update () {
		if(!ControlScript.GAME_OVER)
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.deltaTime * speed + GetComponent<Renderer>().material.mainTextureOffset.x, 0f);

		if(GetComponent<Renderer>().material.mainTextureOffset.x <= -1.0f)
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, GetComponent<Renderer>().material.mainTextureOffset.y);
	}
}
