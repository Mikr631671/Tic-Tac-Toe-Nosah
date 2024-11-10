using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : IPlayer
{
    private PlayerSymbolType _playerSymbolType;
    private IInputStrategy _inputStrategy;

    public PlayerSymbolType PlayerSymbolType => _playerSymbolType;

    public IInputStrategy InputStrategy => _inputStrategy;

    public Player(PlayerSymbolType playerSymbolType, IInputStrategy inputStrategy)
    {
        _playerSymbolType = playerSymbolType;
        _inputStrategy = inputStrategy;
    }

    public void Initialize(GameBoard gameBoard)
    {
        _inputStrategy.Initialize(gameBoard);
    }

    public async UniTask<GridCell> GetMove()
    {
        var selectedCell =  await _inputStrategy.GetSelectedCellAsync();
        return selectedCell;
    }
}

public enum PlayerSymbolType
{
    Cross,
    Nought
}