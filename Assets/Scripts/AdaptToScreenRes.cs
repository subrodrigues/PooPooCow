using UnityEngine;
using System.Collections;

public class AdaptToScreenRes : MonoBehaviour {
	public Vector2 scaleOnRatio = new Vector2(0.1f, 0.1f);
	private Transform myTrans;
	private float widthHeightRatio;

	void Start(){
		myTrans = transform;
		SetScale ();
	}

	void SetScale(){
		widthHeightRatio = (float)Screen.width / Screen.height;

		myTrans.localScale = new Vector3 (scaleOnRatio.x, widthHeightRatio * scaleOnRatio.y, 1);
	}
}
