using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour {


	public GameObject ClosedDoor;
	public GameObject OpenedDoor;
	AudioManager audioManager;
	public GameObject LevelCompleteText;


	void Start()
	{
		audioManager = AudioManager.instance;
	}
	void OnCollisionEnter2D(Collision2D _colInfo)
	{

		Player _player = _colInfo.collider.GetComponent<Player>();

		if (_player != null)
		{
			ClosedDoor.gameObject.SetActive(false);
			OpenedDoor.gameObject.SetActive(true);
			audioManager.PlaySound("DoorOpen");
			//hasDoneOnce = true;
			LevelCompleteText.gameObject.SetActive(true);
		}

	}
}
