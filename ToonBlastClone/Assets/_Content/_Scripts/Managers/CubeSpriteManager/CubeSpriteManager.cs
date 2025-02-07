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
        
        private ColorCubeSpriteHolderSO _colorCubeSpriteHolderSO;

        private GridManager _gridManager;

        private HashSet<Vector2Int> _cellsToCalculateSprite; 

        private HashSet<Vector2Int> _calculatedCells;

        private NeighborCalculator _neighborCalculator;
        
        [Inject]
        void Construct(GroupRules groupRules, ColorCubeSpriteHolderSO colorCubeSpriteHolderSO, GridManager gridManager, NeighborCalculator neighborCalculator)
        {
            _groupRules = groupRules;
            _colorCubeSpriteHolderSO = colorCubeSpriteHolderSO;
            _gridManager = gridManager;
            _neighborCalculator = neighborCalculator;
            
            _cellsToCalculateSprite = new(_gridManager.GetGridSize().GetMultiplication());
            _calculatedCells = new(_gridManager.GetGridSize().GetMultiplication());

            #region EventSubscription

            EventHub.Ev_ColorCubeReachedFallDestination += AddSingleCellToCalculation;
            EventHub.Ev_ColorCubeStartFalling += AddSelfAndNeighborsToCalculateList;
            EventHub.Ev_ColorCubeBlasted += AddSelfAndNeighborsToCalculateList;

            #endregion

        }

        public void Dispose()
        {
            #region UnSubscribe
            
            EventHub.Ev_ColorCubeReachedFallDestination -= AddSingleCellToCalculation;
            EventHub.Ev_ColorCubeStartFalling -= AddSelfAndNeighborsToCalculateList;
            EventHub.Ev_ColorCubeBlasted -= AddSelfAndNeighborsToCalculateList;

            #endregion
        }
        public void Tick()
        {
            CalculateAndChangeSprites();
        }

        public void ResetSpritesOfAllColorCubes()
        {
            _cellsToCalculateSprite = VectorUtilities.GetAllIndexesOfGridAsList(_gridManager.GetGridSize()).ToHashSet();
            CalculateAndChangeSprites();
        }

        public void SetCorrectSprite(ColorCube cube)
        {
            cube.SetSprite(_colorCubeSpriteHolderSO.GetSprite(cube.CubeColor, ECubeColorVersion.Default));
        }
        
        public void SetCorrectSprites(Vector2Int cellIndex)
        {
            
        }

        public void SetCorrectSprites(List<Vector2Int> cellIndexes)
        {
            Sprite sprite = _colorCubeSpriteHolderSO.GetSprite(_gridManager.GetCubeColor(cellIndexes[0]),
                _groupRules.GetColorVersion(cellIndexes.Count));
            
            foreach (var cellIndex in cellIndexes)
            {
                ((ColorCube)_gridManager.GetBaseCube(cellIndex)).SetSprite(sprite);
                
            }
        }

        private void CalculateAndChangeSprites()
        {
            _calculatedCells.Clear();
            
            foreach (var VARIABLE in _cellsToCalculateSprite)
            {
                if(!_gridManager.IsValidIndex(VARIABLE))
                    continue;
                if(_gridManager.GetCubeColor(VARIABLE) == ECubeColor.None)
                    continue;
                if(_calculatedCells.Contains(VARIABLE))
                    continue;

                List<Vector2Int> sameColorNeighbors = _neighborCalculator.CalculateSameColorNeighbors(VARIABLE);

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
