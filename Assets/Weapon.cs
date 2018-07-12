using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Weapon : MonoBehaviour {
	public static Weapon instance;
	public float fireRate = 0;
	public int Damage;
	public LayerMask whatToHit;
	
	public Transform BulletTrailPrefab;
	public Transform HitPrefab;
	public Transform MuzzleFlashPrefab;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;

    public string weaponShootSound = "DefaultShot";

    //caching

    AudioManager audioManager;


	// Handle camera shaking
	public float camShakeAmt = 0.05f;
	public float camShakeLength = 0.1f;
	CameraShake camShake;
	
	float timeToFire = 0;
	Transform firePoint;

	// Use this for initialization
	void Awake () {
        timeToFire = 0;
		firePoint = transform.Find ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No firePoint? WHAT?!");
		}
		if(instance==null){
			instance = this;
		}

	}

	void Start()
	{
        CrossPlatformInputManager.SetButtonUp("Jump");
        CrossPlatformInputManager.SetButtonUp("Left");
        CrossPlatformInputManager.SetButtonUp("Right");
        CrossPlatformInputManager.SetButtonUp("UpgradeButton");
        timeToFire = 0;
		camShake = GameMaster.gm.GetComponent<CameraShake>();
		if (camShake == null)
			Debug.LogError("No CameraShake script found on GM object.");

        audioManager = AudioManager.instance;

        Damage = PlayerPrefs.GetInt("WeaponDamage", 0);
        if (Damage < 10)
            Damage = 10;
    }
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot();
			}
		}
		else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
	}
	
	void Shoot () {

       /* if (CrossPlatformInputManager.GetButton("Jump") || CrossPlatformInputManager.GetButton("Left") || CrossPlatformInputManager.GetButton("Right") || CrossPlatformInputManager.GetButton("UpgradeButton"))
            ;
        else*/
        {
            Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

            Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
            if (hit.collider != null)
            {
                Debug.DrawLine(firePointPosition, hit.point, Color.red);
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DamageEnemy(Damage);
                    GameMaster.setScore(1);
                    //Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
                }
            }

            if (Time.time >= timeToSpawnEffect)
            {
                Vector3 hitPos;
                Vector3 hitNormal;

                if (hit.collider == null)
                {
                    hitPos = (mousePosition - firePointPosition) * 30;
                    hitNormal = new Vector3(999, 999, 999);
                }
                else
                {
                    hitPos = hit.point;
                    hitNormal = hit.normal;
                }

                Effect(hitPos, hitNormal);
                timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
            }
        }
	}

    void Effect(Vector3 hitPos, Vector3 hitNormal)
    {
	    Transform trail = Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
		LineRenderer lr = trail.GetComponent<LineRenderer>();

		if (lr != null)
		{
			lr.SetPosition(0, firePoint.position);
			lr.SetPosition(1, hitPos);
		}

		Destroy(trail.gameObject, 0.04f);

		if (hitNormal != new Vector3(999, 999, 999))
		{
			Transform hitParticle = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation (Vector3.right, hitNormal)) as Transform;
			Destroy(hitParticle.gameObject, 1f);
		}

        Transform clone = Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		clone.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);

		//Shake the camera
			camShake.Shake(camShakeAmt, camShakeLength);

        //PlayAudio
            audioManager.PlaySound(weaponShootSound);

	}


}
