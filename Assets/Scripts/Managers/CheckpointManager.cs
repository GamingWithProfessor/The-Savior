using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    [Header("Settings")]
    private Vector3 lastCheckPointPosition;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        Checkpoint.onInteracted += CheckPointInteractedCallBack;

        GameManager.onGameStateChanged += GameStateChangedCallback;

    }

   

    private void onDestroy()
    {
        Checkpoint.onInteracted -= CheckPointInteractedCallBack;

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
        switch (gameState)
        {
         

            case GameState.LevelComplete:
                lastCheckPointPosition = Vector3.zero;
                break;

           
        }
    }

    private void CheckPointInteractedCallBack(Checkpoint checkpoint)
    {
        lastCheckPointPosition = checkpoint.GetPosition();
    }

    public Vector3 GetCheckpointPosition()
    {
        return lastCheckPointPosition;
    }
}
