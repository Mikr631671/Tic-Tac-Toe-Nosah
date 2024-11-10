using System;

public class LobbyScreenPresenter
{
    private LobbyScreenModel _model;
    private LobbyScreenView _view;

    public event Action<GameMode> GameModeUpdate;
    public event Action StartGameClicked;

    public LobbyScreenPresenter(LobbyScreenModel model, LobbyScreenView view)
    {
        _model = model;
        _view = view;

        view.GameStartButtonClicked += OnGameStartButtonClicked;
        view.GameModeSelected += HandleStartGame;
    }

    private void OnGameStartButtonClicked()
    {
        StartGameClicked?.Invoke();
    }

    private void HandleStartGame(GameMode gameMode)
    {
        GameModeUpdate?.Invoke(gameMode);
    }
}
