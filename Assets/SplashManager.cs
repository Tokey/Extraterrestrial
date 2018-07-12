using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour {

   
    AudioManager audioManager;

    public float stayTime = 17;
    float counter;
    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager==null)
        {
            Debug.Log("NO AUDIO MAN");
        }
        counter = stayTime;
    }

    void Update()
    {
        counter -= Time.deltaTime;
        if(counter<=0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GO()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }




}
