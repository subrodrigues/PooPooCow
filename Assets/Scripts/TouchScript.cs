using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour
{
	public GUIText scoreGUI, scoreGUIBack;
	public static int score = 0;
	public static bool scoreUpdated = false;

	public GameObject touchCollider;
	public StreamCell touch;
	public PooShooter pooShooter;
	public Camera mainCamera;

	static int MAX_POO = 4;
	// [0,MAX]

	bool firstRound = true;

	AudioClip fart1Clip, fart2Clip, fart3Clip, giggleClip, coinCollect, backgroundThemeClip, gameOverClip;
	AudioSource fart1Source, fart2Source, fart3Source, giggleSource, coinCollectSource, backgroundThemeSource, gameOverSource;
	int nextFartIndex;
	int giggleCount;

	float pooRate = 0;
	// decreases each second by 3. If reaches MAXRATE in 3 seconds, stops for COOLINGTIME (seconds)
	float MAXRATE = 9;
	float currentRateTime = 0.0f, COOLINGTIME = 2.0f, currentCooling = 0.0f;
	bool inflamedIntestines = false;

	// GUI
	public float barDisplay;
	//current progress
	private float x, y;
	private Vector2 resolution;
	public Vector2 posGauge = new Vector2 (-10, -10);
	Vector2 sizeGauge = new Vector2 (500, 600);
	public Texture2D emptyGaugeTex, cowGaugeTex;
	public Texture2D fullGaugeTex, fullGaugeBlockedTex;

	public PooPooRateGauge pooPooRate;
	public Sprite fullGauge, normalGauge, gauge2, gauge3, gauge4, gauge5, gauge6, gauge7, gauge8, gauge9;

	private PauseButtonScript pauseButton;

	// Drawing laser
	private LineRenderer laserRenderer;
	private Color c1 = Color.yellow;
	private Color c2 = Color.red;
	public GameObject pooPooShooterGameObject;
	public Material laserRendererMaterial;
	public GameObject laserTouchEnd;

	void Awake ()
	{
		fart1Clip = Resources.Load ("fart1") as AudioClip;
		fart2Clip = Resources.Load ("fart2") as AudioClip;
		fart3Clip = Resources.Load ("fart3") as AudioClip;
		giggleClip = Resources.Load ("giggle") as AudioClip;
		coinCollect = Resources.Load ("coin1") as AudioClip;
		backgroundThemeClip = Resources.Load ("poo_poo_background") as AudioClip;
		gameOverClip = Resources.Load ("game_over") as AudioClip;

		fart1Source = AddAudio (fart1Clip, false, false, 0.7f);
		fart2Source = AddAudio (fart2Clip, false, false, 0.7f);
		fart3Source = AddAudio (fart3Clip, false, false, 0.7f);
		giggleSource = AddAudio (giggleClip, false, false, 0.8f);
		coinCollectSource = AddAudio (coinCollect, false, false, 0.8f);
		backgroundThemeSource = AddAudio (backgroundThemeClip, true, true, 0.3f);
		gameOverSource = AddAudio (gameOverClip, false, true, 0.5f);

		backgroundThemeSource.Play ();

		nextFartIndex = getNextFartIndex ();
	}

	public struct Cell
	{
		public int state;
		public StreamCell touch;
	}

	Cell[] cells = new Cell[MAX_POO + 1];

	// Use this for initialization
	void Start ()
	{
		pauseButton = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<PauseButtonScript>();

		for (int i = 0; i < cells.Length; i++) {
			if (i == 0) {	// first round
				cells [i].state = 1;
			} else
				cells [i].state = 0;
		}	
		score = 0;

		resolution = new Vector2 (Screen.width, Screen.height);
		x = Screen.width / 1280.0f; 
		y = Screen.height / 800.0f; 
		posGauge.x = 10;
		posGauge.y = resolution.y - (resolution.y / 3.0f);

		laserRenderer = pooPooShooterGameObject.AddComponent<LineRenderer>();
		laserRenderer.material = laserRendererMaterial;
	}

	int incCells ()
	{
		for (int i = 0; i < cells.Length; i++) {
			if (cells [i].state == 1) {
				cells [i].state = 0;

				if (i < MAX_POO) {
					cells [i + 1].state = 1;
				} else {
					cells [0].state = 1;
				}
				return i;
			}
		}

		return -1;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKey("escape") && pauseButton.gamePaused){
			pauseButton.UnpauseGame ();
		}
	
		if (ControlScript.GAME_OVER && scoreGUI.enabled) {
			backgroundThemeSource.Stop ();
			scoreGUI.enabled = false;
			scoreGUIBack.enabled = false;
			gameOverSource.Play ();
		} else if (!ControlScript.GAME_OVER && !scoreGUI.enabled) {
			scoreGUI.enabled = true;
			scoreGUIBack.enabled = true;
			backgroundThemeSource.Play ();

			if (gameOverSource.isPlaying) {
				gameOverSource.Pause ();
			}
			backgroundThemeSource.Play ();
		}

		if (scoreUpdated) {
			updateScore ();
		}

		if (Screen.width != resolution.x || Screen.height != resolution.y) {
			resolution = new Vector2 (Screen.width, Screen.height);
			x = resolution.x / 1280.0f;
			y = resolution.y / 800.0f;

		//	posGauge.y = resolution.y - (resolution.y / 4.0f);
		}
						
		if (!ControlScript.GAME_OVER && !pauseButton.isButtonFocused && !pauseButton.gamePaused && !backgroundThemeSource.isPlaying) {
			backgroundThemeSource.UnPause ();
		}

		if (Input.GetMouseButton (0) && !ControlScript.GAME_OVER && Time.timeScale == 1.0f) {

			RaycastHit hit;
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);

			// Se toca no collider
			if (!pauseButton.isButtonFocused && touchCollider.GetComponent<Collider> ().Raycast (ray, out hit, Mathf.Infinity)) {

				// With 3d Camera
				Vector3 target = new Vector3 (hit.point.x, hit.point.y, hit.point.z);

				if (!inflamedIntestines) {
					laserTouchEnd.transform.position = target;
					laserTouchEnd.SetActive (true);
				}

				laserRenderer.SetVertexCount (2);
			//	laserRenderer.SetColors (c1, c2); 
				laserRenderer.SetPosition (0, pooShooter.transform.position); 
				laserRenderer.SetPosition (1, target); 
				laserRenderer.SetWidth (0.01F, 0.01F); 
			}
		}
		else if (Input.GetMouseButtonUp (0) && !ControlScript.GAME_OVER && Time.timeScale == 1.0f) {
			DisableLaser ();

			if (ControlScript.START == false)
				ControlScript.START = true;
			//	if (Input.touchCount == 1) { 

			RaycastHit hit;
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);

			// Se toca no collider
			if (!pauseButton.isButtonFocused && touchCollider.GetComponent<Collider> ().Raycast (ray, out hit, Mathf.Infinity)) {

				// With 3d Camera
				Vector3 target = new Vector3 (hit.point.x, hit.point.y, hit.point.z);

				int index = incCells ();
				if (index != -1 && !inflamedIntestines) {
					if (!firstRound && cells [index].touch != null)
						cells [index].touch.destroyThis ();

					if (firstRound && index == MAX_POO) {
						firstRound = false;
					}

					playFart ();
					pooRate++;

					cells [index].touch = (StreamCell)Instantiate (touch, target, transform.rotation);
					pooShooter.shoot (cells [index].touch);

					// updateScore();

					giggleCount++;
					if (giggleCount == 10) {
						giggleCount = 0;
						giggleSource.Play ();
					}
				}
			} else if (pauseButton.isButtonFocused && backgroundThemeSource.isPlaying) {
				backgroundThemeSource.Pause ();
			}
		}

		updatePooPooRate ();
	}

	void updateScore ()
	{
		coinCollectSource.Play ();
		score++;
		scoreGUI.text = score + "";
		scoreGUIBack.text = score + "";
		scoreUpdated = false;
	}

	void updatePooPooRate ()
	{
		if (!inflamedIntestines && currentRateTime >= 1.0f) { // removes 3 poo each second
			currentRateTime = 0.0f;

			pooRate -= 3; // incremented in update() TODO: change to 3 again
			if (pooRate < 0)
				pooRate = 0;
		}

		if (currentCooling >= COOLINGTIME) { // reset cooling time
			currentCooling = 0.0f;
			inflamedIntestines = false;
			pooRate = 0;
	//		pooPooRate.GetComponent<SpriteRenderer>().sprite = normalGauge;
		}

		if (inflamedIntestines) {
			currentCooling += Time.deltaTime;
		}

		if (pooRate >= MAXRATE) {
			inflamedIntestines = true;			
		//	pooPooRate.GetComponent<SpriteRenderer>().sprite = fullGauge;
		}

		currentRateTime += Time.deltaTime;
		barDisplay = pooRate / MAXRATE;

		if (barDisplay == 0f) {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = normalGauge;
		}
		else if (barDisplay > 0f && barDisplay < 0.2f) {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = gauge2;
		}
		else if (barDisplay > 0.2f && barDisplay < 0.3f) {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = gauge3;
		}
		else if (barDisplay > 0.3f && barDisplay < 0.4f) {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = gauge4;
		}
		else if (barDisplay > 0.3f && barDisplay < 0.4f) {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = gauge4;
		}
		else if (barDisplay > 0.4f && barDisplay < 0.5f) {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = gauge5;
		}
		else if (barDisplay > 0.5f && barDisplay < 0.6f) {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = gauge6;
		}
		else if (barDisplay > 0.6f && barDisplay < 0.7f) {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = gauge7;
		}
		else if (barDisplay > 0.7f && barDisplay < 0.8f) {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = gauge8;
		}
		else if (barDisplay > 0.8f && barDisplay < 0.9f) {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = gauge9;
		}
		else {
			pooPooRate.GetComponent<SpriteRenderer> ().sprite = fullGauge;
		}

		pooPooRate.SetBarDisplay (1f + barDisplay/6);
	}
	/*
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
*/
	AudioSource AddAudio (AudioClip clip, bool loop, bool playAwake, float vol)
	{
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol;
		return newAudio;
	}

	void playFart ()
	{
		switch (nextFartIndex) {
		case 0:
			fart1Source.Play ();
			nextFartIndex = getNextFartIndex ();
			break;
		case 1:
			fart2Source.Play ();
			nextFartIndex = getNextFartIndex ();
			break;
		case 2:
			fart3Source.Play ();
			nextFartIndex = getNextFartIndex ();
			break;
		}
	}

	int getNextFartIndex ()
	{
		return Random.Range (0, 3);
	}

	private void DisableLaser()
	{
		laserRenderer.SetVertexCount(0);
		laserTouchEnd.SetActive (false);
	}
}
