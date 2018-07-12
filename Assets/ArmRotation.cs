using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {


    public int rotationOffset = 90;
	// Update is called once per frame
	void Update () {
        
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize(); //x+y+z = 1 // keeping the same proportion;

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //angle in degs
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        
	}
}
