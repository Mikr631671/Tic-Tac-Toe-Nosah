using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LocalPlayerInputStrategy : IInputStrategy, IDisposable
{
    private GameBoard _gameBoard;

    private GridCell _clickedCell;

    public void Initialize(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
        _gameBoard.CellClicked += OnCellClicked;
    }

    public async UniTask<GridCell> GetSelectedCellAsync()
    {
        _clickedCell = null;
        await UniTask.WaitWhile(() => _clickedCell == null);
        return _clickedCell;

    }

    private void OnCellClicked(GridCell cell)
    {
        _clickedCell = cell;
    }

    public void Dispose()
    {
        _gameBoard.CellClicked -= OnCellClicked;
    }
}