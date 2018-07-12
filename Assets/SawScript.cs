using UnityEngine;
using System.Collections;

public class SawScript : MonoBehaviour {


    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(999);
        }
    }
}
