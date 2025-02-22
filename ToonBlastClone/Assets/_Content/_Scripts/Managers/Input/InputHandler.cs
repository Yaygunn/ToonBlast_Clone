using System;
using UnityEngine;
using Zenject;

namespace YBlast.Managers
{
    public class InputHandler : IDisposable
    {
        private InputListener _inputListener;

        private GridManager _gridManager;

        private ICellPositionManager _cellPositionManager;

        private bool _isGameActive = true;

        [Inject]
        void Construct(InputListener inputListener, GridManager gridManager, ICellPositionManager cellPositionManager)
        {
            _inputListener = inputListener;
            _gridManager = gridManager;
            _cellPositionManager = cellPositionManager;
            
            _inputListener.OnPressed += OnPressed;

            EventHub.Ev_GameWon += OnGameWon;
        }

        public void Dispose()
        {
            _inputListener.OnPressed -= OnPressed;
            _inputListener = null;
            
            EventHub.Ev_GameWon -= OnGameWon;
        }

        private void OnPressed(Vector2 pressPosition)
        {
            if(!_isGameActive)
                return;
            
            Vector2Int cellIndex = _cellPositionManager.GetPossibleCellIndex(pressPosition);

            if (_gridManager.IsValidIndex(cellIndex))
                _gridManager.GetBaseCube(cellIndex)?.OnClick();
        }

        private void OnGameWon()
        {
            _isGameActive = false;
        }

        
    }
}
