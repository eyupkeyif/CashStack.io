using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] public Character player;
    [SerializeField] GameObject moneyPrefab;
    [SerializeField] Transform parent, StartPos;
    [SerializeField] int totalMoneyinGame;
    public delegate void LevelCompleteDelegate();
    public event LevelCompleteDelegate OnLevelCompleted;
    [SerializeField] Joystick joystick;
    int currentLevel = 1;
    public int deliveredMoney = 20;
    public List<GameObject> totalMoneyList = new List<GameObject>();
    public Collect collect;
    GameObject playerPrefab;
    Controller playerController;
    [SerializeField] int enemySpawn;
    private void Awake()
    {
        deliveredMoney = 20;
        currentLevel = 1;
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion

        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }
        else
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }

        if (!PlayerPrefs.HasKey("TotalMoneyDelivered"))
        {
            PlayerPrefs.SetInt("TotalMoneyDelivered", deliveredMoney);
        }
        else
        {
            deliveredMoney = PlayerPrefs.GetInt("TotalMoneyDelivered");
        }

        //if (!PlayerPrefs.HasKey("Capacity"))
        //{
        //    PlayerPrefs.SetInt("Capacity", collect.capacity);
        //}
        //else
        //{
        //    collect.capacity = PlayerPrefs.GetInt("Capacity");
        //}

        Debug.Log("currentLevel: " + currentLevel);
    }

    public void StartCharacterPlay()
    {

        joystick.Enable();

        SpawnCharacter();
        CameraManager cameraManager = FindObjectOfType<CameraManager>();
        //cameraManager.ReturnToFirstPoint();
        cameraManager.StartFollow(playerPrefab);
    }
    public void StopCharacterPlay()
    {
        joystick.Disable();
        Controller controller = playerPrefab.GetComponent<Controller>();
        controller.StopMovement();
        StopAllCoroutines();
    }

    void Start()
    {
        SpawnInitialMoney();
    }

    // Update is called once per frame
    void Update()
    {
        //if (collect)
        //{
        //   //Debug.Log(collect.gameObject.name);
        //    if (collect.totalMoney >= deliveredMoney)
        //    {
        //        Debug.Log("Congrats!");

        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //    }
        if (totalMoneyList.Count < totalMoneyinGame)
        {
            SpawnMoney();
        }
        //}

    }


    public void SpawnInitialMoney()
    {
        for (int i = 0; i < totalMoneyinGame; i++)
        {
            SpawnMoney();
        }
    }

    public void SpawnMoney()
    {
        float randomDistance = Random.Range(60, 170);
        Vector2 randDirection = Random.insideUnitCircle.normalized*randomDistance;
        
        Vector3 randomPos = new Vector3(randDirection.x, moneyPrefab.transform.position.y, randDirection.y);
        GameObject money = Instantiate(moneyPrefab, randomPos, Quaternion.identity, parent);
        Money moneyScript = money.GetComponent<Money>();
        totalMoneyList.Add(money);

    }

    public void LoadNextLevel()
    {
        currentLevel += 1;
        deliveredMoney += 10;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("TotalMoneyDelivered", deliveredMoney);
        PlayerPrefs.SetInt("Capacity", collect.capacity);
        PlayerPrefs.SetFloat("SpeedMultiplier", playerController.speedMultiplier);

        Debug.Log("Multiplier: " + collect.capacity + "Speed.: " + playerController.speedMultiplier);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }

    private void SpawnCharacter()
    {
        //if (player != null)
        //{
        //    collect.onMoneyDeliver += TotalMoneyDelivered;
        //    return;
        //}
        //else
        //{
        playerPrefab = GameManager.Instance.playerPrefabs[0];
            GameObject spawnedChar = Instantiate(playerPrefab,StartPos.position,Quaternion.identity);
            collect = spawnedChar.GetComponent<Collect>();
            player = spawnedChar.GetComponent<Character>();
        playerController = spawnedChar.GetComponent<Controller>();
            collect.onMoneyDeliver += TotalMoneyDelivered;
            //spawnedChar.transform.position = GetRandomPointOnMap();


        //}
    }
    public void SpawnEnemy()
    {
        float randomDistance = Random.Range(60, 170);
        Vector2 randDirection = Random.insideUnitCircle.normalized * randomDistance;
        int randomEnemy = Random.Range(0, GameManager.Instance.enemyPrefabs.Count - 1);
        Vector3 randomPos = new Vector3(randDirection.x, 0, randDirection.y);

        GameObject enemy = Instantiate(GameManager.Instance.enemyPrefabs[randomEnemy],randomPos,Quaternion.identity);
        //enemy.transform.position = randomPos;

    }

    public void SpawnEnemyStart()
    {
        for (int i = 0; i < enemySpawn; i++)
        {
            SpawnEnemy();
        }
    }

    public void TotalMoneyDelivered()
    {
        if (collect.totalMoney>=deliveredMoney)
        {
            OnLevelCompleted?.Invoke();
        }
    }

    public int GetTotalCollectedMoney()
    {
        return collect.moneyCount;
    }
    public int GetTotalMoneyDelivered()
    {
        return collect.SetTotalMoney();
    }

    public int GetMaxDeliveredMoney()
    {
        return deliveredMoney;
    }
    //public void CheckCurrentLevel()
    //{
    //    if (PlayerPrefs.GetInt("CurrentLevel")>=3&& PlayerPrefs.GetInt("CurrentLevel")<6)
    //    {
    //        playerPrefab = GameManager.Instance.playerPrefabs[1];
    //        Debug.Log("player name: " + playerPrefab.gameObject.name);
    //    }
    //   else if (PlayerPrefs.GetInt("CurrentLevel") >= 6 && PlayerPrefs.GetInt("CurrentLevel") < 10)
    //    {
    //        playerPrefab = GameManager.Instance.playerPrefabs[2];
    //        Debug.Log("player name: " + playerPrefab.gameObject.name);


    //    }
    //   else if (PlayerPrefs.GetInt("CurrentLevel") >= 10)
    //    {
    //        playerPrefab = GameManager.Instance.playerPrefabs[3];
    //        Debug.Log("player name: " + playerPrefab.gameObject.name);

    //    }
    //    else
    //    {
    //        playerPrefab = GameManager.Instance.playerPrefabs[0];
    //        Debug.Log("player name: " + playerPrefab.gameObject.name);


    //    }
    //}

    public void UpgradeCapacity()
    {
        if (collect.capacity>20)
        {
            collect.capacity += 0;
        }
        else
        {
            collect.capacity++;
        }
       
        //PlayerPrefs.SetInt("Capacity", collect.capacity);
    }
    public void UpgradeSpeed()
    {

        if (playerController.speedMultiplier>2)
        {
            playerController.speedMultiplier += 0;
        }
        else
        {
            playerController.speedMultiplier += 0.1f;

        }
    }
}
