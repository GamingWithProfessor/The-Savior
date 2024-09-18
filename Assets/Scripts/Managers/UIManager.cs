using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header(" Panels ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }

     private void Destroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameStateChangedCallBack(GameState gameState)
    {
        switch(gameState)
        {
        case GameState.Menu:
            menuPanel.SetActive(true);
            gamePanel.SetActive(false);
            levelCompletePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            break;

        case GameState.Game:
            menuPanel.SetActive(false);
            gamePanel.SetActive(true);
            break;     

        case GameState.Gameover:
            gamePanel.SetActive(false);
            gameOverPanel.SetActive(true);
            break;    

        case GameState.LevelComlete:
            gamePanel.SetActive(false);
            levelCompletePanel.SetActive(true);
            break;  
        }
    }

    public void PlayeButtonCallBack()
    {
        GameManager.instance.SetGameState(GameState.Game);
    }

    public void RetryButtonCallBack()
    {
        GameManager.instance.Retry();
    }

    public void NextButtonCallBack()
    {
        GameManager.instance.NextLevel();
    }
}