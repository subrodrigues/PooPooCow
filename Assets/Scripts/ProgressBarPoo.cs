using UnityEngine;
using System.Collections;

public class ProgressBarPoo : MonoBehaviour {

     public float barDisplay; //current progress
     public Vector2 pos = new Vector2(-10, -10);
     public Vector2 size = new Vector2(60, 20);
     public Texture2D emptyTex;
     public Texture2D fullTex;
     public Camera camera;
     private bool drawBar = false;
	private Vector3 hitPosition;

	private float x, y;
       private Vector2 resolution;

        void Start(){
			camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
	        resolution = new Vector2(Screen.width, Screen.height);

	        x=Screen.width/1280.0f; 

	        y=Screen.height/720.0f; 
        }

     void OnGUI() {
        if(drawBar){
             //draw the background:
             GUI.BeginGroup(new Rect(pos.x, pos.y, size.x*x, size.y*y));
              //   GUI.Box(new Rect(0,0, size.x*x, size.y*y), emptyTex);
             
                 //draw the filled-in part:
                 GUI.BeginGroup(new Rect(0,0, size.x * barDisplay * x, size.y*y));
                     GUI.Label(new Rect(0,0, size.x*x, size.y*y), fullTex);
                 GUI.EndGroup();
             GUI.EndGroup();
         }
     }
     
		void Update() {
        {
            if(Screen.width!=resolution.x || Screen.height!=resolution.y)
            {
                resolution=new Vector2(Screen.width, Screen.height);
                x=resolution.x/1280.0f;
                y=resolution.y/720.0f;
            }
         }

    //    Debug.Log(transform.position);
	//	Vector2 posPoo =  camera.WorldToScreenPoint(GetComponentInParent<Transform>().position);
				Vector2 posPoo =  manualWorldToScreenPoint(transform.GetComponentInChildren<Transform>().position);

				pos.x = posPoo.x - (30*x);
				pos.y = Screen.height - posPoo.y + (30*y);
								// TODO: Remove this
			//	Debug.Log ("Transform Pos " + GetComponentInParent<Transform>().position.x);
				// TODO: Remove this

         //for this example, the bar display is linked to the current time,
         //however you would set this value based on your desired display
         //eg, the loading progress, the player's health, or whatever.
        StreamCell tempVal = transform.GetComponentInChildren<StreamCell>();
        barDisplay =  tempVal.life;

     }

		Vector3 manualWorldToScreenPoint(Vector3 wp) {
				// calculate view-projection matrix
				Matrix4x4 mat = camera.projectionMatrix * camera.worldToCameraMatrix;

				// multiply world point by VP matrix
				Vector4 temp = mat * new Vector4(wp.x, wp.y, wp.z, 1f);

				if (temp.w == 0f) {
						// point is exactly on camera focus point, screen point is undefined
						// unity handles this by returning 0,0,0
						return Vector3.zero;
				} else {
						// convert x and y from clip space to window coordinates
						temp.x = (temp.x/temp.w + 1f)*.5f * Screen.width;
						temp.y = (temp.y/temp.w + 1f)*.5f * Screen.height;
						return new Vector3(temp.x, temp.y, wp.z);
				}
		}

	public void startBar(Vector3 hitPosition){
				this.hitPosition = hitPosition;
        this.drawBar = true;
     }
 
}
