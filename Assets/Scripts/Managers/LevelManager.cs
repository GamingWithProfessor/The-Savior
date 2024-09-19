using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject[] levels;
    private int levelIndex;

    [Header("Debug")]
    [SerializeField] private bool preventSpawing;

    private void Awake()
    {
        LoadData();
        if (!preventSpawing)
        SpawnLevel();

        GameManager.onGameStateChanged += GameStateChangedCallBack;

    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {

            case GameState.LevelComplete:
                levelIndex++;
                SaveData();
            break;

        }
    }

    private void SpawnLevel()
    {
        if (levelIndex >= levels.Length)
            levelIndex = 0;

        GameObject levelInstance = Instantiate(levels[levelIndex], transform);

        StartCoroutine(EnableLevelCoroutine());

        IEnumerator EnableLevelCoroutine()
        {
            yield return new WaitForSeconds(Time.deltaTime);
            levelInstance.SetActive(true);

        }
    }

    private void LoadData()
    {
        levelIndex = PlayerPrefs.GetInt("Level");
    }

    private void SaveData() 
    {
        PlayerPrefs.SetInt("Level", levelIndex);
    }
}
