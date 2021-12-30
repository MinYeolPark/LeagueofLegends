using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    private GameState curGameState;

    private void Start()
    {
        SetupBattle();
    }

    private void SetupBattle()
    {
        SetState(new GameStartState(this));
    }

    public void SetState(GameState newGameState)
    {
        curGameState = newGameState;
        StartCoroutine(curGameState.GameStart());
    }
}
