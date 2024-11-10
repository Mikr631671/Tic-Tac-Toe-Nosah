using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AIPlayerInputStrategy : IInputStrategy
{
    private GameBoard _gameBoard;

    public void Initialize(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    public async UniTask<GridCell> GetSelectedCellAsync()
    {
        await SimulateThinkingDelay();

        var emptyCells = _gameBoard.GetEmptyCells();

        var selectedCell = emptyCells[Random.Range(0, emptyCells.Length)];
        return selectedCell;
    }

    /// Adds a random delay to simulate AI thinking effect.
    private async UniTask SimulateThinkingDelay()
    {
        float delay = Random.Range(0.1f, 0.5f);
        await UniTask.WaitForSeconds(delay);
    }
}
