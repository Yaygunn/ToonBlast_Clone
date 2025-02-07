using System;
using UnityEngine;
using Zenject;
using YBlast.Scriptables;

namespace YBlast.Managers
{
    public class InputHandler : IDisposable
    {
        private InputListener _inputListener;

        private LayerMask _cubeLayerMask;

        private bool _isGameActive = true;

        [Inject]
        void Construct(InputListener inputListener, LayerMasksSO layerMasksSO)
        {
            _inputListener = inputListener;
            _inputListener.OnPressed += OnPressed;

            _cubeLayerMask = layerMasksSO.CubeLayerMask;

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
            
            BaseCube cubeUnderMouse = GetCubeUnderMouse(pressPosition);
            
            cubeUnderMouse?.OnClick();
        }

        private BaseCube GetCubeUnderMouse(Vector2 pressPosition)
        {
            Collider2D[] colliders = Physics2D.OverlapPointAll(pressPosition, _cubeLayerMask);

            foreach (var VARIABLE in colliders)
            {
                BaseCube cube = VARIABLE.GetComponent<BaseCube>();
                if (cube != null)
                    return cube;
            }
            return null;
        }
        
        private void OnGameWon()
        {
            _isGameActive = false;
        }

        
    }
}
