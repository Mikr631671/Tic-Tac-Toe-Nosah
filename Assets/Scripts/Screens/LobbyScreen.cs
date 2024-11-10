using System;
using UnityEngine;

namespace Managers
{
    public class LobbyScreen : MonoBehaviour, IGameModeProvider
    {
        [SerializeField] private LobbyScreenView _lobbyScreenView;
        
        private LobbyScreenPresenter presenter;
        
        public GameMode CurrentGameMode { get; private set; }

        public event Action<GameMode> GameModeUpdate;
        public event Action StartGameClicked;
        
        public void Initialize()
        {
            var model = new LobbyScreenModel();
            presenter = new LobbyScreenPresenter(model, _lobbyScreenView);

            presenter.StartGameClicked += OnStartGameClicked;
            presenter.GameModeUpdate += OnGameModeUpdate;
        }
        
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        private void OnStartGameClicked()
        {
            StartGameClicked?.Invoke();
        }

        private void OnGameModeUpdate(GameMode gameMode)
        {
            GameModeUpdate?.Invoke(gameMode);
            CurrentGameMode = gameMode;
        }

        private void OnDestroy()
        {
            presenter.StartGameClicked -= OnStartGameClicked;
            presenter.GameModeUpdate -= OnGameModeUpdate;
        }
    }

    public interface IGameModeProvider
    {
        GameMode CurrentGameMode { get; }
    }
}