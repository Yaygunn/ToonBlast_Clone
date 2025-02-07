using System;
using UnityEngine;
using YBlast.Data;
using Zenject;

namespace YBlast.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private int _gridWidth;
        
        [Inject]
        void Construct(GridCreationData gridCreationData)
        {
            _gridWidth = gridCreationData.GridSize.y;
        }

        private void Start()
        {
            if (_gridWidth > 10)
                _camera.orthographicSize = 6;
        }
    }
}
