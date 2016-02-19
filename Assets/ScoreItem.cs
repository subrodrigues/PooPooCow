using UnityEngine;
using System.Collections;

public class ScoreItem : MonoBehaviour {
	public Transform target;
	Vector3 initialTargetPosition;
	public float speed;
	public Transform player;

	// Use this for initialization
	void Start () {
			target = GameObject.FindGameObjectWithTag ("EndPoint").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;

	//s	this.transform.rotation = new Quaternion(this.transform.rotation.x, 180.0f, this.transform.rotation.z, this.transform.rotation.w);

		initialTargetPosition = new Vector3 (target.position.x, target.position.y, target.position.z);
		speed = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!ControlScript.GAME_OVER) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, initialTargetPosition, step); 

			if (transform.position == initialTargetPosition)
					Destroy (gameObject);
		}
	}
		void LookAtTarget ()
	{
		Vector3 newRotation = Quaternion.LookRotation (target.position - transform.position).eulerAngles;
		transform.rotation = Quaternion.Euler (newRotation);
	}

void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player" && !ControlScript.GAME_OVER) {
				TouchScript.scoreUpdated = true;
				Destroy (gameObject);
			} else if (other.tag == "EndPoint") {
				Debug.Log ("DESTROY");
				Destroy (gameObject);
		}
		
	}
}
