using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    #region Singleton
    public static ViewController instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }


    }
    #endregion

    [SerializeField] ViewManager viewManager;
    [SerializeField] MainMenuView mainMenuView;
    [SerializeField]  InGameView inGameView;
    [SerializeField] UpgradeView upgradeView;
    [SerializeField] NewCharacterView newCharacterView;


    private void Start()
    {
        viewManager.ShowStartView();

        mainMenuView = viewManager.GetView<MainMenuView>();
        inGameView = viewManager.GetView<InGameView>();
        upgradeView = viewManager.GetView<UpgradeView>();
        newCharacterView = viewManager.GetView<NewCharacterView>();

        if (mainMenuView!=null)
        {
            mainMenuView.PlayButtonEvent += StartPlay;
        }
        if (inGameView!=null)
        {
            //Debug.Log(LevelManager.Instance.GetTotalCollectedMoney());
            //inGameView.SetMoneyCountTextBar(LevelManager.Instance.GetTotalCollectedMoney());
        }
        if (upgradeView!=null)
        {
            upgradeView.MultiplierButtonEvent += LevelManager.Instance.UpgradeCapacity;
            upgradeView.SpeedButtonEvent += LevelManager.Instance.UpgradeSpeed;
            upgradeView.MultiplierButtonEvent += OnLoadNextLevel;
            upgradeView.SpeedButtonEvent += OnLoadNextLevel;
        }
        if (newCharacterView!=null)
        {
            newCharacterView.ConfirmButtonEvent += OnLoadNextLevel;
            newCharacterView.ConfirmButtonEvent += newCharacterView.ResetNewCharacter;
        }
    }

    public void StartPlay()
    {
        viewManager.Show(inGameView);
        //LevelManager.Instance.CheckCurrentLevel();
        //UpdateProgressBar();
        LevelManager.Instance.StartCharacterPlay();
        LevelManager.Instance.SpawnEnemyStart();
        UpdateComboBar();
        UpdateCurrency();
        UpdateProgressBar();
        LevelManager.Instance.OnLevelCompleted += GameManager.Instance.GameOverSuccess;
    }

    public void OnLoadNextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
        viewManager.Show(mainMenuView);
    }

    public void UpdateProgressBar()
    {
        inGameView.SetProgressBarAmount((float)LevelManager.Instance.collect.totalMoney/(float)LevelManager.Instance.deliveredMoney);
        inGameView.SetProgressBarText(LevelManager.Instance.collect.totalMoney, LevelManager.Instance.deliveredMoney);
        Debug.Log(LevelManager.Instance.collect.totalMoney + " " + LevelManager.Instance.deliveredMoney);
    }
    public void UpdateComboBar()
    {

        inGameView.SetComboBarAmount((float) LevelManager.Instance.collect.moneyCount / (float)LevelManager.Instance.collect.capacity);
        if (inGameView.GetComboBarValues()>=0.9f)
        {
            inGameView.SetComboMeterText("x2");
        }
        else if (inGameView.GetComboBarValues()>=0.4f && inGameView.GetComboBarValues()<0.9f)
        {
            inGameView.SetComboMeterText("x1.5");
        }
        else if (inGameView.GetComboBarValues()<0.4f)
        {

            inGameView.SetComboMeterText("x1");
        }
        
        Debug.Log(LevelManager.Instance.collect.moneyCount / LevelManager.Instance.collect.capacity);

    }
    public void UpdateCurrency()
    {
        inGameView.SetMoneyCountTextBar(LevelManager.Instance.collect.moneyCount);
    }

    public void ResetComboBar()
    {
        inGameView.ResetComboBarAmount();
    }

    public void GoToUpgradeView()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") == 1 || PlayerPrefs.GetInt("CurrentLevel") >=3 && PlayerPrefs.GetInt("CurrentLevel") <5 || PlayerPrefs.GetInt("CurrentLevel") >= 6 && PlayerPrefs.GetInt("CurrentLevel") < 9|| PlayerPrefs.GetInt("CurrentLevel") >=10)
        {
            //LevelManager.Instance.LoadNextLevel();

            viewManager.Show(upgradeView);
        }
        else if (PlayerPrefs.GetInt("CurrentLevel") ==2 || PlayerPrefs.GetInt("CurrentLevel") ==5 || PlayerPrefs.GetInt("CurrentLevel") == 9)
        {
            //LevelManager.Instance.LoadNextLevel();

            viewManager.Show(newCharacterView);

            if (PlayerPrefs.GetInt("CurrentLevel") == 2)
            {
                Debug.Log("current char");
                newCharacterView.GetNewCharacter(0);
            }
            else if (PlayerPrefs.GetInt("CurrentLevel") == 5)
            {
                newCharacterView.GetNewCharacter(1);
            }
            else if (PlayerPrefs.GetInt("CurrentLevel") == 9)
            {
                newCharacterView.GetNewCharacter(2);
            }
        }
        //else
        //{
        //    viewManager.Show(mainMenuView);
        //    //LevelManager.Instance.LoadNextLevel();
        //}
    }
}
