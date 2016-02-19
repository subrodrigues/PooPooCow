using UnityEngine;
using System.Collections;

public class StreamCell : MonoBehaviour {

	public Sprite poo;

	bool fallAnimation = false;
	public float startPlayFall;
	float playFallTime;
	public float life = 1.0f;
	public float effectiveLifeLossAtHalfLife = 1.0f; // only starts decreasing when Poo life reaches 50%
	float decreaseSec = 0.1f;

	// Use this for initialization
	void Start () {
		startPlayFall = -1.0f;
		playFallTime = 0.35f;
		fallAnimation = false;
		life = 1.0f;
		decreaseSec = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {

		if(life <= 0.0f || ControlScript.GAME_OVER){
			destroyThis();
		}

		if (fallAnimation && startPlayFall == -1.0f){
			// Start death animation
			startPlayFall = Time.time + playFallTime;

			Vector3 fallPos = new Vector3 (this.transform.position.x + 0.2f, this.transform.position.y - 0.2f, this.transform.position.z -0.8f);
			this.transform.position = fallPos;
		}
		else if (fallAnimation) {
			if(Time.time > startPlayFall){
				fallAnimation = false;
				this.GetComponent<Rigidbody2D> ().gravityScale = 0.5f;
				this.GetComponentInChildren<CircleCollider2D>().enabled = false;
				this.GetComponentInChildren<BoxCollider2D>().enabled = true;
			}
		}
		float decreasedLife = decreaseSec*Time.deltaTime;
		life -= decreasedLife;

	/*	if(life >= 0.0f && gameObject.GetComponent<SpriteRenderer>().sprite == poo)
			gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, life + 0.4f);
			*/
		if (life >= 0.0f && life <= 0.5f && gameObject.GetComponent<SpriteRenderer> ().sprite == poo) {
			effectiveLifeLossAtHalfLife -= decreasedLife;
			float newScale = (effectiveLifeLossAtHalfLife * 0.2f);
			transform.localScale = new Vector3 (newScale, newScale, newScale);
		}
	}

	public void destroyThis(){
		fallAnimation = true;
		gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, life + 0.75f);

		if (life > 0.0f)
		life = 0.0f;
		// Destroy(gameObject);
	}

	public void enableCollider(){
		if(this != null){
			gameObject.GetComponent<CircleCollider2D>().enabled = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = poo;

			if(!gameObject.GetComponent<ParticleSystem>().isPlaying)
	        {
	            gameObject.GetComponent<ParticleSystem>().Play();
	        }	

	 //       ProgressBarPoo pb = this.GetComponentInParent<ProgressBarPoo>();
	//		pb.startBar();
    	}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "EndPoint") {
			Destroy (gameObject);
		} 
	}
}
