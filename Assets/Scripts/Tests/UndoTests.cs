using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UndoTests
{
    //private GameBoard _gameBoard;

    [SetUp]
    public void SetUp()
    {
        //_gameBoard = new GameBoard();
    }

    [Test]
    public void Undo_Should_Remove_Last_Move()
    {
        // Make moves on the board
        //_gameBoard.MakeMove(0, 0, PlayerSymbolType.Cross);
        //_gameBoard.MakeMove(0, 1, PlayerSymbolType.Nought);

        // Undo the last move
        //_gameBoard.Undo();

        // Check if cell (0, 1) is empty after Undo
        //Assert.IsTrue(_gameBoard.IsCellEmpty(0, 1));

        // Ensure cell (0, 0) still holds the Cross symbol
        //Assert.AreEqual(_gameBoard.GetCellSymbol(0, 0), PlayerSymbolType.Cross);
    }
}
