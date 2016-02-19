using UnityEngine;
using System.Collections;

public class StreamCell : MonoBehaviour {

	public Sprite poo;

	bool fallAnimation = false;
	public float startPlayFall;
	float playFallTime;
	public float life = 1.0f;
	float decreaseSec = 0.1f;
		Vector3 hitPosition;

	public void setTouchPosition(Vector3 pos){
				this.hitPosition = pos;
	}

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

		life -= decreaseSec*Time.deltaTime;

	}

	public void destroyThis(){
		fallAnimation = true;

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

	        ProgressBarPoo pb = this.GetComponentInParent<ProgressBarPoo>();
						pb.startBar(hitPosition);
    	}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "EndPoint") {
			Destroy (gameObject);
		} 
	}
}
