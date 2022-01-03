using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected readonly GameManager gameState;

    public GameState(GameManager newGameState)
    {
        gameState = newGameState;
    }

    public virtual IEnumerator GameStart()
    {
        yield break;
    }

    public virtual IEnumerator Win()
    {
        yield break;
    }
    public virtual IEnumerator Lost()
    {
        yield break;
    }
}
