using System;
using System.Collections.Generic;
using ModestTree.Util;
using UnityEngine;
using YBlast.Data;
using Zenject;

namespace YBlast.Managers
{
    public class SpawnManager
    {
        private NeighborCalculator _neighborCalculator;
        
        private GridManager _gridManager;

        private CubeSpawner _cubeSpawner;

        private ICellPositionManager _cellPositionManager;

        private ColorPossibilities _colorPossibilities;

        private List<Vector2Int> _spawnTargets = new(10);
        
        private Dictionary<Vector2Int, int> _placementDictionary = new(10);
        
        [Inject]
        void Construct(GridManager gridManager, CubeSpawner cubeSpawner, NeighborCalculator neighborCalculator, ICellPositionManager cellPositionManager, ColorPossibilities colorPossibilities)
        {
            _gridManager = gridManager;
            _cubeSpawner = cubeSpawner;
            _neighborCalculator = neighborCalculator;
            _cellPositionManager = cellPositionManager;
            _colorPossibilities = colorPossibilities;
        }

        public void SpawnCubes(List<Vector2Int> spawnTargets, Action<BaseCube,Vector2Int> fallAction)
        {
            _spawnTargets = spawnTargets;
            SetCorrectDictionary();
            
            _gridManager.ResetTestedColors(spawnTargets);
            
            SpawnCubesAccordingToDictionary(fallAction);
        }

        private void SpawnCubesAccordingToDictionary(Action<BaseCube, Vector2Int> fallAction)
        {
            int previousColumb = -1;
            int spawnChainOrderInColumb = 0;

            foreach (var VARIABLE in _placementDictionary)
            {
                if (VARIABLE.Key.y != previousColumb)
                {
                    previousColumb = VARIABLE.Key.y;
                    spawnChainOrderInColumb = 0;
                }
                
                BaseCube cube = _cubeSpawner.SpawnColorCube(VARIABLE.Value);
                cube.transform.position = _cellPositionManager.GetSpawnPos(previousColumb, spawnChainOrderInColumb);           
                fallAction(cube, VARIABLE.Key);

                spawnChainOrderInColumb++;
            }
        }

        private void SetCorrectDictionary()
        {
            while (true)
            {
                SetTestDictionary();
                
                _gridManager.SetColorsForTesting(_placementDictionary);
                
                if(IsDictionaryPassesTest())
                    return;
            }
        }

        private bool IsDictionaryPassesTest()
        {
            Vector2Int cellIndex = Vector2Int.zero;
            Vector2Int gridSize = _gridManager.GetGridSize();
            
            for (cellIndex.x = 1; cellIndex.x < gridSize.x -1; cellIndex.x++)
                for (cellIndex.y = 1; cellIndex.y < gridSize.y -1; cellIndex.y++)
                    if(_neighborCalculator.CalculateSameColorNeighbors(cellIndex).Count>=2)
                        return true;
            return false;
        }
        
        private void SetTestDictionary()
        {
            _placementDictionary.Clear();
            
            foreach (var VARIABLE in _spawnTargets)
                _placementDictionary.Add(VARIABLE, GetRandomColorFromPossibleColors());    
        }

        private int GetRandomColorFromPossibleColors()
        {
            return _colorPossibilities.GetRandomColorIndex();
        }
    }
}
