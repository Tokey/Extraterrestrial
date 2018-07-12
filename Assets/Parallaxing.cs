using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds; //srrsy of bck and forgrnd to be parallaxd
	private float[] parrallaxScales; //proportion of movements of the cam
	public float smoothing;

	private Transform cam;
	private Vector3 previousCamPos;

	//is called b4 start
	void Awake(){
		//setup
		cam=Camera.main.transform;
	}

	void Start () {
		previousCamPos = cam.position;
		parrallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) {
			parrallaxScales [i] = backgrounds [i].position.z * -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgrounds.Length; i++) {
			//the parallax is the opposite of the cam movment
			float parallax = (previousCamPos.x-cam.position.x) * parrallaxScales[i];

			// set a target x pos 
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			//create a target pos
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			backgrounds [i].position = Vector3.Lerp(backgrounds[i].position,backgroundTargetPos,smoothing* Time.deltaTime);
		}

		previousCamPos = cam.position;
	}
}
