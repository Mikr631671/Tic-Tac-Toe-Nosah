using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameScreen : MonoBehaviour
    {
        [field: SerializeField] public Button RestartButton {get; private set;}
        [field: SerializeField] public  Button HintButton {get; private set;}
        [field: SerializeField] public  Button UndoButton {get; private set;}
        
        private PlayerAllocator playerAllocator;
        private GameBoard gameBoard;
        private GameTimer gameTimer;
        private Player currentPlayer;
        private GameMode _gameMode;
        private Stack<GridCell> _moveHistory;
        

        public void Show(GameMode gameMode)
        {
            var isActionButtonsAvailable = gameMode == GameMode.PVE;
            HintButton.gameObject.SetActive(isActionButtonsAvailable);
            UndoButton.gameObject.SetActive(isActionButtonsAvailable);

            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            
            gameObject.SetActive(false);
        }
    }
}