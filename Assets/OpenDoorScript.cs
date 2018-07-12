using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class OpenDoorScript : MonoBehaviour
{

    //public int nextLevelIndex = 0;

    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();

        if (_player != null)
        {


            /* if (nextLevelIndex < 0 || nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
             {
                 Debug.LogError("Can't load scene with index " + nextLevelIndex);
             }
             else
             {
                 LoadingScreenManager.LoadScene(nextLevelIndex);
             }*/
          
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


        }

    }
}
