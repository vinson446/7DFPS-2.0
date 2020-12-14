using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] int currentRound;
    public int CurrentRound { get => currentRound; set => currentRound = value; }

    [SerializeField] int currentScore;
    public int CurrentScore { get => currentScore; set => currentScore = value; }

    [SerializeField] int numEnemiesKilledThisRound;
    public int NumEnemiesKilledThisRound => numEnemiesKilledThisRound;

    [SerializeField] int numEnemiesKilledTotal;
    [SerializeField] bool isStartingNewRound;

    [Header("Spawn Settings")]
    [SerializeField] int currentNumEnemiesSpawned;
    [SerializeField] int totalNumEnemiesToSpawn;
    public int TotalNumEnemiesToSpawn => totalNumEnemiesToSpawn;
    //[SerializeField] float timeBetweenSpawn1;
    //[SerializeField] float timeBetweenSpawn2;
    [SerializeField] float timeBetweenWaves;

    [Header("Spawn References")]
    [SerializeField] GameObject[] enemiesToSpawn;
    [SerializeField] Transform[] spawnSettings;

    UIGameManager uiGameManager;

    // public static GameManager instance;

    private void Awake()
    {
        /*
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        uiGameManager = FindObjectOfType<UIGameManager>();

        StartNewRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartNewRound()
    {
        IncreaseRound();

        uiGameManager.ShowEnemyCount();

        StartCoroutine(SpawnEnemyCoroutine());
    }

    public void IncreaseRound()
    {
        currentRound += 1;
        uiGameManager.UpdateRound(currentRound);

        if (currentRound != 1)
        {
            currentNumEnemiesSpawned = 0;
            totalNumEnemiesToSpawn = Random.Range(totalNumEnemiesToSpawn * 2, totalNumEnemiesToSpawn * 3);
        }
    }

    public void IncreaseScore(int amt)
    {
        currentScore += amt;
        uiGameManager.UpdateScore(currentScore);
    }

    public void UpdateEnemyKilled()
    {
        numEnemiesKilledThisRound += 1;
        numEnemiesKilledTotal += 1;

        uiGameManager.ShowEnemyCount();

        if (numEnemiesKilledThisRound >= totalNumEnemiesToSpawn && !isStartingNewRound)
        {
            numEnemiesKilledThisRound = 0;
            uiGameManager.ShowEnemyCount();

            uiGameManager.StartNewRoundCoroutine();
            isStartingNewRound = true;
        }
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        while (currentNumEnemiesSpawned < totalNumEnemiesToSpawn)
        {
            for (int i = 0; i < totalNumEnemiesToSpawn / 3; i++)
            {
                if (currentNumEnemiesSpawned < totalNumEnemiesToSpawn)
                {
                    int spawnedEnemyIndex = Random.Range(0, enemiesToSpawn.Length);
                    GameObject e = enemiesToSpawn[spawnedEnemyIndex];

                    GameObject enemy = Instantiate(e, spawnSettings[Random.Range(0, spawnSettings.Length)].position, Quaternion.identity);

                    // melee / boss
                    if (spawnedEnemyIndex == 0 || spawnedEnemyIndex == 1 || spawnedEnemyIndex == 4)
                    {
                        MeleeEnemy mEnemy = enemy.GetComponent<MeleeEnemy>();
                        mEnemy.LevelUp(currentRound);

                        mEnemy.CurrentHP = mEnemy.MaximumHP;
                    }
                    // range
                    else if (spawnedEnemyIndex == 2 || spawnedEnemyIndex == 3)
                    {
                        RangeEnemy rEnemy = enemy.GetComponent<RangeEnemy>();
                        rEnemy.LevelUp(currentRound);

                        rEnemy.CurrentHP = rEnemy.MaximumHP;
                    }

                    currentNumEnemiesSpawned += 1;
                }
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }

        isStartingNewRound = false;
    }
}
