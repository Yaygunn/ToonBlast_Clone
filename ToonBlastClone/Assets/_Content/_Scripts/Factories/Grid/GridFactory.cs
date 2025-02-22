using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using YBlast.Managers;
using YBlast.Data;
using YBlast.Managers.CellPosition.Classic;
using YBlast.Utilities;
using Zenject;

namespace YBlast.Factories
{
    public class GridFactory : MonoBehaviour
    {
        private GridManager _gridManager;

        private ICellPositionManager _cellPositionManager;
        
        private GridCreationData _gridCreationData;

        private CubeSpawner _cubeSpawner;

        private NeighborCalculator _neighborCalculator;

        private CubeSpriteManager _cubeSpriteManager;

        [Inject]
        void Construct(GridCreationData creationData, GridManager gridManager, ICellPositionManager cellPositionManager,
            CubeSpawner cubeSpawner, NeighborCalculator neighborCalculator, CubeSpriteManager cubeSpriteManager)
        {
            _gridCreationData = creationData;
            _gridManager = gridManager;
            _cellPositionManager = cellPositionManager;
            _cubeSpawner = cubeSpawner;
            _neighborCalculator = neighborCalculator;
            _cubeSpriteManager = cubeSpriteManager;
        }

        private void Start()
        {
            FillGridAccordingToGridCreationData();
            _cubeSpriteManager.ResetSpritesOfAllColorCubes();
            OtherSettings();
        }

        private void FillGridAccordingToGridCreationData()
        {
            //VectorUtilities.OperateOnEachIndex(_gridCreationData.GridSize ,FillACell);

            Vector2Int gridSize = _gridCreationData.GridSize.Add(2, 2);
            Vector2Int cellIndex = Vector2Int.zero;

            for (cellIndex.x = 1; cellIndex.x < gridSize.x -1; cellIndex.x++)
            {
                for (cellIndex.y = 1; cellIndex.y < gridSize.y -1; cellIndex.y++)
                {
                    FillACell(cellIndex);
                }
            }

            void FillACell(Vector2Int cellIndex)
            {
                ECubeColor cubeColor = _gridCreationData.GetCubeColor(cellIndex.Add(-1, -1));
                
                ColorCube cube = _cubeSpawner.SpawnColorCube(_cubeSpriteManager.GetColorIndex(cubeColor));

                Transform cubeTransform = cube.transform;

                cubeTransform.position = _cellPositionManager.GetCellPos(cellIndex);
                    
                _gridManager.PlaceCube(cube, cellIndex);
            }
        }
        
        private void OtherSettings()
        {
            DOTween.SetTweensCapacity(500, 250);
        }
    }
}
