using UnityEngine;
using System.Collections;


public class PlayerStats : MonoBehaviour
{	

	public static PlayerStats instance;

	public int maxHealth = 100;

	private int _curHealth;
	public int curHealth
	{
		get { return _curHealth; }
		set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
	}

	public float healthRegenRate = 2f;

	public float movementSpeed = 10f;

	void Awake()
	{
		if(instance==null){
			instance = this;
		}

	}

    void Start()
    {
        maxHealth = PlayerPrefs.GetInt("Health", 0);
        if (maxHealth < 100)
            maxHealth = 100;

        movementSpeed = PlayerPrefs.GetFloat("MovementSpeed");
        if (movementSpeed < 10f)
            movementSpeed = 10f;

        _curHealth = maxHealth;

        healthRegenRate = maxHealth / 70f;
    }
}
