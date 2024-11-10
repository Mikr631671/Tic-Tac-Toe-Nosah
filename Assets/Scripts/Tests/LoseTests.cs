using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LoseTests
{
    //private GameBoard _gameBoard;

    [SetUp]
    public void SetUp()
    {
        //_gameBoard = new GameBoard();
    }

    [Test]
    public void Lose_Should_Return_True_When_Opponent_Has_Winning_Combination()
    {
        // Set up a winning combination for the opponent
        //_gameBoard.SetCell(0, 0, PlayerSymbolType.Nought);
        //_gameBoard.SetCell(0, 1, PlayerSymbolType.Nought);
        //_gameBoard.SetCell(0, 2, PlayerSymbolType.Nought);

        // Ensure CheckWinCondition returns false for the current player Cross
        //Assert.IsFalse(_gameBoard.CheckWinCondition(PlayerSymbolType.Cross));

        // Ensure CheckWinCondition returns true for the opponent Nought
        //Assert.IsTrue(_gameBoard.CheckWinCondition(PlayerSymbolType.Nought));
    }
}
