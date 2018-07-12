using UnityEngine;
using System.Collections;

public class WormHoleCheck : MonoBehaviour {


	public GameObject ClosedDoor;
	public GameObject OpenedDoor;
	public GameObject LevelCompleteText;
	public int e1, f1, e2, f2, e3, f3;

	AudioManager audioManager;
	public static WormHoleCheck instance;

	void Start()
	{	audioManager = AudioManager.instance;
		if(instance==null){
			instance = this;
		}
	}

	void Update()
	{
		if (e1 == f1&&e2 == f2&&e3 == f3) {
			ClosedDoor.gameObject.SetActive(false);
			OpenedDoor.gameObject.SetActive(true);
			audioManager.PlaySound("DoorOpen");

			LevelCompleteText.gameObject.SetActive(true);
		
		}
	}
}
