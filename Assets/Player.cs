using UnityEngine;
using System.Collections;



[RequireComponent(typeof(UnityStandardAssets._2D.Platformer2DUserControl))]
public class Player : MonoBehaviour
{


    public int fallBoundary = -20;
	public int jumpBoundary = 100;
    [SerializeField]
    private StatusIndicator statusIndicator;
    private AudioManager audioManager;
    public string deathSound = "RobotDeath";
    public string gruntSound = "RobotGrunt";

	private PlayerStats stats;
    public Transform playerDeathParticle;
	public bool letLifeRegen = true;
    void Start()
    {
		stats = PlayerStats.instance;
		stats.curHealth = stats.maxHealth;

        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on Player");
        }
        else
        {
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

		GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;
        audioManager = AudioManager.instance;
        if(audioManager==null)
        {
            Debug.LogError("no reference to audio dhur :@");
        }

		InvokeRepeating ("RegenHealth",1f/stats.healthRegenRate,1f/stats.healthRegenRate);
    }

    void Update()
    {
		if (transform.position.y <= fallBoundary||transform.position.y>=jumpBoundary)
            DamagePlayer(9999);
    }


	void OnUpgradeMenuToggle(bool active){

		GetComponent<UnityStandardAssets._2D.Platformer2DUserControl> ().enabled = !active;
		Weapon _weapon = GetComponentInChildren<Weapon> ();
		if (_weapon != null)
			_weapon.enabled = !active;

	}

	void RegenHealth(){
		if(letLifeRegen)
		{
			stats.curHealth += 1;
			statusIndicator.SetHealth(stats.curHealth,stats.maxHealth);
		}

	}
    public void DamagePlayer(int damage)
    {
		PlayerStats.instance.curHealth -= damage;

        if (PlayerPrefs.GetInt("VibrationToggle",0)==0)
        Handheld.Vibrate();
		if (PlayerStats.instance.curHealth <= 0)
        {
            audioManager.PlaySound(deathSound);
            GameMaster.KillPlayer(this);
        }
        else
            audioManager.PlaySound(gruntSound);

		statusIndicator.SetHealth(stats.curHealth,stats.maxHealth);
       // AdManager.Instance.ShowBanner();
    }


	void OnDestroy(){
		GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
	}
}
