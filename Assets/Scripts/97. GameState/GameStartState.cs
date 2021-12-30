using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartState : GameState
{
    public GameStartState(GameManager gameState) : base(gameState)
    {
    }

    public override IEnumerator GameStart()
    {
        Debug.Log("Game Start");

        return base.GameStart();
    }
}
