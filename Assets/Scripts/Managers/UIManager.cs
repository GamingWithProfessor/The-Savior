using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header(" Panels ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject gameoverPanel;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Menu:
                menuPanel.SetActive(true);
                gamePanel.SetActive(false);
                levelCompletePanel.SetActive(false);
                gameoverPanel.SetActive(false);
                break;

            case GameState.Game:
                menuPanel.SetActive(false);
                gamePanel.SetActive(true);
                break;

            case GameState.LevelComplete:
                gamePanel.SetActive(false);
                levelCompletePanel.SetActive(true);
                break;

            case GameState.Gameover:
                gamePanel.SetActive(false);
                gameoverPanel.SetActive(true);
                break;
        }
    }

    public void PlayButtonCallback()
    {
        GameManager.instance.SetGameState(GameState.Game);        
    }

    public void RetryButtonCallback()
    {
        GameManager.instance.Retry();
    }

    public void NextButtonCallback()
    {
        GameManager.instance.NextLevel();
    }
}
