using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.CrossPlatformInput;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;
    public bool isLevel1 = false;

    [SerializeField]
    private int maxLives;

    private static int _remainingLives = 1;

    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    private static int HighScore;

    private static int _Score = 0;
    public static int getScore
    {
        get { return _Score; }
    }

    public static int getHighScore
    {
        get { return HighScore; }
    }
    public static void setScore(int am)
    {
        _Score += am;
        PlayerPrefs.SetInt("Score", _Score);
        if (_Score >= HighScore)
        {
            HighScore = _Score;
            PlayerPrefs.SetInt("Highscore", HighScore);
        }
    }

    [SerializeField]
    private int startingCrystals;

    public static int Crystals;
    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject updrageMenu;

    [SerializeField]
    private GameObject UI;


    [SerializeField]
    private GameObject getReadyText;

    [SerializeField]
    private GameObject MobileButtons;

    [SerializeField]
    private GameObject PButton;

    [SerializeField]
    private WaveSpawner waveSpawner;

    public delegate void UpgradeMenuCallBack(bool active);

    public UpgradeMenuCallBack onToggleUpgradeMenu;


    private AudioManager audioManager;

    public string respawnSound = "RespawnCountdown";
    public string spawnSound = "Spawn";
    public string gameOver = "GameOver";

    public CameraShake cameraShake;

    void Start()
    {


    

        if (cameraShake == null)
        {
            Debug.LogError("No camera shake referenced in GameMaster");
        }
        _Score = 0;

        Crystals = startingCrystals;

        //caching 
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.Log("No audio manager found i the scene");
        }

        HighScore = PlayerPrefs.GetInt("Highscore", 0);

        Crystals = PlayerPrefs.GetInt("Crystals", 0);

        if (isLevel1)
        {
            PlayerPrefs.SetInt("Score", 0);
            _Score = PlayerPrefs.GetInt("Score", 0);
            if (PlayerPrefs.GetInt("Lives", 0) == 0)
                PlayerPrefs.SetInt("Lives", 1);
            _remainingLives = PlayerPrefs.GetInt("Lives", 0);

            PlayerPrefs.SetInt("Death", 1);
           // AdManager.Instance.ShowBanner();
        }
        else
        {
            //PlayerPrefs.SetInt("Death", 0);
            _Score = PlayerPrefs.GetInt("Score", 0);
           // AdManager.Instance.ShowBanner();
        }

        
    }

    public void QuitGame()
    {
        audioManager.PlaySound("ButtonPress");
        Application.Quit();
    }

    public IEnumerator _RespawnPlayer()
    {
        audioManager.PlaySound(respawnSound);

        yield return new WaitForSeconds(spawnDelay);
        audioManager.PlaySound(spawnSound);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone, 3f);
    }

    public void EndGame()
    {
        audioManager.PlaySound(gameOver);
        gameOverUI.SetActive(true);
        Debug.Log("Game over!");
    }
    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        Transform _clone = Instantiate(player.playerDeathParticle, player.transform.position, Quaternion.identity);
        Destroy(_clone, 5f);
        _remainingLives--;
        if (_remainingLives <= 0)
        {
            gm.EndGame();

        }
        else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }

    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButton("Upgrade"))
        {
            ToggleUpgradeMenu();
        }

    }


    public void ToggleUpgradeMenu()
    {
        bool paused = updrageMenu.activeSelf;
        updrageMenu.SetActive(!updrageMenu.activeSelf);
        MobileButtons.SetActive(!MobileButtons.activeSelf);
        PButton.SetActive(!PButton.activeSelf);
        UI.SetActive(!UI.activeSelf);

        if (paused)
        {
            
            StartCoroutine(GettingReady(1.0f));
        }
        else
        {
            
            onToggleUpgradeMenu.Invoke(updrageMenu.activeSelf);
            waveSpawner.enabled = !updrageMenu.activeSelf;
        }

        //Time.timeScale = Time.timeScale == 1 ? 0 : 1;

    }

    IEnumerator GettingReady(float duration)
    {
        //This is a coroutine

        Debug.Log("Start Wait() function. The time is: " + Time.time);
        Debug.Log("Float duration = " + duration);
        getReadyText.SetActive(true);
        yield return new WaitForSeconds(duration);   //Wait
        getReadyText.SetActive(false);
        onToggleUpgradeMenu.Invoke(updrageMenu.activeSelf);
        waveSpawner.enabled = !updrageMenu.activeSelf;
        Debug.Log("End Wait() function and the time is: " + Time.time);
    }

    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }



    public static void updateCrystal(CrystalScript crystal)
    {
        _updateCrystal(crystal);
    }
    public static void _updateCrystal(CrystalScript _crystal)
    {
        Crystals += (int)Random.Range(1f, (float)_crystal.crystalDrop);
        PlayerPrefs.SetInt("Crystals", Crystals);
        Destroy(_crystal.gameObject);
        Transform _clone = Instantiate(_crystal.CrystalDeathParticles, _crystal.transform.position, Quaternion.identity);
        Destroy(_clone, 5f);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        audioManager.PlaySound(_enemy.deathSoundName);

        //Crystals +=(int) Random.Range(0f,(float) _enemy.crystalDrop);

        //audioManager.PlaySound ("Crystal");
        //  _Score += 1;
        Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) ;
        Destroy(_clone, 5f);
        Transform _clone2 = Instantiate(_enemy.crystal, _enemy.transform.position, Quaternion.identity);

        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
       
    }

    public void backFromDead()
    {
        _remainingLives = 1;
    }

}
