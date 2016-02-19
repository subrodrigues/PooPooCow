using UnityEngine;
using System.Collections;

public class PooBullet : MonoBehaviour {
	Vector3 target = Vector3.zero;
	StreamCell cell;
	Vector3 initialTargetPosition;
	public float speed;

	void Awake(){
	}
	// Use this for initialization
	void Start () {
		initialTargetPosition = new Vector3 (target.x, target.y, target.z);
		speed = 18.0f;
		LookAtTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, initialTargetPosition, step); 
		
		if (transform.position == initialTargetPosition){
				cell.enableCollider();
				Destroy (gameObject);
		}
	}

	public void setTarget(StreamCell c){
		this.target = c.transform.position;
		this.cell = c;
	}

	void LookAtTarget ()
	{
			Vector3 newRotation = Quaternion.LookRotation (target - transform.position).eulerAngles;
			transform.rotation = Quaternion.Euler (newRotation);
	}
}
