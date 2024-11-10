using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Threading;

namespace Managers
{
    public class GameLoopState : IState
    {
        private readonly IStationStateSwitcher stateSwitcher;
        private readonly GameScreen gameScreen;
        private readonly GameBoard gameBoard;
        private readonly GameTimer gameTimer;
        private readonly CompletPopupView completPopupView;
        private readonly IGameModeProvider gameModeProvider;

        private PlayerAllocator playerAllocator;
        private Player currentPlayer;
        private Stack<GridCell> moveHistory;
        private CancellationTokenSource turnCancellationTokenSource;

        public GameLoopState(IStationStateSwitcher stateSwitcher, GameScreen gameScreen,
            GameBoard gameBoard, GameTimer gameTimer,
            CompletPopupView completPopupView, IGameModeProvider gameModeProvider)
        {
            this.stateSwitcher = stateSwitcher;
            this.gameScreen = gameScreen;
            this.gameBoard = gameBoard;
            this.gameTimer = gameTimer;
            this.completPopupView = completPopupView;
            this.gameModeProvider = gameModeProvider;
        }

        public async UniTask Enter()
        {
            gameScreen.UndoButton.onClick.AddListener(TryUndo);
            gameScreen.HintButton.onClick.AddListener(TryGetHint);
            gameScreen.RestartButton.onClick.AddListener(Restart);

            playerAllocator = new PlayerAllocator(gameBoard);

            currentPlayer = null;
            moveHistory = new Stack<GridCell>();
            turnCancellationTokenSource = new CancellationTokenSource();

            gameTimer.ResetTimer();
            gameTimer.StartTimer();
            gameScreen.Show(gameModeProvider.CurrentGameMode);

            await gameBoard.DrawGameBoard();

            StartGame().Forget();
        }

        public UniTask Exit()
        {
            gameScreen.UndoButton.onClick.RemoveListener(TryUndo);
            gameScreen.HintButton.onClick.RemoveListener(TryGetHint);
            gameScreen.RestartButton.onClick.RemoveListener(Restart);

            turnCancellationTokenSource?.Cancel();
            turnCancellationTokenSource?.Dispose();

            gameTimer.StopTimer();
            gameScreen.Hide();

            return UniTask.CompletedTask;
        }

        private async UniTask StartGame()
        {
            var players = playerAllocator.GetAllocatedPlayers(gameModeProvider.CurrentGameMode);
            int currentPlayerIndex = 0;

            while (gameBoard.HasEmptyCells() && gameBoard.GetWinnerSymbol() == null)
            {
                currentPlayer = players[currentPlayerIndex];

                turnCancellationTokenSource = new CancellationTokenSource();

                try
                {
                    var move = await currentPlayer.GetMove().AttachExternalCancellation(turnCancellationTokenSource.Token);
                    await MakeMove(move).AttachExternalCancellation(turnCancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("Turn was interrupted for a hint.");
                }

                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }

            var winnerSymbol = gameBoard.GetWinnerSymbol();
            await OnGameCompleted(winnerSymbol, gameTimer.ElapsedTime);
            stateSwitcher.SwitchState<LobbyState>().Forget();
        }

        private async UniTask OnGameCompleted(PlayerSymbolType? winningSymbol, TimeSpan gameDuration)
        {
            gameTimer.StopTimer();
            var model = new CompletPopupModel(winningSymbol, gameDuration);
            var presenter = new CompletPopupPresenter(model, completPopupView);
            await presenter.ShowPopupAsync();
        }

        private void TryUndo()
        {
            if (moveHistory.Count == 0)
                return;

            if (currentPlayer.InputStrategy is not LocalPlayerInputStrategy)
                return;

            var lastMove = moveHistory.Pop();
            lastMove.ResetCell();
        }

        private async void TryGetHint()
        {
            if (!gameBoard.HasEmptyCells())
                return;

            if (currentPlayer.InputStrategy is not LocalPlayerInputStrategy)
                return;

            var emptyCells = gameBoard.GetEmptyCells();
            var randomCell = emptyCells[UnityEngine.Random.Range(0, emptyCells.Length)];

            await MakeMove(randomCell);

            turnCancellationTokenSource?.Cancel();
        }

        private async UniTask MakeMove(GridCell move)
        {
            if (currentPlayer == null)
                return;

            await gameBoard.SelectCell(currentPlayer.PlayerSymbolType, move);
            moveHistory.Push(move);
        }

        private void Restart()
        {
            gameBoard.ClearPlayingField();
            gameTimer.ResetTimer();
        }
    }
}