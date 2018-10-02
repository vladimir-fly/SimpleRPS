using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game
{
    public event Action OnRoundStarted;
    public event Action<EMoveSet> OnAiMoveDone;
    public event Action<bool?> OnRoundEnded;
    public event Action<string> OnScoreUpdated;
    
    private int _playerWins;
    private int _aiWins;
    private float _aiCheatChance;
    
    private EMoveSet _playerMove;
    private EMoveSet _aiMove;
    
    // aiChance < 0 => ai plays honestly 
    public void Start(float aiChance = -1)
    {
        _playerWins = 0;
        _aiWins = 0;
        _aiCheatChance = aiChance;
        
        OnRoundStarted?.Invoke();
        OnScoreUpdated?.Invoke($"{_playerWins} : {_aiWins}");
        Debug.Log("New game started!");
    }

    public void MakeRound(EMoveSet move, float aiCheatChance = -1)
    {
        _aiCheatChance = aiCheatChance;
        
        Debug.Log($"Round started, ai cheat chance is {_aiCheatChance}");
        
        MakePlayerMove(move);
        MakeAiMove();
        CheckResults();
    }

    private void MakePlayerMove(EMoveSet move)
    {
        _playerMove = move;
        Debug.Log($"Player move: {move}");
    }

    private void MakeAiMove()
    {
        if (_aiCheatChance < 0)
            _aiMove = (EMoveSet) Random.Range(0, 3);
        else
        {
            var cheat = Math.Round(Random.value, 2);
            Debug.Log($"AI cheat: {cheat}");
            if (Math.Abs(cheat) <= _aiCheatChance)
            {
                switch (_playerMove)
                {
                    case EMoveSet.Rock:
                        _aiMove = EMoveSet.Paper;
                        break;
                    case EMoveSet.Paper:
                        _aiMove = EMoveSet.Scissors;
                        break;
                    case EMoveSet.Scissors:
                        _aiMove = EMoveSet.Rock;
                        break;
                }
                
                Debug.Log($"Cheat aiMove is {_aiMove}");
            }
            else
            {
                // replay honestly
                _aiMove = (EMoveSet) Random.Range(0, 3);
                Debug.Log($"Honest aiMove is {_aiMove}");
            }

        }
        
        Debug.Log($"AI move: {_aiMove}");
        OnAiMoveDone?.Invoke(_aiMove);
    }
    
    private void CheckResults()
    {
        if (_playerMove == _aiMove)
        {
            OnRoundEnded?.Invoke(null);
            return;
        }

        var result = (int) _playerMove - (int) _aiMove;
        var roundResult = !(result == -1 || result == 2);
        
        if (roundResult) _playerWins++;
        else _aiWins++;
        
        Debug.Log($"Round result: {roundResult}");
        
        OnScoreUpdated?.Invoke($"{_playerWins} : {_aiWins}");
        OnRoundEnded?.Invoke(roundResult);
    }
}
