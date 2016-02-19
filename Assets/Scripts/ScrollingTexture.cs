using UnityEngine;
using System.Collections;

public class ScrollingTexture : MonoBehaviour {

	public float scrollSpeedY = 0.8F, scrollSpeedX = 0.2F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.deltaTime * scrollSpeedX + GetComponent<Renderer>().material.mainTextureOffset.x, Time.deltaTime * scrollSpeedY + GetComponent<Renderer>().material.mainTextureOffset.y);
		
		if(GetComponent<Renderer>().material.mainTextureOffset.x >= 1.0f)
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, GetComponent<Renderer>().material.mainTextureOffset.y);

		if(GetComponent<Renderer>().material.mainTextureOffset.y >= 1.0f)
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(GetComponent<Renderer>().material.mainTextureOffset.x, 0);
	}
}
