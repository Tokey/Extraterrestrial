using UnityEngine;
using System.Collections;

public class CrystalScript : MonoBehaviour {


	public int crystalDrop = 100;
    public float stayTime=5f;
    private float stayTimeCounter;
    public Transform CrystalDeathParticles;
    private GameObject Crystal;
	void OnCollisionEnter2D(Collision2D _colInfo)
	{
		Player _player = _colInfo.collider.GetComponent<Player>();
		if (_player != null)
		{
			
			Debug.Log("Cystal found");
			GameMaster.updateCrystal (this);

		}
	}
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
