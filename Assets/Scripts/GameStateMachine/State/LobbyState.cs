using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class LobbyState : IState
    {
        private readonly IStationStateSwitcher stateSwitcher;
        private readonly LobbyScreen lobbyScreen;

        public LobbyState(IStationStateSwitcher stateSwitcher, LobbyScreen lobbyScreen)
        { 
            this.stateSwitcher = stateSwitcher;
            this.lobbyScreen = lobbyScreen;
        } 
        
        public UniTask Enter()
        {
            lobbyScreen.Show();
            
            lobbyScreen.StartGameClicked += OnStartGameClicked;
           
            
            return UniTask.CompletedTask;
        }

        private void OnStartGameClicked()
        {
           stateSwitcher.SwitchState<GameLoopState>().Forget();
        }

        public UniTask Exit()
        {
            lobbyScreen.Hide();
            lobbyScreen.StartGameClicked -= OnStartGameClicked;
            
            return UniTask.CompletedTask;
        }
    }
}