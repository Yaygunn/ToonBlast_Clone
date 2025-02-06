using System;
using System.Collections.Generic;
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
        }

        private void FillGridAccordingToGridCreationData()
        {
            VectorUtilities.OperateOnEachIndex(_gridCreationData.GridSize ,FillACell);

            void FillACell(Vector2Int cellIndex)
            {
                ColorCube cube = _cubeSpawner.SpawnColorCube(_gridCreationData.GetCubeColor(cellIndex));

                Transform cubeTransform = cube.transform;

                cubeTransform.position = _cellPositionManager.GetCellPos(cellIndex);
                    
                _gridManager.PlaceCube(cube, cellIndex);
            }
        }
    }
}
