using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {
    [SerializeField]
    string mouseHoverSound = "ButtonHover";
    AudioManager audioManager;
    [SerializeField]
    string buttonPressedSound = "ButtonPress";

    [SerializeField]
    GameObject DeathButton;
    void Start()
    {
        audioManager = AudioManager.instance;
        if(PlayerPrefs.GetInt("Death",0)==1)
        {
            
            DeathButton.gameObject.SetActive(true);

        }
        else
        {
            DeathButton.gameObject.SetActive(false);
        }
    }
	public void Quit ()
	{
        audioManager.PlaySound(buttonPressedSound);
		Application.Quit();
	}

	public void Retry ()
	{
        audioManager.PlaySound(buttonPressedSound);
		SceneManager.LoadScene(3);
	}

    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseHoverSound);
    }
	
}
