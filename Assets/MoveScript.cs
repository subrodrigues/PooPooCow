using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {
	// COMMENTED CODE IS USED TO MAKE THE ORDERED JUMP

//	static int NUM_JUMPING_POOS = 3;
//	public static int jumpingDude = 0;

//	public float jumpSpeed;
	private bool isGrounded = true;
//	public bool randomJumps = true;
//	public int jumpIndex = 0;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isGrounded /*&& randomJumps*/) {
			rb.AddForce(Vector3.up * Random.Range(100, 150));
			isGrounded = false;
		}
	/*	else if (isGrounded && jumpingDude == jumpIndex) {
			rb.AddForce(Vector3.up * jumpSpeed);
			isGrounded = false;
		}
		*/
	}

	void OnCollisionEnter(Collision theCollision){
		if (theCollision.gameObject.tag == "LoadingFloor") {
		/*	jumpingDude++;

			if (jumpingDude > NUM_JUMPING_POOS - 1)
				jumpingDude = 0;
		*/	
			isGrounded = true;
		}
	}
}
