using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public int totalWaves=2;
    public int levelWaveCouter;
	private int currLevel = 0;

    AudioManager audioManager;

    bool hasDoneOnce = false;

    public GameObject ClosedDoor;
    public GameObject OpenedDoor;

    public GameObject LevelCompleteText;


    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    public float WaveCountdown
    {
        get { return waveCountdown; }
    }

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    public SpawnState State
    {
        get { return state; }
    }

    void Start()
    {
        audioManager = AudioManager.instance;
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }
        levelWaveCouter = 1;
        waveCountdown = timeBetweenWaves;

		currLevel = SceneManager.GetActiveScene ().buildIndex-2; //11.08.16: current level getter
		//totalWaves = getTotalWaveNumbers (currLevel); //11.08.16: getting total number of waves

    }


	//11.08.16: total number of waves getter
	/*int getTotalWaveNumbers(int levelIndex)
	{
		switch (levelIndex)
		{
		case 1:
			return 2;
			break;
		case 2:
			return 5;
			break;
		case 3:
			return 5;
			break;
		case 4:
			return 6;
			break;
		default:
			return 0;
			break;
		}
	}*/
    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                if(levelWaveCouter<=totalWaves)
                StartCoroutine(SpawnWave(waves[nextWave]));

                else
                {
                    if (!hasDoneOnce)
                    {
                        ClosedDoor.gameObject.SetActive(false);
                        OpenedDoor.gameObject.SetActive(true);
                        audioManager.PlaySound("DoorOpen");
                        hasDoneOnce = true;
                        LevelCompleteText.gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {
            nextWave++;
        }
        levelWaveCouter++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            for (int j = 0; j < _wave.enemy.Length; j++)
                SpawnEnemy(_wave.enemy[j]);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

}
