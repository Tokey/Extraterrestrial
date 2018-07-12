using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadSceneScript : MonoBehaviour {

    public void LoadSceneNum(int num)
    {
        if (num < 0 || num >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Can't load scene with index " + num);
        }
        else
        {
            LoadingScreenManager.LoadScene(num);
        }

    }
}
