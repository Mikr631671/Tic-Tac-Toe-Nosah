using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerAllocator
{
    private GameBoard _gameBoard;

    public PlayerAllocator(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    public List<Player> GetAllocatedPlayers(GameMode gameMode)
    {
        var players = new List<Player>();

        switch (gameMode)
        {
            case GameMode.PVP:
                players = GetPvPPlayers();
                break;

            case GameMode.PVE:
                players = GetPvEPlayers();
                break;

            case GameMode.EVE:
                players = GetEvEPlayers();
                break;

            default:
                throw new NotSupportedException($"Game mode {gameMode} is not supported");
        }

        foreach (var player in players)
            player.Initialize(_gameBoard);

        players = players.OrderBy(_ => Guid.NewGuid()).ToList();

        return players;
    }

    private List<Player> CreatePlayersWithStrategies(params IInputStrategy[] strategies)
    {
        var shuffledSymbols = GetShuffledSymbols();

        return strategies.Select((strategy, index) => new Player(shuffledSymbols[index], strategy)).ToList();
    }

    private List<Player> GetPvPPlayers()
    {
        return CreatePlayersWithStrategies(new LocalPlayerInputStrategy(), new LocalPlayerInputStrategy());
    }

    private List<Player> GetPvEPlayers()
    {
        return CreatePlayersWithStrategies(new LocalPlayerInputStrategy(), new AIPlayerInputStrategy());
    }

    private List<Player> GetEvEPlayers()
    {
        return CreatePlayersWithStrategies(new AIPlayerInputStrategy(), new AIPlayerInputStrategy());
    }

    private List<PlayerSymbolType> GetShuffledSymbols()
    {
        List<PlayerSymbolType> symbols = Enum.GetValues(typeof(PlayerSymbolType))
                                             .Cast<PlayerSymbolType>()
                                             .ToList();

        symbols = symbols.OrderBy(_ => Guid.NewGuid()).ToList();

        return symbols;
    }
}
