using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YBlast.Data;
using YBlast.Scriptables;
using YBlast.Utilities;
using Zenject;

namespace YBlast.Managers
{
    public class CubeSpriteManager : ITickable, IDisposable
    {
        private GroupRules _groupRules;
        
        private GridManager _gridManager;

        private HashSet<Vector2Int> _cellsToCalculateSprite; 

        private HashSet<Vector2Int> _calculatedCells;

        private NeighborCalculator _neighborCalculator;

        private Sprite[][] _sprites;
        
        [Inject]
        void Construct(GroupRules groupRules, ColorCubeSpriteHolderSO colorCubeSpriteHolderSO, GridManager gridManager, NeighborCalculator neighborCalculator)
        {
            _groupRules = groupRules;
            _gridManager = gridManager;
            _neighborCalculator = neighborCalculator;
            
            _cellsToCalculateSprite = new(_gridManager.GetGridSize().GetMultiplication());
            _calculatedCells = new(_gridManager.GetGridSize().GetMultiplication());

            _sprites = colorCubeSpriteHolderSO.GetSpritesArray();

            #region EventSubscription

            EventHub.Ev_ColorCubeReachedFallDestination += AddSingleCellToCalculation;
            EventHub.Ev_ColorCubeStartFalling += AddSelfAndNeighborsToCalculateList;

            #endregion

        }

        public void Dispose()
        {
            #region UnSubscribe
            
            EventHub.Ev_ColorCubeReachedFallDestination -= AddSingleCellToCalculation;
            EventHub.Ev_ColorCubeStartFalling -= AddSelfAndNeighborsToCalculateList;

            #endregion
        }
        public void Tick()
        {
            CalculateAndChangeSprites(true);
        }

        public void ResetSpritesOfAllColorCubes()
        {
            _cellsToCalculateSprite = VectorUtilities.GetAllIndexesOfGridAsList(_gridManager.GetGridSize()).ToHashSet();
            CalculateAndChangeSprites();
        }

        public void SetCorrectSprite(ColorCube cube)
        {
            cube.SetSprite(_sprites[cube.ColorIndex][0]);
        }
        
        public void SetCorrectSprites(List<Vector2Int> cellIndexes)
        {
            Sprite sprite =
                _sprites[_gridManager.GetCubeColor(cellIndexes[0])][_groupRules.GetColorVersion(cellIndexes.Count)];
                
            
            foreach (var cellIndex in cellIndexes)
            {
                ((ColorCube)_gridManager.GetBaseCube(cellIndex)).SetSprite(sprite);
                
            }
        }

        private void CalculateAndChangeSprites(bool ignorePerforming = false)
        {
            _calculatedCells.Clear();
            
            foreach (var VARIABLE in _cellsToCalculateSprite)
            {
                if(_gridManager.GetCubeColor(VARIABLE) == -1)
                    continue;
                if(_calculatedCells.Contains(VARIABLE))
                    continue;

                List<Vector2Int> sameColorNeighbors = _neighborCalculator.CalculateSameColorNeighbors(VARIABLE, ignorePerforming);

                for (int i = 0; i < sameColorNeighbors.Count; i++)
                    _calculatedCells.Add(sameColorNeighbors[i]);
                
                SetCorrectSprites(sameColorNeighbors);
            }
            
            _cellsToCalculateSprite.Clear();
        }

        private void AddSingleCellToCalculation(Vector2Int cellIndex)
        {
            _cellsToCalculateSprite.Add(cellIndex);
        }

        private void AddSelfAndNeighborsToCalculateList(Vector2Int cellIndex)
        {
            VectorUtilities.OperateForEachDirection(cellIndex, AddSingleCellToCalculation);
            AddSingleCellToCalculation(cellIndex);
        }
    }
}
