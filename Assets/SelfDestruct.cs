using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float stayTime=5f;
	private float stayTimeCounter;

	void Start ()
	{
		stayTimeCounter = stayTime;

	}
	void Update()
	{
		stayTimeCounter -= Time.deltaTime;
		if(stayTimeCounter<=0)
		{
			Destroy(this.gameObject);     
		}
	}
}
