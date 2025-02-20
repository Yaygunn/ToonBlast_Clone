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
            ECubeColor desiredColor = _gridManager.GetCubeColor(initialCellIndex);

            List<Vector2Int> blastingCubes = new List<Vector2Int>();
            blastingCubes.Add(initialCellIndex);

            if (ignorePerformingCubes)
            {
                if (_gridManager.GetBaseCube(initialCellIndex).IsPerforming)
                    return blastingCubes;
            }
            
            CheckAllDirectionOfACell(initialCellIndex);
            
            return blastingCubes;

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
                if(blastingCubes.Contains(cellIndex))
                   return;
                if(!IsSameColor(cellIndex))
                    return;
                
                blastingCubes.Add(cellIndex);
                CheckAllDirectionOfACell(cellIndex);
            }
            
            void CheckACellIgnorePerformingCubes(Vector2Int cellIndex)
            {
                if(blastingCubes.Contains(cellIndex))
                    return;
                if(!IsSameColor(cellIndex))
                    return;
                if(_gridManager.GetBaseCube(cellIndex).IsPerforming)
                    return;
                
                blastingCubes.Add(cellIndex);
                CheckAllDirectionOfACell(cellIndex);
            }

            bool IsSameColor(Vector2Int cellIndex)
            {
                if (!_gridManager.IsValidIndex(cellIndex))
                    return false;
                return _gridManager.GetCubeColor(cellIndex) == desiredColor;
            }
        }
    }
}
