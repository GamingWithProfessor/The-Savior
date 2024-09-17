using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Menu, Game, LevelComplete, GameOver }

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    private GameState gameState;

    [Header("Actions")]
    public static Action<GameState> onGameStateChanged;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Menu;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }
}
