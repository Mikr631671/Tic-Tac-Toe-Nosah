using Managers;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private InputService inputService;
    [SerializeField] private GameBoard gameBoard;
    
    [SerializeField] private CompletPopupView _completPopupView;
    [SerializeField] private LobbyScreen lobbyScreen;
    [SerializeField] private GameScreen gameScreen;

    [SerializeField] private GameAssetsLoader gameAssetsLoader;
    
    private StateMachine stateMachine;
    
    private void Start()
    {
        gameBoard.Initialize(inputService, gameAssetsLoader);
        lobbyScreen.Initialize();

        InitializeStateMachine();
    }

    private async void InitializeStateMachine()
    {
        stateMachine = new StateMachine();

        stateMachine
            .AddState(new LobbyState(stateMachine, lobbyScreen))
            .AddState(new GameLoopState(stateMachine, gameScreen, gameBoard, gameTimer, _completPopupView, lobbyScreen));

        await stateMachine.Initialize();
    }
}
