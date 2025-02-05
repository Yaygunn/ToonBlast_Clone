using System;
using Unity.Mathematics;
using UnityEngine;
using YBlast.Managers;
using YBlast.Data;
using YBlast.Managers.CellPosition.Classic;
using Zenject;

namespace YBlast.Factories
{
    public class GridFactory : MonoBehaviour
    {
        private GridManager _gridManager;

        private ICellPositionManager _cellPositionManager;
        
        private GridCreationData _gridCreationData;

        private CubeSpawner _cubeSpawner;

        [Inject]
        void Construct(GridCreationData creationData, GridManager gridManager, ICellPositionManager cellPositionManager, CubeSpawner cubeSpawner)
        {
            _gridCreationData = creationData;
            _gridManager = gridManager;
            _cellPositionManager = cellPositionManager;
            _cubeSpawner = cubeSpawner;
        }

        private void Start()
        {
            FillGridAccordingToGridCreationData();
        }

        private void FillGridAccordingToGridCreationData()
        {
            Vector2Int cellIndex = Vector2Int.zero;
            
            for (cellIndex.x = 0 ; cellIndex.x < _gridCreationData.GridSize.x; cellIndex.x++)
            {
                for (cellIndex.y = 0; cellIndex.y < _gridCreationData.GridSize.y; cellIndex.y++)
                {
                    ColorCube cube = _cubeSpawner.SpawnColorCube(_gridCreationData.GetCubeColor(cellIndex));

                    Transform cubeTransform = cube.transform;

                    cubeTransform.position = _cellPositionManager.GetCellPos(cellIndex);
                    
                    _gridManager.PlaceCube(cube, cellIndex);
                }
            }
        }
    }
}
