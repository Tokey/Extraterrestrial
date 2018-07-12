using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{

    [SerializeField]
    Slider MusicSlider;

    [SerializeField]
    Slider VibrationSlider;

    public static Manager ins;
    public float vol;
    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    AudioManager audioManager;

    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private GameObject upgradeMenu;

    [SerializeField]
    private GameObject ResetMenu;
    [SerializeField]
    private GameObject tutorialMenu;

    [SerializeField]
    private GameObject creditsMenu;

    void Start()
    {

        

        if (ins == null)
            ins = this;
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.Log("NO AUDIO MAN");
        }
        if (MusicSlider != null)
        {
            MusicSlider.value = PlayerPrefs.GetInt("MusicToggle", 0);
        }
        if (VibrationSlider != null)
        {
            VibrationSlider.value = PlayerPrefs.GetInt("VibrationToggle", 0);
        }

    }
    public void StardGame()
    {
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

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
    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);
        Application.Quit();
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }


    public void ToggleSettingsMenu()
    {
        audioManager.PlaySound(pressButtonSound);
        settingsMenu.SetActive(!settingsMenu.activeSelf);

    }

    public void ToggleResetMenu()
    {
        audioManager.PlaySound(pressButtonSound);
        ResetMenu.SetActive(!ResetMenu.activeSelf);

    }


    public void ToggleUpgradeMenu()
    {
        audioManager.PlaySound(pressButtonSound);
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
    }
    public void ToggleTutorialMenu()
    {
        audioManager.PlaySound(pressButtonSound);
        tutorialMenu.SetActive(!tutorialMenu.activeSelf);
    }

    public void ToggleCreditsMenu()
    {
        audioManager.PlaySound(pressButtonSound);
        creditsMenu.SetActive(!creditsMenu.activeSelf);
    }

    public void VolumeControl(float volume)
    {
        if (volume == 0)
        {
            audioManager.PlaySound("Music");
            PlayerPrefs.SetInt("MusicToggle", 0);
        }
        else if (volume == 1)
        {
            audioManager.StopSound("Music");
            PlayerPrefs.SetInt("MusicToggle", 1);
        }
        Debug.Log("FUCK THIS SHIT" + vol);
    }
    public void VibrationControl(float vibration)
    {
        if (vibration == 0)
        {
            PlayerPrefs.SetInt("VibrationToggle", 0);
        }
        else if (vibration == 1)
        {
            PlayerPrefs.SetInt("VibrationToggle", 1);
        }
    }

}
