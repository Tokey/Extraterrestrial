using UnityEngine;
using System.Collections;

public class WormHolePassword : MonoBehaviour {

	public Transform endPoint;
	public int portHoleNum;



	void OnCollisionEnter2D(Collision2D _colInfo)
	{
		Player _player = _colInfo.collider.GetComponent<Player>();

		if (_player != null)
		{

			Debug.Log("Wormhole found");
			_player.transform.position = endPoint.position;
			if(portHoleNum == 1)
			{
				WormHoleCheck.instance.e1++;
			}
			else if(portHoleNum == 2)
			{
				WormHoleCheck.instance.e2++;
			}
			else if(portHoleNum == 3)
			{
				WormHoleCheck.instance.e3++;
			}



		}

	}

}
