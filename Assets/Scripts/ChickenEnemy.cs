using UnityEngine;
using System.Collections;

public class ChickenEnemy : MonoBehaviour {
	public Transform target;
	Vector3 initialTargetPosition;
	public float speed;
	public float damage = 1.0f;
	public Transform player;

	bool playDeathAnimation = false;
	public float startPlayDeath;
	float playDeathTime;

	Animator anim;

	// Use this for initialization
	void Start ()
	{
		anim = this.GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("ChickenEndPoint").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		initialTargetPosition = new Vector3 (target.position.x, target.position.y, target.position.z);
		speed = 1.0f + (Random.value * 1.2f);
	//	LookAtTarget ();
		damage = 1.0f;

		startPlayDeath = -1.0f;
		playDeathTime = 0.4f;
		playDeathAnimation = false;
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		if (!playDeathAnimation && !ControlScript.GAME_OVER && startPlayDeath == -1.0f) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, initialTargetPosition, step); 
			
			if (transform.position == initialTargetPosition)
				Destroy (gameObject);
		}
		else if (playDeathAnimation && startPlayDeath == -1.0f){    	
			// Start death animation
			startPlayDeath = Time.time + playDeathTime;

			if(!this.GetComponent<ParticleSystem>().isPlaying)
	        {
	            this.GetComponent<ParticleSystem>().Play();
	    	}

			Vector3 gameoverPosition = new Vector3 (this.transform.position.x - 0.1f, this.transform.position.y + 0.1f, this.transform.position.z -0.3f);
			this.transform.position = gameoverPosition;
			
			anim.SetBool ("Death", true);	
		}
		else if (playDeathAnimation) {
			if(Time.time > startPlayDeath){
				playDeathAnimation = false;
				this.GetComponent<Rigidbody2D> ().gravityScale = 1.0f;
				this.GetComponentInChildren<CircleCollider2D>().enabled = false;
			}
		}
	}
	
	void LookAtTarget ()
	{
		Vector3 newRotation = Quaternion.LookRotation (target.position - transform.position).eulerAngles;
		transform.rotation = Quaternion.Euler (newRotation);
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			ControlScript.GAME_OVER = true;
			//	Destroy (gameObject);
		} else if (other.tag == "EndPoint") {
			Debug.Log ("DESTROY");
			Destroy (gameObject);
		} else if (other.tag == "Poo" || other.tag == "PooBullet") {
			playDeathAnimation = true;
		}
		
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player") {
		}
	}
}
