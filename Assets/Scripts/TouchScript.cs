using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {
	public GUIText scoreGUI, scoreGUIBack;
	public static int score = 0;
	public static bool scoreUpdated = false;

	public GameObject touchCollider;
	public StreamCell touch;
	public PooShooter pooShooter;
		public Camera mainCamera;

	static int MAX_POO = 3; // [0,MAX]

	bool firstRound = true;

	AudioClip fart1Clip, fart2Clip, fart3Clip, giggleClip, coinCollect, backgroundThemeClip, gameOverClip;
	AudioSource fart1Source, fart2Source, fart3Source, giggleSource, coinCollectSource, backgroundThemeSource, gameOverSource;
	int nextFartIndex;
	int giggleCount;

	float pooRate = 0; // decreases each second by 3. If reaches MAXRATE in 3 seconds, stops for COOLINGTIME (seconds)
	float MAXRATE = 9;
	float currentRateTime = 0.0f, COOLINGTIME = 2.0f, currentCooling = 0.0f;
	bool inflamedIntestines = false;

	// GUI
	public float barDisplay; //current progress
	private float x, y;
	private Vector2 resolution;
	public Vector2 posGauge = new Vector2(-10, -10);
    Vector2 sizeGauge = new Vector2(500, 600);
         public Texture2D emptyGaugeTex, cowGaugeTex;
     public Texture2D fullGaugeTex, fullGaugeBlockedTex;

	
	void Awake ()
	{
			fart1Clip = Resources.Load ("fart1") as AudioClip;
			fart2Clip = Resources.Load ("fart2") as AudioClip;
			fart3Clip = Resources.Load ("fart3") as AudioClip;
			giggleClip = Resources.Load ("giggle") as AudioClip;
			coinCollect = Resources.Load ("coin1") as AudioClip;
			backgroundThemeClip = Resources.Load ("poo_poo_background") as AudioClip;
			gameOverClip = Resources.Load ("game_over") as AudioClip;

			fart1Source = AddAudio (fart1Clip, false, false, 0.3f);
			fart2Source = AddAudio (fart2Clip, false, false, 0.3f);
			fart3Source = AddAudio (fart3Clip, false, false, 0.3f);
			giggleSource = AddAudio (giggleClip, false, false, 0.4f);
			coinCollectSource = AddAudio (coinCollect, false, false, 0.8f);
			backgroundThemeSource = AddAudio(backgroundThemeClip, true, true, 0.05f);
			gameOverSource = AddAudio(gameOverClip, false, true, 0.1f);

			backgroundThemeSource.Play();

			nextFartIndex = getNextFartIndex();
	}

	public struct Cell
	{
	    public int state;
    	public StreamCell touch;
	}
	Cell[] cells = new Cell[MAX_POO + 1]; 

	// Use this for initialization
	void Start () {
		for (int i = 0; i < cells.Length; i++){
			if(i == 0){	// first round
				cells[i].state = 1;
			}else
				cells[i].state = 0;
		}	
		score = 0;

		resolution = new Vector2(Screen.width, Screen.height);
        x=Screen.width/1280.0f; 
        y=Screen.height/800.0f; 
        posGauge.x = 10;
        posGauge.y = resolution.y - (resolution.y/3.0f);
	}

	int incCells(){
		for (int i = 0; i < cells.Length; i++){
			if(cells[i].state == 1){
				cells[i].state = 0;

				if(i < MAX_POO){
					cells[i+1].state = 1;
				}
				else {
					cells[0].state = 1;
				}
				return i;
			}
		}

		return -1;
	}
	
	// Update is called once per frame
	void Update () {
		if(ControlScript.GAME_OVER && scoreGUI.enabled){
			backgroundThemeSource.Stop();
			scoreGUI.enabled = false;
			scoreGUIBack.enabled = false;
			gameOverSource.Play();
		} else if(!ControlScript.GAME_OVER && !scoreGUI.enabled){
			scoreGUI.enabled = true;
			scoreGUIBack.enabled = true;
			backgroundThemeSource.Play();

			if(gameOverSource.isPlaying){
				gameOverSource.Pause();
			}
			backgroundThemeSource.Play();
		}

		if(scoreUpdated){
			updateScore();
		}
		if(Screen.width!=resolution.x || Screen.height!=resolution.y){
		resolution=new Vector2(Screen.width, Screen.height);
		x=resolution.x/1280.0f;
		y=resolution.y/800.0f;

		        posGauge.y = resolution.y - (resolution.y/4.0f);
		}
						
				/*

				if (Application.platform == RuntimePlatform.WindowsEditor) {
						RaycastHit hit;
						Ray ray;

						ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
						if (Input.GetMouseButtonDown(0) && !ControlScript.GAME_OVER && Time.timeScale == 1.0f)
						{
								if (Physics.Raycast(ray, out hit)) 
								{
										if (touchCollider.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity))
										{
												// Here transform.collider is the collider of that gameobject on which you attach this script
												// Your Rest of the Logic Here

												if(ControlScript.START == false)
														ControlScript.START = true;

												//	if (Input.touchCount == 1) { 
												// Se toca no collider
														// With 3d Camera
														Vector3 target = new Vector3 (hit.point.x, hit.point.y, hit.point.z);

														int index = incCells();
														if(index != -1 && !inflamedIntestines){
																if(!firstRound)
																		cells[index].touch.destroyThis();

																if(firstRound && index == MAX_POO){
																		firstRound = false;
																}

																playFart();
																pooRate++;

																cells[index].touch = (StreamCell)Instantiate (touch, target, transform.rotation);
																pooShooter.shoot(cells[index].touch);

																// updateScore();

																giggleCount++;
																if(giggleCount == 10){
																		giggleCount = 0;
																		giggleSource.Play();
																}

												}
										}
								}
						}
				}

				// This Will work on Android Device ////////////////////////////////////////////////////////////////////////
				else if(Application.platform == RuntimePlatform.Android){   
						RaycastHit hit;
						Ray ray;
						if (Input.touchCount > 0 && !ControlScript.GAME_OVER && Time.timeScale == 1.0f)
						{
								for (int i = 0; i < Input.touchCount; i++)
								{
										ray =  Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
										if (Input.GetTouch(i).phase == TouchPhase.Began)
										{
												if (Physics.Raycast(ray, out hit))
												{
														if (touchCollider.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity))
														{
																if(ControlScript.START == false)
																		ControlScript.START = true;

											
																// Se toca no collider
																		// With 3d Camera
																		Vector3 target = new Vector3 (hit.point.x, hit.point.y, hit.point.z);

																		int index = incCells();
																		if(index != -1 && !inflamedIntestines){
																				if(!firstRound)
																						cells[index].touch.destroyThis();

																				if(firstRound && index == MAX_POO){
																						firstRound = false;
																				}

																				playFart();
																				pooRate++;

																				cells[index].touch = (StreamCell)Instantiate (touch, target, transform.rotation);
																				pooShooter.shoot(cells[index].touch);

																				// updateScore();

																				giggleCount++;
																				if(giggleCount == 10){
																						giggleCount = 0;
																						giggleSource.Play();
																				}
																		}

														}
												}
										}

										if (Input.GetTouch(i).phase == TouchPhase.Moved)
										{
												// Logic for finger move on screen
										}

										if (Input.GetTouch(i).phase == TouchPhase.Ended)
										{
											/*	if (Input.GetTouch(i).fingerId == fingerId)
												{
														fingerId = -1;
														// Logic when touch ends 
												}
												*/
								/*		}
								}
						}
				}
				*/
		if(Input.GetMouseButtonDown (0) && !ControlScript.GAME_OVER && Time.timeScale == 1.0f){

			if(ControlScript.START == false)
				ControlScript.START = true;
		//	if (Input.touchCount == 1) { 

				RaycastHit hit;
				Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);

			//	Debug.Log ("TOUCHED " + ray.direction.x + " " + ray.direction.y + " " + ray.direction.z );

				// Se toca no collider
				if (touchCollider.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)){
				//	Debug.Log ("TOUCHED INSIDE" + hit.point.x + " " + hit.point.y + " " + hit.point.z);

				//	Debug.Log ("HIT POINT " + hit.point.x);
					// With 3d Camera
					Vector3 target = new Vector3 (hit.point.x, hit.point.y, hit.point.z);

					int index = incCells();
					if(index != -1 && !inflamedIntestines){
						if(!firstRound)
							cells[index].touch.destroyThis();

						if(firstRound && index == MAX_POO){
							firstRound = false;
						}

						playFart();
						pooRate++;

						cells[index].touch = (StreamCell)Instantiate (touch, target, transform.rotation);
						cells [index].touch.setTouchPosition (hit.point);
						pooShooter.shoot(cells[index].touch);

						// updateScore();

						giggleCount++;
						if(giggleCount == 10){
							giggleCount = 0;
							giggleSource.Play();
						}
					}
				}
		//	}
		}

		updatePooPooRate();
	}

	void updateScore(){
		coinCollectSource.Play();
		score++;
		scoreGUI.text = score+"";
		scoreGUIBack.text = score+"";
		scoreUpdated = false;
	}

	void updatePooPooRate(){
		if(!inflamedIntestines && currentRateTime >= 1.0f){ // removes 3 poo each second
			currentRateTime = 0.0f;

			pooRate -= 3; // incremented in update()
			if(pooRate < 0)
				pooRate = 0;
		}

		if(currentCooling >= COOLINGTIME){ // reset cooling time
			currentCooling = 0.0f;
			inflamedIntestines = false;
			pooRate = 0;
		}

		if(inflamedIntestines){
			currentCooling += Time.deltaTime;
		}
		if(pooRate >= MAXRATE){
			inflamedIntestines = true;
		}

		currentRateTime += Time.deltaTime;
		barDisplay = pooRate/MAXRATE;
	}

	void OnGUI() {
		if(!ControlScript.GAME_OVER){
	         //draw the background:
	         GUI.BeginGroup(new Rect(posGauge.x, posGauge.y, sizeGauge.x*x, sizeGauge.y*y));
	             GUI.Label(new Rect(0,0, sizeGauge.y*y, sizeGauge.x*x), emptyGaugeTex);
	         
	             //draw the filled-in part:
	             GUI.BeginGroup(new Rect(0,0, sizeGauge.y*y, sizeGauge.x * barDisplay * x));
	             if(!inflamedIntestines){
	                GUI.Label(new Rect(0,0, sizeGauge.y*y, sizeGauge.x*x), fullGaugeTex);
	             }
	             
	             GUI.EndGroup();
	             GUI.Label(new Rect(0,0, sizeGauge.y*y, sizeGauge.x*x), cowGaugeTex);

	             if(inflamedIntestines){
	        
	               	GUI.Label(new Rect(0,0, sizeGauge.y*y, sizeGauge.x*x), fullGaugeBlockedTex);
	             
	             }
	         GUI.EndGroup();
     	}
    }

	AudioSource AddAudio (AudioClip clip, bool loop, bool playAwake, float vol)
	{
			AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
			newAudio.clip = clip;
			newAudio.loop = loop;
			newAudio.playOnAwake = playAwake;
			newAudio.volume = vol;
			return newAudio;
	}

	void playFart(){
		switch(nextFartIndex){
			case 0:
				fart1Source.Play();
				nextFartIndex = getNextFartIndex();
			break;
			case 1:
				fart2Source.Play();
				nextFartIndex = getNextFartIndex();
			break;
			case 2:
				fart3Source.Play();
				nextFartIndex = getNextFartIndex();
			break;
		}
	}

	int getNextFartIndex(){
		return Random.Range(0, 3);
	}

}
