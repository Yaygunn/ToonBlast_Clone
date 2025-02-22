using System;
using System.Collections.Generic;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace YBlast.Managers
{
    public class NeighborCalculator
    {
        private GridManager _gridManager;

        private List<Vector2Int> _sameColorNeighbors = new();
            
         Vector2Int[] _directions = 
        {
            Vector2Int.right, 
            Vector2Int.left, 
            Vector2Int.up, 
            Vector2Int.down
        };
        
        [Inject]
        void Construct(GridManager gridManager)
        {
            _gridManager = gridManager;
        }
        

        public List<Vector2Int> CalculateSameColorNeighbors(Vector2Int initialCellIndex, bool ignorePerformingCubes = false)
        {
            int desiredColor = _gridManager.GetCubeColor(initialCellIndex);

            _sameColorNeighbors.Clear();
            
            _sameColorNeighbors.Add(initialCellIndex);

            if (ignorePerformingCubes)
            {
                if (_gridManager.GetBaseCube(initialCellIndex).IsPerforming)
                    return _sameColorNeighbors;
            }
            
            CheckAllDirectionOfACell(initialCellIndex);
            
            return _sameColorNeighbors;

            void CheckAllDirectionOfACell(Vector2Int cellIndex)
            {
                if (ignorePerformingCubes)
                    foreach (Vector2Int direction in _directions)
                        CheckACellIgnorePerformingCubes(cellIndex + direction);
                else
                    foreach (Vector2Int direction in _directions)
                        CheckACell(cellIndex + direction);
            }

            void CheckACell(Vector2Int cellIndex)
            {
                if(_sameColorNeighbors.Contains(cellIndex))
                   return;
                if(!IsSameColor(cellIndex))
                    return;
                
                _sameColorNeighbors.Add(cellIndex);
                CheckAllDirectionOfACell(cellIndex);
            }
            
            void CheckACellIgnorePerformingCubes(Vector2Int cellIndex)
            {
                if(_sameColorNeighbors.Contains(cellIndex))
                    return;
                if(!IsSameColor(cellIndex))
                    return;
                if(_gridManager.GetBaseCube(cellIndex).IsPerforming)
                    return;
                
                _sameColorNeighbors.Add(cellIndex);
                CheckAllDirectionOfACell(cellIndex);
            }

            bool IsSameColor(Vector2Int cellIndex)
            {
                return _gridManager.GetCubeColor(cellIndex) == desiredColor;
            }
        }
    }
}
