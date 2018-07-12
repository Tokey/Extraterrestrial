using UnityEngine;
using System.Collections;

public class Oscilator : MonoBehaviour {

	public float timeCounter;
	public float speed;
	public float width;
	public float height;


	void Start () {
		timeCounter = 0;

	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime * speed;

		transform.position = new Vector2 (width* Mathf.Cos(timeCounter),height*Mathf.Sin(timeCounter));
	}
}
