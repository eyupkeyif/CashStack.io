using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> playerPrefabs;
    [SerializeField] public List<GameObject> enemyPrefabs;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
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
    }

    public void GameOverSuccess()
    {
        ViewController.instance.GoToUpgradeView();
        LevelManager.Instance.StopCharacterPlay();
    }
    }
