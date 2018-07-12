using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

    public static UpgradeMenu Instance { set; get; }

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Text healthAdderText;

    [SerializeField]
    private Text damageAdderText;


    [SerializeField]
    private Text CrystalText;

    [SerializeField]
    private Text CostH;

    [SerializeField]
    private Text CostD;
    [SerializeField]
    private Text CostL;


	[SerializeField]
	private Text damageText;

    [SerializeField]
    private Text livesText;
	[SerializeField]
	private int upgradeHealtCost=50;

	[SerializeField]
    private int upgradeDamageCost = 100;
    [SerializeField]
    private int upgradeLivesCost = 500;

    [SerializeField]
    private int upgradeDamageMul = 2;

    [SerializeField]
    private int upgradeHealthMul = 2;

    [SerializeField]
    private int upgradeLivesMul = 5;

    [SerializeField]
    private int healthAdder = 15;

	[SerializeField]
	private int DamageAdder = 10;

    void Start()
    {
        Instance = this;
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("Crystals", 0);
        PlayerPrefs.SetInt("Health", 0);
        PlayerPrefs.SetInt("WeaponDamage", 0);
        PlayerPrefs.SetInt("HealthMul", 0);
        PlayerPrefs.SetInt("DamageMul", 0);
        PlayerPrefs.SetInt("LivesMul", 0);
        PlayerPrefs.SetInt("Highscore", 0);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Lives", 1);

        if (PlayerPrefs.GetInt("WeaponDamage", 0) < 10)
            PlayerPrefs.SetInt("WeaponDamage", 10);
        if (PlayerPrefs.GetInt("Health", 0) < 100)
            PlayerPrefs.SetInt("Health", 100);

        /*if (PlayerPrefs.GetInt("HealthMul", 0) <= 0)
               PlayerPrefs.SetInt("HealthMul", 1);
           if (PlayerPrefs.GetInt("DamageMul", 0) <= 0)
               PlayerPrefs.SetInt("DamageMul", 1);
           if (PlayerPrefs.GetInt("LivesMul", 0) <= 0)
               PlayerPrefs.SetInt("LivesMul", 1);*/
    }
    void OnEnable()
    {

        //PlayerPrefs.SetInt("Crystals", 10000);
       
       /* PlayerPrefs.SetInt("Health", 0);
        PlayerPrefs.SetInt("WeaponDamage", 0);
        PlayerPrefs.SetInt("HealthMul", 0);
        PlayerPrefs.SetInt("DamageMul", 0);*/

        if (PlayerPrefs.GetInt("WeaponDamage", 0) < 10)
            PlayerPrefs.SetInt("WeaponDamage", 10);
        if (PlayerPrefs.GetInt("Health", 0) < 100)
            PlayerPrefs.SetInt("Health", 100);
        if (PlayerPrefs.GetInt("Lives", 0) < 0)
            PlayerPrefs.SetInt("Lives", 1);


        /*if (PlayerPrefs.GetInt("HealthMul", 0) <= 0)
            PlayerPrefs.SetInt("HealthMul", 1);
        if (PlayerPrefs.GetInt("DamageMul", 0) <= 0)
            PlayerPrefs.SetInt("DamageMul", 1);
        if (PlayerPrefs.GetInt("LivesMul", 0) <= 0)
            PlayerPrefs.SetInt("LivesMul", 1);*/
        UpdatValues();

    }
    
   public  void UpdatValues()
    {
        
        healthText.text = "Health: " + PlayerPrefs.GetInt("Health", 0).ToString();
        livesText.text = "Lives: " + PlayerPrefs.GetInt("Lives", 0).ToString();
        CrystalText.text = "x" + PlayerPrefs.GetInt("Crystals", 0).ToString();
        damageText.text = "Weapon: " + PlayerPrefs.GetInt("WeaponDamage", 0).ToString();
        healthAdderText.text = "+" + healthAdder ;
        damageAdderText.text = "+" + DamageAdder;

        CostH.text = "-" + (upgradeHealtCost + upgradeHealtCost * upgradeDamageMul * PlayerPrefs.GetInt("HealthMul", 0)).ToString();
        CostD.text = "-" + (upgradeDamageCost + upgradeDamageCost * upgradeHealthMul * PlayerPrefs.GetInt("DamageMul", 0)).ToString();
        CostL.text = "-" + (upgradeLivesCost + upgradeLivesCost * upgradeLivesMul * PlayerPrefs.GetInt("LivesMul", 0)).ToString(); 

    }

    public void UpgradeHealth()
    {
        if (PlayerPrefs.GetInt("Crystals", 0) >= (upgradeHealtCost + upgradeHealtCost * upgradeDamageMul * PlayerPrefs.GetInt("HealthMul", 0)))
        {
            PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals", 0) - (upgradeHealtCost + upgradeHealtCost * upgradeDamageMul * PlayerPrefs.GetInt("HealthMul", 0)));
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health", 0) + (int)healthAdder);
             PlayerPrefs.SetInt("HealthMul",PlayerPrefs.GetInt("HealthMul", 0) + 1);
			UpdatValues ();
			AudioManager.instance.PlaySound ("Crystal");
		}
		else
		AudioManager.instance.PlaySound ("NoCrystal");
		return;
    }

	public void UpgradeDamage()
	{
        if (PlayerPrefs.GetInt("Crystals", 0) >= (upgradeDamageCost + upgradeDamageCost * upgradeHealthMul * PlayerPrefs.GetInt("DamageMul", 0)))
        {
            PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals", 0) - (upgradeDamageCost + upgradeDamageCost * upgradeHealthMul * PlayerPrefs.GetInt("DamageMul", 0)));
            PlayerPrefs.SetInt("WeaponDamage", PlayerPrefs.GetInt("WeaponDamage", 0) + (int)DamageAdder);
            PlayerPrefs.SetInt("DamageMul", PlayerPrefs.GetInt("DamageMul", 0) + 1);
			UpdatValues ();

			
			AudioManager.instance.PlaySound ("Crystal");
		}
		else
			AudioManager.instance.PlaySound ("NoCrystal");
		return;
	}

    public void UpgradeLives()
    {
        if (PlayerPrefs.GetInt("Crystals", 0) >= (upgradeLivesCost + upgradeLivesCost * upgradeLivesMul * PlayerPrefs.GetInt("LivesMul", 0)))
        {
            PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals", 0) - (upgradeLivesCost + upgradeLivesCost * upgradeLivesMul * PlayerPrefs.GetInt("LivesMul", 0)));
            PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives", 0) + 1);
            PlayerPrefs.SetInt("LivesMul", PlayerPrefs.GetInt("LivesMul", 0) + 1);
            UpdatValues();


            AudioManager.instance.PlaySound("Crystal");
        }
        else
            AudioManager.instance.PlaySound("NoCrystal");
        return;
    }
		
}
