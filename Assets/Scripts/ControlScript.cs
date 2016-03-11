using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour {
	public GameObject cow;

	public static bool GAME_OVER = false, START = false;
	public GUIText GAME_OVER_TEXT, GAME_OVER_TEXT_BACK;
	public GameObject scoreButton, playButton;

	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius;
	public LayerMask whatIsGround;
	public float jumpForce;
	bool jumpTime = false, plumpTime = false;
	float plumpTimeSeconds;
	float startJumpTime;
	float defaultPositionX, defaultPositionY;
	Vector3 maxWidth, minWidth;
	
	bool playDeathAnimation = false;
	float startPlayDeath;
	float playDeathTime;

	bool moveXAxisLeft = false, moveXAxisRight = false; // keyboard


	Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();

		groundRadius = 0.2f;
		jumpForce = 500f;
		plumpTimeSeconds = 0.2f; // 200ms de breaking
		startJumpTime = 0.0f;

		defaultPositionX = -2.997161f;
		defaultPositionY = -0.2007912f;

		maxWidth = new Vector3 (1280f, 720f, 0.0f);
		minWidth = new Vector3 (0f, 0f, 0.0f);

		playDeathTime = 0.7f;

		GAME_OVER = false;
		START = false;
	}

	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround); 
		anim.SetBool("Ground", grounded);

	}

	void KeyboardMovement ()
	{
		if (grounded && Input.GetKeyDown (KeyCode.Space) && !plumpTime && !jumpTime) {	
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 0f));

				moveXAxisRight = false;
				moveXAxisLeft = false;
	
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-200f, 50f));
				plumpTime = true;
				startJumpTime = Time.time + plumpTimeSeconds;
				anim.SetBool ("Break", true); // to start breaking
		} 
		else {
				if (grounded && !plumpTime && !jumpTime && Input.GetKeyDown (KeyCode.LeftArrow)) {
						// Keyboard Left
						moveXAxisLeft = true;
						moveXAxisRight = false;
				} else if (grounded && !plumpTime && !jumpTime && Input.GetKeyDown (KeyCode.RightArrow)) {
						// Keyboard Right
						moveXAxisLeft = false;
						moveXAxisRight = true;
				}

				if (grounded && !plumpTime && !jumpTime && Input.GetKeyUp (KeyCode.LeftArrow)) {
						// Keyboard Left
						moveXAxisLeft = false;
					//	moveXAxisRight = false;
				} else if (grounded && !plumpTime && !jumpTime && Input.GetKeyUp (KeyCode.RightArrow)) {
						// Keyboard Right
				//		moveXAxisLeft = false;
						moveXAxisRight = false;
				}


				if (moveXAxisLeft) {
						float step = 1 * Time.deltaTime * 5.0f;
						transform.position = Vector3.MoveTowards (transform.position, minWidth, step);
				} else if (moveXAxisRight) {
						float step = 1 * Time.deltaTime * 5.0f;
						transform.position = Vector3.MoveTowards (transform.position, maxWidth, step);
				}
		}


	}

	void AndroidMovement ()
	{
		// Move in x Axis
		float acceleration = Input.acceleration.x;
		if (grounded && !plumpTime && Mathf.Abs (acceleration) > 0.1) {
			// Android
			float step = Mathf.Abs (acceleration) * Time.deltaTime * 8.0f;
			if (acceleration >= 0.0f) {
				transform.position = Vector3.MoveTowards (transform.position, maxWidth, step);
			}
			else {
				transform.position = Vector3.MoveTowards (transform.position, minWidth, step);
			}
		}
		else if (grounded && Input.touchCount >= 1 && !plumpTime && !jumpTime) {	
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (-200f, 50f));
			plumpTime = true;
			startJumpTime = Time.time + plumpTimeSeconds;
			anim.SetBool ("Break", true); // to start breaking
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!ControlScript.GAME_OVER) {
			// AndroidMovement ();
			// KeyboardMovement (); 
			
			if(GetComponent<RectTransform>().localPosition.x < -15f){
					GetComponent<RectTransform>().localPosition = new Vector3(-15f, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().localPosition.z);
			}
			if(GetComponent<RectTransform>().localPosition.x > 15f){
					GetComponent<RectTransform>().localPosition = new Vector3(15f, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().localPosition.z);
			}
			if(GetComponent<RectTransform>().localPosition.y > 9.4f){
					GetComponent<RectTransform>().localPosition = new Vector3(GetComponent<RectTransform>().localPosition.x, 9.4f, GetComponent<RectTransform>().localPosition.z);
			}

			/*	if (plumpTime && Time.time > startJumpTime) {
						plumpTime = false;
						jumpTime = true;
						anim.SetBool ("Break", false); // to stop breaking
				}
				else if (jumpTime && !plumpTime) {
						jumpTime = false;
						anim.SetBool ("Ground", false);		
						rigidbody2D.AddForce (new Vector2 (150f, jumpForce));		
				}
			*/
		} 
		else if (!playDeathAnimation && cow.GetComponentInChildren<CircleCollider2D>().enabled) { // Se acabou de morrer
			playDeathAnimation = true;

			// Start death animation
			startPlayDeath = Time.time + playDeathTime;
			Vector3 gameoverPosition = new Vector3 (this.transform.position.x, this.transform.position.y + 0.25f, this.transform.position.z - 0.4f);
			cow.transform.position = gameoverPosition;
			
			anim.SetBool ("Death", true);	
		}
		else if (playDeathAnimation) {
			if(Time.time > startPlayDeath){
				playDeathAnimation = false;
				cow.GetComponentInChildren<CircleCollider2D>().enabled = false;

				GAME_OVER_TEXT.gameObject.SetActive(true);
				GAME_OVER_TEXT_BACK.gameObject.SetActive(true);
				scoreButton.gameObject.SetActive(true);
				playButton.gameObject.SetActive(true);

				if (GPLeaderBoard.gpgInstance.checkIfIsAuthenticated ()) {
					if (PlayerPrefs.HasKey ("HiScore")) {
						float hiScore = PlayerPrefs.GetFloat ("HiScore");
						if (TouchScript.score >= hiScore) { 
							GPLeaderBoard.gpgInstance.ScoreToLeaderboard (TouchScript.score, true);
						} else {
							GPLeaderBoard.gpgInstance.ScoreToLeaderboard (TouchScript.score, false);
						}
					} else {
						GPLeaderBoard.gpgInstance.ScoreToLeaderboard (TouchScript.score, true);
					}
					PlayerPrefs.SetInt ("GPSignOut", 0);
				} else {
					PlayerPrefs.SetFloat ("NotSavedHiScore", TouchScript.score);
				}
			}
		}
	}


}
