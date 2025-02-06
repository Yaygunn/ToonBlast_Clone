using System;
using UnityEngine;
using Zenject;

namespace YBlast.Managers
{
    public class InputListener : ITickable
    {
        public event Action<Vector2> OnPressed;
        
        private Camera _camera;
        
        public InputListener()
        {
            _camera = Camera.main;;
        }

        #if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        public void Tick()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                OnPressed?.Invoke(clickPosition);
            }
        }
        #else
        public void Tick()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 touchPosition = _camera.ScreenToWorldPoint(touch.position);
                    OnPressed?.Invoke(touchPosition);
                }
            }
        }
        #endif
        
    }
}
